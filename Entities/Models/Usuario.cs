using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
   public class Usuario : IdentityUser
    {
        //ApplicationUser
        public bool Google { get; set; }
        public string Img { get; set; }

        public  string Nombre
        {
            get
            {
                return base.UserName;
            }
            set
            {
                base.UserName = value;
            }
        }

        public  string Password
        {
            get
            {
                return base.PasswordHash;
            }
            set
            {
                base.PasswordHash = value;
            }
        }
        [Column("IdAngular")]
        public string _id
        {
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;
            }
        }

        public string Role { get; set; }
    }
}
