using JiaYao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.DAL
{
    public class IngredientDAL
    {
        // 创建菜谱
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
    }
}
