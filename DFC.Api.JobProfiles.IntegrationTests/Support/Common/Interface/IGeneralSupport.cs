namespace DFC.Api.JobProfiles.IntegrationTests.Support.CommonAction.Interface
{
    internal interface IGeneralSupport
    {
        void InitialiseAppSettings();

        byte[] ConvertObjectToByteArray(object obj);

        string RandomString(int length);
    }
}
