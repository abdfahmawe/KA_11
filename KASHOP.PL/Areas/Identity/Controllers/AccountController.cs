using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requists;
using KASHOP.DAL.DTO.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;


namespace KASHOP.PL.Areas.Identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountController : ControllerBase
    {
        private readonly IAthinticationService _athinticationService;

        public AccountController(IAthinticationService athinticationService)
        {
            _athinticationService = athinticationService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserResponse>> Register([FromBody] RegisterRequist registerRequest)
        {
            var resulet = await _athinticationService.RegisterAsync(registerRequest);
            return Ok(resulet);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserResponse>> Login([FromBody]LoginRequist  loginRequest)
        {
            var resulet = await _athinticationService.LoginAsync(loginRequest);
            return Ok(resulet);
        }
    }
}
