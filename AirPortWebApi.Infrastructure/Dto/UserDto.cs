using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirPortWebApi.Infrastructure.Dto
{
    public class UserDto
    {
        public int UserUId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserRoleId { get; set; }

        public UserDto()
        {
            UserUId = 1;
            FirstName = "Alex";
            LastName = "OAnder";
            UserRoleId = 1;
        }
    }
}
