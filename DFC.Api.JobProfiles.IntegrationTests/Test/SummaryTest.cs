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
        private JobProfileApi authorisedApi;
        private JobProfileApi unauthorisedApi;

        [SetUp]
        public void SetUp()
        {
            var apiSettingsWithoutParameters = new APISettings { Endpoint = this.AppSettings.APIConfig.EndpointBaseUrl.ProfileSummary };
            var tempAppSettings = new AppSettings
            {
                APIConfig = new APIConfig
                {
                    ApimSubscriptionKey = this.CommonAction.RandomString(10),
                    Version = this.AppSettings.APIConfig.Version,
                },
            };
            this.authorisedApi = new JobProfileApi(new RestClientFactory(), new RestRequestFactory(), this.AppSettings, apiSettingsWithoutParameters);
            this.unauthorisedApi = new JobProfileApi(new RestClientFactory(), new RestRequestFactory(), tempAppSettings, apiSettingsWithoutParameters);
        }

        [Test]
        public async Task SuccessfulJobProfileSummaryRequest()
        {
            var response = await this.authorisedApi.GetSummaries().ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Job profile summary: Unable to search job profile summaries.");
            Assert.IsTrue(response.Data.Count > 0);
        }

        [Test]
        public async Task UnauthorisedProfileSummaryRequest()
        {
            var response = await this.unauthorisedApi.GetSummaries().ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "Job profile summary: The service should report that the request is unauthorised.");
        }
    }
}