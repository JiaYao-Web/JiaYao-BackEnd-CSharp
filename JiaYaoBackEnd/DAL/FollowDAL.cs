using JiaYao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.DAL
{
    public class FollowDAL
    {
        // 查询关注
        public static Task<bool> FindFollow(int userId,int targetId, JiaYaoContext context)
        {
            var result = context.Follows.Where(a => a.FollowId == userId && a.FollowedId == targetId).ToList();
            return Task.FromResult(result.Count == 1);
        }
        // 关注&取消关注
        public static void Follow(int userId, int targetId, JiaYaoContext context, bool follow)
        {
            // 关注
            if (follow)
            {
                context.Add(new Follow
                {
                    FollowId = userId,
                    FollowedId = targetId
                });
                context.SaveChanges();
            }
            // 取关
            else
            {
                var follow1 = context.Follows.FirstOrDefault(a => a.FollowId == userId && a.FollowedId == targetId);
                context.Remove(follow1);
                context.SaveChanges();
            }
        }
    }
}
