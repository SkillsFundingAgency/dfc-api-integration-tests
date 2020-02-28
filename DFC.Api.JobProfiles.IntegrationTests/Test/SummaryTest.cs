using DFC.Api.JobProfiles.Common.APISupport;
using DFC.Api.JobProfiles.IntegrationTests.Model;
using DFC.Api.JobProfiles.IntegrationTests.Model.SummaryAPIResponse;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class SummaryTest : JobProfileCreateHook
    {
        [Test]
        public async Task ResponseCode200()
        {
            Response<List<JobSummary>> authorisedAPIResponseWithContent = await this.CommonAction.ExecuteGetRequest<List<JobSummary>>(this.Settings.APIConfig.EndpointBaseUrl.ProfileSummary, new List<KeyValuePair<string, string>>()).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, authorisedAPIResponseWithContent.HttpStatusCode);
        }

        [Test]
        public async Task ResponseCode401()
        {
            Response<List<JobSummary>> unauthorisedAPIResponse = await this.CommonAction.ExecuteGetRequest<List<JobSummary>>(this.Settings.APIConfig.EndpointBaseUrl.ProfileSummary, new List<KeyValuePair<string, string>>(), false).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, unauthorisedAPIResponse.HttpStatusCode);
        }
    }
}