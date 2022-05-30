using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaYao.Models;
using Pomelo.EntityFrameworkCore.MySql;

namespace JiaYao.DAL
{
    public class UserDAL
    {
        // 判断用户是否存在 -- 邮箱
        public static Task<bool> FindUserByEmail(string email, JiaYaoContext context)
        {
            var user = context.Users.FirstOrDefault(user => user.Email == email);
            if (user == null) return Task.FromResult(false);
            else return Task.FromResult(true);

        }

        // 判断用户是否存在 -- Id
        public static Task<bool> FindUserById(int userId, JiaYaoContext context)
        {
            var user = context.Users.FirstOrDefault(user => user.Id == userId);
            if (user == null) return Task.FromResult(false);
            else return Task.FromResult(true);
        }

        // 创建用户
        public static async Task<int> Create(User user, JiaYaoContext context)
        {
            var u = context.Users.AddAsync(new User
            {
                Name = user.Name,
                Password = user.Password,
                Email = user.Email
            });
            await context.SaveChangesAsync();
            return u.Result.Entity.Id;
        }
    }
}
