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
    public class CategoryController : Controller
    {
        private JiaYaoContext _context;
        public CategoryController(JiaYaoContext context)
        {
            _context = context;
        }
        // 获取某一个分类下的所有菜谱
        [Route("getMenuCategory")]
        [HttpPost]
        public async Task<ActionResult<List<AllMenuResponse>>> getMenuCategory([FromBody] CategoryRequest request)
        {
            return await CategoryService.getMenuCategory(request.category, _context);
        }
        // 获取某一个分类下的所有食材
        [Route("getIngredientCategory")]
        [HttpPost]
        public async Task<ActionResult<List<Ingredient>>> getIngredientCategory([FromBody] CategoryRequest request)
        {
            return await CategoryService.getIngredientCategory(request.category, _context);
        }
    }
}
