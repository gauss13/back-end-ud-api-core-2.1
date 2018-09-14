using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
  public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public decimal Ingreso { get; set; }
        public string Email { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public bool Activo { get; set; }
    }
}
