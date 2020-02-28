using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.Api.JobProfiles.IntegrationTests.Support.AppSettings;
using NUnit.Framework;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support
{
    public class Hook
    {
        public Settings Settings { get; set; }

        public Common.CommonAction CommonAction { get; set; }

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.CommonAction = new Common.CommonAction();
            this.Settings = this.CommonAction.GetAppSettings();
        }
    }
}
