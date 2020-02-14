using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.APIResponse
{
    public class RestrictionsAndRequirements
    {
        public List<string> RelatedRestrictions { get; set; }

        public List<object> OtherRequirements { get; set; }
    }
}
