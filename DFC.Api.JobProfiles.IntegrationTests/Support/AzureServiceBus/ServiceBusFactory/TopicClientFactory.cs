using DFC.Api.JobProfiles.IntegrationTests.Support.AzureServiceBus.ServiceBusFactory.Interfaces;
using Microsoft.Azure.ServiceBus;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.AzureServiceBus.ServiceBusFactory
{
    public class TopicClientFactory : ITopicClientFactory
    {
        public ITopicClient Create(string connectionString)
        {
            return new TopicClient(new ServiceBusConnectionStringBuilder(connectionString));
        }
    }
}