using SM.Contracts.Models.Codes;

namespace SM.Contracts.Models.Result
{
    public class ResultCode
    {
        /// <summary>
        /// Indicates an information code.
        /// </summary>
        public CoreCode InformationCode { get; set; }

        /// <summary>
        /// Indicates a warning code.
        /// </summary>
        public CoreCode WarningCode { get; set; }

        /// <summary>
        /// Indicates an error code.
        /// </summary>
        public CoreCode ErrorCode { get; set; }        
    }
}