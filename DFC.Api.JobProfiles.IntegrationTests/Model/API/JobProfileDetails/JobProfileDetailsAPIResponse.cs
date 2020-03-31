using System;
using System.Collections.Generic;

namespace DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileDetails
{
    public class JobProfileDetailsAPIResponse
    {
        public string Title { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string Url { get; set; }

        public string Soc { get; set; }

        public string ONetOccupationalCode { get; set; }

        public string AlternativeTitle { get; set; }

        public string Overview { get; set; }

        public string SalaryStarter { get; set; }

        public string SalaryExperienced { get; set; }

        public double MinimumHours { get; set; }

        public double MaximumHours { get; set; }

        public string WorkingHoursDetails { get; set; }

        public string WorkingPattern { get; set; }

        public string WorkingPatternDetails { get; set; }

        public HowToBecomeModel HowToBecome { get; set; }

        public WhatItTakesModel WhatItTakes { get; set; }

        public WhatYouWillDoModel WhatYouWillDo { get; set; }

        public CareerPathAndProgressionApiModel CareerPathAndProgression { get; set; }

        public List<RelatedCareer> RelatedCareers { get; set; }
    }
}