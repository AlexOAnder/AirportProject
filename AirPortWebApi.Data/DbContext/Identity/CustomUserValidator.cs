using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirPortWebApi.Data.DbContext.Models;
using Microsoft.AspNet.Identity;

namespace AirPortWebApi.Data.DbContext.Identity
{
    public class CustomUserValidator : UserValidator<ApplicationUser>
    {
        public CustomUserValidator(ApplicationUserManager appUserManager)
            : base(appUserManager)
        {
            AllowOnlyAlphanumericUserNames = false;
        }
    }
}
