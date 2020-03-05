using DFC.Api.JobProfiles.IntegrationTests.Model.ContentType.JobProfile;
using DFC.Api.JobProfiles.IntegrationTests.Support.AzureServiceBus;
using DFC.Api.JobProfiles.IntegrationTests.Support.AzureServiceBus.ServiceBusFactory;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support
{
    public class SetUpAndTearDown : SetUpAndTearDownBase
    {
        protected JobProfileContentType jobProfile;
        protected ServiceBusSupport serviceBus;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.serviceBus = new ServiceBusSupport(new TopicClientFactory(), this.appSettings);
            this.jobProfile = this.commonAction.GetResource<JobProfileContentType>("JobProfileContentType");
            var jobProfileMessageBody = this.commonAction.ConvertObjectToByteArray(this.jobProfile);
            var message = new MessageFactory().Create(this.jobProfile.JobProfileId, jobProfileMessageBody, "Published", "JobProfile");
            await this.serviceBus.SendMessage(message).ConfigureAwait(false);
            await Task.Delay(10000).ConfigureAwait(false);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            var jobProfileDelete = this.commonAction.GetResource<JobProfileContentType>("JobProfileDelete");
            var messageBody = this.commonAction.ConvertObjectToByteArray(jobProfileDelete);
            var message = new MessageFactory().Create(this.jobProfile.JobProfileId, messageBody, "Deleted", "JobProfile");
            await this.serviceBus.SendMessage(message).ConfigureAwait(false);
        }
    }
}
