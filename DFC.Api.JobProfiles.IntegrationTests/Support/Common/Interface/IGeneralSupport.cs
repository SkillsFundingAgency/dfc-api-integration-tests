using DFC.Api.JobProfiles.IntegrationTests.Support.AppSettings;

namespace DFC.Api.JobProfiles.IntegrationTests.Support.CommonAction.Interface
{
    internal interface IGeneralSupport
    {
        Settings GetAppSettings();

        byte[] ConvertObjectToByteArray(object obj);

        string RandomString(int length);
    }
}
