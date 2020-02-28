using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.Api.JobProfiles.IntegrationTests.Support.AppSettings;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support
{
    public class JobProfileCreateHook : Hook
    {
        public Topic Topic { get; set; }

        public Guid MessageId { get; set; }

        public string CanonicalName { get; set; }

        public Common.CommonAction CommonAction { get; set; }

        public Settings Settings { get; set; }

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.CommonAction = new Common.CommonAction();
            this.Settings = this.CommonAction.GetAppSettings();
            this.MessageId = Guid.NewGuid();
            this.CanonicalName = this.CommonAction.RandomString(10).ToLowerInvariant();
            this.Topic = new Topic(this.Settings.ServiceBusConfig.Endpoint);
            await this.CommonAction.CreateJobProfile(this.Topic, this.MessageId, this.CanonicalName).ConfigureAwait(false);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await this.CommonAction.DeleteJobProfileWithId(this.Topic, this.MessageId).ConfigureAwait(false);
        }

    }
}
