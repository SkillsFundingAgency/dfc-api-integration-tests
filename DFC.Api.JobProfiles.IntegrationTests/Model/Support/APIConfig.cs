using System;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.Support
{
    public class APIConfig
    {
        public string Version { get; set; }

        public string ApimSubscriptionKey { get; set; }

        public EndpointBaseUrl EndpointBaseUrl { get; set; }
    }
}
