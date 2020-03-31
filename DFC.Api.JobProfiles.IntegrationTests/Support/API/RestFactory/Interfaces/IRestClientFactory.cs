using RestSharp;
using System;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory.Interfaces
{
    public interface IRestClientFactory
    {
        IRestClient Create(Uri baseUrl);
    }
}