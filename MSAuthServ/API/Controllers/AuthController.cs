using System.Threading.Tasks;
using System.Web.Http;
using MSAuthServ.Application.Interfaces;
using MSAuthServ.Application.Services;
using MSAuthServ.Domain;
using MSAuthServ.Infrastructure.Data;
using MSAuthServ.Infrastructure.Repositories;

namespace MSAuthServ.API.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;

        public AuthController() : this(new AuthService(new AuthRepository(new AuthDbContext()),
                             "test-issuer",
                             "test-audience",
                             "ClaveSuperSeguraConExactamente32Caracteres!!"))
        {
        }

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid login request.");
            }

            var token = await _authService.AuthenticateAsync(request.Username, request.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(new { token });
        }
    }
}