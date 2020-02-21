using DFC.Api.JobProfiles.Common.APISupport;
using DFC.Api.JobProfiles.IntegrationTests.Model.DetailsAPIResponse;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support.AppSettings;
using DFC.Api.JobProfiles.IntegrationTests.Support.Common;
using NUnit.Framework;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class DetailTest : SetUpAndTearDown
    {
        [Test]
        public async Task ResponseCode200()
        {
            Response<JobDetails> authorisedAPIResponseWithContent = await this.CommonAction.ExecuteGetRequest<JobDetails>(this.Settings.APIConfig.EndpointBaseUrl.ProfileDetail + this.CanonicalName).ConfigureAwait(true);
            Assert.AreEqual(HttpStatusCode.OK, authorisedAPIResponseWithContent.HttpStatusCode);
        }

        [Test]
        public async Task ResponseCode204()
        {
            Response<JobDetails> authorisedAPIResponseNoContent = await this.CommonAction.ExecuteGetRequest<JobDetails>(this.Settings.APIConfig.EndpointBaseUrl.ProfileDetail + this.CommonAction.RandomString(10).ToLower(CultureInfo.CurrentCulture)).ConfigureAwait(true);
            Assert.AreEqual(HttpStatusCode.NoContent, authorisedAPIResponseNoContent.HttpStatusCode);
        }

        [Test]
        public async Task ResponseCode401()
        {
            Response<JobDetails> unauthorisedAPIResponse = await this.CommonAction.ExecuteGetRequest<JobDetails>(this.Settings.APIConfig.EndpointBaseUrl.ProfileDetail + this.CanonicalName, "InvalidKey").ConfigureAwait(true);
            Assert.AreEqual(HttpStatusCode.Unauthorized, unauthorisedAPIResponse.HttpStatusCode);
        }
    }
}