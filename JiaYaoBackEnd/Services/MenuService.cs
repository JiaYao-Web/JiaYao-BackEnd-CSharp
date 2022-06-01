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
        // 获取所有菜单
        public static Task<List<AllMenuResponse>> allMenu(JiaYaoContext context)
        {
            return Task.FromResult(context.Menus.Join(context.Users,m =>m.UserId, u=>u.Id, (m,u)=> new AllMenuResponse
            {menuId = m.Id,menuName = m.Name,menuImage=m.Image,category=m.Category,userId=u.Id,userName=u.Name,userImage=u.Image})
             .ToList());
        }

        // 获取菜单详情
        public static async Task<MenuDetailResponse> menuDetail(int id, string token, JiaYaoContext context)
        {
            MenuDetailResponse response = new MenuDetailResponse();
            User? user = MemoryCacheHelper.Get(token) as User;
            if (user != null)
            {
                if(await MenuDAL.FindMenuById(id, context))
                {
                    var menu = context.Menus.FirstOrDefault(m => m.Id == id);
                    //基本信息
                    response.id = menu.Id;
                    response.name = menu.Name;
                    response.category = menu.Category;
                    response.image = menu.Image;
                    response.content = menu.Content;
                    response.introduction = menu.Introduction;
                    //点赞与收藏信息
                    response.favoriteNumber = context.MenuFavorites.Select(a => a.MenuId == id).ToList().Count();
                    response.likeNumber = context.MenuLikes.Select(a => a.MenuId == id).ToList().Count();
                    //个人是否点赞与收藏
                    response.ifFavorite = context.MenuFavorites.Select(a => (a.MenuId == id && a.UserId == user.Id)).ToList().Count == 1;
                    response.ifLike = context.MenuLikes.Select(a => (a.MenuId == id && a.UserId == user.Id)).ToList().Count == 1;
                }
            }
            return response;
        }
        // 收藏&取消收藏菜单
        public static async Task<Message> favoriteMenu(FavoriteLikeRequest request, string token, JiaYaoContext context)
        {
            User? user = MemoryCacheHelper.Get(token) as User;
            if (user == null)
            {
                return new Message { msg = "用户不存在", status = false };
            }
            else
            {
                int menuId = int.Parse(request.id);
                int userId = user.Id;
                // 收藏
                if (request.confirm)
                {
                    if(await MenuDAL.FindFavorite(menuId, userId, context))
                    {
                        return new Message { msg = "不能重复收藏此菜谱", status = false };
                    }
                    else
                    {
                        MenuDAL.Favorite(menuId, userId, context, true);
                        return new Message { msg = "收藏成功", status = true };
                    }
                }
                // 取消收藏
                else
                {
                    if (! await MenuDAL.FindFavorite(menuId, userId, context))
                    {
                        return new Message { msg = "不能取消收藏未收藏的菜谱", status = false };
                    }
                    else
                    {
                        MenuDAL.Favorite(menuId, userId, context, false);
                        return new Message { msg = "取消收藏成功", status = true };
                    }
                }
            }
        }
        // 点赞&取消点赞菜单
        public static async Task<Message> likeMenu(FavoriteLikeRequest request, string token, JiaYaoContext context)
        {
            User? user = MemoryCacheHelper.Get(token) as User;
            if (user == null)
            {
                return new Message { msg = "用户不存在", status = false };
            }
            else
            {
                int menuId = int.Parse(request.id);
                int userId = user.Id;
                // 点赞
                if (request.confirm)
                {
                    if (await MenuDAL.FindLike(menuId, userId, context))
                    {
                        return new Message { msg = "不能重复点赞此商品", status = false };
                    }
                    else
                    {
                        MenuDAL.Like(menuId, userId, context, true);
                        return new Message { msg = "点赞成功", status = true };
                    }
                }
                // 取消点赞
                else
                {
                    if (!(await MenuDAL.FindLike(menuId, userId, context)))
                    {
                        return new Message { msg = "不能取消点赞未点赞的商品", status = false };
                    }
                    else
                    {
                        MenuDAL.Like(menuId, userId, context, false);
                        return new Message { msg = "取消点赞成功", status = true };
                    }
                }
            }
        }
    }
}
