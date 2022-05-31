using JiaYao.Models;
using JiaYao.Request;
using JiaYao.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : Controller
    {
        private JiaYaoContext _context;

        public MenuController(JiaYaoContext context)
        {
            _context = context;
        }

        // 上传菜谱
        [Route("uploadMenu")]
        [HttpPost]
        public async Task<ActionResult<Message>> uploadMenu([FromForm]UploadMenuRequest request, [FromHeader] string myAuthentication)
        {
            return await MenuService.uploadMenu(request, myAuthentication, _context);
        }
    }
}
