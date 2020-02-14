using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.APIResponse
{
    public class WhatYouWillDoModel
    {
        public List<string> WYDDayToDayTasks { get; set; }

        public WorkingEnvironment WorkingEnvironment { get; set; }
    }
}
