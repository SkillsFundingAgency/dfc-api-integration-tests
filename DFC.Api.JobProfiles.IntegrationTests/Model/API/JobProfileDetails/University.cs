using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileDetails
{
    public class University
    {
        public List<string> RelevantSubjects { get; set; }

        public List<string> FurtherInformation { get; set; }

        public string EntryRequirementPreface { get; set; }

        public List<string> EntryRequirements { get; set; }

        public List<string> AdditionalInformation { get; set; }
    }
}