using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DFC.Api.JobProfiles.Common.APISupport;
using DFC.Api.JobProfiles.IntegrationTests.Model;
using DFC.Api.JobProfiles.IntegrationTests.Model.API;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using NUnit.Framework;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class SearchTest : Hook
    {

        internal static List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();

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
            Assert.AreEqual(authorisedResponse.Data.CurrentPage, Convert.ToInt32(GetQueryParamaterValue("page")), "Expected current page to be 2");
        }

        [Test]
        public async Task SearchApiResponseCode204()
        {
            Response<JobProfileSearchAPIResponse> authorisedResponse = await CommonAction.ExecuteGetRequest<JobProfileSearchAPIResponse>(Settings.APIConfig.EndpointBaseUrl.ProfileSearch + "noprofile");
            Assert.AreEqual(HttpStatusCode.NoContent, authorisedResponse.HttpStatusCode, "Search API did not respond with a 204");
        }

        [Test]
        public async Task SearchApiResponseCode401()
        {
            Response<JobProfileSearchAPIResponse> unauthorisedResponse = await CommonAction.ExecuteGetRequest<JobProfileSearchAPIResponse>(Settings.APIConfig.EndpointBaseUrl.ProfileSearch + "plumber", false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, unauthorisedResponse.HttpStatusCode);
        }

        public static List<KeyValuePair<string, string>> SetQueryParameter(string parameterName, string parameterValue)
        {
            queryParameters.Add(new KeyValuePair<string, string>(parameterName, parameterValue));

            return queryParameters;
        }

        public static string GetQueryParamaterValue(string queryParameter)
        {
            return queryParameters.Where(x => x.Key.Equals(queryParameter)).FirstOrDefault().Value.ToString();
        }
    }
}
