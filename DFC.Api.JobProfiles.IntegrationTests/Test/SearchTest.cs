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

            var tempAppSettings = new AppSettings();
            tempAppSettings.APIConfig = new APIConfig();
            tempAppSettings.APIConfig.ApimSubscriptionKey = this.commonAction.RandomString(10);
            tempAppSettings.APIConfig.Version = this.appSettings.APIConfig.Version;
            this.authorisedApi = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), this.appSettings, apiSettingsWithoutParameters);
            this.authorisedApiWithQueryParameters = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), this.appSettings, apiSettingsWithParameters);
            this.unauthorisedApi = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), tempAppSettings, apiSettingsWithoutParameters);
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
            var response = await this.authorisedApiWithQueryParameters.GetByName<JobProfileSearchAPIResponse>("nurse").ConfigureAwait(false);
            Assert.AreEqual(response.Data.CurrentPage, 2, "Job profile search: The service returned an unexpected page parameter.");
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
