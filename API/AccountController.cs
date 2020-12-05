using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenservice;
        private readonly IMapper _mapper;
        public AccountController(DataContext context, ITokenService tokenservice, IMapper mapper)
        {
            _mapper = mapper;
            _tokenservice = tokenservice;
            _context = context;
        }
        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("username already taken");

            var user =_mapper .Map<AppUser>(registerDto);
            using var hmac = new HMACSHA512();
             user.UserName = registerDto.Username.ToLower();
             user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
             user. PasswordSalt = hmac.Key;
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenservice.CreateToken(user),
                KnowAs = user.KnowAs,
                Gender =user.Gender,
                // PhotoUrl =user.Photos.FirstOrDefault (x=>x.IsMain)?.Url,

            };

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("invalid username");


            using var hmac = new HMACSHA512(user.PasswordSalt);

            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if (ComputeHash[i] != user.PasswordHash[i])
                    return Unauthorized("invalied password");
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenservice.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnowAs =user.KnowAs,
                Gender= user.Gender
            };



        }



        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}