using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.JobProfile
{
    public class SocSkillsMatrixData
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Contextualised { get; set; }

        public string ONetAttributeType { get; set; }

        public double Rank { get; set; }

        public double ONetRank { get; set; }

        public List<RelatedSkill> RelatedSkill { get; set; }

        public List<object> RelatedSOC { get; set; }
    }
}