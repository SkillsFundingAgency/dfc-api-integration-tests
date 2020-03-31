using DFC.Api.JobProfiles.IntegrationTests.Model.Support;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DFC.Api.JobProfiles.IntegrationTests.Support
{
    public class SetUpAndTearDownBase
    {
        public SetUpAndTearDownBase()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            this.AppSettings = configuration.Get<AppSettings>();
            this.CommonAction = new CommonActions.CommonAction();
        }

        protected CommonActions.CommonAction CommonAction { get; set; }

        protected AppSettings AppSettings { get; set; }
    }
}