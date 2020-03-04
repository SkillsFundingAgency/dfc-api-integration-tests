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
        [Test]
        public async Task ResponseCode200()
        {
            var apiResponse = await this.api.GetById(this.jobProfile.JobProfileId).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, apiResponse.StatusCode);
        }

        [Test]
        public async Task ResponseCode204()
        {
            var apiResponse = await this.api.GetById(Guid.NewGuid().ToString()).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.NoContent, apiResponse.StatusCode);
        }

        [Test]
        public async Task ResponseCode401()
        {
            var tempAppSettings = this.appSettings;
            tempAppSettings.APIConfig.ApimSubscriptionKey = this.commonAction.RandomString(10);
            var unauthorisedAPI = new JobProfileOverviewAPI(new RestClientFactory(), new RestRequestFactory(), tempAppSettings);
            var apiResponse = await this.api.GetById(this.jobProfile.JobProfileId).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, apiResponse.StatusCode);
        }
    }
}