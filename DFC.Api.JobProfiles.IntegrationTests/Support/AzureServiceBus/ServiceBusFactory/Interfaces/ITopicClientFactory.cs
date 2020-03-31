using Microsoft.Azure.ServiceBus;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.AzureServiceBus.ServiceBusFactory.Interfaces
{
    public interface ITopicClientFactory
    {
        ITopicClient Create(string connectionString);
    }
}