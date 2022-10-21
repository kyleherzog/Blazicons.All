using System.IO.Compression;

namespace Blazicons.Generating;

public class RepoDownloader
{
    private static readonly HttpClient client = new();

    public RepoDownloader(Uri address)
    {
        Address = address;
        var parts = Address.AbsolutePath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        AuthorName = parts[0];
        RepoName = parts[1];
        BranchName = Path.GetFileNameWithoutExtension(parts.Last());
    }

    public Uri Address { get; }

    public string? RootFolder { get; set; }

    public string ExtractedFolder => Path.Combine(RootFolder, "files");

    public void CleanUp()
    {
        Directory.Delete(RootFolder, true);
    }

    public string AuthorName { get; }

    public string RepoName { get; }

    public string BranchName { get; }

    public async Task Download()
    {
        var fileName = Path.GetFileNameWithoutExtension(Address.AbsolutePath);

        var bytes = await client.GetByteArrayAsync(Address).ConfigureAwait(false);

        if (string.IsNullOrEmpty(RootFolder))
        {
            RootFolder = Path.Combine(Path.GetTempPath(), $"{RepoName}-{Guid.NewGuid().ToString("N")}");
            Directory.CreateDirectory(RootFolder);
        }

        var zipFileName = Path.Combine(RootFolder, fileName);
        File.WriteAllBytes(zipFileName, bytes);

        ZipFile.ExtractToDirectory(zipFileName, ExtractedFolder);
    }
}

