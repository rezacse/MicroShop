using MicroShop.Services.AuthAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MircroShop.Services.AuthAPI.Models;

namespace MicroShop.Services.AuthAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;
        private readonly IUserService userService;

        public AuthController(ILogger<AuthController> logger
            , IUserService userService)
        {
            _logger = logger;
            this.userService = userService;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> Register(RegisterDto req)
        {
            var resp = await userService.Register(req);
            if(!resp.IsSuccess) return BadRequest(resp.Message); 

            return Ok(resp);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto req)
        {
            var resp = await userService.Login(req); 
            if (!resp.IsSuccess) return BadRequest(resp.Message);
            
            return Ok(resp);
        }
    }
}
