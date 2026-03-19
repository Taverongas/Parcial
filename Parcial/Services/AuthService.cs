using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Parcial.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Parcial.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> Register(string email, string password, string role)
        {
            var usuario = new IdentityUser { UserName = email, Email = email };
            var resultado = await _userManager.CreateAsync(usuario, password);

            if (resultado.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));

                await _userManager.AddToRoleAsync(usuario, role);
            }

            return resultado;
        }

        public async Task<string?> Login(string email, string password)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario != null && await _userManager.CheckPasswordAsync(usuario, password))
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                return GenerarTokenJwt(usuario, roles);
            }

            return null;
        }

        private string GenerarTokenJwt(IdentityUser usuario, IList<string> roles)
        {
            var reclamaciones = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var rol in roles)
                reclamaciones.Add(new Claim(ClaimTypes.Role, rol));

            var firmaServidor = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: reclamaciones,
                signingCredentials: new SigningCredentials(firmaServidor, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}