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
    class SessionManagerOMService : IObjectManagementService<ComputerQueueViewModel, ComputerQueueViewModel, string>
    {
        SessionService Service;
        IMapper Mapper;

        public SessionManagerOMService(SessionService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        public Task<string> Add(ComputerQueueViewModel details, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ComputerQueueViewModel>> GetAll(CancellationToken token = default(CancellationToken))
        {
            return Mapper.Map<List<ComputerQueueViewModel>>(await Service.GetAll());
        }

        public async Task<ComputerQueueViewModel> GetForUploadByID(string id, CancellationToken token = default(CancellationToken))
        {
            var dto = await Service.GetForUploadByID(id, token);

            return Mapper.Map<ComputerQueueViewModel>(dto);
        }

        public Task Remove(string id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task Update(ComputerQueueViewModel details, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
