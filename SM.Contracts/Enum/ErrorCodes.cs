using SM.Contracts.Attributes;

namespace SM.Contracts.Enum
{
    [Code]
    public enum ErrorCodes
    {
        [Description("There was no error.")]
        NoError = 1,

        [Description("A common generic error happend.")]
        GenericError = -1000,

        [Description("WebserverStartFailure.")]
        WebServerStartFailure = -1001
    }
}