using System;
using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model
{
    public class College
    {
        public List<object> RelevantSubjects { get; set; }

        public List<object> FurtherInformation { get; set; }

        public object EntryRequirementPreface { get; set; }

        public List<object> EntryRequirements { get; set; }

        public List<object> AdditionalInformation { get; set; }
    }
}
