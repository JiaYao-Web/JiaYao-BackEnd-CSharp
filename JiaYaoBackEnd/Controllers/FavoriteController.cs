using JiaYao.Models;
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
    public class FavoriteController : Controller
    {
        private JiaYaoContext _context;

        public FavoriteController(JiaYaoContext context)
        {
            _context = context;
        }

        // 获取用户收藏的食材
        [Route("getFavoriteIngredient")]
        [HttpGet]
        public async Task<ActionResult<List<Ingredient>>> getFavoriteIngredient([FromHeader] string myAuthentication)
        {
            return await FavoriteService.getFavoriteIngredient(myAuthentication, _context);
        }
        // 获取用户收藏的菜谱
        [Route("getFavoriteMenu")]
        [HttpGet]
        public async Task<ActionResult<List<Menu>>> getFavoriteMenu([FromHeader] string myAuthentication)
        {
            return await FavoriteService.getFavoriteMenu(myAuthentication, _context);
        }
    }
}
