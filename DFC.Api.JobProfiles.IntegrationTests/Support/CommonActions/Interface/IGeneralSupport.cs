namespace DFC.Api.JobProfiles.IntegrationTests.Support.CommonAction.Interface
{
    internal interface IGeneralSupport
    {
        string RandomString(int length);

        byte[] ConvertObjectToByteArray(object obj);

        T GetResource<T>(string resourceName);
    }
}