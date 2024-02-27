using Azure.Core;
using Business;
using Business.Abstract;
using Business.Requests.Auth;
using Core.Utilities.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AccessToken = Core.Utilities.JWT.AccessToken;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public void Register([FromBody] RegisterRequest request)
        {
            _authService.Register(request);
        }

        [HttpPost("Login")]
        public AccessToken Login([FromBody] LoginRequest request)
        {
           return _authService.Login(request);
        }
    }
}
