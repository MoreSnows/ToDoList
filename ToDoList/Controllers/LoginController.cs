using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Model;
using ToDoList.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace ToDoList.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;

        public LoginController(ILoginService loginService, ITokenService tokenService)
        {
            _loginService = loginService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Faz login com as credenciais do usuário.
        /// </summary>
        /// <param name="login">O objeto LoginDTO contendo as credenciais do usuário.</param>
        /// <returns>Um token JWT se as credenciais forem válidas.</returns>
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Faz login do usuário", Description = "Autentica o usuário e retorna um token JWT.")]
        [SwaggerResponse(200, "Login bem-sucedido", typeof(object))]
        [SwaggerResponse(400, "Credenciais inválidas ou dados de entrada incorretos")]
        [SwaggerResponse(401, "Credenciais inválidas")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            try
            {
                if (login == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await _loginService.Authenticate(login))
                {
                    return Unauthorized(new { message = "Invalid credentials" });
                }

                var token = await _tokenService.GenerateJwtToken(login.Login);
                return Ok(new { token });
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <param name="login">O objeto LoginDTO contendo os dados do novo usuário.</param>
        /// <returns>Mensagem de sucesso se o registro for bem-sucedido.</returns>
        [HttpPost("register")]
        [SwaggerOperation(Summary = "Registra um novo usuário", Description = "Cria um novo usuário no sistema.")]
        [SwaggerResponse(200, "Usuário registrado com sucesso", typeof(object))]
        [SwaggerResponse(400, "Tentativa de registro inválida")]
        public async Task<IActionResult> Register([FromBody] LoginModel login)
        {
            if (login == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid registration attempt");
            }

            await _loginService.RegisterUser(login);
            return Ok(new { message = "User registered successfully" });
        }

        /// <summary>
        /// Atualiza o token JWT do usuário autenticado.
        /// </summary>
        /// <returns>Um novo token JWT se o usuário estiver autenticado.</returns>
        [HttpPost("refresh-token")]
        [Authorize]
        [SwaggerOperation(Summary = "Atualiza o token JWT", Description = "Gera um novo token JWT para o usuário autenticado.")]
        [SwaggerResponse(200, "Token JWT atualizado com sucesso", typeof(object))]
        [SwaggerResponse(401, "Token inválido ou não autenticado")]
        public IActionResult RefreshToken()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var newToken = _tokenService.GenerateJwtToken(email);
            return Ok(new { token = newToken });
        }
    }
}
