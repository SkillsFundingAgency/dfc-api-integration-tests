using DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory.Interfaces;
using RestSharp;
using System;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory
{
    internal class RestClientFactory : IRestClientFactory
    {
        public IRestClient Create(Uri baseUrl)
        {
            return new RestClient(baseUrl);
        }
    }
}