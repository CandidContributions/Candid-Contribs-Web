using Umbraco.Core;
using CandidContribs.Core.Services;
using Umbraco.Core.Composing;

namespace CandidContribs.Core.Composers
{
    public class ServicesComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IGuestbookGitHubService, GuestbookGitHubService>();
        }
    }
}
