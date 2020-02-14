using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.DetailsAPIResponse
{
    public class RestrictionsAndRequirements
    {
        public List<string> RelatedRestrictions { get; set; }

        public List<object> OtherRequirements { get; set; }
    }
}
