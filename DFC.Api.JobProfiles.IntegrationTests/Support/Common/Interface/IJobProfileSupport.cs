using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using System;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.CommonAction.Interface
{
    internal interface IJobProfileSupport
    {
        Task DeleteJobProfileWithId(Topic topic, Guid jobProfileId);

        Task CreateJobProfile(Topic topic, Guid messageId, string canonicalName);
    }
}
