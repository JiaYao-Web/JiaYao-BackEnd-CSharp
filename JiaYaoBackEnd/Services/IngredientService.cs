using JiaYao.Authorization;
using JiaYao.DAL;
using JiaYao.Models;
using JiaYao.OSS;
using JiaYao.Request;
using JiaYao.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Services
{
    public class IngredientService
    {
        // 上传食材
        public static async Task<Message> uploadIngredient(UploadIngredientRequest request, JiaYaoContext context)
        {
            // 上传图片
            PicUploadResult result = PicUploadBll.AsyncPutObject(request.file.OpenReadStream(), request.file.FileName);
            if (!result.status) return new Message { msg = "上传失败", status = false };
            Ingredient ingredient = new Ingredient();
            ingredient.Name = request.name;
            ingredient.Url = result.url;
            ingredient.Introduction = request.introduction;
            ingredient.Category = request.category;
            var ingredientId = await IngredientDAL.Create(ingredient, context);
            if (ingredientId == -1) return new Message { msg = "请填写完整的信息", status = false };
            return new Message { msg = "上传成功", status = true };
        }
        //获取所有的食材
        public static Task<List<Ingredient>> allIngredient(JiaYaoContext context)
        {
            return Task.FromResult(context.Ingredients.Select(a => a).ToList());
        }
        // 获取食材详情
        public static async Task<IngredientDetailReponse> ingredientDetail(int id, string token,JiaYaoContext context)
        {
            IngredientDetailReponse response = new IngredientDetailReponse();
            User? user = MemoryCacheHelper.Get(token) as User;
            if (user != null)
            {
                if (await IngredientDAL.FindIngredientById(id, context))
                {
                    var ingredient = context.Ingredients.FirstOrDefault(ingredient => ingredient.Id == id);
                    // 基本信息
                    response.id = ingredient.Id;
                    response.name = ingredient.Name;
                    response.category = ingredient.Category;
                    response.image = ingredient.Url;
                    response.introduction = ingredient.Introduction;
                    // 点赞与收藏信息
                    response.favoriteNumber = context.IngredientFavorites.Select(a => a.IngredientId == id).ToList().Count();
                    response.likeNumber = context.IngredientLikes.Select(a => a.IngredientId == id).ToList().Count();
                    // 个人是否点赞与收藏
                    response.ifFavorite = context.IngredientFavorites.Select(a => (a.IngredientId == id && (a.UserId == user.Id))).ToList().Count == 1;
                    response.ifLike = context.IngredientLikes.Select(a => (a.IngredientId == id && (a.UserId == user.Id))).ToList().Count == 1;
                    // 相关的菜单
                    response.menus = context.Menus.Where(a => a.Content.Contains(ingredient.Name)).ToList();
                }
            }
            return response;
        }
        // 收藏食材
        public static async Task<Message> favoriteIngredient(FavoriteLikeRequest request, string token, JiaYaoContext context)
        {
            User? user = MemoryCacheHelper.Get(token) as User;
            if (user == null)
            {
                return new Message { msg = "用户不存在", status = false };
            }
            else
            {
                int ingredientId = int.Parse(request.id);
                int userId = user.Id;
                // 收藏
                if (request.confirm)
                {
                    if(await IngredientDAL.FindFavorite(ingredientId, userId, context))
                    {
                        return new Message { msg = "不能重复收藏此食材", status = false };
                    }
                    else
                    {
                        IngredientDAL.Favorite(ingredientId, userId, context, true);
                        return new Message { msg = "收藏成功", status = true };
                    }
                }
                // 取消收藏
                else
                {
                    if (!(await IngredientDAL.FindFavorite(ingredientId, userId, context)))
                    {
                        return new Message { msg = "不能取消收藏未收藏的食材", status = false };
                    }
                    else
                    {
                        IngredientDAL.Favorite(ingredientId, userId, context, false);
                        return new Message { msg = "取消收藏成功", status = true };
                    }
                }
            }
        }

        // 点赞食材
        public static async Task<Message> likeIngredient(FavoriteLikeRequest request, string token, JiaYaoContext context)
        {
            User? user = MemoryCacheHelper.Get(token) as User;
            if (user == null)
            {
                return new Message { msg = "用户不存在", status = false };
            }
            else
            {
                int ingredientId = int.Parse(request.id);
                int userId = user.Id;
                // 点赞
                if (request.confirm)
                {
                    if (await IngredientDAL.FindLike(ingredientId, userId, context))
                    {
                        return new Message { msg = "不能重复点赞此商品", status = false };
                    }
                    else
                    {
                        IngredientDAL.Like(ingredientId, userId, context, true);
                        return new Message { msg = "点赞成功", status = true };
                    }
                }
                // 取消点赞
                else
                {
                    if (!(await IngredientDAL.FindLike(ingredientId, userId, context)))
                    {
                        return new Message { msg = "不能取消点赞未点赞的商品", status = false };
                    }
                    else
                    {
                        IngredientDAL.Like(ingredientId, userId, context, false);
                        return new Message { msg = "取消点赞成功", status = true };
                    }
                }
            }
        }
    }
}
