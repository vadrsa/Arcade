using BusinessEntities;
using Common.Core;
using Common.Faults;
using DataAccess;
using Facade.Managers;
using Facade.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Managers.Implementation
{
    public class EmployeeManager : ManagerBase, IEmployeeManager
    {
        public EmployeeManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [Transaction]
        public async Task<EmployeeAddResultDto> AddAsync(EmployeeUploadDto dto, CancellationToken token = default)
        {
            var addResult = new EmployeeAddResultDto();
            var authenticationManager = ServiceProvider.GetService<IAuthenticationManager>();
            var password = SecurityHelpers.GenerateRandomPassword(ServiceProvider.GetService<PasswordOptions>());
            var user = await authenticationManager.RegisterAsync(
                new SharedEntities.Users.RegisterDto
                {
                    Username = dto.UserName,
                    Password = dto.Password
                });
            if (dto.Role == ApplicationRole.Unset)
                throw new FaultException(FaultType.BadRequest, "Position cannot be unset");
            await authenticationManager.SetRoleAsync(user.Id, dto.Role);
            var employee = Mapper.Map<EmployeeUploadDto, Employee>(dto);
            employee.Id = user.Id;
            addResult = Mapper.Map<Employee, EmployeeAddResultDto>(await ServiceProvider.GetService<IEmployeeRepository>().InsertAsync(employee, token));
            addResult.Role = GetRole(user.Roles);
            addResult.Password = password;
            addResult.UserName = user.UserName;
            return addResult;
        }

        private ApplicationRole GetRole(IList<string> roles)
        {
            if (roles == null || roles.Count == 0) return ApplicationRole.Unset;
            foreach (string roleString in roles)
            {
                ApplicationRole role;
                Enum.TryParse(roleString, out role);
                if (role != ApplicationRole.Unset)
                    return role;
            }
            return ApplicationRole.Unset;
        }

        public async Task<List<EmployeeDto>> GetAll()
        {
            return Mapper.Map<List<EmployeeDto>>(await ServiceProvider.GetService<IEmployeeRepository>().LoadWith(p => p.User).GetAllAsync());
        }

        public async Task<EmployeeDto> GetById(string id)
        {
            var employee = await ServiceProvider.GetService<IEmployeeRepository>().LoadWith(p => p.User).FindByIDAsync(id);
            var dto = Mapper.Map<EmployeeDto>(employee);
            dto.Role  = GetRole(await ServiceProvider.GetService<UserManager<User>>().GetRolesAsync(employee.User));
            return dto;
        }

        public async Task RemoveAsync(string id, CancellationToken token = default)
        {
            var transaction = ServiceProvider.GetService<ArcadeContext>().BeginTransaction(System.Data.IsolationLevel.Serializable);
            try
            {
                var employee = await ServiceProvider.GetService<IEmployeeRepository>().LoadWith(p => p.User).FindByIDAsync(id, token);

                await ServiceProvider.GetService<IEmployeeRepository>().RemoveAsync(employee, token);
                await ServiceProvider.GetService<IAuthenticationManager>().RemoveAsync(employee.Id);
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Dispose();
            }

        }

        [Transaction]
        public async Task UpdateAsync(EmployeeDto employee, CancellationToken token = default)
        {
            var authenticationManager = ServiceProvider.GetService<IAuthenticationManager>();
            if (employee.Role == ApplicationRole.Unset)
                throw new FaultException(FaultType.BadRequest, "Position cannot be unset");
            await authenticationManager.SetRoleAsync(employee.UserId, employee.Role);
            await ServiceProvider.GetService<IEmployeeRepository>().UpdateAsync(Mapper.Map<Employee>(employee), token);
        }
    }
}
