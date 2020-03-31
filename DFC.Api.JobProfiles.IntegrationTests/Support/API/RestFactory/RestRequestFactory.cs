using DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory.Interfaces;
using RestSharp;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory
{
    internal class RestRequestFactory : IRestRequestFactory
    {
        public IRestRequest Create(string urlSuffix = null)
        {
            return new RestRequest(urlSuffix);
        }
    }
}