using DFC.Api.JobProfiles.IntegrationTests.Model.API.JobProfileDetails;
using DFC.Api.JobProfiles.IntegrationTests.Model.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support;
using DFC.Api.JobProfiles.IntegrationTests.Support.API;
using DFC.Api.JobProfiles.IntegrationTests.Support.API.RestFactory;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Test
{
    public class DetailTest : SetUpAndTearDown
    {
        private JobProfileAPI authorisedApi;
        private JobProfileAPI unauthorisedApi;

        [SetUp]
        public void SetUp()
        {
            APISettings apiSettings = new APISettings
            {
                Endpoint = this.appSettings.APIConfig.EndpointBaseUrl.ProfileDetail,
            };

            var tempAppSettings = new AppSettings();
            tempAppSettings.APIConfig = new APIConfig();
            tempAppSettings.APIConfig.ApimSubscriptionKey = this.commonAction.RandomString(10);
            tempAppSettings.APIConfig.Version = this.appSettings.APIConfig.Version;
            this.authorisedApi = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), this.appSettings, apiSettings);
            this.unauthorisedApi = new JobProfileAPI(new RestClientFactory(), new RestRequestFactory(), tempAppSettings, apiSettings);
        }

        [Test]
        public async Task SuccessfulJobDetailsRequest()
        {
            var apiResponse = await this.authorisedApi.GetByName<JobProfileDetailsAPIResponse>(this.jobProfile.CanonicalName).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, apiResponse.StatusCode, "Job details: Unable to retrieve the job details for a job profile.");
            Assert.AreEqual(apiResponse.Data.Title, this.jobProfile.Title);
            Assert.AreEqual(apiResponse.Data.LastUpdatedDate, this.jobProfile.LastModified);
            Assert.AreEqual(apiResponse.Data.Soc, this.jobProfile.SocCodeData.SOCCode.Substring(0, 4));
            Assert.AreEqual(apiResponse.Data.ONetOccupationalCode, this.jobProfile.SocCodeData.ONetOccupationalCode);
            Assert.AreEqual(apiResponse.Data.AlternativeTitle, this.jobProfile.AlternativeTitle);
            Assert.AreEqual(apiResponse.Data.Overview, this.jobProfile.Overview);
            Assert.AreEqual(apiResponse.Data.SalaryStarter, ((int)this.jobProfile.SalaryStarter).ToString(CultureInfo.CurrentCulture));
            Assert.AreEqual(apiResponse.Data.SalaryExperienced, ((int)this.jobProfile.SalaryExperienced).ToString(CultureInfo.CurrentCulture));
            Assert.AreEqual(apiResponse.Data.MinimumHours, this.jobProfile.MinimumHours);
            Assert.AreEqual(apiResponse.Data.MaximumHours, this.jobProfile.MaximumHours);
            Assert.AreEqual(apiResponse.Data.WorkingHoursDetails, this.jobProfile.WorkingHoursDetails[0].Title);
            Assert.AreEqual(apiResponse.Data.WorkingPattern, this.jobProfile.WorkingPattern[0].Title);
            Assert.AreEqual(apiResponse.Data.WorkingPatternDetails, this.jobProfile.WorkingPatternDetails[0].Title);
        }

        [Test]
        public async Task NoContentJobDetailsRequest()
        {
            var apiResponse = await this.authorisedApi.GetByName<JobProfileDetailsAPIResponse>(this.commonAction.RandomString(10)).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.NoContent, apiResponse.StatusCode, "Job details: The service should report that the job profile is not present.");
        }

        [Test]
        public async Task UnauthorisedJobDetailsRequest()
        {
            var apiResponse = await this.unauthorisedApi.GetByName<JobProfileDetailsAPIResponse>(this.jobProfile.CanonicalName).ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.Unauthorized, apiResponse.StatusCode, "Job details: The service should report that the request is unauthorised.");
        }
    }
}