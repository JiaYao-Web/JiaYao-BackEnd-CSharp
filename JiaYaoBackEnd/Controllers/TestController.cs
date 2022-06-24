using JiaYao.Authorization;
using JiaYao.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace JiaYao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        [DllImport("JiaYaoWin32DLL.dll")]
        private static extern char getPrefix(int code);
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
        [HttpGet]
        [Route("Win32")]
        [MyNoAuthentication]
        public async Task<ActionResult<Message>> Win32TestAsync()
        {
            Message message = new Message();
            message.status = true;
            message.msg += getPrefix(3);
            return Ok(message);
        }
        [HttpGet]
        [Route("COM")]
        [MyNoAuthentication]
        public async Task<ActionResult<Message>> COMTestAsync()
        {
            Message message = new Message();
            message.status = true;
            Guid clsid = new Guid("8fd0a715-45ca-4956-9496-f56545be4388");
            Type comType = Type.GetTypeFromCLSID(clsid);
            Object comObj = Activator.CreateInstance(comType);
            Object[] paras = { "4" };
            Object result = comType.InvokeMember("Number", BindingFlags.InvokeMethod, null, comObj, paras);
            message.msg = result.ToString();
            return Ok(message);
        }
    }
}
