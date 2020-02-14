using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.APIResponse
{
    public class MoreInformation
    {
        public List<string> Registrations { get; set; }

        public List<object> CareerTips { get; set; }

        public List<object> ProfessionalAndIndustryBodies { get; set; }

        public List<string> FurtherInformation { get; set; }
    }
}
