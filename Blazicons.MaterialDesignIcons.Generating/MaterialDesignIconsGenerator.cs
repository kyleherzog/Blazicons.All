using Blazicons.Generating;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Threading;

namespace Blazicons.FontAwesome.Generating;

[Generator]
internal class MaterialDesignIconsGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        using var taskContext = new JoinableTaskContext();
        var taskFactory = new JoinableTaskFactory(taskContext);
        var downloader = new RepoDownloader(new Uri("https://github.com/Templarian/MaterialDesign-SVG/archive/refs/heads/master.zip"));
        taskFactory.Run(
            async () =>
            {
                await downloader.Download(@"^svg\/.*.svg$").ConfigureAwait(true);
            });

        var svgFolder = Path.Combine(downloader.ExtractedFolder, $"{downloader.RepoName}-{downloader.BranchName}", "svg");
        context.WriteIconsClass("MdiIcon", svgFolder);

        downloader.CleanUp();
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}