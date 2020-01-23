using BusinessEntities;
using Facade.Managers;
using Microsoft.AspNetCore.Identity;
using System;

namespace Managers.Implementation
{
    public class UserRolesManager : IUserRolesManager
    {

        private IServiceProvider serviceProvider;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;

        public UserRolesManager(IServiceProvider provider, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            serviceProvider = provider;
            _userManager = userManager;
            _roleManager = roleManager;
        }

    }
}
