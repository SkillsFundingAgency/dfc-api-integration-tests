using DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory.Interface;
using RestSharp;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory
{
    internal class RestRequestFactory : IRestRequestFactory
    {
        public IRestRequest Create(string urlSuffix)
        {
            return new RestRequest(urlSuffix);
        }
    }
}