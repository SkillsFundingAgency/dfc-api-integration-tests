using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.APIResponse
{
    public class WhatItTakesModel
    {
        public string DigitalSkillsLevel { get; set; }

        public List<Skill> Skills { get; set; }

        public RestrictionsAndRequirements RestrictionsAndRequirements { get; set; }
    }
}
