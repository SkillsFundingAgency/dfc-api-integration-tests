using RestSharp;
using System;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory.Interface
{
    public interface IRestClientFactory
    {
        IRestClient Create(Uri baseUrl);
    }
}
