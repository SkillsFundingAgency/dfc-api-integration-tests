using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DFC.Api.JobProfiles.Common.APISupport;
using DFC.Api.JobProfiles.IntegrationTests.Model;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using NUnit.Framework;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class SearchTest : Hook
    {

        private static List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();

        [Test]
        public async Task SearchApiResponseCode200()
        {
            Response<JobProfileSearchAPIResponse> authorisedResponse = await CommonAction.ExecuteGetRequest<JobProfileSearchAPIResponse>(Settings.APIConfig.EndpointBaseUrl.ProfileSearch + "nurse");
            Assert.AreEqual(HttpStatusCode.OK, authorisedResponse.HttpStatusCode, "Search API did not respond with a 200");
        }

        [Test]
        public async Task SearchApiRetrievesAdditionalPages()
        {
            SetQueryParameter("page", "2");
            SetQueryParameter("pageSize", "15");
            Response<JobProfileSearchAPIResponse> authorisedResponse = await CommonAction.ExecuteGetRequest<JobProfileSearchAPIResponse>(Settings.APIConfig.EndpointBaseUrl.ProfileSearch + "nurse", queryParameters);
            Assert.IsTrue(authorisedResponse.Data.CurrentPage == 2, "Expected current page to be 2");
        }

        [Test]
        public async Task SearchApiResponseCode204()
        {
            Response<JobProfileSearchAPIResponse> authorisedResponse = await CommonAction.ExecuteGetRequest<JobProfileSearchAPIResponse>(Settings.APIConfig.EndpointBaseUrl.ProfileSearch + "noprofile");
            Assert.AreEqual(HttpStatusCode.NoContent, authorisedResponse.HttpStatusCode, "Search API did not respond with a 204");
        }

        public static List<KeyValuePair<string, string>> SetQueryParameter(string parameterName, string parameterValue)
        {
            queryParameters.Add(new KeyValuePair<string, string>(parameterName, parameterValue));

            return queryParameters;
        }
    }
}
