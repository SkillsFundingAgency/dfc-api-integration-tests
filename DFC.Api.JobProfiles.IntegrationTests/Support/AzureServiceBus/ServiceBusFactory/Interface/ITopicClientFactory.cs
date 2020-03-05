using Microsoft.Azure.ServiceBus;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.AzureServiceBus.ServiceBusFactory.Interface
{
    public interface ITopicClientFactory
    {
        ITopicClient Create(string connectionString);
    }
}
