using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AirPortWebApi.Data.DbContext.Identity
{
    public class CustomPasswordValidator : PasswordValidator
    {
        public CustomPasswordValidator() : base()
        {
#if DEBUG
            RequiredLength = 6;
            RequireNonLetterOrDigit = false;
            RequireDigit = false;
            RequireLowercase = false;
            RequireUppercase = false;
#else
	        int requiredLength;
	        bool requireNonLetterOrDigit;
	        bool requireDigit;
	        bool requireLowercase;
	        bool requireUppercase;
			RequiredLength = int.TryParse(ConfigurationManager.AppSettings["RequiredLength"], out requiredLength) ? requiredLength : 8;
            RequireNonLetterOrDigit = !bool.TryParse(ConfigurationManager.AppSettings["RequireNonLetterOrDigit"], out requireNonLetterOrDigit) || requireNonLetterOrDigit; 
            RequireDigit = !bool.TryParse(ConfigurationManager.AppSettings["RequireDigit"], out requireDigit) || requireDigit; 
			RequireLowercase = !bool.TryParse(ConfigurationManager.AppSettings["RequireLowercase"], out requireLowercase) || requireLowercase; 
			RequireUppercase = !bool.TryParse(ConfigurationManager.AppSettings["RequireUppercase"], out requireUppercase) || requireUppercase; 
#endif
        }
    }
}
