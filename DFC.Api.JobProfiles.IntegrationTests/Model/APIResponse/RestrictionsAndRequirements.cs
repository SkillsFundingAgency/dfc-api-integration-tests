﻿using System;
using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model
{
    public class RestrictionsAndRequirements
    {
        public List<string> RelatedRestrictions { get; set; }

        public List<object> OtherRequirements { get; set; }
    }
}
