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
        public async Task<ActionResult<Message>> uploadIngredient([FromForm] UploadIngredientRequest request)
        {
            return await IngredientService.uploadIngredient(request, _context);
        }

        // 获取所有的食材
        [Route("getAllIngredient")]
        [HttpGet]
        public async Task<ActionResult<List<Ingredient>>> getAllIngredient()
        {
            return await IngredientService.allIngredient(_context);
        }

        // 获取食材详情
        [Route("IngredientDetail")]
        [HttpPost]
        public async Task<ActionResult<IngredientDetailReponse>> getIngredientDetail(IngredientDetailRequest request, [FromHeader] string myAuthentication)
        {
            return await IngredientService.ingredientDetail(int.Parse(request.ingredientId), myAuthentication, _context);
        }
        
        // 收藏与取消收藏
        [Route("favoriteIngredient")]
        [HttpPost]
        public async Task<ActionResult<Message>> favoriteIngredient([FromBody]FavoriteLikeRequest request, [FromHeader] string myAuthentication)
        {
            return await IngredientService.favoriteIngredient(request, myAuthentication, _context);
        }

        // 点赞与取消点赞
        [Route("likeIngredient")]
        [HttpPost]
        public async Task<ActionResult<Message>> likeIngredient([FromBody] FavoriteLikeRequest request, [FromHeader] string myAuthentication)
        {
            return await IngredientService.likeIngredient(request, myAuthentication, _context);
        }
    }
}
