using System;
using System.Collections.Generic;
using System.Text;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.API
{
    public class JobProfileSearchAPIResponse
    {
        public int Count { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<Result> Results { get; set; }

        public class Result
        {
            public string ResultItemTitle { get; set; }
            public string ResultItemAlternativeTitle { get; set; }
            public string ResultItemOverview { get; set; }
            public string ResultItemSalaryRange { get; set; }
            public string ResultItemUrlName { get; set; }
            public List<JobProfileCategory> JobProfileCategories { get; set; }
        }

        public class JobProfileCategory
        {
            public string Title { get; set; }
            public string Name { get; set; }
        }
    }
}
