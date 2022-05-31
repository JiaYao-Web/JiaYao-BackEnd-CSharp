using JiaYao.Authorization;
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
    public class MenuService
    {
        // 上传商品
        public static async Task<Message> uploadMenu(UploadMenuRequest request, string token,JiaYaoContext context)
        {
            User? user = MemoryCacheHelper.Get(token) as User;
            if(user == null)
            {
                return new Message { msg = "该用户不存在", status = false };
            }
            // 上传图片
            PicUploadResult result = PicUploadBll.AsyncPutObject(request.file.OpenReadStream(), request.file.FileName);
            if (!result.status) return new Message { msg = "上传失败", status = false };
            Menu menu = new Menu();
            menu.Name = request.name;
            menu.Content = request.content;
            menu.Category = request.category;
            menu.Introduction = request.introduction;
            menu.Image = result.url;
            menu.UserId = user.Id;
            var menuId = await MenuDAL.Create(menu, context);
            if(menuId == -1) return new Message { msg = "请填写完整的信息", status = false };
            return new Message { msg = "上传成功", status = true };
        }
    }
}
