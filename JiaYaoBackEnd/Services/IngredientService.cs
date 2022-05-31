using JiaYao.DAL;
using JiaYao.Models;
using JiaYao.OSS;
using JiaYao.Request;
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
    }
}
