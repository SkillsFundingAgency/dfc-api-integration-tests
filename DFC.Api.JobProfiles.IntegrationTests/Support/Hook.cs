using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.Api.JobProfiles.IntegrationTests.Support.AppSettings;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support
{
    public class Hook
    {
        public Topic Topic { get; set; }

        public Guid MessageId { get; set; }

        public string CanonicalName { get; set; }

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.MessageId = Guid.NewGuid();
            this.CanonicalName = CommonAction.RandomString(10).ToLower(CultureInfo.CurrentCulture);

            CommonAction.InitialiseAppSettings();
            this.Topic = new Topic(Settings.ServiceBusConfig.ConnectionString);
            await CommonAction.CreateJobProfile(this.Topic, this.MessageId, this.CanonicalName).ConfigureAwait(true);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await CommonAction.DeleteJobProfileWithId(this.Topic, this.MessageId).ConfigureAwait(true);
        }
    }
}
