using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ApiCoreAngular.Controllers
{
    [Route("/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _rolManager;
        private readonly SignInManager<Usuario> _signInManager;

        private IRepositoryWrapper _repoWrapper;

        public LoginController(IRepositoryWrapper repoWrapper, SignInManager<Usuario> signInManager, UserManager<Usuario> userManager)
        {

         

            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        [HttpPost]
        //public async Task<IActionResult> PostLogin([FromForm] string email, [FromForm] string password)
        public async Task<IActionResult> PostLogin([FromBody] Usuario usuario)
        {
            var r = await _userManager.FindByEmailAsync(usuario.Email);

            if (r == null)

            {
                return BadRequest(
                    new
                    {
                        ok = false,
                        mensaje = "No se encontró el usuario ",
                        errors = new { mensaje = "No se encontró el usuario " }
                    });
            }


            var result = await _signInManager.PasswordSignInAsync(r.UserName, usuario.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            else
            {
                var token = TokenBuilder(r);
                return Ok(new
                {
                    ok = true,
                    usuario = r,
                    token = token,
                    id = r.Id,
                    menu = ObtenerMenu(r.Role)
                });

            }

            #region OPCIONES DE RESULT
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Intento de inicio de sesión no válido.");
            //        return View(model);
            //} 
            #endregion

        }

        [Route("/login/google")]
        [HttpPost]
        public async Task<IActionResult> PostLoginGoogle([FromBody] TokenGoogle strToken)
        {

            try
            {
                var test = HttpContext.Request.Body;

                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(strToken.token);


                if (payload == null)
                {
                    return BadRequest(
                  new
                  {
                      ok = false,
                      mensaje = "Token no valido ",
                      errors = new { mensaje = "Token no valido " }
                  });
                }


                var r = await _userManager.FindByEmailAsync(payload.Email);

                if (r == null)
                {

                    // creamos el usuario
                    Usuario user = new Usuario()
                    {
                        Google = true,
                        Img = payload.Picture,
                        Nombre = payload.Name,
                        UserName = payload.Name,
                        Email = payload.Email,
                        Password = ":):):):)",
                        Role = "USER_ROLE"
                    };

               

                    var result = await _userManager.CreateAsync(user, user.PasswordHash);


                    if (!result.Succeeded)
                    {
                        return BadRequest(new
                        {
                            ok = false,
                            mensaje = "Error al crear el usuario ",
                            errors = result.Errors
                        });
                    }

                    r = user;
                }



                // generamos el token
                var token = TokenBuilder(r);
                return Ok(new
                {
                    ok = true,
                    usuario = r,
                    token = token,
                    id = r.Id,
                    menu = ObtenerMenu(r.Role)
                });



            }
            catch (Exception)
            {
                return BadRequest(
                   new
                   {
                       ok = false,
                       mensaje = "Error al buscar el usuario ",
                       errors = new { mensaje = "Error al buscar el usuario " }
                   });

            }

        }



        private string TokenBuilder(Usuario user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "ADMIN_ROLE")
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:59183",//Servidor del token
                audience: "http://localhost:59183",// destinatarios validos
                claims: claims, // claims del usuario - new List<Claim>()
                expires: DateTime.Now.AddHours(4), // fecha y hora de la caducidad del token
                signingCredentials: signinCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }


        private object ObtenerMenu(string role)
        {

            var root = new[] {
           new {
                    titulo = "Principal",
                    icono = "mdi mdi-gauge",
                    submenu = new [] { new { titulo = @"Dashboard", url="/dashboard" },
                                       new { titulo = "ProgressBar", url="/progress" },
                                       new { titulo = "Gráficas", url="/graficas1" },
                                       new { titulo = "Promesas", url="/promesas" },
                                       new { titulo = "Rxjs", url="/rxjs" },
                                     }
                },
               new
                {
                   titulo ="Mantenimientos",
                   icono = "mdi mdi-folder-lock-open",
                   submenu = new[] { new { titulo = "Hospitales", url = "/hospitales" },
                                     new { titulo = "Médicos", url="/medicos" }
                   }
               }


            };


            if (role == "ADMIN_ROLE")
            {
                root[1].submenu.SetValue(new { titulo = "Usuarios", url = "/usuarios" }, 0);
            }


            //var test = root.ToJ
            //var resultado = JsonConvert.SerializeObject(root);



            return root;


        }
    }

    public class TokenGoogle
    {
        public string token { get; set; }
    }
}