using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JiaYao.Models;
using JiaYao.Request;
using JiaYao.Services;
using JiaYao.Authorization;
using MailService;
using JiaYao.Response;
using static JiaYao.Controllers.PictureController;

namespace JiaYao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private JiaYaoContext _context;

        public UserController(JiaYaoContext context)
        {
            _context = context;
        }

        // 用户注册
        [Route("register")]
        [HttpPost]
        [MyNoAuthentication]
        public async Task<ActionResult<Message>> Register([FromBody] RegisterRequest registerRequest)
        {
            Message message = await UserService.register(registerRequest, _context);
            if (message.status) MailSender.sendingMail(registerRequest.email, "佳肴注册通知", "恭喜您注册成功！欢迎畅享美食");
            return Ok(message);
        }

        // 用户登录
        [Route("login")]
        [HttpPost]
        [MyNoAuthentication]
        public async Task<ActionResult<Message>> Login([FromBody] LoginRequest loginRequest)
        {
            Message message = await UserService.login(loginRequest, _context);
            if (message.status)
            {
                // 设置Token
                string token = AESEncrypt.Encrypt(loginRequest.email);
                MemoryCacheHelper.AddMemoryCache(token, UserService.getUser(loginRequest.email, _context).Result);
                message.msg = token;
            }
            return message;
        }
        // 用户修改密码
        [Route("changePassword")]
        [HttpPost]
        public async Task<ActionResult<Message>> changePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            return await UserService.changePassword(changePasswordRequest, _context);
        }

        // 获取用户信息
        [Route("getMyInfo")]
        [HttpGet]
        public async Task<ActionResult<MyInfoResponse>> getMyInfo([FromHeader] string myAuthentication)
        {
            return await UserService.getMyInfo(myAuthentication, _context);
        }

        // 修改用户名
        [Route("changeName")]
        [HttpPost]
        public async Task<ActionResult<Message>> changeName([FromBody] ChangeNameRequest changeNameRequest, [FromHeader] string myAuthentication)
        {
            return await UserService.changeName(changeNameRequest, myAuthentication, _context);
        }

        // 修改用户头像
        [Route("changeImage")]
        [HttpPost]
        public async Task<ActionResult<Message>> changeImage([FromForm] FileReportDto fileModel, [FromHeader] string myAuthentication)
        {
            return await UserService.changeImage(fileModel, myAuthentication, _context);
        }
    }
}
