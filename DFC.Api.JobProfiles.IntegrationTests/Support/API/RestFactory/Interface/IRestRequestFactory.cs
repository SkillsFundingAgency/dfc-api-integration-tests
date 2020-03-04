using RestSharp;
using System;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory.Interface
{
    public interface IRestRequestFactory
    {
        IRestRequest Create(string urlSuffix);
    }
}
