using DFC.Api.JobProfiles.IntegrationTests.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support.API;
using DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class SummaryTest : SetUpAndTearDown
    {
        private JobProfileAPI authorisedApi;
        private JobProfileAPI unauthorisedApi;

        [SetUp]
        public void SetUp()
        {
            APISettings apiSettingsWithoutParameters = new APISettings
            {
                Endpoint = this.appSettings.APIConfig.EndpointBaseUrl.ProfileSummary,
            };

            var tempAppSettings = this.appSettings;
            tempAppSettings.APIConfig.ApimSubscriptionKey = this.commonAction.RandomString(10);
            this.authorisedApi = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), this.appSettings, apiSettingsWithoutParameters);
            this.unauthorisedApi = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), tempAppSettings, apiSettingsWithoutParameters);
        }

        [Test]
        public async Task ResponseCode200()
        {
            var response = await this.authorisedApi.GetSummaries().ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task ResponseCode401()
        {
            var response = await this.unauthorisedApi.GetSummaries().ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}