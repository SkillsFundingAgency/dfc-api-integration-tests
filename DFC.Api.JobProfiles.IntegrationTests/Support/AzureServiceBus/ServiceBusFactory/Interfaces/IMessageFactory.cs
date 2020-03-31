using Microsoft.Azure.ServiceBus;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.AzureServiceBus.ServiceBusFactory.Interfaces
{
    public interface IMessageFactory
    {
        Message Create(string messageId, byte[] messageBody, string actionType, string contentType);
    }
}