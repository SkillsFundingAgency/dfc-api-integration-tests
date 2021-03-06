﻿using System;
using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.API
{
    public class APISettings
    {
        public Uri Endpoint { get; set; }

        public List<KeyValuePair<string, string>> QueryParameters { get; set; } = new List<KeyValuePair<string, string>>();
    }
}