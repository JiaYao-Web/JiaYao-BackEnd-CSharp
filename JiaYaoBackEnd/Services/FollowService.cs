using JiaYao.Authorization;
using JiaYao.DAL;
using JiaYao.Models;
using JiaYao.Request;
using JiaYao.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Services
{
    public class FollowService
    {
        // 获取所有用户-用户页
        public static List<User> getAllUsers(JiaYaoContext context)
        {
            return context.Users.Select(u => u).ToList();
        }

        // 获取所有的用户-首页
        public static async Task<List<AllUserResponse>> getHomeUsers(string token, JiaYaoContext context)
        {
            List<AllUserResponse> response = new List<AllUserResponse>();
            User? user = MemoryCacheHelper.Get(token) as User;
            if(user != null)
            {
                var list = context.Users.Select(u => u).ToList();
                for(int i = 0; i < list.Count; i++)
                {
                    AllUserResponse allUserResponse = new AllUserResponse();
                    allUserResponse.id = list[i].Id;
                    allUserResponse.name = list[i].Name;
                    allUserResponse.image = list[i].Image;
                    allUserResponse.isMyself = (list[i].Id == user.Id) ? true : false;
                    if (allUserResponse.isMyself) allUserResponse.ifFollow = false;
                    else
                    {
                        allUserResponse.ifFollow = context.Follows.Where(a => a.FollowId == user.Id && a.FollowedId == list[i].Id).ToList().Count == 1;
                    }
                    allUserResponse.fanNumber = context.Follows.Where(a => a.FollowedId == list[i].Id).ToList().Count;
                    response.Add(allUserResponse);
                }
            }
            return response;
        }

        // 关注&取关用户
        public static async Task<Message> followUser(FavoriteLikeRequest request, string token, JiaYaoContext context)
        {
            User? user = MemoryCacheHelper.Get(token) as User;
            if (user == null)
            {
                return new Message { msg = "用户不存在", status = false };
            }
            else
            {
                int targetId = int.Parse(request.id);
                int userId = user.Id;
                // 关注
                if (request.confirm)
                {
                    if (await FollowDAL.FindFollow(userId, targetId, context))
                    {
                        return new Message { msg = "不能重复关注此用户", status = false };
                    }
                    else
                    {
                        FollowDAL.Follow(userId, targetId, context,true);
                        return new Message { msg = "关注成功", status = true };
                    }
                }
                // 取消关注
                else
                {
                    if (!await FollowDAL.FindFollow(userId, targetId, context))
                    {
                        return new Message { msg = "不能取消关注未关注的用户", status = false };
                    }
                    else
                    {
                        FollowDAL.Follow(userId, targetId, context, false);
                        return new Message { msg = "取消关注成功", status = true };
                    }
                }
            }
        }
        
        // 获取用户的详细信息
        public static async Task<MyInfoDetailResponse> userInfoDetail(int userId, string token,JiaYaoContext context)
        {
            MyInfoDetailResponse response = new MyInfoDetailResponse();
            if (await UserDAL.FindUserById(userId, context))
            {
                var user = context.Users.FirstOrDefault(user => user.Id == userId);
                response.name = user.Name;
                response.email = user.Email;
                response.userId = user.Id;
                response.image = user.Image;
                User? currentUser = MemoryCacheHelper.Get(token) as User;
                if (currentUser != null)
                {
                    response.isMyself = currentUser.Id == userId ? true : false;
                    if (response.isMyself) response.ifFollow = false;
                    else
                    {
                        response.ifFollow = context.Follows.Where(follow => follow.FollowId == currentUser.Id && follow.FollowedId == userId).ToList().Count == 1;
                    }
                    var list = context.Users.Select(u => u).ToList();
                    response.followNumber = context.Follows.Where(follow => follow.FollowedId == userId).ToList().Count;
                }
            }
            return response;
        }

        // 获取用户上传的菜谱
        public static async Task<List<Menu>> userMenu(int userId, JiaYaoContext context)
        {
            List<Menu> result = new List<Menu>();
            result = context.Menus.Where(menu => menu.UserId == userId).ToList();
            return result;
        }
        // 获取用户点赞的内容
        // 点赞食材
        public static async Task<List<Ingredient>> likeIngredient(int userId, JiaYaoContext context)
        {
            List<Ingredient> result = new List<Ingredient>();
            var ingredientIds = context.IngredientLikes.Where(i => i.UserId == userId);
            result = context.Ingredients.Join(ingredientIds, ingredient => ingredient.Id, ids => ids.IngredientId, (ingredient, ids) => new Ingredient
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Url = ingredient.Url,
                Introduction = ingredient.Introduction,
                Category = ingredient.Category
            }).ToList();
            return result;
        }
        // 点赞菜谱
        public static async Task<List<Menu>> likeMenu(int userId, JiaYaoContext context)
        {
            List<Menu> result = new List<Menu>();
            var menuIds = context.MenuLikes.Where(i => i.UserId == userId);
            result = context.Menus.Join(menuIds, menu => menu.Id, ids => ids.MenuId, (menu, ids) => new Menu
            {
                Id = menu.Id,
                Name = menu.Name,
                Image = menu.Image,
                Introduction = menu.Introduction,
                Content = menu.Content,
                Category = menu.Category,
                UserId = menu.UserId
            }).ToList();
            return result;
        }
        // 获取用户关注的用户
        public static async Task<List<User>> userFollow(int userId, JiaYaoContext context)
        {
            List<User> result = new List<User>();
            var list = context.Follows.Where(follow => follow.FollowId == userId).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                result.Add(context.Users.FirstOrDefault(user => user.Id == list[i].FollowedId));
            }
            return result;
        }
        // 获取收藏的数量
        public static async Task<int> userFavorite(int userId, JiaYaoContext context)
        {
            int result = 0;
            result += context.IngredientFavorites.Where(menu => menu.UserId == userId).ToList().Count;
            result += context.MenuFavorites.Where(ingredient => ingredient.UserId == userId).ToList().Count;
            return result;
        }
        // 获取收藏内容
        public static async Task<List<Menu>> getFavoriteMenu(int userId, JiaYaoContext context)
        {
            List<Menu> result = new List<Menu>();
            var menuIds = context.MenuFavorites.Where(i => i.UserId == userId);
            result = context.Menus.Join(menuIds, menu => menu.Id, ids => ids.MenuId, (menu, ids) => new Menu
            {
                Id = menu.Id,
                Name = menu.Name,
                Image = menu.Image,
                Introduction = menu.Introduction,
                Content = menu.Content,
                Category = menu.Category,
                UserId = menu.UserId
            }).ToList();
            return result;
        }
        public static async Task<List<Ingredient>> getFavoriteIngredient(int userId, JiaYaoContext context)
        {
            List<Ingredient> result = new List<Ingredient>();
            var ingredientIds = context.IngredientFavorites.Where(i => i.UserId == userId);
            result = context.Ingredients.Join(ingredientIds, ingredient => ingredient.Id, ids => ids.IngredientId, (ingredient, ids) => new Ingredient
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Url = ingredient.Url,
                Introduction = ingredient.Introduction,
                Category = ingredient.Category
            }).ToList();
            return result;
        }
    }
}
