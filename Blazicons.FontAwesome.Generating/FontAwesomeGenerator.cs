using Blazicons.Generating;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Threading;

namespace Blazicons.FontAwesome.Generating;

[Generator]
internal class FontAwesomeGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        using var taskContext = new JoinableTaskContext();
        var taskFactory = new JoinableTaskFactory(taskContext);
        var downloader = new RepoDownloader(new Uri("https://github.com/FortAwesome/Font-Awesome/archive/refs/heads/6.x.zip"));
        taskFactory.Run(
            async () =>
            {
                await downloader.Download(@"^svgs\/.*.svg$").ConfigureAwait(true);
            });

        var svgFolder = Path.Combine(downloader.ExtractedFolder, $"{downloader.RepoName}-{downloader.BranchName}", "svgs", "regular");
        context.WriteIconsClass("FontAwesomeRegularIcon", svgFolder);

        svgFolder = Path.Combine(downloader.ExtractedFolder, $"{downloader.RepoName}-{downloader.BranchName}", "svgs", "solid");
        context.WriteIconsClass("FontAwesomeSolidIcon", svgFolder);

        downloader.CleanUp();
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}