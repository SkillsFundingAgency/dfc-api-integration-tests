using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileDetails
{
    public class WhatYouWillDoModel
    {
        public List<string> WYDDayToDayTasks { get; set; }

        public WorkingEnvironment WorkingEnvironment { get; set; }
    }
}