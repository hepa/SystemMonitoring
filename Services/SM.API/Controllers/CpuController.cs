using System;
using System.Web.Http;

namespace SM.API.Controllers
{
    [RoutePrefix("cpu")]
    public class CpuController : ApiController
    {        
        [Authorize]
        [Route("cores")]
        public int GetCores()
        {
            Console.WriteLine(Request.RequestUri);
            return 4;
        }
    }
}