﻿namespace DFC.Api.JobProfiles.IntegrationTests.Model.Support
{
    public class AppSettings
    {
        public ServiceBusConfig ServiceBusConfig { get; set; } = new ServiceBusConfig();

        public APIConfig APIConfig { get; set; } = new APIConfig();
    }
}
