using JiaYao.Models;
using JiaYao.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Services
{
    public class CategoryService
    {
        // 获取分类菜单
        public static async Task<List<AllMenuResponse>> getMenuCategory(string category, JiaYaoContext context)
        {
            return context.Menus.Join(context.Users, m => m.UserId, u => u.Id, (m, u) => new AllMenuResponse
            { menuId = m.Id, menuName = m.Name, menuImage = m.Image, category = m.Category, userId = u.Id, userName = u.Name, userImage = u.Image })
             .Where(menu=>menu.category==category).ToList();
        }
        // 获取分类食材
        public static async Task<List<Ingredient>> getIngredientCategory(string category, JiaYaoContext context)
        {
            return context.Ingredients.Where(ingredient => ingredient.Category == category).ToList();
        }
    }
}
