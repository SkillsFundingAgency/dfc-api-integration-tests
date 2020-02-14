using DFC.Api.JobProfiles.Common.APISupport;
using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.Api.JobProfiles.IntegrationTests.Model.JobProfile;
using DFC.Api.JobProfiles.IntegrationTests.Support.AppSettings;
using DFC.Api.JobProfiles.IntegrationTests.Support.CommonAction.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.Common
{
    public class CommonAction : IGeneralSupport, IJobProfileSupport
    {
        private static readonly Random Random = new Random();

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public void InitialiseAppSettings()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            Settings.ServiceBusConfig.ConnectionString = configuration.GetSection("ServiceBusConfig").GetSection("Endpoint").Value;
            Settings.APIConfig.Version = configuration.GetSection("APIConfig").GetSection("Version").Value;
            Settings.APIConfig.ApimSubscriptionKey = configuration.GetSection("APIConfig").GetSection("ApimSubscriptionKey").Value;
            Settings.APIConfig.EndpointBaseUrl.ProfileDetail = configuration.GetSection("APIConfig").GetSection("EndpointBaseUrl").GetSection("ProfileDetail").Value;
            Settings.APIConfig.EndpointBaseUrl.ProfileSearch = configuration.GetSection("APIConfig").GetSection("EndpointBaseUrl").GetSection("ProfileSearch").Value;
            Settings.APIConfig.EndpointBaseUrl.ProfileSummary = configuration.GetSection("APIConfig").GetSection("EndpointBaseUrl").GetSection("ProfileSummary").Value;
            if (!int.TryParse(configuration.GetSection("GracePeriodInSeconds").Value, out int gracePeriodInSeconds))
            {
                throw new InvalidCastException("Unable to retrieve an integer value for the grace period setting");
            }

            Settings.GracePeriod = TimeSpan.FromSeconds(gracePeriodInSeconds);
            if (!int.TryParse(configuration.GetSection("DeploymentWaitInMinutes").Value, out int deploymentWaitInMinutes))
            {
                throw new InvalidCastException("Unable to retrieve an integer value for the deployment wait setting");
            }

            Settings.DeploymentWaitInMinutes = TimeSpan.FromMinutes(deploymentWaitInMinutes);
        }

        public async Task DeleteJobProfileWithId(Topic topic, Guid jobProfileId)
        {
            JobProfileContentType messageBody = ResourceManager.GetResource<JobProfileContentType>("JobProfileDelete");
            messageBody.JobProfileId = jobProfileId.ToString();
            Message deleteMessage = this.CreateDeleteMessage(jobProfileId, this.ConvertObjectToByteArray(messageBody));
            await topic.SendAsync(deleteMessage).ConfigureAwait(true);
        }

        public async Task CreateJobProfile(Topic topic, Guid messageId, string canonicalName)
        {
            JobProfileContentType messageBody = ResourceManager.GetResource<JobProfileContentType>("JobProfileCreate");
            messageBody.JobProfileId = messageId.ToString();
            messageBody.UrlName = canonicalName;
            messageBody.CanonicalName = canonicalName;
            Message message = this.CreateCreateMessage(messageId, this.ConvertObjectToByteArray(messageBody));
            await topic.SendAsync(message).ConfigureAwait(true);
        }

        public async Task<Response<T>> ExecuteGetRequest<T>(string endpoint, bool authoriseRequest = true)
        {
            GetRequest getRequest = new GetRequest(endpoint);
            getRequest.AddVersionHeader(Settings.APIConfig.Version);

            if (authoriseRequest)
            {
                getRequest.AddApimKeyHeader(Settings.APIConfig.ApimSubscriptionKey);
            }
            else
            {
                getRequest.AddApimKeyHeader(this.RandomString(20).ToLower(CultureInfo.CurrentCulture));
            }

            await Task.Delay(5000).ConfigureAwait(true);
            Response<T> response = getRequest.Execute<T>();

            DateTime startTime = DateTime.Now;
            while (response.HttpStatusCode.Equals(HttpStatusCode.NoContent) && DateTime.Now - startTime < Settings.GracePeriod)
            {
                await Task.Delay(500).ConfigureAwait(true);
                response = getRequest.Execute<T>();
            }

            return response;
        }

        public byte[] ConvertObjectToByteArray(object obj)
        {
            string serialisedContent = JsonConvert.SerializeObject(obj);
            return Encoding.ASCII.GetBytes(serialisedContent);
        }

        private Message CreateCreateMessage(Guid messageId, byte[] messageBody)
        {
            Message message = new Message();
            message.ContentType = "application/json";
            message.Body = messageBody;
            message.CorrelationId = Guid.NewGuid().ToString();
            message.Label = "Automated message";
            message.MessageId = Guid.NewGuid().ToString();
            message.UserProperties.Add("Id", messageId);
            message.UserProperties.Add("ActionType", "Published");
            message.UserProperties.Add("CType", "JobProfile");
            return message;
        }

        private Message CreateDeleteMessage(Guid messageId, byte[] messageBody)
        {
            Message message = new Message();
            message.ContentType = "application/json";
            message.UserProperties.Add("Id", messageId);
            message.UserProperties.Add("ActionType", "Deleted");
            message.UserProperties.Add("CType", "JobProfile");
            message.Label = "Automated message";
            message.Body = messageBody;
            return message;
        }
    }
}
