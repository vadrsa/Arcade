using Arcade.ViewModels;
using AutoMapper;
using Infrastructure.ObjectManagement;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StaffModule.Services
{
    class StaffManagerOMService : IObjectManagementService<EmployeeViewModel, EmployeeUploadViewModel, string>
    {
        StaffService Service;
        IMapper Mapper;

        public StaffManagerOMService(StaffService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        public async Task<string> Add(EmployeeUploadViewModel vm, CancellationToken token = default(CancellationToken))
        {
            return (await Service.Add(Mapper.Map<EmployeeUploadDto>(vm))).UserId;
        }

        public async Task<List<EmployeeViewModel>> GetAll(CancellationToken token = default(CancellationToken))
        {
            return Mapper.Map<List<EmployeeViewModel>>(await Service.GetAll());
        }

        public async Task<EmployeeUploadViewModel> GetForUploadByID(string id, CancellationToken token = default(CancellationToken))
        {
            var dto = await Service.GetForUploadByID(id, token);

            return Mapper.Map<EmployeeUploadViewModel>(dto);
        }

        public async Task Remove(string id, CancellationToken token = default(CancellationToken))
        {
            await Service.Remove(id);
        }

        public async Task Update(EmployeeUploadViewModel vm, CancellationToken token = default(CancellationToken))
        {

            await Service.Update(Mapper.Map<EmployeeDto>(vm));
        }
    }
}
