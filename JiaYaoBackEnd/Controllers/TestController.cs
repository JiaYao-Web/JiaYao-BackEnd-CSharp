using JiaYao.Authorization;
using JiaYao.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        [HttpGet]
        [Route("CLI")]
        [MyNoAuthentication]
        public async Task<ActionResult<Message>> CLITestAsync()
        {
            Message message = new Message();
            message.status =  true;
            message.msg = JiaYaoCLI.IDGenerator.generate_id();
            return Ok(message);
        }
    }
}
