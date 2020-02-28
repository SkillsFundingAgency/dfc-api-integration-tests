using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.Api.JobProfiles.IntegrationTests.Support.AppSettings;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support
{
    public class SetUpAndTearDown
    {
        public Settings Settings { get; set; }

        public Topic Topic { get; set; }

        public Guid WakeUpMessageId { get; set; }

        public Guid MessageId { get; set; }

        public string CanonicalName { get; set; }

        public Common.CommonAction CommonAction { get; set; }

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.CommonAction = new Common.CommonAction();
            this.Settings = this.CommonAction.GetAppSettings();
            this.Topic = new Topic(this.Settings.ServiceBusConfig.Endpoint);
            string wakeUpCanonicalName = this.CommonAction.RandomString(10).ToLower(CultureInfo.CurrentCulture);
            this.WakeUpMessageId = Guid.NewGuid();
            await this.CommonAction.CreateJobProfile(this.Topic, this.WakeUpMessageId, wakeUpCanonicalName).ConfigureAwait(true);
            await Task.Delay(TimeSpan.FromMinutes(this.Settings.DeploymentWaitInMinutes)).ConfigureAwait(true);
            this.MessageId = Guid.NewGuid();
            this.CanonicalName = this.CommonAction.RandomString(10).ToLower(CultureInfo.CurrentCulture);
            await this.CommonAction.CreateJobProfile(this.Topic, this.MessageId, this.CanonicalName).ConfigureAwait(true);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await this.CommonAction.DeleteJobProfileWithId(this.Topic, this.WakeUpMessageId).ConfigureAwait(true);
            await this.CommonAction.DeleteJobProfileWithId(this.Topic, this.MessageId).ConfigureAwait(true);
        }
    }
}
