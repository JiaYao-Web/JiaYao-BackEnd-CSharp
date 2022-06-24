using JiaYao.Models;
using JiaYao.Request;
using JiaYao.Response;
using JiaYao.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private JiaYaoContext _context;
        public SearchController(JiaYaoContext context)
        {
            _context = context;
        }
        // 搜索菜谱
        [Route("searchMenu")]
        [HttpPost]
        public async Task<ActionResult<List<AllMenuResponse>>> searchMenu([FromBody] SearchRequest request)
        {
            return await SearchService.searchMenu(request.searchInfo, _context);
        }
        // 搜索食材
        [Route("searchIngredient")]
        [HttpPost]
        public async Task<ActionResult<List<Ingredient>>> searchIngredient([FromBody] SearchRequest request)
        {
            return await SearchService.searchIngredient(request.searchInfo, _context);
        }
        // 搜索用户
        [Route("searchUser")]
        [HttpPost]
        public async Task<ActionResult<List<User>>> searchUser([FromBody] SearchRequest request)
        {
            return await SearchService.searchUser(request.searchInfo, _context);
        }
    }
}
    