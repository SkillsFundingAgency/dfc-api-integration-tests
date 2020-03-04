using DFC.Api.JobProfiles.IntegrationTests.Model.ContentType.JobProfile;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.ContentType
{
    public class SOCCodeContentType : SocCodeData
    {
        public string JobProfileId { get; set; }

        public string JobProfileTitle { get; set; }

        public string Title { get; set; }
    }
}
