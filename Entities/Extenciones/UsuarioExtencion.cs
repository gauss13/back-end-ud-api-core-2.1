using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extenciones
{
   public static class UsuarioExtencion
    {
        public static void Map(this Usuario usuarioDb, Usuario itemNuevo)
        {
            usuarioDb.UserName = itemNuevo.UserName;
            usuarioDb.Email = itemNuevo.Email;
            usuarioDb.Google = itemNuevo.Google;
            usuarioDb.Img = itemNuevo.Img;
            usuarioDb.Role = itemNuevo.Role;
        }

        public static void Map(this Hospital hospitalDb, Hospital itemNuevo)
        {
            hospitalDb.Nombre = itemNuevo.Nombre;
            hospitalDb.Usuario = itemNuevo.Usuario;
            hospitalDb.Img = itemNuevo.Img;
        }

        public static void Map(this Medico medicoDb, Medico itemNuevo)
        {
            medicoDb.Nombre = itemNuevo.Nombre;
            medicoDb.IdHospital = itemNuevo.IdHospital;
            medicoDb.IdUsuario = itemNuevo.IdUsuario;
            medicoDb.Img = itemNuevo.Img;
        }
    }
}
