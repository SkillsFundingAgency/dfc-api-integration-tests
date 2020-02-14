using DFC.Api.JobProfiles.Common.APISupport;
using DFC.Api.JobProfiles.IntegrationTests.Model;
using DFC.Api.JobProfiles.IntegrationTests.Model.APIResponse;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support.AppSettings;
using NUnit.Framework;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Test.Create
{
    public class DetailTest : Hook
    {
        [Test]
        public async Task ResponseCode200()
        {
            Response<JobDetails> authorisedAPIResponseWithContent = await CommonAction.ExecuteGetRequest<JobDetails>(Settings.APIConfig.EndpointBaseUrl.ProfileDetail + this.CanonicalName).ConfigureAwait(true);
            Assert.AreEqual(HttpStatusCode.OK, authorisedAPIResponseWithContent.HttpStatusCode);
        }

        [Test]
        public async Task ResponseCode204()
        {
            Response<JobDetails> authorisedAPIResponseNoContent = await CommonAction.ExecuteGetRequest<JobDetails>(Settings.APIConfig.EndpointBaseUrl.ProfileDetail + CommonAction.RandomString(10).ToLower(CultureInfo.CurrentCulture)).ConfigureAwait(true);
            Assert.AreEqual(HttpStatusCode.NoContent, authorisedAPIResponseNoContent.HttpStatusCode);
        }

        [Test]
        public async Task ResponseCode401()
        {
            Response<JobDetails> unauthorisedAPIResponse = await CommonAction.ExecuteGetRequest<JobDetails>(Settings.APIConfig.EndpointBaseUrl.ProfileDetail + this.CanonicalName, false).ConfigureAwait(true);
            Assert.AreEqual(HttpStatusCode.Unauthorized, unauthorisedAPIResponse.HttpStatusCode);
        }
    }
}