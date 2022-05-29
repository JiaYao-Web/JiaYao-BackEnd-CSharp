using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaYao.DAL;
using JiaYao.Models;
using JiaYao.Request;

namespace JiaYao.Services
{
    public class UserService
    {
        // 注册
        public static async Task<Message> register(RegisterRequest request, JiaYaoContext context)
        {
            // 判断用户是否已经存在
            if (await UserDAL.FindUserByEmail(request.email, context))
            {
                return new Message { msg = "用户已存在，注册失败！", status = false };
            }
            User u = new User();
            u.Name = request.name;
            u.Email = request.email;
            u.Password = request.password;
            var user_id = await UserDAL.Create(u, context);

            if(user_id == -1) return new Message { msg = "请填写完整的信息", status = false };
            else return new Message { msg = "注册成功!", status = true };
        }


        // 登录
        public static async Task<Message> login(LoginRequest request, JiaYaoContext context)
        {
            if (await UserDAL.FindUserByEmail(request.email, context))
            {
                var user = context.Users.FirstOrDefault(user => user.Email == request.email);
                if (user.Password == request.password)
                {
                    return new Message { msg = "", status = true };
                }
                else return new Message { msg = "密码错误", status = false };
            }
            else
            {
                return new Message { msg = "用户名不存在", status = false };
            }
        }

        // 修改密码
        public static async Task<Message> changePassword(ChangePasswordRequest request, JiaYaoContext context)
        {
            if (await UserDAL.FindUserByEmail(request.email, context))
            {
                var user = context.Users.FirstOrDefault(user => user.Email == request.email);
                if (user.Password == request.oldPassword)
                {
                    user.Password = request.newPassword;
                    context.SaveChangesAsync();
                    return new Message { msg = "修改密码成功", status = true };
                }
                else
                {
                    return new Message { msg = "密码错误，无法修改密码", status = false };
                }
            }
            else
            {
                return new Message { msg = "用户不存在", status = false };
            }
        }
    }
}
