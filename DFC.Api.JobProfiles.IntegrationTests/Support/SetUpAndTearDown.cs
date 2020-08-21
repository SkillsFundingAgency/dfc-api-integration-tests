using DFC.Api.JobProfiles.IntegrationTests.Model.ContentType.JobProfile;
using DFC.Api.JobProfiles.IntegrationTests.Model.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support.AzureServiceBus;
using DFC.Api.JobProfiles.IntegrationTests.Support.AzureServiceBus.ServiceBusFactory;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support
{
    public class SetUpAndTearDown : SetUpAndTearDownBase
    {
        protected ExpectedResponse ExpectedAPIResponse { get; set; }

        protected JobProfileContentType WakeUpJobProfile { get; set; }

        protected JobProfileContentType JobProfile { get; set; }

        protected ServiceBusSupport ServiceBus { get; set; }

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var wakeUpJobProfileId = Guid.NewGuid().ToString();
            var testJobProfileId = Guid.NewGuid().ToString();
            NLog.LogManager.GetCurrentClassLogger().Info($"Wake up job profile ID: {wakeUpJobProfileId}");
            NLog.LogManager.GetCurrentClassLogger().Info($"Test job profile ID: {testJobProfileId}");
            this.ServiceBus = new ServiceBusSupport(new TopicClientFactory(), this.AppSettings);
            this.WakeUpJobProfile = this.CommonAction.GetResource<JobProfileContentType>("JobProfileTemplate");
            this.WakeUpJobProfile.JobProfileId = wakeUpJobProfileId;
            this.WakeUpJobProfile.CanonicalName = this.CommonAction.RandomString(10).ToLowerInvariant();
            var jobProfileMessageBody = this.CommonAction.ConvertObjectToByteArray(this.WakeUpJobProfile);
            var message = new MessageFactory().Create(this.WakeUpJobProfile.JobProfileId, jobProfileMessageBody, "Published", "JobProfile");
            await this.ServiceBus.SendMessage(message).ConfigureAwait(false);
            await Task.Delay(TimeSpan.FromMinutes(this.AppSettings.DeploymentWaitInMinutes)).ConfigureAwait(true);
            this.JobProfile = this.CommonAction.GetResource<JobProfileContentType>("JobProfileTemplate");
            this.JobProfile.JobProfileId = testJobProfileId;
            this.JobProfile.CanonicalName = this.CommonAction.RandomString(10).ToLowerInvariant();
            jobProfileMessageBody = this.CommonAction.ConvertObjectToByteArray(this.JobProfile);
            message = new MessageFactory().Create(this.JobProfile.JobProfileId, jobProfileMessageBody, "Published", "JobProfile");
            await this.ServiceBus.SendMessage(message).ConfigureAwait(false);
            await Task.Delay(10000).ConfigureAwait(false);
            this.ExpectedAPIResponse = new ExpectedResponse(this.CommonAction.GetResource("ExpectedAPIResponse"));
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            var wakeUpJobProfileDelete = this.CommonAction.GetResource<JobProfileContentType>("JobProfileTemplate");
            wakeUpJobProfileDelete.JobProfileId = this.WakeUpJobProfile.JobProfileId;
            var messageBody = this.CommonAction.ConvertObjectToByteArray(wakeUpJobProfileDelete);
            var message = new MessageFactory().Create(this.WakeUpJobProfile.JobProfileId, messageBody, "Deleted", "JobProfile");
            await this.ServiceBus.SendMessage(message).ConfigureAwait(false);

            var jobProfileDelete = this.CommonAction.GetResource<JobProfileContentType>("JobProfileTemplate");
            jobProfileDelete.JobProfileId = this.JobProfile.JobProfileId;
            messageBody = this.CommonAction.ConvertObjectToByteArray(jobProfileDelete);
            message = new MessageFactory().Create(this.JobProfile.JobProfileId, messageBody, "Deleted", "JobProfile");
            await this.ServiceBus.SendMessage(message).ConfigureAwait(false);

            this.LogFile.Dispose();
        }
    }
}