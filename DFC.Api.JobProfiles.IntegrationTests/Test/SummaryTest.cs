using DFC.Api.JobProfiles.IntegrationTests.Model.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support.API;
using DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class SummaryTest : SetUpAndTearDownBase
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

            var tempAppSettings = new AppSettings();
            tempAppSettings.APIConfig = new APIConfig();
            tempAppSettings.APIConfig.ApimSubscriptionKey = this.commonAction.RandomString(10);
            tempAppSettings.APIConfig.Version = this.appSettings.APIConfig.Version;
            this.authorisedApi = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), this.appSettings, apiSettingsWithoutParameters);
            this.unauthorisedApi = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), tempAppSettings, apiSettingsWithoutParameters);
        }

        [Test]
        public async Task SuccessfulJobProfileSummaryRequest()
        {
            var response = await this.authorisedApi.GetSummaries().ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Job profile summary: Unable to search job profile summaries.");
        }

        [Test]
        public async Task UnauthorisedProfileSummaryRequest()
        {
            var response = await this.unauthorisedApi.GetSummaries().ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "Job profile summary: The service should report that the request is unauthorised.");
        }
    }
}