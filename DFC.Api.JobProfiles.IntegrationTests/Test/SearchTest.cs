using DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileSearch;
using DFC.Api.JobProfiles.IntegrationTests.Model.Support;
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
        private JobProfileApi authorisedApi;
        private JobProfileApi unauthorisedApi;
        private JobProfileApi authorisedApiWithQueryParameters;

        [SetUp]
        public void SetUp()
        {
            var apiSettingsWithParameters = new APISettings
            {
                Endpoint = this.AppSettings.APIConfig.EndpointBaseUrl.ProfileSearch,
                QueryParameters = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("page", "2"),
                    new KeyValuePair<string, string>("pageSize", "15"),
                },
            };

            var apiSettingsWithoutParameters = new APISettings { Endpoint = this.AppSettings.APIConfig.EndpointBaseUrl.ProfileSearch };

            var tempAppSettings = new AppSettings
            {
                APIConfig = new APIConfig
                {
                    ApimSubscriptionKey = this.CommonAction.RandomString(10),
                    Version = this.AppSettings.APIConfig.Version,
                },
            };
            this.authorisedApi = new JobProfileApi(new RestClientFactory(), new RestRequestFactory(), this.AppSettings, apiSettingsWithoutParameters);
            this.authorisedApiWithQueryParameters = new JobProfileApi(new RestClientFactory(), new RestRequestFactory(), this.AppSettings, apiSettingsWithParameters);
            this.unauthorisedApi = new JobProfileApi(new RestClientFactory(), new RestRequestFactory(), tempAppSettings, apiSettingsWithoutParameters);
        }

        [Test]
        public async Task SuccessfulJobProfileSearchRequest()
        {
            var response = await this.authorisedApi.GetByName<JobProfileSearchAPIResponse>("nurse").ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Job profile search: Unable to search job profiles.");
        }

        [Test]
        public async Task JobProfileSearchRequestWithPageQueryParameter()
        {
            const int expectedPageNumber = 2;
            var response = await this.authorisedApiWithQueryParameters.GetByName<JobProfileSearchAPIResponse>("nurse").ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Job profile search: The service should report a successful request.");
            Assert.AreEqual(expectedPageNumber, response.Data.CurrentPage, "Job profile search: The service returned an unexpected page parameter.");
        }

        [Test]
        public async Task NoContentJobProfileSearchRequest()
        {
            var response = await this.authorisedApi.GetByName<JobProfileSearchAPIResponse>("noprofile").ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode, "Job profile search: The service should report that the job profile is not present.");
        }

        [Test]
        public async Task UnauthorisedJobProfileSearchRequest()
        {
            var response = await this.unauthorisedApi.GetByName<JobProfileSearchAPIResponse>("nurse").ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "Job profile search: The service should report that the request is unauthorised.");
        }
    }
}