using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users.Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

      /*  public async Task<IEnumerable<MemberDto>> GetMembersAsync(UserParams userParams)
        {
           var query = _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .AsNoTracking();
            return await PagedList<MemberDto>.CreateAsync(query,userParams.PageNumber,userParams.PageSize);
        } */

        public async Task<IEnumerable<AppUser>> GetUserAsync()
        {
            return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public Task<AppUser> GetUserByUsername(string username)
        {
            // return await _context.Users.SingleOrDefaultAsync(x=> x.UserName ==username);
            throw new System.NotImplementedException();
        }

        public async Task<ActionResult<AppUser>> GetUserByUsernameAsync(string username)
        {

            return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

     /*   public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        } */

        public void Update(ActionResult<AppUser> user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Update(AppUser user)
        {
             _context.Entry(user).State = EntityState.Modified;
        }

         Task<PagedList<MemberDto>> IUserRepository.GetMembersAsync(UserParams userParams)
        {
           var query = _context.Users.AsQueryable();
        
            query=query.Where(u =>u.UserName !=userParams.CurrentUsername);
             query =query.Where(u =>u.Gender ==userParams.Gender);

              query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Created),
                _ => query.OrderByDescending(u => u.LastActive)
            };

             var minDob =DateTime.Today.AddYears(-userParams.MaxAge - 1);
               var maxDob =DateTime.Today.AddYears(-userParams.MinAge);

                 query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
             return  PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper
                .ConfigurationProvider).AsNoTracking(), 
                    userParams.PageNumber, userParams.PageSize);
    }
}
}