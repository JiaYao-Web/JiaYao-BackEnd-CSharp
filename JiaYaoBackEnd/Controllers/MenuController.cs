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

        // 获取所有的菜谱
        [Route("getAllMenu")]
        [HttpGet]
        public async Task<ActionResult<List<AllMenuResponse>>> getAllMenu()
        {
            return await MenuService.allMenu(_context);
        }
        // 获取菜谱详情
        [Route("menuDetail")]
        [HttpPost]
        public async Task<ActionResult<MenuDetailResponse>> getMenuDetail(MenuDetailRequest request,[FromHeader] string myAuthentication)
        {
            return await MenuService.menuDetail(int.Parse(request.menuId), myAuthentication, _context);
        }
        // 收藏与取消收藏
        [Route("favoriteMenu")]
        [HttpPost]
        public async Task<ActionResult<Message>> favoriteMenu([FromBody]FavoriteLikeRequest request, [FromHeader] string myAuthentication)
        {
            return await MenuService.favoriteMenu(request, myAuthentication, _context);
        }
        // 点赞与取消点赞
        [Route("likeMenu")]
        [HttpPost]
        public async Task<ActionResult<Message>> likeMenu([FromBody] FavoriteLikeRequest request, [FromHeader] string myAuthentication)
        {
            return await MenuService.likeMenu(request, myAuthentication, _context);
        }
    }
}
