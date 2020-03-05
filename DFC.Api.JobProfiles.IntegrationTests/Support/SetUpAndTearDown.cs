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
        protected JobProfileContentType wakeUpJobProfile;
        protected JobProfileContentType jobProfile;
        protected ServiceBusSupport serviceBus;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.serviceBus = new ServiceBusSupport(new TopicClientFactory(), this.appSettings);
            this.wakeUpJobProfile = this.commonAction.GetResource<JobProfileContentType>("JobProfileContentType");
            this.wakeUpJobProfile.JobProfileId = Guid.NewGuid().ToString();
            var jobProfileMessageBody = this.commonAction.ConvertObjectToByteArray(this.wakeUpJobProfile);
            var message = new MessageFactory().Create(this.wakeUpJobProfile.JobProfileId, jobProfileMessageBody, "Published", "JobProfile");
            await this.serviceBus.SendMessage(message).ConfigureAwait(false);
            await Task.Delay(TimeSpan.FromMinutes(this.appSettings.DeploymentWaitInMinutes)).ConfigureAwait(true);
            this.jobProfile = this.commonAction.GetResource<JobProfileContentType>("JobProfileContentType");
            this.jobProfile.JobProfileId = Guid.NewGuid().ToString();
            this.jobProfile.CanonicalName = this.commonAction.RandomString(10).ToLowerInvariant();
            jobProfileMessageBody = this.commonAction.ConvertObjectToByteArray(this.jobProfile);
            message = new MessageFactory().Create(this.jobProfile.JobProfileId, jobProfileMessageBody, "Published", "JobProfile");
            await this.serviceBus.SendMessage(message).ConfigureAwait(false);
            await Task.Delay(10000).ConfigureAwait(false);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            var wakeUpJobProfileDelete = this.commonAction.GetResource<JobProfileContentType>("JobProfileDelete");
            var messageBody = this.commonAction.ConvertObjectToByteArray(wakeUpJobProfileDelete);
            var message = new MessageFactory().Create(this.wakeUpJobProfile.JobProfileId, messageBody, "Deleted", "JobProfile");
            await this.serviceBus.SendMessage(message).ConfigureAwait(false);

            var jobProfileDelete = this.commonAction.GetResource<JobProfileContentType>("JobProfileDelete");
            messageBody = this.commonAction.ConvertObjectToByteArray(jobProfileDelete);
            message = new MessageFactory().Create(this.jobProfile.JobProfileId, messageBody, "Deleted", "JobProfile");
            await this.serviceBus.SendMessage(message).ConfigureAwait(false);
        }
    }
}
