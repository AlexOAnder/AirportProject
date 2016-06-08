using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirPortWebApi.Data.Application;
using AirPortWebApi.Data.DbContext.Models;
using Autofac;
using Autofac.Integration.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace AirPortWebApi.Data.DbContext.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var autoFacContainer = context.GetAutofacLifetimeScope();
            if (autoFacContainer == null) throw new ApplicationException("Cannot extract AutoFac container from Owin context");
            var appDbContextRegistered = autoFacContainer.IsRegistered<ApplicationDbContext>();
            if (!appDbContextRegistered) throw new ApplicationException("AppDbContext not registered in Autofac registry");
            var appDbContext = autoFacContainer.Resolve<ApplicationDbContext>();
            var appRoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(appDbContext));

            return appRoleManager;
        }
    }
}
