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

        // 根据ID查询食材
        public static Task<bool> FindMenuById(int menuId, JiaYaoContext context)
        {
            var menu = context.Menus.FirstOrDefault(m => m.Id == menuId);
            if (menu == null) return Task.FromResult(false);
            else return Task.FromResult(true);
        }
        // 查询收藏
        public static Task<bool> FindFavorite(int menuId,int userId,JiaYaoContext context)
        {
            var result = context.MenuFavorites.Where(a => a.MenuId == menuId && a.UserId == userId).ToList();
            return Task.FromResult(result.Count == 1);
        }
        // 收藏&取消收藏
        public static void Favorite(int menuId, int userId, JiaYaoContext context, bool favorite)
        {
            // 收藏
            if (favorite)
            {
                context.Add(new MenuFavorite
                {
                    MenuId = menuId,
                    UserId = userId
                });
                context.SaveChanges();
            }
            // 取消收藏
            else
            {
                var menuFavorite = context.MenuFavorites.FirstOrDefault(a => a.MenuId == menuId && a.UserId == userId);
                context.Remove(menuFavorite);
                context.SaveChanges();
            }
        }
        // 查询点赞
        public static Task<bool> FindLike(int menuId, int userId, JiaYaoContext context)
        {
            var result = context.MenuLikes.Where(a => a.MenuId == menuId && a.UserId == userId).ToList();
            return Task.FromResult(result.Count == 1);
        }

        // 点赞&取消点赞
        public static void Like(int menuId, int userId, JiaYaoContext context, bool favorite)
        {
            // 点赞
            if (favorite)
            {
                context.Add(new MenuLike
                {
                    MenuId = menuId,
                    UserId = userId
                });
                context.SaveChanges();
            }
            // 取消点赞
            else
            {
                var menuLike = context.MenuLikes.FirstOrDefault(a => a.MenuId == menuId && a.UserId == userId);
                context.Remove(menuLike);
                context.SaveChanges();
            }
        }
    }
}
