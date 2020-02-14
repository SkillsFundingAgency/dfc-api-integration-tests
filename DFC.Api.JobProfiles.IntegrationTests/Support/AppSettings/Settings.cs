using System;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.AppSettings
{
    public static class Settings
    {
        public static TimeSpan DeploymentWaitInMinutes { get; set; }

        public static TimeSpan GracePeriod { get; set; }

        public static APIConfig APIConfig { get; } = new APIConfig();

        public static ServiceBusConfig ServiceBusConfig { get; } = new ServiceBusConfig();
    }
}
