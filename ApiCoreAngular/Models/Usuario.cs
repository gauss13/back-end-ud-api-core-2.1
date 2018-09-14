using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreAngular.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool Google { get; set; }
        public string Img { get; set; }
    }
}
