using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await _context.Likes.FindAsync(sourceUserId, likedUserId);
        }

      

      
        public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
        {
           
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if(likesParams.Predicate =="Liked")
            {
                likes =likes.Where(like =>like.SourceUserId ==likesParams.UserId);
                users =likes.Select(like =>like.LikedUser);
            }
            if(likesParams.Predicate=="LikedBy")
            {
                likes =likes.Where(like =>like.LikedUserId ==likesParams.UserId);
                users =likes.Select(like =>like.SourceUser);
            }
           var likedUsers = users.Select(user => new LikeDto
            {
                Username = user.UserName,
                KnowAs = user.KnowAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                City = user.City,
                Id = user.Id
            });
            return await PagedList<LikeDto>.CreateAsync(likedUsers, 
                likesParams.PageNumber, likesParams.PageSize);
        
        }

        public Task<PagedList<LikeDto>> GetUserLikes(string predicate, int userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users
                .Include(x => x.LikedUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        Task ILikesRepository.GetUserLike(int sourceUserId, object id)
        {
            throw new System.NotImplementedException();
        }

        Task ILikesRepository.GetUserLikes(LikesParams likesParams)
        {
            throw new System.NotImplementedException();
        }
    }
}