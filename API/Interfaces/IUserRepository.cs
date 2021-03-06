using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IUserRepository
    {

        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUserAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsername( string username);
        Task<ActionResult<AppUser>> GetUserByUsernameAsync(string username);

        Task<PagedList<MemberDto>> GetMembersAsync( UserParams userParams);
        Task<MemberDto> GetMemberAsync( string username);
        void Update(ActionResult<AppUser> user);
    }
}