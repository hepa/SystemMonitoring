namespace SM.Contracts.Models.Codes
{
    public static class ErrorCodes
    {
        /// <summary>
        /// Indicates a successful run with no error(s).
        /// </summary>
        public static readonly CoreCode NoError = new CoreCode {Code = 1, Message = "There was no error."};
    }
}