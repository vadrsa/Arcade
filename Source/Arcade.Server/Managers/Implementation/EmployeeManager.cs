using BusinessEntities;
using Common.Core;
using Common.Faults;
using DataAccess;
using Facade.Managers;
using Facade.Repositories;
using LinqToDB;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<EmployeeReportDto> GetReport(string id, DateTime date)
        {
            var employee = await ServiceProvider.GetService<IEmployeeRepository>().LoadWith(p => p.User).LoadWith(p => p.Activities).FindByIDAsync(id);
            var activitiesOfDay = employee.Activities.Where(a => a.Date.Date == date.Date).ToList();
            employee.Activities = activitiesOfDay;
            var report =  Mapper.Map<EmployeeReportDto>(employee);
            report.Date = date;
            CalculateAmountWorked(report);
            return report;
        }

        private void CalculateAmountWorked(EmployeeReportDto report)
        {
            Stack<ActivityDto> stack = new Stack<ActivityDto>();
            double amountWorked = 0;
            var activites = report.Activities.OrderBy(r => r.Date);
            foreach(var activity in activites)
            {
                if(stack.Count != 0 && stack.Peek().Type == SharedEntities.ActivityType.Login && activity.Type == SharedEntities.ActivityType.Logout)
                {
                    var login = stack.Pop();
                    amountWorked += (activity.Date - login.Date).TotalMinutes;
                }
                else
                    stack.Push(activity);
            }
            report.AmountWorked = amountWorked;
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

        [Transaction]
        public async Task TerminateAsync(string id, CancellationToken token = default)
        {
            var employee = await ServiceProvider.GetService<IEmployeeRepository>().LoadWith(p => p.User).FindByIDAsync(id, token);
            employee.IsTerminated = !employee.IsTerminated;
            await ServiceProvider.GetService<IEmployeeRepository>().UpdateAsync(employee, token);
        }

        public async Task RemoveAsync(string id, CancellationToken token = default)
        {

            var context = ServiceProvider.GetService<ArcadeContext>();
            using (var transaction = context.BeginTransaction())
            {
                var employee = await ServiceProvider.GetService<IEmployeeRepository>().LoadWith(p => p.User).FindByIDAsync(id, token);
                var activities = await context.EmployeeActivity.Where(a => a.EmployeeId == employee.Id).ToListAsync();

                foreach (var activity in activities)
                    await context.DeleteAsync(activity);

                await ServiceProvider.GetService<IEmployeeRepository>().RemoveAsync(employee, token);
                await ServiceProvider.GetService<IAuthenticationManager>().RemoveAsync(id);
                await transaction.CommitAsync(token);
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
