using DFC.Api.JobProfiles.IntegrationTests.Model.Support;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support
{
    public class SetUpAndTearDownBase
    {
        protected CommonAction commonAction;
        protected AppSettings appSettings;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            this.appSettings = configuration.Get<AppSettings>();
            this.appSettings.APIConfig.EndpointBaseUrl.ProfileDetail = new Uri(this.appSettings.APIConfig.EndpointBaseUrl.ProfileDetail.AbsoluteUri.TrimEnd('/'));
            this.appSettings.APIConfig.EndpointBaseUrl.ProfileSearch = new Uri(this.appSettings.APIConfig.EndpointBaseUrl.ProfileSearch.AbsoluteUri.TrimEnd('/'));
            this.appSettings.APIConfig.EndpointBaseUrl.ProfileSummary = new Uri(this.appSettings.APIConfig.EndpointBaseUrl.ProfileSummary.AbsoluteUri.TrimEnd('/'));
            this.commonAction = new CommonAction();
        }
    }
}
