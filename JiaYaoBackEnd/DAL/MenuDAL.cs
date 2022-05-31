using JiaYao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.DAL
{
    public class MenuDAL
    {
        // 创建Menu
        public static async Task<int> Create(Menu menu, JiaYaoContext context)
        {
            var u = context.Menus.AddAsync(new Menu
            {
                Name = menu.Name,
                Content = menu.Content,
                Category = menu.Category,
                Introduction = menu.Introduction,
                Image = menu.Image,
                UserId = menu.UserId
            });
            await context.SaveChangesAsync();
            return u.Result.Entity.Id;
        }
    }
}
