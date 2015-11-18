namespace SM.Contracts.Models.Codes
{
    public static class InformationCodes
    {
        /// <summary>
        /// Indicates a successful run with no information(s).
        /// </summary>
        public static CoreCode NoInformation { get; } = new CoreCode {Code = 1, Message = "There was no information."};
    }
}