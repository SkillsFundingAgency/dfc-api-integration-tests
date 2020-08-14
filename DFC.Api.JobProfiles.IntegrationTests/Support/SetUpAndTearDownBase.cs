using DFC.Api.JobProfiles.IntegrationTests.Model.Support;
using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.IO;

namespace DFC.Api.JobProfiles.IntegrationTests.Support
{
    public class SetUpAndTearDownBase
    {
        public SetUpAndTearDownBase()
        {
            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = $"ExecutionLog_{DateTime.Now:dd-M-yyyy_HH-mm-ss}.txt" };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            NLog.LogManager.Configuration = config;
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            this.AppSettings = configuration.Get<AppSettings>();
            this.AppSettings.APIConfig.EndpointBaseUrl.ProfileDetail = new Uri(this.AppSettings.APIConfig.EndpointBaseUrl.ProfileDetail.AbsoluteUri.TrimEnd('/'));
            this.AppSettings.APIConfig.EndpointBaseUrl.ProfileSearch = new Uri(this.AppSettings.APIConfig.EndpointBaseUrl.ProfileSearch.AbsoluteUri.TrimEnd('/'));
            this.AppSettings.APIConfig.EndpointBaseUrl.ProfileSummary = new Uri(this.AppSettings.APIConfig.EndpointBaseUrl.ProfileSummary.AbsoluteUri.TrimEnd('/'));
            this.CommonAction = new CommonActions.CommonAction();
        }

        protected CommonActions.CommonAction CommonAction { get; set; }

        protected AppSettings AppSettings { get; set; }
    }
}