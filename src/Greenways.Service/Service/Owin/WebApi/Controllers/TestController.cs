using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Greenways.Service.WebApi.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public string Now()
        {
            return DateTime.Now.ToString("o");
        }

        [HttpGet]
        public HttpResponseMessage Index()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(typeof(TestController).Name)
            };
        }
    }
}
