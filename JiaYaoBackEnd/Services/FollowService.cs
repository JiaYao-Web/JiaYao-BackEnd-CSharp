using JiaYao.Authorization;
using JiaYao.DAL;
using JiaYao.Models;
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
                        allUserResponse.ifFollow = context.Follows.Select(a => a.FollowedId == user.Id && a.FollowedId == list[i].Id).ToList().Count == 1;
                    }
                    allUserResponse.fanNumber = context.Follows.Select(a => a.FollowedId == list[i].Id).ToList().Count;
                    response.Add(allUserResponse);
                }
            }
            return response;
        }
    }
}
