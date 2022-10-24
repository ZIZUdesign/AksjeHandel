using AksjeHandel.Models;
using AksjeHandel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AksjeHandel.Interfaces
{
    public interface IAccount
    {
        Task<UserManagerResponse> Register(RegisterUserVM registerUser);
        Task<UserManagerResponse> Login(LoginUserVM loginUser);

    }
}
