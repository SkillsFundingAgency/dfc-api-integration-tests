using DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileSearch;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support.API;
using DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class SearchTest : SetUpAndTearDownBase
    {

        private JobProfileAPI authorisedApi;
        private JobProfileAPI unauthorisedApi;
        private JobProfileAPI authorisedApiWithQueryParameters;

        [SetUp]
        public void SetUp()
        {
            APISettings apiSettingsWithParameters = new APISettings
            {
                Endpoint = this.appSettings.APIConfig.EndpointBaseUrl.ProfileSearch,
                QueryParameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("page", "2"),
                    new KeyValuePair<string, string>("pageSize", "15"),
                },
            };

            APISettings apiSettingsWithoutParameters = new APISettings
            {
                Endpoint = this.appSettings.APIConfig.EndpointBaseUrl.ProfileSearch,
            };

            var tempAppSettings = this.appSettings;
            tempAppSettings.APIConfig.ApimSubscriptionKey = this.commonAction.RandomString(10);
            this.authorisedApi = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), this.appSettings, apiSettingsWithoutParameters);
            this.authorisedApiWithQueryParameters = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), this.appSettings, apiSettingsWithParameters);
            this.unauthorisedApi = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), tempAppSettings, apiSettingsWithoutParameters);
        }

        [Test]
        public async Task SearchApiResponseCode200()
        {
            var response = await this.authorisedApi.GetByName<JobProfileSearchAPIResponse>("nurse").ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Search API did not respond with a 200");
        }

        [Test]
        public async Task SearchApiRetrievesAdditionalPages()
        {
            var response = await this.authorisedApiWithQueryParameters.GetByName<JobProfileSearchAPIResponse>("nurse").ConfigureAwait(false);
            Assert.AreEqual(response.Data.CurrentPage, 2, "Expected current page to be 2");
        }

        [Test]
        public async Task SearchApiResponseCode204()
        {
            var response = await this.authorisedApi.GetByName<JobProfileSearchAPIResponse>("noprofile").ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode, "Search API did not respond with a 204");
        }

        [Test]
        public async Task SearchApiResponseCode401()
        {
            var response = await this.unauthorisedApi.GetByName<JobProfileSearchAPIResponse>("nurse").ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
