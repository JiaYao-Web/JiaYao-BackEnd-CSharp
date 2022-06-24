using JiaYao.Models;
using JiaYao.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Services
{
    public class SearchService
    {
        // 搜索菜谱
        public static async Task<List<AllMenuResponse>> searchMenu(string searchInfo,JiaYaoContext context)
        {
            List<AllMenuResponse> list = new List<AllMenuResponse>();
            list = context.Menus.Join(context.Users, m => m.UserId, u => u.Id, (m, u) => new AllMenuResponse
            { menuId = m.Id, menuName = m.Name, menuImage = m.Image, category = m.Category, userId = u.Id, userName = u.Name, userImage = u.Image })
             .Where(menu => menu.menuName.Contains(searchInfo)).ToList();
            return list;
        }
        // 搜索食材
        public static async Task<List<Ingredient>> searchIngredient(string searchInfo, JiaYaoContext context)
        {
            List<Ingredient> list = new List<Ingredient>();
            list = context.Ingredients.Where(ingredient => ingredient.Name.Contains(searchInfo)).ToList();
            return list;
        }
        // 搜索用户
        public static async Task<List<User>> searchUser(string searchInfo, JiaYaoContext context)
        {
            List<User> list = new List<User>();
            list = context.Users.Where(user => user.Name.Contains(searchInfo)).ToList();
            return list;
        }
    }
}
