using JiaYao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.DAL
{
    public class IngredientDAL
    {
        // 创建食材
        public static async Task<int> Create(Ingredient ingredient, JiaYaoContext context)
        {
            var u = context.Ingredients.AddAsync(new Ingredient
            {
                Name = ingredient.Name,
                Introduction = ingredient.Introduction,
                Category = ingredient.Category,
                Url = ingredient.Url
            });
            await context.SaveChangesAsync();
            return u.Result.Entity.Id;
        }

        // 根据ID查询食材
        public static Task<bool> FindIngredientById(int ingredientId, JiaYaoContext context)
        {
            var ingredient = context.Ingredients.FirstOrDefault(ingredient => ingredient.Id == ingredientId);
            if (ingredient == null) return Task.FromResult(false);
            else return Task.FromResult(true);
        }

        // 查询收藏
        public static Task<bool> FindFavorite(int ingredientId, int userId, JiaYaoContext context)
        {
            var result = context.IngredientFavorites.Select(a => a.IngredientId == ingredientId && a.UserId == userId).ToList();
            return Task.FromResult(result.Count == 1);
        }
        // 收藏&取消收藏
        public static void Favorite(int ingredientId, int userId, JiaYaoContext context, bool favorite)
        {
            // 收藏
            if (favorite)
            {
                context.Add(new IngredientFavorite
                {
                    IngredientId = ingredientId,
                    UserId = userId
                });
                 context.SaveChanges();
            }
            // 取消收藏
            else
            {
                IngredientFavorite ingredientFavorite = new IngredientFavorite { IngredientId = ingredientId, UserId = userId };
                 context.Remove(ingredientFavorite);
                 context.SaveChanges();
            }
        }

        // 查询点赞
        public static Task<bool> FindLike(int ingredientId, int userId, JiaYaoContext context)
        {
            var result = context.IngredientLikes.Select(a => a.IngredientId == ingredientId && a.UserId == userId).ToList();
            return Task.FromResult(result.Count == 1);
        }

        // 点赞&取消点赞
        public static  void Like(int ingredientId, int userId, JiaYaoContext context, bool favorite)
        {
            // 点赞
            if (favorite)
            {
                context.Add(new IngredientLike
                {
                    IngredientId = ingredientId,
                    UserId = userId
                });
                context.SaveChanges();
            }
            // 取消点赞
            else
            {
                IngredientLike ingredientLike = new IngredientLike { IngredientId = ingredientId, UserId = userId };
                context.Remove(ingredientLike);
                context.SaveChanges();
            }
        }
    }
}
