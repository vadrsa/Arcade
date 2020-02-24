using BusinessEntities;
using Common.Core;
using Common.Enums;
using Common.Faults;
using Common.ResponseHandling;
using DataAccess;
using Facade.Managers;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedEntities;
using SharedEntities.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Implementation
{
    public class AuthenticationManager : IAuthenticationManager
    {

        private readonly ArcadeContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IConfiguration _configuration;

        public AuthenticationManager(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmployeeManager employeeManager,
            ArcadeContext context,
            IConfiguration configuration
            )
        {
            _employeeManager = employeeManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
            EmployeeDto employee = null;
            try
            {
                employee = await _employeeManager.GetById(appUser.Id);
            }
            catch (Exception)
            {

            }

            if (employee != null && employee.IsTerminated)
                throw new FaultException(FaultType.InvalidCredentials, "Your account has been suspended");

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                List<string> roles = (await _userManager.GetRolesAsync(appUser)).ToList();
                return new UserDto { UserName = appUser.UserName, Token = GenerateJwtToken(appUser, roles).ToString(), Roles = roles };
            }
            throw new FaultException(FaultType.InvalidCredentials) { Descriptor = result};
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task RemoveAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var logins = user.Logins;
            var rolesForUser = await _userManager.GetRolesAsync(user);

            foreach (var login in logins.ToList())
            {
                await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
            }

            if (rolesForUser.Count() > 0)
            {
                foreach (var item in rolesForUser.ToList())
                {
                    var role = await _context.Roles.SingleAsync(r => r.Name == item);
                    var userRole = await _context.UserRoles.SingleAsync(r => r.RoleId == role.Id && r.UserId == id);
                    // item should be the name of the role
                    var result = await _context.DeleteAsync(userRole);
                }
            }
            await _context.DeleteAsync(user);
        }

        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
            var user = new User
            {
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var roles = (await _userManager.GetRolesAsync(user)).ToList();
                return new UserDto { Id = user.Id, UserName = user.UserName, Roles = roles, Token = GenerateJwtToken(user, roles).ToString() };
            }

            throw new FaultException(FaultType.InvalidUserRegisteration) { Descriptor = result.Errors.Select(e => e.Description)};
        }

        public async Task SetRoleAsync(string id, ApplicationRole role)
        {
            var user = await _userManager.FindByIdAsync(id);

            var rolesForUser = await _userManager.GetRolesAsync(user);

            if (rolesForUser.Count() > 0)
            {
                foreach (var item in rolesForUser.ToList())
                {
                    // item should be the name of the role
                    var roleRemoveResult = await _userManager.RemoveFromRoleAsync(user, item);
                    if(!roleRemoveResult.Succeeded)
                        throw new FaultException(FaultType.BadRequest) { Descriptor = roleRemoveResult.Errors.Select(e => e.Description) };
                }
            }
            var result = await _userManager.AddToRoleAsync(user, role.ToString());
            if (!result.Succeeded)
                throw new FaultException(FaultType.BadRequest) { Descriptor = result.Errors.Select(e => e.Description) };
        }

        private object GenerateJwtToken(User user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claimsIdentity.Claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
