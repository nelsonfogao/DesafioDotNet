using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ApplicationService
{
    public class UsuarioServices
    {
        private WebApiContext Context { get; set; }

        private IConfiguration Configuration { get; set; }
        public UsuarioServices(WebApiContext webapicontext, IConfiguration configuration)
        {
            this.Context = webapicontext;
            this.Configuration = configuration;
        }

        public String Login(String login, String password)
        {
            var usuario = Context.Usuarios.Where(x => x.Login == login && x.Password == password).FirstOrDefault();

            if (usuario == null)
            {
                return null;
            }
            return CreateToken(usuario);
        }
        private String CreateToken(Usuario usuario)
        {
            var key = Encoding.UTF8.GetBytes(this.Configuration["Token:Secret"]);

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, usuario.Login));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Audience = "ESCOLA-API",
                Issuer = "ESCOLA-API"
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            var token = tokenHandler.WriteToken(securityToken);

            return token;

        }
    }
}
