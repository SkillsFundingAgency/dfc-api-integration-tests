using System;
using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model
{

    public class RouteEntry
    {
        public int RouteName { get; set; }

        public List<object> EntryRequirements { get; set; }

        public List<object> MoreInformationLinks { get; set; }

        public string RouteSubjects { get; set; }

        public string FurtherRouteInformation { get; set; }

        public object RouteRequirement { get; set; }
    }
}
