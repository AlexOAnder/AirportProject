using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirPortWebApi.Data.DbContext.Models;
using Microsoft.AspNet.Identity;

namespace AirPortWebApi.Data.DbContext.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            UserValidator = new CustomUserValidator(this);
            PasswordValidator = new CustomPasswordValidator();
        }
    }
}
