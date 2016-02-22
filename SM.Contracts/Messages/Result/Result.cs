using SM.Contracts.Models.Core;

namespace SM.Contracts.Messages.Result
{
    public class Result
    {
        public RequestContext RequestContext { get; set; }

        /// <summary>
        /// Contains the result code.
        /// </summary>
        public int ResultCode { get; set; }
    }

    public class Result<T> : Result
    {
        /// <summary>
        /// Contains the data information.
        /// </summary>
        public T Data { get; set; }        
    }
}