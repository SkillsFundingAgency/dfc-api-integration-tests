using DFC.Api.JobProfiles.IntegrationTests.Model.ContentType.JobProfile;
using DFC.Api.JobProfiles.IntegrationTests.Model.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory.Interface;
using RestSharp;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.API
{
    public class JobProfileOverviewAPI : IJobProfileOverviewAPI
    {
        private IRestClientFactory restClientFactory;
        private IRestRequestFactory restRequestFactory;
        private AppSettings appSettings;

        public JobProfileOverviewAPI(IRestClientFactory restClientFactory, IRestRequestFactory restRequestFactory, AppSettings appSettings)
        {
            this.restClientFactory = restClientFactory;
            this.restRequestFactory = restRequestFactory;
            this.appSettings = appSettings;
        }

        public async Task<IRestResponse<JobProfileContentType>> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var restClient = this.restClientFactory.Create(this.appSettings.APIConfig.EndpointBaseUrl);
            var restRequest = this.restRequestFactory.Create($"{id}/contents");
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Ocp-Apim-Subscription-Key", this.appSettings.APIConfig.ApimSubscriptionKey);
            restRequest.AddHeader("version", this.appSettings.APIConfig.Version);
            return await Task.Run(() => restClient.Execute<JobProfileContentType>(restRequest)).ConfigureAwait(false);
        }
    }
}
