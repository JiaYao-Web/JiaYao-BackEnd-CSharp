using JiaYao.Authorization;
using JiaYao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Services
{
    public class FavoriteService
    {
        // 获取用户收藏的食材
        public static async Task<List<Ingredient>> getFavoriteIngredient(string token, JiaYaoContext context)
        {
            List<Ingredient> result = new List<Ingredient>();
            User? user = MemoryCacheHelper.Get(token) as User;
            if (user != null)
            {
                var ingredientIds = context.IngredientFavorites.Where(i => i.UserId == user.Id);
                result = context.Ingredients.Join(ingredientIds, i => i.Id, ids => ids.IngredientId, (i, ids) => new Ingredient
                {
                    Id = i.Id,
                    Name = i.Name,
                    Url = i.Url,
                    Introduction = i.Introduction,
                    Category = i.Category
                }).ToList();
            }
            return result;
        }
        // 获取用户收藏的菜谱
        public static async Task<List<Menu>> getFavoriteMenu(string token, JiaYaoContext context)
        {
            List<Menu> result = new List<Menu>();
            User? user = MemoryCacheHelper.Get(token) as User;
            if (user != null)
            {
                var menuIds = context.MenuFavorites.Where(i => i.UserId == user.Id);
                result = context.Menus.Join(menuIds, i => i.Id, ids => ids.MenuId, (i, ids) => new Menu
                {
                    Id = i.Id,
                    Name = i.Name,
                    Image = i.Image,
                    Introduction = i.Introduction,
                    Content = i.Content,
                    Category = i.Category,
                    UserId = i.UserId
                }).ToList();
            }
            return result;
        }
    }
}
