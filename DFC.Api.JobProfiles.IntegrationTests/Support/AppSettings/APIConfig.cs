namespace DFC.Api.JobProfiles.IntegrationTests.Support.AppSettings
{
    public class APIConfig
    {
        public string Version { get; set; }

        public string ApimSubscriptionKey { get; set; }

        public EndpointBaseUrl EndpointBaseUrl { get; } = new EndpointBaseUrl();
    }
}
