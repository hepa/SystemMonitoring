namespace SM.Contracts.Models.Codes
{
    public static class WarningCodes
    {
        /// <summary>
        /// Indicates a successful run with no warning(s).
        /// </summary>
        public static CoreCode NoWarning { get; } = new CoreCode {Code = 1, Message = "There was no warning."};
    }
}