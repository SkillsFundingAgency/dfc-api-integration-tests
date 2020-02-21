using System;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.AppSettings
{
    public class Settings
    {
        public int DeploymentWaitInMinutes { get; set; }

        public int GracePeriod { get; set; }

        public APIConfig APIConfig { get; set; } = new APIConfig();

        public ServiceBusConfig ServiceBusConfig { get; set; }
    }
}
