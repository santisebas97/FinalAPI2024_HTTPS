using FinalAPI2024_HTTPS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalAPI2024_HTTPS.Helpers;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using FinalAPI2024_HTTPS.Services;
using FinalAPI2024_HTTPS.Models.Dto;

namespace FinalAPI2024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AplicacionRecorridos2024Context _context;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;

        public UsuarioController(AplicacionRecorridos2024Context context, IConfiguration config, IEmailService emailService) //inyectamos el contexto de la base de datos y pasamos a la variable privada
        {
            _context = context;
            _config = config;
            _emailService = emailService;
        }

        [HttpPost("authenticate")] 
        public async Task<IActionResult> Authenticate([FromBody] Usuario userObj)
        {
            if (userObj == null)
            {
                return BadRequest(new { Message = "Usuario no encontrado!" });
            }

            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == userObj.Email);
            if (user == null)
            {
                return NotFound(new { Message = "Usuario no encontrado!" });
            }

            if (!PasswordHasher.VerifyPassword(userObj.Passwd, user.Passwd))
            {
                return BadRequest(new { Message = "Contraseña incorrecta" });
            }

            //
            user.Token = CreateJwt(user); 

            return Ok(new
            {
                
                Token = user.Token, 
                Message = "Acceso exitoso!"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] Usuario userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            else
            {
                if (await CheckEmailExistAsync(userObj.Email))
                {
                    return BadRequest(new { Message = "el email ya está registrado!" });
                }
                var pass = CheckPasswordStrength(userObj.Passwd);

                if (!string.IsNullOrEmpty(pass))
                {
                    return BadRequest(new { Message = pass.ToString() });
                }
                userObj.Passwd = PasswordHasher.HashPassword(userObj.Passwd);

                userObj.Token = CreateJwt(userObj);
                await _context.Usuarios.AddAsync(userObj);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Usuario Registrado correctamente!" });
            }
        }

        //metodo para verificar si el email ya existe en la base
        private Task<bool> CheckEmailExistAsync(string email)
        {
            return _context.Usuarios.AnyAsync(x => x.Email == email);
        }

        //metodo para verificar la fuerza de la contraseña
        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
            {
                sb.Append("La contraseña debe tener al menos 8 caracteres" + Environment.NewLine);
            }
            if (!(Regex.IsMatch(password, "[a-z]") /*&& Regex.IsMatch(password, "[A-Z]")*/
                && Regex.IsMatch(password, "[0-9]")))
            {
                sb.Append("la contraseña debe tener letras y numeros" + Environment.NewLine);
            }
            /*
            if (!Regex.IsMatch(password,"[<,>,@,!,#,$,%]"))
            {
                sb.Append("La contraseña debe contener caracteres especiales"+Environment.NewLine);
            }
            */
            return sb.ToString();
        }

        //metodo para crear el token de usuario - el token tiene 3 partes/ despues de crear ir a program.cs
        private string CreateJwt(Usuario user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("AplicacionTesisFinal.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()), //id del usuario
            new Claim(ClaimTypes.Role, user.Rol),//rol
            new Claim(ClaimTypes.Name,$"{user.Nombre} {user.Apellido}") //nombre y apellido
            }); ;

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Usuario>> GetAllUsers()
        {
            return Ok(await _context.Usuarios.ToListAsync());
        }

        //[Authorize]//esto sirve solo para usuarios autorizados
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> ListarUsuario(int id)
        {
            return Ok(await _context.Usuarios.Where(x => x.IdUser == id).ToListAsync());
        }

        //enviar el email de cambio de contraseña
        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(a => a.Email == email);
            if (user is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "el correo no existe"
                });
            }
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);
            user.ResetPasswdTkn = emailToken;
            //user.ResetPasswordExpiry = DateTime.Now.AddMinutes(15);
            string from = _config["EmailSettings:From"];
            var emailModel = new Email(email, "Cambia tu contraseña!!", EmailBody.EmailStringBody(email, emailToken));
            _emailService.SendEmail(emailModel);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Correo enviado!"
            });
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var newToken = resetPasswordDto.EmailToken.Replace(" ", "+");
            var user = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(a => a.Email == resetPasswordDto.Email);
            if (user is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "el usuario no existe"
                });
            }

            var tokenCode = user.ResetPasswdTkn;
            if (tokenCode != resetPasswordDto.EmailToken)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "link de correo inválido"
                });
            }
            user.Passwd = PasswordHasher.HashPassword(resetPasswordDto.NewPassword);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Contraseña cambiada correctamente"
            });
        }
    }
}
