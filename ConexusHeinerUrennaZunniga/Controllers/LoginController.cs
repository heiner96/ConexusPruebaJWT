using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ConexusHeinerUrennaZunniga.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace ConexusHeinerUrennaZunniga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        /// <summary>
        /// permite hacer el ingreso de las credenciales al API
        /// </summary>
        /// <param name="UserName">es el user name enviado por el usuario</param>
        /// <param name="Password">es el password enviado por el usuario</param>
        /// <returns>el token en caso de si tener acceso o un 401 si no tiene acceso</returns>
        [HttpPost]
        public IActionResult Login(String UserName, String Password) 
        {
            UserModel login = new UserModel();
            login.UserName = UserName;
            login.Password = Password;
            //por defecto no esta autorizado para ingresar
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null) 
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });//si todo salio bien estatus 200, ok
            }

            return response;
        }
        /// <summary>
        /// este metodo es el que genera el token como tal
        /// </summary>
        /// <param name="user">es un objeto de tipo user con la informacion contenida</param>
        /// <returns>un string que es el token como tal ya generado y con todo incluido</returns>
        private String GenerateJSONWebToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));//se jalo la llave de la configuracion
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);//se usa el esquema de encriptacion 

            var claims = new[]//se reclama los parametros del usuario
            {
                new Claim(JwtRegisteredClaimNames.Sub ,user.UserName),
                new Claim(JwtRegisteredClaimNames.Email , user.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(//se asignan los parametros que se le envian en el token a generar
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),//se le agrega el tiempo de duracion del token, esto depende del sistema y requisitos
                signingCredentials: credentials);

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);//se genera el token como tal

            return encodeToken;

        }
        /// <summary>
        /// este metodo dice si las credenciales enviadas son correctas, que para efecto del demo estaran quemados en el codigo
        /// </summary>
        /// <param name="login">es un objeto de tipo usuario con los datos a validar</param>
        /// <returns>un usuario creado con los datos quemados</returns>
        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;
            if (login.UserName == "heiner96@gmail.com" && login.Password == "123") //se realiza la comparacion
            {
                user = new UserModel { UserName = "Heiner", EmailAddress = "heiner96@gmail.com" ,  Password = "123"};//se crea un nuevo usuario
            }
            return user;
        }
        /// <summary>
        /// este metodo es solo para verificar que el token de acceso se asigne correctamente a la persona con las credenciales enviadas
        /// </summary>
        /// <returns>un texto que dice bienvenido [UserName]</returns>
        [Authorize]
        [HttpPost("Post")]
        public string Post() 
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> cliam = identity.Claims.ToList();
            var userName = cliam[0].Value;
            return "Welcome To:" + userName;
        }
    }
}