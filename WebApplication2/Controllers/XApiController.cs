using Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TinCan;
using TinCan.LRSResponses;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XApiController : ControllerBase
    {
         readonly IXApi apiService;

        public XApiController(IXApi apiService)
        {
            this.apiService = apiService;
        }


        [HttpGet]
        [Route("GetStatement")]
        public List<Statement> getStatement()
        {
            return apiService.GetStatements();
        }


        [HttpPost]
        [Route("SendStatement")]
        public bool sendStatement()
        {
            return apiService.SendStatement();        }
        }
 
}
