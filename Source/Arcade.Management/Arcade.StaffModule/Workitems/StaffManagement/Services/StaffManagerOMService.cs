using Arcade.Management.ViewModels;
using Arcade.StaffModule.Services;
using AutoMapper;
using Infrastructure.ObjectManagment;
using SharedEntities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arcade.StaffModule.Workitems.StaffManagement.Services
{

    class StaffManagerOMService : IObjectManagementService<EmployeeViewModel, EmployeeViewModel, string>
    {
        EmployeeService Service;
        IMapper Mapper;

        public StaffManagerOMService(EmployeeService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        public async Task<int> Add(EmployeeViewModel vm, CancellationToken token = default(CancellationToken))
        {
            return await Service.Add(Mapper.Map<EmployeeDto>(vm));
        }

        public async Task<List<EmployeeViewModel>> GetAll(CancellationToken token = default(CancellationToken))
        {
            return Mapper.Map<List<EmployeeViewModel>>(await Service.GetAll());
        }

        public async Task<EmployeeViewModel> GetForUploadByID(string id, CancellationToken token = default(CancellationToken))
        {
            var dto = await Service.GetForUploadByID(id, token);

            return Mapper.Map<EmployeeViewModel>(dto);
        }

        public async Task Remove(string id, CancellationToken token = default(CancellationToken))
        {
            await Service.Remove(id);
        }

        public async Task Update(EmployeeViewModel vm, CancellationToken token = default(CancellationToken))
        {

            await Service.Update(Mapper.Map<EmployeeDto>(vm));
        }
    }
}
