﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.JobProfile
{
    public class SocCodeData
    {
        public string Id { get; set; }

        public string SOCCode { get; set; }

        public string Description { get; set; }

        public string ONetOccupationalCode { get; set; }

        public string UrlName { get; set; }

        public List<object> ApprenticeshipFramework { get; set; }

        public List<ApprenticeshipStandard> ApprenticeshipStandards { get; set; }
    }
}