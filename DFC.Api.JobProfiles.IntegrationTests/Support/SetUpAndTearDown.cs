using DFC.Api.JobProfiles.IntegrationTests.Model.ContentType.JobProfile;
using DFC.Api.JobProfiles.IntegrationTests.Support.AzureServiceBus;
using DFC.Api.JobProfiles.IntegrationTests.Support.AzureServiceBus.ServiceBusFactory;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support
{
    public class SetUpAndTearDown : SetUpAndTearDownBase
    {
        protected JobProfileContentType WakeUpJobProfile { get; set; }

        protected JobProfileContentType JobProfile { get; set; }

        protected ServiceBusSupport ServiceBus { get; set; }

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.ServiceBus = new ServiceBusSupport(new TopicClientFactory(), this.AppSettings);
            this.WakeUpJobProfile = this.CommonAction.GetResource<JobProfileContentType>("JobProfileContentType");
            this.WakeUpJobProfile.JobProfileId = Guid.NewGuid().ToString();
            var jobProfileMessageBody = this.CommonAction.ConvertObjectToByteArray(this.WakeUpJobProfile);
            var message = new MessageFactory().Create(this.WakeUpJobProfile.JobProfileId, jobProfileMessageBody, "Published", "JobProfile");
            await this.ServiceBus.SendMessage(message).ConfigureAwait(false);
            await Task.Delay(TimeSpan.FromMinutes(this.AppSettings.DeploymentWaitInMinutes)).ConfigureAwait(true);
            this.JobProfile = this.CommonAction.GetResource<JobProfileContentType>("JobProfileContentType");
            this.JobProfile.JobProfileId = Guid.NewGuid().ToString();
            this.JobProfile.CanonicalName = this.CommonAction.RandomString(10).ToLowerInvariant();
            jobProfileMessageBody = this.CommonAction.ConvertObjectToByteArray(this.JobProfile);
            message = new MessageFactory().Create(this.JobProfile.JobProfileId, jobProfileMessageBody, "Published", "JobProfile");
            await this.ServiceBus.SendMessage(message).ConfigureAwait(false);
            await Task.Delay(10000).ConfigureAwait(false);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            var wakeUpJobProfileDelete = this.CommonAction.GetResource<JobProfileContentType>("JobProfileDelete");
            var messageBody = this.CommonAction.ConvertObjectToByteArray(wakeUpJobProfileDelete);
            var message = new MessageFactory().Create(this.WakeUpJobProfile.JobProfileId, messageBody, "Deleted", "JobProfile");
            await this.ServiceBus.SendMessage(message).ConfigureAwait(false);

            var jobProfileDelete = this.CommonAction.GetResource<JobProfileContentType>("JobProfileDelete");
            messageBody = this.CommonAction.ConvertObjectToByteArray(jobProfileDelete);
            message = new MessageFactory().Create(this.JobProfile.JobProfileId, messageBody, "Deleted", "JobProfile");
            await this.ServiceBus.SendMessage(message).ConfigureAwait(false);
        }
    }
}