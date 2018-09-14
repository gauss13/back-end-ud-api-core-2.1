using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
  public  class Medico
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Nombre { get; set; }
        public string Img { get; set; }
        public int IdHospital { get; set; }
        public string IdUsuario { get; set; }
    }
}
