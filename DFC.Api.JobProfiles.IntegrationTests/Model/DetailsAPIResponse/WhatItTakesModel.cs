using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.DetailsAPIResponse
{
    public class WhatItTakesModel
    {
        public string DigitalSkillsLevel { get; set; }

        public List<Skill> Skills { get; set; }

        public RestrictionsAndRequirements RestrictionsAndRequirements { get; set; }
    }
}
