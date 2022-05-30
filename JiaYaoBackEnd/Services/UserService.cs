using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaYao.DAL;
using JiaYao.Models;
using JiaYao.Request;
using JiaYao.Authorization;
using JiaYao.Response;
using static JiaYao.Controllers.PictureController;
using JiaYao.OSS;

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

        // 通过邮箱获取当前用户
        public static async Task<User> getUser(string email, JiaYaoContext context)
        {
            if (await UserDAL.FindUserByEmail(email, context))
            {
                var user = context.Users.FirstOrDefault(user => user.Email == email);
                return user;
            }
            else return new User();
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
            if (await UserDAL.FindUserById(request.userId, context))
            {
                var user = context.Users.FirstOrDefault(user => user.Id == request.userId);
                if (user.Password == request.oldPassword)
                {
                    user.Password = request.newPassword;
                    await context.SaveChangesAsync();
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
        
        // 获取登录用户信息
        public static async Task<MyInfoResponse> getMyInfo(string token, JiaYaoContext context)
        {
            User? user = MemoryCacheHelper.Get(token) as User;
            MyInfoResponse response = new MyInfoResponse();
            if (user == null)
            {
                response.status = false;
                response.msg = "获取失败";
            }
            else
            {
                response.status = true;
                response.msg = "获取成功";
                response.name = user.Name;
                response.email = user.Email;
                response.userId = user.Id;
                response.image = user.Image;
            }
            return response;
        }

        // 修改用户昵称
        public static async Task<Message> changeName(ChangeNameRequest request, string token, JiaYaoContext context)
        {
            User? user = MemoryCacheHelper.Get(token) as User;
            if (user == null)
            {
                return new Message { msg = "获取失败", status = false };
            }
            else
            {
                if (await UserDAL.FindUserById(user.Id, context))
                {
                    var u = context.Users.FirstOrDefault(u => u.Id == user.Id);
                    u.Name = request.name;
                    await context.SaveChangesAsync();
                    // 这里需要更新Token的内容！
                    user.Name = request.name;
                    return new Message { msg = "修改密码成功", status = true };
                }
                else
                    return new Message { msg = "用户不存在", status = false };
            }
        }

        // 修改用户头像
        public static async Task<Message> changeImage(FileReportDto file, string token, JiaYaoContext context)
        {
            User? user = MemoryCacheHelper.Get(token) as User;
            if (user == null)
            {
                return new Message { msg = "获取用户失败", status = false };
            }
            else
            {
                if (await UserDAL.FindUserById(user.Id, context))
                {
                    var u = context.Users.FirstOrDefault(u => u.Id == user.Id);
                    PicUploadResult result = PicUploadBll.AsyncPutObject(file.File.OpenReadStream(), file.File.FileName);
                    u.Image = result.url;
                    await context.SaveChangesAsync();
                    // 这里需要更新Token的内容！
                    user.Image = result.url;
                    return new Message { msg = result.url, status = true };
                }
                else
                    return new Message { msg = "用户不存在", status = false };
            }
        }
    }
}
