using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.APIResponse
{
    public class HowToBecomeModel
    {
        public List<string> EntryRouteSummary { get; set; }

        public EntryRoutes EntryRoutes { get; set; }

        public MoreInformation MoreInformation { get; set; }
    }
}
