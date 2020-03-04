using System;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.Support
{
    public class APIConfig
    {
        public string Version { get; set; }

        public string ApimSubscriptionKey { get; set; }

        public Uri EndpointBaseUrl { get; set; }
    }
}
