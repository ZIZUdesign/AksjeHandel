using AksjeHandel.Interfaces;
using AksjeHandel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AksjeHandel.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountRepo;

        public AccountController(IAccount accountRepo)
        {
            _accountRepo = accountRepo;
        }

        
        public async Task<ActionResult> Register (RegisterUserVM registerUser)
        {
            var result = await _accountRepo.Register(registerUser);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        public async Task<ActionResult> Login(LoginUserVM loginUser)
        {
            var result = await _accountRepo.Login(loginUser);

            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result); 
        }
    }
}
