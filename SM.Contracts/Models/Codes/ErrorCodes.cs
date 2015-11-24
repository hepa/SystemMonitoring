namespace SM.Contracts.Models.Codes
{
    public static class ErrorCodes
    {
        /// <summary>
        /// Indicates a successful run with no error(s).
        /// </summary>
        public static readonly CoreCode NoError = new CoreCode {Code = 1, Message = "There was no error."};

        public static readonly CoreCode GenericError = new CoreCode { Code = -1000, Message = "A common generic error happend."};

        #region 10001-10999 
        public static readonly CoreCode WebServerStartFailure = new CoreCode { Code = -10001, Message = "The web server has not been started." };
        #endregion

    }
}