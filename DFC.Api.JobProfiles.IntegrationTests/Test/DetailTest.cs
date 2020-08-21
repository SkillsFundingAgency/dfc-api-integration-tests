using DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileDetails;
using DFC.Api.JobProfiles.IntegrationTests.Model.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support.API;
using DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class DetailTest : SetUpAndTearDown
    {
        private JobProfileApi authorisedApi;
        private JobProfileApi unauthorisedApi;

        [SetUp]
        public void SetUp()
        {
            var apiSettings = new APISettings { Endpoint = this.AppSettings.APIConfig.EndpointBaseUrl.ProfileDetail };
            var tempAppSettings = new AppSettings
            {
                APIConfig = new APIConfig
                {
                    ApimSubscriptionKey = "unauthorised-apim-key",
                    Version = this.AppSettings.APIConfig.Version,
                },
            };

            this.authorisedApi = new JobProfileApi(new RestClientFactory(), new RestRequestFactory(), this.AppSettings, apiSettings);
            this.unauthorisedApi = new JobProfileApi(new RestClientFactory(), new RestRequestFactory(), tempAppSettings, apiSettings);
        }

        [Test]
        public async Task SuccessfulJobDetailsRequest()
        {
            var apiResponse = await this.authorisedApi.GetByName<JobProfileDetailsAPIResponse>(this.JobProfile.CanonicalName).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, apiResponse.StatusCode, "Job details: The service should report a successful request.");
            Assert.AreEqual(this.ExpectedAPIResponse.JobProfileDetails(this.AppSettings.APIConfig.EndpointBaseUrl.ProfileDetail, this.JobProfile.CanonicalName), apiResponse.Content);
        }

        [Test]
        public async Task NoContentJobDetailsRequest()
        {
            var apiResponse = await this.authorisedApi.GetByName<JobProfileDetailsAPIResponse>(this.CommonAction.RandomString(10)).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.NoContent, apiResponse.StatusCode, "Job details: The service should report that the job profile is not present.");
        }

        [Test]
        public async Task UnauthorisedJobDetailsRequest()
        {
            var apiResponse = await this.unauthorisedApi.GetByName<JobProfileDetailsAPIResponse>(this.JobProfile.CanonicalName).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, apiResponse.StatusCode, "Job details: The service should report that the request is unauthorised.");
        }
    }
}