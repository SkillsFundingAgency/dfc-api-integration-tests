using DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileSummary;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.API
{
    public interface IJobProfileAPI
    {
        Task<IRestResponse<T>> GetById<T>(string id) where T : class, new();

        Task<IRestResponse<T>> GetByName<T>(string id) where T : class, new();

        Task<IRestResponse<List<JobProfileSummaryAPIResponse>>> GetSummaries();
    }
}
