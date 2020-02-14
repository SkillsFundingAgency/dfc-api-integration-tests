using DFC.Api.JobProfiles.Common.APISupport;
using DFC.Api.JobProfiles.IntegrationTests.Model.SummaryAPIResponse;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support.AppSettings;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class SummaryTest : SetUpAndTearDown
    {
        [Test]
        public async Task ResponseCode200()
        {
            Response<List<JobSummary>> authorisedAPIResponseWithContent = await this.CommonAction.ExecuteGetRequest<List<JobSummary>>(Settings.APIConfig.EndpointBaseUrl.ProfileSummary).ConfigureAwait(true);
            Assert.AreEqual(HttpStatusCode.OK, authorisedAPIResponseWithContent.HttpStatusCode);
        }

        [Test]
        public async Task ResponseCode401()
        {
            Response<List<JobSummary>> unauthorisedAPIResponse = await this.CommonAction.ExecuteGetRequest<List<JobSummary>>(Settings.APIConfig.EndpointBaseUrl.ProfileSummary, false).ConfigureAwait(true);
            Assert.AreEqual(HttpStatusCode.Unauthorized, unauthorisedAPIResponse.HttpStatusCode);
        }
    }
}