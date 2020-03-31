using DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileDetails;
using DFC.Api.JobProfiles.IntegrationTests.Model.ContentType.JobProfile;
using DFC.Api.JobProfiles.IntegrationTests.Model.Support;
using NUnit.Framework;
using RestSharp;
using System.Globalization;

namespace DFC.Api.JobProfiles.IntegrationTests.Support
{
    public class CustomAssert
    {
        private IRestResponse<JobProfileDetailsAPIResponse> apiResponse;
        private JobProfileContentType jobProfile;
        private AppSettings appSettings;

        public CustomAssert(IRestResponse<JobProfileDetailsAPIResponse> apiResponse, JobProfileContentType jobProfile)
        {
            this.apiResponse = apiResponse;
            this.jobProfile = jobProfile;
            this.appSettings = appSettings;
        }

        public void PropertiesMatch()
        {
            Assert.AreEqual(this.apiResponse.Data.Title, this.jobProfile.Title);
            Assert.AreEqual(this.apiResponse.Data.LastUpdatedDate, this.jobProfile.LastModified);
            Assert.AreEqual(this.apiResponse.Data.Soc, this.jobProfile.SocCodeData.SOCCode.Substring(0, 4));
            Assert.AreEqual(this.apiResponse.Data.ONetOccupationalCode, this.jobProfile.SocCodeData.ONetOccupationalCode);
            Assert.AreEqual(this.apiResponse.Data.AlternativeTitle, this.jobProfile.AlternativeTitle);
            Assert.AreEqual(this.apiResponse.Data.Overview, this.jobProfile.Overview);
            Assert.AreEqual(this.apiResponse.Data.SalaryStarter, ((int)this.jobProfile.SalaryStarter).ToString(CultureInfo.CurrentCulture));
            Assert.AreEqual(this.apiResponse.Data.SalaryExperienced, ((int)this.jobProfile.SalaryExperienced).ToString(CultureInfo.CurrentCulture));
            Assert.AreEqual(this.apiResponse.Data.MinimumHours, this.jobProfile.MinimumHours);
            Assert.AreEqual(this.apiResponse.Data.MaximumHours, this.jobProfile.MaximumHours);
            Assert.AreEqual(this.apiResponse.Data.WorkingHoursDetails, this.jobProfile.WorkingHoursDetails[0].Title);
            Assert.AreEqual(this.apiResponse.Data.WorkingPattern, this.jobProfile.WorkingPattern[0].Title);
            Assert.AreEqual(this.apiResponse.Data.WorkingPatternDetails, this.jobProfile.WorkingPatternDetails[0].Title);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.University.EntryRequirements[0], this.jobProfile.HowToBecomeData.RouteEntries[0].EntryRequirements[0].Info);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.University.AdditionalInformation[0], "[" + this.jobProfile.HowToBecomeData.RouteEntries[0].MoreInformationLinks[0].Text + " | " + this.jobProfile.HowToBecomeData.RouteEntries[0].MoreInformationLinks[0].Url + "]");
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.University.EntryRequirementPreface, this.jobProfile.HowToBecomeData.RouteEntries[0].RouteRequirement);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.University.FurtherInformation[0], this.jobProfile.HowToBecomeData.RouteEntries[0].FurtherRouteInformation);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.University.RelevantSubjects[0], this.jobProfile.HowToBecomeData.RouteEntries[0].RouteSubjects);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.College.EntryRequirements[0], this.jobProfile.HowToBecomeData.RouteEntries[1].EntryRequirements[0].Info);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.College.AdditionalInformation[0], "[" + this.jobProfile.HowToBecomeData.RouteEntries[1].MoreInformationLinks[0].Text + " | " + this.jobProfile.HowToBecomeData.RouteEntries[1].MoreInformationLinks[0].Url + "]");
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.College.EntryRequirementPreface, this.jobProfile.HowToBecomeData.RouteEntries[1].RouteRequirement);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.College.FurtherInformation[0], this.jobProfile.HowToBecomeData.RouteEntries[1].FurtherRouteInformation);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.College.RelevantSubjects[0], this.jobProfile.HowToBecomeData.RouteEntries[1].RouteSubjects);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.Apprenticeship.EntryRequirements[0], this.jobProfile.HowToBecomeData.RouteEntries[2].EntryRequirements[0].Info);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.Apprenticeship.AdditionalInformation[0], "[" + this.jobProfile.HowToBecomeData.RouteEntries[2].MoreInformationLinks[0].Text + " | " + this.jobProfile.HowToBecomeData.RouteEntries[2].MoreInformationLinks[0].Url + "]");
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.Apprenticeship.EntryRequirementPreface, this.jobProfile.HowToBecomeData.RouteEntries[2].RouteRequirement);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.Apprenticeship.FurtherInformation[0], this.jobProfile.HowToBecomeData.RouteEntries[2].FurtherRouteInformation);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.Apprenticeship.RelevantSubjects[0], this.jobProfile.HowToBecomeData.RouteEntries[2].RouteSubjects);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.Work[0], this.jobProfile.HowToBecomeData.FurtherRoutes.Work);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.Volunteering[0], this.jobProfile.HowToBecomeData.FurtherRoutes.Volunteering);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRoutes.OtherRoutes[0], this.jobProfile.HowToBecomeData.FurtherRoutes.OtherRoutes);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.EntryRouteSummary[0], this.jobProfile.HowToBecomeData.IntroText);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.MoreInformation.CareerTips[0], this.jobProfile.HowToBecomeData.FurtherInformation.CareerTips);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.MoreInformation.FurtherInformation[0], this.jobProfile.HowToBecomeData.FurtherInformation.FurtherInformation);
            Assert.AreEqual(this.apiResponse.Data.HowToBecome.MoreInformation.ProfessionalAndIndustryBodies[0], this.jobProfile.HowToBecomeData.FurtherInformation.ProfessionalAndIndustryBodies);
            Assert.AreEqual(this.apiResponse.Data.WhatItTakes.DigitalSkillsLevel, this.jobProfile.DigitalSkillsLevel);
            Assert.AreEqual(this.apiResponse.Data.WhatItTakes.Skills[0].Description, this.jobProfile.SocSkillsMatrixData[0].RelatedSkill[0].Description);
            Assert.AreEqual(this.apiResponse.Data.WhatItTakes.Skills[0].ONetAttributeType, this.jobProfile.SocSkillsMatrixData[0].ONetAttributeType);
            Assert.AreEqual(this.apiResponse.Data.WhatItTakes.Skills[0].ONetRank, this.jobProfile.SocSkillsMatrixData[0].ONetRank.ToString(CultureInfo.CurrentCulture));
            Assert.AreEqual(this.apiResponse.Data.WhatItTakes.Skills[0].ONetElementId, this.jobProfile.SocSkillsMatrixData[0].RelatedSkill[0].ONetElementId);
            Assert.AreEqual(this.apiResponse.Data.WhatItTakes.RestrictionsAndRequirements.OtherRequirements[0], this.jobProfile.OtherRequirements);
            Assert.AreEqual(this.apiResponse.Data.WhatItTakes.RestrictionsAndRequirements.RelatedRestrictions[0], this.jobProfile.Restrictions[0].Info);
            Assert.AreEqual(this.apiResponse.Data.WhatYouWillDo.WYDDayToDayTasks[0], this.jobProfile.WhatYouWillDoData.DailyTasks);
            Assert.AreEqual(this.apiResponse.Data.WhatYouWillDo.WorkingEnvironment.Environment, $"Your working environment may be {this.jobProfile.WhatYouWillDoData.Environments[0].Description}.");
            Assert.AreEqual(this.apiResponse.Data.WhatYouWillDo.WorkingEnvironment.Location, $"You could work {this.jobProfile.WhatYouWillDoData.Locations[0].Description}.");
            Assert.AreEqual(this.apiResponse.Data.WhatYouWillDo.WorkingEnvironment.Uniform, $"You may need to wear {this.jobProfile.WhatYouWillDoData.Uniforms[0].Description}.");
            Assert.AreEqual(this.apiResponse.Data.CareerPathAndProgression.CareerPathAndProgression[0], this.jobProfile.CareerPathAndProgression);
            Assert.AreEqual(this.apiResponse.Data.RelatedCareers[0].Title, this.jobProfile.RelatedCareersData[0].Title);
            Assert.AreEqual(this.apiResponse.Data.RelatedCareers[0].Url, $"{this.appSettings.APIConfig.EndpointBaseUrl.ProfileDetail}{this.jobProfile.RelatedCareersData[0].ProfileLink}");
        }
    }
}
