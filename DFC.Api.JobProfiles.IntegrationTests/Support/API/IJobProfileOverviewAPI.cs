using DFC.Api.JobProfiles.IntegrationTests.Model.ContentType.JobProfile;
using RestSharp;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.API
{
    public interface IJobProfileOverviewAPI
    {
        Task<IRestResponse<JobProfileContentType>> GetById(string id);
    }
}
