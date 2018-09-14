using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreAngular.Policies
{
    public class CategoriaUsuarioHandler : AuthorizationHandler<CategoriaUsuarioRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            CategoriaUsuarioRequirement requirement)
        {
            //logica del requerimiento

            if(context.User.Claims.Any(x => x.Type == "CategoriaUsuario"))
            {
                // con esto indicamos que el usuario a cumplido con el requerimiento
                // pasamos el recurso a otro validador
                //
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
