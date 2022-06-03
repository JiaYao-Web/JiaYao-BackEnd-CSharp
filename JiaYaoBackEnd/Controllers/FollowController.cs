using JiaYao.Models;
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
        // 用户详情页面
    }
}
