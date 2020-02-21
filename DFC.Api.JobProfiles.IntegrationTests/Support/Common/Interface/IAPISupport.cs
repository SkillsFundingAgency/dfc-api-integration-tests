using DFC.Api.JobProfiles.Common.APISupport;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.CommonAction.Interface
{
    internal interface IAPISupport
    {
        Task<Response<T>> ExecuteGetRequest<T>(string endpoint, string apimSubscriptionKey);
    }
}
