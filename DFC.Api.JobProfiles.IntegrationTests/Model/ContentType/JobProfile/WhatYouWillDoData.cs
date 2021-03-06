﻿using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.ContentType.JobProfile
{
    public class WhatYouWillDoData
    {
        public string DailyTasks { get; set; }

        public List<Location> Locations { get; set; }

        public List<Uniform> Uniforms { get; set; }

        public List<Environment> Environments { get; set; }

        public string Introduction { get; set; }
    }
}