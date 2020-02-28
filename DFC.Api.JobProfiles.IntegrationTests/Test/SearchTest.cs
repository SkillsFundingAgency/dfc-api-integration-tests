using DFC.Api.JobProfiles.Common.APISupport;
using DFC.Api.JobProfiles.IntegrationTests.Model;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class SearchTest : Hook
    {
        internal static List<KeyValuePair<string, string>> QueryParameters = new List<KeyValuePair<string, string>>();

        public static List<KeyValuePair<string, string>> SetQueryParameter(string parameterName, string parameterValue)
        {
            QueryParameters.Add(new KeyValuePair<string, string>(parameterName, parameterValue));

            return QueryParameters;
        }

        public static string GetQueryParamaterValue(string queryParameter)
        {
            return QueryParameters.Where(x => x.Key.Equals(queryParameter)).FirstOrDefault().Value.ToString();
        }

        [Test]
        public async Task SearchApiResponseCode200()
        {
            Response<JobProfileSearchAPIResponse> authorisedResponse = await this.CommonAction.ExecuteGetRequest<JobProfileSearchAPIResponse>(this.Settings.APIConfig.EndpointBaseUrl.ProfileSearch + "nurse", new List<KeyValuePair<string, string>>()).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, authorisedResponse.HttpStatusCode, "Search API did not respond with a 200");
        }

        [Test]
        public async Task SearchApiRetrievesAdditionalPages()
        {
            SetQueryParameter("page", "2");
            SetQueryParameter("pageSize", "15");
            Response<JobProfileSearchAPIResponse> authorisedResponse = await this.CommonAction.ExecuteGetRequest<JobProfileSearchAPIResponse>(this.Settings.APIConfig.EndpointBaseUrl.ProfileSearch + "nurse", QueryParameters).ConfigureAwait(false);
            Assert.AreEqual(authorisedResponse.Data.CurrentPage, Convert.ToInt32(GetQueryParamaterValue("page")), "Expected current page to be 2");
        }

        [Test]
        public async Task SearchApiResponseCode204()
        {
            Response<JobProfileSearchAPIResponse> authorisedResponse = await this.CommonAction.ExecuteGetRequest<JobProfileSearchAPIResponse>(this.Settings.APIConfig.EndpointBaseUrl.ProfileSearch + "noprofile", new List<KeyValuePair<string, string>>()).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.NoContent, authorisedResponse.HttpStatusCode, "Search API did not respond with a 204");
        }

        [Test]
        public async Task SearchApiResponseCode401()
        {
            Response<JobProfileSearchAPIResponse> unauthorisedResponse = await this.CommonAction.ExecuteGetRequest<JobProfileSearchAPIResponse>(this.Settings.APIConfig.EndpointBaseUrl.ProfileSearch + "plumber", new List<KeyValuePair<string, string>>(), false).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, unauthorisedResponse.HttpStatusCode);
        }

        
    }
}
