using System;
using System.Collections.Generic;
using System.Text;
using Blazicons.Generating;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Threading;

[Generator]
internal class IoniconsGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        using var taskContext = new JoinableTaskContext();
        var taskFactory = new JoinableTaskFactory(taskContext);
        var downloader = new RepoDownloader(new Uri("https://github.com/ionic-team/ionicons/archive/refs/heads/main.zip"));
        taskFactory.Run(
            async () =>
            {
                await downloader.Download().ConfigureAwait(true);
            });

        var svgFolder = Path.Combine(downloader.ExtractedFolder, $"{downloader.RepoName}-{downloader.BranchName}", "src", "svg");
        context.WriteIconsClass("Ionicon", svgFolder);

        downloader.CleanUp();
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}

