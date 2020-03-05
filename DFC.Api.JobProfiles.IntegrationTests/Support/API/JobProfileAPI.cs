using DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileSummary;
using DFC.Api.JobProfiles.IntegrationTests.Model.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory.Interface;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.API
{
    public class JobProfileAPI : IJobProfileAPI
    {
        private IRestClientFactory restClientFactory;
        private IRestRequestFactory restRequestFactory;
        private AppSettings appSettings;
        private APISettings apiSettings;

        public JobProfileAPI(IRestClientFactory restClientFactory, IRestRequestFactory restRequestFactory, AppSettings appSettings, APISettings apiSettings)
        {
            this.restClientFactory = restClientFactory;
            this.restRequestFactory = restRequestFactory;
            this.appSettings = appSettings;
            this.apiSettings = apiSettings;
        }

        public async Task<IRestResponse<T>> GetById<T>(string id) where T : class, new()
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var restClient = this.restClientFactory.Create(this.apiSettings.Endpoint);
            var restRequest = this.restRequestFactory.Create($"{id}");

            foreach (KeyValuePair<string, string> queryParameter in this.apiSettings.QueryParameters)
            {
                restRequest.AddParameter(queryParameter.Key, queryParameter.Value);
            }

            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Ocp-Apim-Subscription-Key", this.appSettings.APIConfig.ApimSubscriptionKey);
            restRequest.AddHeader("version", this.appSettings.APIConfig.Version);
            return await Task.Run(() => restClient.Execute<T>(restRequest)).ConfigureAwait(false);
        }

        public async Task<IRestResponse<T>> GetByName<T>(string name) where T : class, new()
        {
            return await this.GetById<T>(name).ConfigureAwait(false);
        }

        public async Task<IRestResponse<List<JobProfileSummaryAPIResponse>>> GetSummaries()
        {
            var restClient = this.restClientFactory.Create(this.apiSettings.Endpoint);
            var restRequest = this.restRequestFactory.Create();
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Ocp-Apim-Subscription-Key", this.appSettings.APIConfig.ApimSubscriptionKey);
            restRequest.AddHeader("version", this.appSettings.APIConfig.Version);
            return await Task.Run(() => restClient.Execute<List<JobProfileSummaryAPIResponse>>(restRequest)).ConfigureAwait(false);
        }
    }
}
