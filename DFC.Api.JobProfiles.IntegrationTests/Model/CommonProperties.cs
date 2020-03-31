using System;

namespace DFC.Api.JobProfiles.IntegrationTests.Model
{
    public class CommonProperties
    {
        public string Title { get; set; }

        public DateTime? LastModified { get; set; }

        public string SOCCode { get; set; }

        public string ONetOccupationalCode { get; set; }

        public string AlternativeTitle { get; set; }

        public string Overview { get; set; }

        public string SalaryStarter { get; set; }

        public string SalaryExperienced { get; set; }

        public string MinimumHours { get; set; }
    }
}