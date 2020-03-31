using RestSharp;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory.Interfaces
{
    public interface IRestRequestFactory
    {
        IRestRequest Create(string urlSuffix = null);
    }
}