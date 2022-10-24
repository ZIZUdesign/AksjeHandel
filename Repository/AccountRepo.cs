using AksjeHandel.DAL;
using AksjeHandel.Interfaces;
using AksjeHandel.Models;
using AksjeHandel.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AksjeHandel.Repository
{
    public class AccountRepo : IAccount
    {
        private readonly AksjeContext _context;

        public AccountRepo(AksjeContext context )
        {
            _context = context;
        }

        public async Task<UserManagerResponse> Login(LoginUserVM loginUser)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user => user.UserName == loginUser.Username);

            if (user == null)
                return new UserManagerResponse
                {
                    Message = "Username is invalid"
                };

            var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUser.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return new UserManagerResponse
                    { 
                      Message = "Invalid password"
                    };
                }

            }

            return new UserManagerResponse
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Message = "Successfully logged in",
                IsSuccess = true
            };
        }

        public async Task<UserManagerResponse> Register(RegisterUserVM registerUser)
        {
            if (await UserExists(registerUser))
            {
                return new UserManagerResponse
                {
                    Message = "Username is already taken"
                };
            }

            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = registerUser.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserManagerResponse
            {
              UserId = user.UserId,
              UserName = registerUser.Username,
              Message = "User successfully created",
              IsSuccess = true
            };



        }

        private async Task<bool> UserExists(RegisterUserVM registerUser)
        {
            return await _context.Users.AnyAsync(u => u.UserName == registerUser.Username);
        }

    }



}
