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
    public class IngredientController : Controller
    {
        private JiaYaoContext _context;

        public IngredientController(JiaYaoContext context)
        {
            _context = context;
        }

        // 上传食材
        [Route("uploadIngredient")]
        [HttpPost]
        public async Task<ActionResult<Message>> uploadIngredient([FromForm]UploadIngredientRequest request)
        {
            return await IngredientService.uploadIngredient(request, _context);
        }

    }
}
