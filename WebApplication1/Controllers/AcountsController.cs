using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Formats.Asn1;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
 
    public class AcountsController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _tokenServices;

        public AcountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenServices tokenServices)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenServices = tokenServices;
        }

        //Register
        [HttpPost("Register")]
        //{BaseUrl}/api/Acounts/Register
        public async Task<ActionResult<UserDto>> Registe (RegisterDto model)
        {
            var User = new AppUser() 
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,

            };
            var Result = await _userManager.CreateAsync(User, model.Password);

            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));
            var ReturnedUser = new UserDto()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                Token =await _tokenServices.CreateTokenAsync(User, _userManager)
            };
            return Ok(ReturnedUser);
        }



        [HttpPost("LogIn")]

        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var Doctor = await _userManager.FindByEmailAsync(model.Email);
            if (Doctor is null) return Unauthorized(new ApiResponse(401));
            var Result = await _signInManager.CheckPasswordSignInAsync(Doctor, model.Password, false);
            if (!Result.Succeeded) return Unauthorized(new ApiResponse(401));
            return (Ok(new UserDto()
            {
                DisplayName = Doctor.DisplayName,
                Email = Doctor.Email,
                Token = await _tokenServices.CreateTokenAsync(Doctor, _userManager)
            }));  
        }
    }
}
