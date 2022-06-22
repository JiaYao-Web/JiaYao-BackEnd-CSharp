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
    public class FollowController : Controller
    {
        private JiaYaoContext _context;
        public FollowController(JiaYaoContext context)
        {
            _context = context;
        }
        // 获取所有用户-所有用户页面
        [Route("getAllUsers")]
        [HttpGet]
        public ActionResult<List<User>> getAllUsers()
        {
            return FollowService.getAllUsers(_context);
        }

        // 获取所有用户-首页
        [Route("getHomeUsers")]
        [HttpGet]
        public async Task<ActionResult<List<AllUserResponse>>> getHomeUsers([FromHeader] string myAuthentication)
        {
            return await FollowService.getHomeUsers(myAuthentication, _context);
        }
        // 关注&取关用户
        [Route("followUser")]
        [HttpPost]
        public async Task<ActionResult<Message>> followUser([FromBody] FavoriteLikeRequest request, [FromHeader] string myAuthentication)
        {
            return await FollowService.followUser(request, myAuthentication, _context);
        }
        // 用户详情页面
        // 获取用户的个人信息
        [Route("getUserInfoDetail")]
        [HttpPost]
        public async Task<ActionResult<MyInfoDetailResponse>> userInfoDetail([FromBody] UserRequest request, [FromHeader] string myAuthentication)
        {
            return await FollowService.userInfoDetail(request.userId, myAuthentication, _context);
        }
        // 获取用户上传的菜谱
        [Route("getUserMenu")]
        [HttpPost]
        public async Task<ActionResult<List<Menu>>> userMenu([FromBody] UserRequest request)
        {
            return await FollowService.userMenu(request.userId, _context);
        }
        // 获取用户点赞的内容
        // 点赞食材
        [Route("getLikeIngredient")]
        [HttpPost]
        public async Task<ActionResult<List<Ingredient>>> getLikeIngredient([FromBody] UserRequest request)
        {
            return await FollowService.likeIngredient(request.userId, _context);
        }
        // 点赞菜谱
        [Route("getLikeMenu")]
        [HttpPost]
        public async Task<ActionResult<List<Menu>>> getLikeMenu([FromBody] UserRequest request)
        {
            return await FollowService.likeMenu(request.userId, _context);
        }
        // 获取用户关注的用户
        [Route("getUserFollow")]
        [HttpPost]
        public async Task<ActionResult<List<User>>> userFollow([FromBody] UserRequest request)
        {
            return await FollowService.userFollow(request.userId, _context);
        }
        // 获取收藏的数量
        [Route("getUserFavorite")]
        [HttpPost]
        public async Task<ActionResult<int>> userFavorite([FromBody] UserRequest request)
        {
            return await FollowService.userFavorite(request.userId, _context);
        }
        // 获取收藏的东西
        [Route("getFavoriteMenu")]
        [HttpPost]
        public async Task<ActionResult<List<Menu>>> getFavoriteMenu([FromBody] UserRequest request)
        {
            return await FollowService.getFavoriteMenu(request.userId, _context);
        }
        [Route("getFavoriteIngredient")]
        [HttpPost]
        public async Task<ActionResult<List<Ingredient>>> getFavoriteIngredient([FromBody] UserRequest request)
        {
            return await FollowService.getFavoriteIngredient(request.userId, _context);
        }
    }
}
