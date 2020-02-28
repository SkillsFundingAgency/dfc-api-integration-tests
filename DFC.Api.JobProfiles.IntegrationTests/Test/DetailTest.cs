using DFC.Api.JobProfiles.Common.APISupport;
using DFC.Api.JobProfiles.IntegrationTests.Model.DetailsAPIResponse;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class DetailTest : JobProfileCreateHook
    {
        [Test]
        public async Task ResponseCode200()
        {
            Response<JobDetails> authorisedAPIResponseWithContent = await this.CommonAction.ExecuteGetRequest<JobDetails>(this.Settings.APIConfig.EndpointBaseUrl.ProfileDetail + this.CanonicalName, new List<KeyValuePair<string, string>>()).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, authorisedAPIResponseWithContent.HttpStatusCode);
        }

        [Test]
        public async Task ResponseCode204()
        {
            Response<JobDetails> authorisedAPIResponseNoContent = await this.CommonAction.ExecuteGetRequest<JobDetails>(this.Settings.APIConfig.EndpointBaseUrl.ProfileDetail + this.CommonAction.RandomString(10).ToLowerInvariant(), new List<KeyValuePair<string, string>>()).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.NoContent, authorisedAPIResponseNoContent.HttpStatusCode);
        }

        [Test]
        public async Task ResponseCode401()
        {
            Response<JobDetails> unauthorisedAPIResponse = await this.CommonAction.ExecuteGetRequest<JobDetails>(this.Settings.APIConfig.EndpointBaseUrl.ProfileDetail + this.CanonicalName, new List<KeyValuePair<string, string>>(), false).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, unauthorisedAPIResponse.HttpStatusCode);
        }
    }
}