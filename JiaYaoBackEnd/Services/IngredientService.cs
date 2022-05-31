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
    }
}
