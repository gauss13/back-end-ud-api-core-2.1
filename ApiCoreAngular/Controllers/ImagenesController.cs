using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Utils;
using Contracts;
using Entities.Models;

namespace ApiCoreAngular.Controllers
{
    [Route("img")]
    [ApiController]
    public class ImagenesController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;

        public ImagenesController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        [HttpGet("{*id}")]
        public IActionResult GetImagen(string id)
        {  


            var arreglo = id.Split('/');

            var tipo = arreglo[0];
            var nombreImg = arreglo[1];


            var file = Path.Combine(Directory.GetCurrentDirectory(),
                             "wwwroot", "uploads", tipo, nombreImg);

            if (!System.IO.File.Exists(file))
            {
                var fileNoImg = Path.Combine(Directory.GetCurrentDirectory(),
                           "wwwroot", "uploads", "no-img.jpg");

                file = fileNoImg;
            }

            //return PhysicalFile(file, "image/jpg");
            return PhysicalFile(file, Http.GetContentType(file));

        }

        [HttpPut("/upload/{*parametros}")]
        public async Task<IActionResult> GuardarImagen(string parametros,[FromForm] IFormFile file)
        {

            var test = HttpContext.Request.Form.Files[0];

            file = test;

            if (file == null || file.Length == 0)
            {
                return BadRequest(
                   new
                   {
                       ok = false,
                       mensaje = "No selecciono un archivo ",
                       errors = new { mensaje = " Debe seleccionar una imagen " }
                   });
            }



            var arreglo = parametros.Split('/');
            var tipo = arreglo[0];
            var idUsuario = arreglo[1];

            // tipos
            var tiposValidos = "hospitales, medicos, usuarios";

            var tes = tipo.IndexOf(tiposValidos);

            var tes1 = tiposValidos.IndexOf(tipo);

            if (tiposValidos.IndexOf(tipo) < 0)
            {
                return BadRequest(
                    new
                    {
                        ok = false,
                        mensaje = "El tipo de coleccion no es valido ",
                        errors = new { mensaje = " Tipo de coleccion no es valido " }
                    });
            }


            // obtener nombre del archivo
            var archivo = file.FileName;
            var nombreCortado = archivo.Split('.');
            var extensionArchivo = nombreCortado[nombreCortado.Length - 1];

            var extencionesValidad = "png, jpg, gif, jpeg";

            if (extencionesValidad.IndexOf(extensionArchivo) < 0)
            {

                return BadRequest(
                   new
                   {
                       ok = false,
                       mensaje = "extencion no valida ",
                       errors = new { mensaje = " las extenciones validas " + extencionesValidad }
                   });              
            }


            // Nombre de archivo personalizado          
            var imgNueva = $"{idUsuario}-{ DateTime.Now.Millisecond.ToString()}.{extensionArchivo}";

            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot", "uploads", tipo, imgNueva);                  
            

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
               await file.CopyToAsync(stream);

            }

            // buscar el usuario
            var usuariodb = await _repoWrapper.Usuario.GetUsuarioByIdAsync(idUsuario);

            if (usuariodb.Img != null)
            {
                var imgAnterior = Path.Combine(Directory.GetCurrentDirectory(),
                               "wwwroot", "uploads", tipo, usuariodb.Img);

                // obtener la img guardada y eliminarla
                // si el archivo existe lo borramos
                if (System.IO.File.Exists(imgAnterior))
                {
                    System.IO.File.Delete(imgAnterior);
                }
            }

            // actualizamos el dato del usuario         
            usuariodb.Img = imgNueva;
            await _repoWrapper.Usuario.SaveChangesAsync();

            //await _repoWrapper.Usuario.UpdateImgAsync(idUsuario, imgNueva);

            return Ok(new
            {
                ok = true,
                mensaje= "Imagen actualizado de usuario",
                usuario= usuariodb
            });        


        }
    }
}