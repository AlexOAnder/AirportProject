using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AirPortWebApi.Data.DbContext.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? UserUId { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public virtual ICollection<UserClient> UserClients { get; set; }
    }
}
