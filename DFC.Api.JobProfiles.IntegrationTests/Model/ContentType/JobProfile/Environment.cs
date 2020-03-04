using System;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.ContentType.JobProfile
{
    public class Environment
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Uri Url { get; set; }

        public bool IsNegative { get; set; }
    }
}
