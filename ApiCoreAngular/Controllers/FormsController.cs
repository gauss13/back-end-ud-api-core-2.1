using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        List<Persona> lista = new List<Persona>();
        

        public IEnumerable<Persona> GetPersonas()
        {
            lista.Add(new Persona { Id = 1, Nombre = "Juan", Edad = 25, Email = "test@test.com", FechaNacimiento = new DateTime(2014, 12, 8), Activo = true, Ingreso = 2345.89M });
            lista.Add(new Persona { Id = 1, Nombre = "Pedro", Edad = 25, Email = "test@test.com", FechaNacimiento = new DateTime(2014, 12, 8), Activo = true, Ingreso = 2345.89M });
            lista.Add(new Persona { Id = 1, Nombre = "Miguel", Edad = 25, Email = "test@test.com", FechaNacimiento = new DateTime(2014, 12, 8), Activo = true, Ingreso = 2345.89M });
            lista.Add(new Persona { Id = 1, Nombre = "Carlos", Edad = 25, Email = "test@test.com", FechaNacimiento = new DateTime(2014, 12, 8), Activo = true, Ingreso = 2345.89M });
            lista.Add(new Persona { Id = 1, Nombre = "Antonio", Edad = 25, Email = "test@test.com", FechaNacimiento = new DateTime(2014, 12, 8), Activo = true, Ingreso = 2345.89M });

            return lista;
        }
    }
}