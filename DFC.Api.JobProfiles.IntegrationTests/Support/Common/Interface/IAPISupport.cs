using DFC.Api.JobProfiles.Common.APISupport;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.CommonAction.Interface
{
    internal interface IAPISupport
    {
        Task<Response<T>> ExecuteGetRequest<T>(string endpoint, List<KeyValuePair<string, string>> queryParams, bool authoriseRequest = true);
    }
}
