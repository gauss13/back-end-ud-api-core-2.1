using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreAngular.Models
{
    public class Medico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Img { get; set; }  
        public int IdHospital { get; set; }        
        public string IdUsuario { get; set; }
    }
}
