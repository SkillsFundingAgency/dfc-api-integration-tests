using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.AzureServiceBus
{
    public interface IServiceBusSupport
    {
        Task SendMessage(Message message);
    }
}
