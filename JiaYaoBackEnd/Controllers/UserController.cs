using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JiaYao.Models;
using JiaYao.Request;
using JiaYao.Services;
using JiaYao.Authorization;
using MailService;


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
                MemoryCacheHelper.AddMemoryCache(token, User);
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
    }
}
