using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreAngular.Filtros;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreAngular.Controllers
{
    //[Route("api/[controller]")]
    [Route("/usuario")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _rolManager;
        private readonly SignInManager<Usuario> _signInManager;

        private IRepositoryWrapper _repoWrapper;

        public UsuariosController(RoleManager<IdentityRole> rolManager, UserManager<Usuario> userManager, IRepositoryWrapper repoWrapper)
        {
            this._rolManager = rolManager;
            this._userManager = userManager;
            this._repoWrapper = repoWrapper;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {

            var lista = await _repoWrapper.Usuario.GetAllUsuarioAsync();
            //return lista;
            //return new string[] { "value1", "value2", "value3" };

            if (lista == null)
            {
                var ob = new
                {
                    ok = false,
                    mensaje = "No se encontro usuarios",
                    errors = new { mensaje = "No existe un usuario con ese id" }
                };
                return BadRequest(ob);
            }


            var obj = new
            {
                ok = true,
                total = lista.Count(),
                usuarios = lista
            };

            return Ok(obj);

        }


        [HttpPost]
        public async Task<IActionResult> PostCreate([FromBody] Usuario user)
        {

            if (!ModelState.IsValid)
            {

            }

            //var itemCreado =   _repoWrapper.Usuario.CreateUsuario(user);           

            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            if (result.Succeeded == false)
            {
                //
                return BadRequest(new
                {
                    ok = false,
                    mensaje = "No se pudo crear el usuario",
                    errors = result.Errors
                });
            }


            var itemCreado = await _userManager.FindByIdAsync(user.Id);





            var obj = new
            {
                ok = true,
                usuario = itemCreado
            };

            return Created("", obj);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(string id, [FromBody] Usuario user)
        {

            //if (User.Identity.IsAuthenticated)
            //{
            //    // obtener los claims del usuario
            //    var claims = User.Claims.ToList();

            //    // agregar claims al usuario
            //    var us = await _userManager.GetUserAsync(HttpContext.User);
            //   await _userManager.AddClaimAsync(us, new System.Security.Claims.Claim("CategoriaEmpleado","4"));

            //    //Revisamos el listado, pero no estará el nuevo claim, esto es porque los claims
            //    // se generan cuando el usuario inicia session
            //    var claimsnuevos = User.Claims.ToList();
            //}


            //if (!User.Identity.IsAuthenticated)
            //{
            //    return BadRequest(new
            //    {
            //        ok = false,
            //        mensaje = "Debe iniciar session. ",
            //        errors = new { mensaje = "Es necesario iniciar session "  }
            //    });
            //}

            var itemdb = await _repoWrapper.Usuario.GetUsuarioByIdAsync(id);

            if (itemdb == null)
            {
                return BadRequest(new
                {
                    ok = false,
                    mensaje = "No se encontró el usuario con id : " + id,
                    errors = new { mensaje = "No se encontró el usuario con id : " + id }
                });
            }

            var itemCreado = await _repoWrapper.Usuario.UpdateUsuarioAsync(itemdb, user);

            return Ok(new
            {
                ok = true,
                usuario = itemCreado
            });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            var itemEncontrado = await _userManager.FindByIdAsync(id);


            if (itemEncontrado == null)
            {
                return BadRequest(new
                {
                    ok = false,
                    mensaje = "No se encontró el usuario con id : " + id,
                    errors = new { mensaje = "No se encontró el usuario con id : " + id }
                });
            }

            var result = await _userManager.DeleteAsync(itemEncontrado);


            return Ok(new
            {
                ok = true,
                usuario = itemEncontrado
            });


        }

        //[HttpPost]
        ////[ModelValidation]
        //public async Task<IActionResult> PostDemo()
        //{
        //    //instanciar el dbcontext
        //    IdentityRole role = new IdentityRole()
        //    {
        //        Id = "ADMIN_ROLE",
        //        Name = "ADMIN_ROLE"
        //    };

        //    //var user = new ApplicationUser { UserName = "juan", Email = "test@test.com", Google=false, Img="nada" };
        //    //var result = await _userManager.CreateAsync(user, "Ax123456#");
        //    var user = await _userManager.FindByIdAsync("55dece55-e1fa-4f77-bf1a-02df96570497");

        //    //var result = await _rolManager.CreateAsync(role);

        //    var result = await _userManager.AddToRoleAsync(user, "ADMIN_ROLE");

        //    if (result.Succeeded)
        //    {
        //        return Ok("Se asigno el rol");
        //    }

        //    return BadRequest("No se creo el rol");

        //    // crear un obteto user manager

        //    //Crear un rol

        //    //Crear un usario





        //}

    }
}