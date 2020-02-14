using System;
using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model
{
    public class WhatYouWillDoData
    {
        public string DailyTasks { get; set; }

        public List<object> Locations { get; set; }

        public List<object> Uniforms { get; set; }

        public List<object> Environments { get; set; }

        public string Introduction { get; set; }
    }
}
