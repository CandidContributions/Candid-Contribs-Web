
namespace CandidContribs.Core.Services
{
    public interface IGuestbookGitHubService
    {
        void DownloadGuestbookFiles(string eventName);

        void PersistAsJson(string eventName);
    }
}
