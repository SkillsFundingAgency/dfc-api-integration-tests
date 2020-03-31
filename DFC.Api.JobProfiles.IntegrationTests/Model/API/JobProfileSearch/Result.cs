using DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileDetails;
using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileSearch
{
    public class Result
    {
        public string ResultItemTitle { get; set; }

        public string ResultItemAlternativeTitle { get; set; }

        public string ResultItemOverview { get; set; }

        public string ResultItemSalaryRange { get; set; }

        public string ResultItemUrlName { get; set; }

        public List<JobProfileCategory> JobProfileCategories { get; set; }
    }
}