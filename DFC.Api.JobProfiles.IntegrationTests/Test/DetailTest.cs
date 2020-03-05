using DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileDetails;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support.API;
using DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class DetailTest : SetUpAndTearDown
    {
        private JobProfileAPI authorisedApi;
        private JobProfileAPI unauthorisedApi;

        [SetUp]
        public void SetUp()
        {
            APISettings apiSettings = new APISettings
            {
                Endpoint = this.appSettings.APIConfig.EndpointBaseUrl.ProfileDetail,
            };

            var tempAppSettings = this.appSettings;
            tempAppSettings.APIConfig.ApimSubscriptionKey = this.commonAction.RandomString(10);
            this.authorisedApi = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), this.appSettings, apiSettings);
            this.unauthorisedApi = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), tempAppSettings, apiSettings);
        }

        [Test]
        public async Task ResponseCode200()
        {
            var apiResponse = await this.authorisedApi.GetById<JobProfileDetailsAPIResponse>(this.jobProfile.JobProfileId).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, apiResponse.StatusCode);
        }

        [Test]
        public async Task ResponseCode204()
        {
            var apiResponse = await this.authorisedApi.GetById<JobProfileDetailsAPIResponse>(Guid.NewGuid().ToString()).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.NoContent, apiResponse.StatusCode);
        }

        [Test]
        public async Task ResponseCode401()
        {
            var apiResponse = await this.unauthorisedApi.GetById<JobProfileDetailsAPIResponse>(this.jobProfile.JobProfileId).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, apiResponse.StatusCode);
        }
    }
}