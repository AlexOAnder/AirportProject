using AirPortWebApi.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirPortWebApi.Data.DbContext;
using AirPortWebApi.Infrastructure.Dto;
using AirPortWebApi.Infrastructure.Repositories;

namespace AirPortWebApi.BusinessLogic.Services
{
    public class PersonalService : IPersonalService
    {
        private readonly IRepository<UserTable> _userRepository;

        public PersonalService(IRepository<UserTable> userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto GetUserById(int id)
        {
            var res = _userRepository.Get(x => x.Id == id).FirstOrDefault();
            if (res==null)
                throw new ValidationException("Cannot find user with that Id!");
            var user = new UserDto()
            {
                UserUId = res.Id,
                FirstName = res.Name,
                LastName = res.Name,
                UserRoleId = res.RoleId ?? -1
            };
            return user;
        }
    }
}
