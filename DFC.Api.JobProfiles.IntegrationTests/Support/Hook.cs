using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using NUnit.Framework;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support
{
    public class Hook
    {

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            CommonAction.InitialiseAppSettings();
        }

    }
}
