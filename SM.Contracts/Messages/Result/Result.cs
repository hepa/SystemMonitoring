namespace SM.Contracts.Models.Result
{
    public class Result<T>
    {
        /// <summary>
        /// Contains the data information.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Contains the result code.
        /// </summary>
        public ResultCode ResultCode { get; set; }        
    }
}