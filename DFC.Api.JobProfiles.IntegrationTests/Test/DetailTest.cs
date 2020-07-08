using DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileDetails;
using DFC.Api.JobProfiles.IntegrationTests.Model.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support.API;
using DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory;
using NUnit.Framework;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class DetailTest : SetUpAndTearDown
    {
        private JobProfileApi authorisedApi;
        private JobProfileApi unauthorisedApi;

        [SetUp]
        public void SetUp()
        {
            var apiSettings = new APISettings { Endpoint = this.AppSettings.APIConfig.EndpointBaseUrl.ProfileDetail };
            var tempAppSettings = new AppSettings
            {
                APIConfig = new APIConfig
                {
                    ApimSubscriptionKey = this.CommonAction.RandomString(10),
                    Version = this.AppSettings.APIConfig.Version,
                },
            };
            this.authorisedApi = new JobProfileApi(new RestClientFactory(), new RestRequestFactory(), this.AppSettings, apiSettings);
            this.unauthorisedApi = new JobProfileApi(new RestClientFactory(), new RestRequestFactory(), tempAppSettings, apiSettings);
        }

        [Test]
        public async Task SuccessfulJobDetailsRequest()
        {
            var apiResponse = await this.authorisedApi.GetByName<JobProfileDetailsAPIResponse>(this.JobProfile.CanonicalName).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, apiResponse.StatusCode, "Job details: Unable to retrieve the job details for a job profile.");
            Assert.AreEqual(apiResponse.Data.Title, "Automatically generated record");
            Assert.AreEqual(apiResponse.Data.LastUpdatedDate, this.JobProfile.LastModified);
            Assert.AreEqual(apiResponse.Data.Soc, this.JobProfile.SocCodeData.SOCCode.Substring(0, 4));
            Assert.AreEqual(apiResponse.Data.ONetOccupationalCode, this.JobProfile.SocCodeData.ONetOccupationalCode);
            Assert.AreEqual(apiResponse.Data.AlternativeTitle, this.JobProfile.AlternativeTitle);
            Assert.AreEqual(apiResponse.Data.Overview, this.JobProfile.Overview);
            Assert.AreEqual(apiResponse.Data.SalaryStarter, ((int)this.JobProfile.SalaryStarter).ToString(CultureInfo.CurrentCulture));
            Assert.AreEqual(apiResponse.Data.SalaryExperienced, ((int)this.JobProfile.SalaryExperienced).ToString(CultureInfo.CurrentCulture));
            Assert.AreEqual(apiResponse.Data.MinimumHours, this.JobProfile.MinimumHours);
            Assert.AreEqual(apiResponse.Data.MaximumHours, this.JobProfile.MaximumHours);
            Assert.AreEqual(apiResponse.Data.WorkingHoursDetails, this.JobProfile.WorkingHoursDetails[0].Title);
            Assert.AreEqual(apiResponse.Data.WorkingPattern, this.JobProfile.WorkingPattern[0].Title);
            Assert.AreEqual(apiResponse.Data.WorkingPatternDetails, this.JobProfile.WorkingPatternDetails[0].Title);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.University.EntryRequirements[0], this.JobProfile.HowToBecomeData.RouteEntries[0].EntryRequirements[0].Info);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.University.AdditionalInformation[0], "[" + this.JobProfile.HowToBecomeData.RouteEntries[0].MoreInformationLinks[0].Text + " | " + this.JobProfile.HowToBecomeData.RouteEntries[0].MoreInformationLinks[0].Url + "]");
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.University.EntryRequirementPreface, this.JobProfile.HowToBecomeData.RouteEntries[0].RouteRequirement);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.University.FurtherInformation[0], this.JobProfile.HowToBecomeData.RouteEntries[0].FurtherRouteInformation);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.University.RelevantSubjects[0], this.JobProfile.HowToBecomeData.RouteEntries[0].RouteSubjects);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.College.EntryRequirements[0], this.JobProfile.HowToBecomeData.RouteEntries[1].EntryRequirements[0].Info);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.College.AdditionalInformation[0], "[" + this.JobProfile.HowToBecomeData.RouteEntries[1].MoreInformationLinks[0].Text + " | " + this.JobProfile.HowToBecomeData.RouteEntries[1].MoreInformationLinks[0].Url + "]");
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.College.EntryRequirementPreface, this.JobProfile.HowToBecomeData.RouteEntries[1].RouteRequirement);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.College.FurtherInformation[0], this.JobProfile.HowToBecomeData.RouteEntries[1].FurtherRouteInformation);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.College.RelevantSubjects[0], this.JobProfile.HowToBecomeData.RouteEntries[1].RouteSubjects);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.Apprenticeship.EntryRequirements[0], this.JobProfile.HowToBecomeData.RouteEntries[2].EntryRequirements[0].Info);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.Apprenticeship.AdditionalInformation[0], "[" + this.JobProfile.HowToBecomeData.RouteEntries[2].MoreInformationLinks[0].Text + " | " + this.JobProfile.HowToBecomeData.RouteEntries[2].MoreInformationLinks[0].Url + "]");
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.Apprenticeship.EntryRequirementPreface, this.JobProfile.HowToBecomeData.RouteEntries[2].RouteRequirement);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.Apprenticeship.FurtherInformation[0], this.JobProfile.HowToBecomeData.RouteEntries[2].FurtherRouteInformation);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.Apprenticeship.RelevantSubjects[0], this.JobProfile.HowToBecomeData.RouteEntries[2].RouteSubjects);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.Work[0], this.JobProfile.HowToBecomeData.FurtherRoutes.Work);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.Volunteering[0], this.JobProfile.HowToBecomeData.FurtherRoutes.Volunteering);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRoutes.OtherRoutes[0], this.JobProfile.HowToBecomeData.FurtherRoutes.OtherRoutes);
            Assert.AreEqual(apiResponse.Data.HowToBecome.EntryRouteSummary[0], this.JobProfile.HowToBecomeData.IntroText);
            Assert.AreEqual(apiResponse.Data.HowToBecome.MoreInformation.CareerTips[0], this.JobProfile.HowToBecomeData.FurtherInformation.CareerTips);
            Assert.AreEqual(apiResponse.Data.HowToBecome.MoreInformation.FurtherInformation[0], this.JobProfile.HowToBecomeData.FurtherInformation.FurtherInformation);
            Assert.AreEqual(apiResponse.Data.HowToBecome.MoreInformation.ProfessionalAndIndustryBodies[0], this.JobProfile.HowToBecomeData.FurtherInformation.ProfessionalAndIndustryBodies);
            Assert.AreEqual(apiResponse.Data.WhatItTakes.DigitalSkillsLevel, this.JobProfile.DigitalSkillsLevel);
            Assert.AreEqual(apiResponse.Data.WhatItTakes.Skills[0].Description, this.JobProfile.SocSkillsMatrixData[0].RelatedSkill[0].Description);
            Assert.AreEqual(apiResponse.Data.WhatItTakes.Skills[0].ONetAttributeType, this.JobProfile.SocSkillsMatrixData[0].ONetAttributeType);
            Assert.AreEqual(apiResponse.Data.WhatItTakes.Skills[0].ONetRank, this.JobProfile.SocSkillsMatrixData[0].ONetRank.ToString(CultureInfo.CurrentCulture));
            Assert.AreEqual(apiResponse.Data.WhatItTakes.Skills[0].ONetElementId, this.JobProfile.SocSkillsMatrixData[0].RelatedSkill[0].ONetElementId);
            Assert.AreEqual(apiResponse.Data.WhatItTakes.RestrictionsAndRequirements.OtherRequirements[0], this.JobProfile.OtherRequirements);
            Assert.AreEqual(apiResponse.Data.WhatItTakes.RestrictionsAndRequirements.RelatedRestrictions[0], this.JobProfile.Restrictions[0].Info);
            Assert.AreEqual(apiResponse.Data.WhatYouWillDo.WYDDayToDayTasks[0], this.JobProfile.WhatYouWillDoData.DailyTasks);
            Assert.AreEqual(apiResponse.Data.WhatYouWillDo.WorkingEnvironment.Environment, $"Your working environment may be {this.JobProfile.WhatYouWillDoData.Environments[0].Description}.");
            Assert.AreEqual(apiResponse.Data.WhatYouWillDo.WorkingEnvironment.Location, $"You could work {this.JobProfile.WhatYouWillDoData.Locations[0].Description}.");
            Assert.AreEqual(apiResponse.Data.WhatYouWillDo.WorkingEnvironment.Uniform, $"You may need to wear {this.JobProfile.WhatYouWillDoData.Uniforms[0].Description}.");
            Assert.AreEqual(apiResponse.Data.CareerPathAndProgression.CareerPathAndProgression[0], this.JobProfile.CareerPathAndProgression);
            Assert.AreEqual(apiResponse.Data.RelatedCareers[0].Title, this.JobProfile.RelatedCareersData[0].Title);
            Assert.AreEqual(apiResponse.Data.RelatedCareers[0].Url, $"{this.AppSettings.APIConfig.EndpointBaseUrl.ProfileDetail}{this.JobProfile.RelatedCareersData[0].ProfileLink}");
        }

        [Test]
        public async Task NoContentJobDetailsRequest()
        {
            var apiResponse = await this.authorisedApi.GetByName<JobProfileDetailsAPIResponse>(this.CommonAction.RandomString(10)).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.NoContent, apiResponse.StatusCode, "Job details: The service should report that the job profile is not present.");
        }

        [Test]
        public async Task UnauthorisedJobDetailsRequest()
        {
            var apiResponse = await this.unauthorisedApi.GetByName<JobProfileDetailsAPIResponse>(this.JobProfile.CanonicalName).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, apiResponse.StatusCode, "Job details: The service should report that the request is unauthorised.");
        }
    }
}