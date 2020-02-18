using Arcade.ViewModels;
using AutoMapper;
using Infrastructure.ObjectManagement;
using SharedEntities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityModule.Services
{
    public class ComputersOMService : IObjectManagementService<ComputerViewModel, ComputerViewModel, string>
    {

        private ComputersService _service;
        private IMapper _mapper;

        public ComputersOMService(ComputersService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        public async Task<string> Add(ComputerViewModel details, CancellationToken token = default)
        {
            return (await _service.Add(_mapper.Map<ComputerDto>(details), token)).Id;
        }

        public async Task<List<ComputerViewModel>> GetAll(CancellationToken token = default)
        {
            return _mapper.Map<List<ComputerViewModel>>(await _service.GetAll(token));
        }

        public async Task<ComputerViewModel> GetForUploadByID(string id, CancellationToken token = default)
        {
            return _mapper.Map<ComputerViewModel>(await _service.GetById(id, token));
        }

        public async Task Remove(string id, CancellationToken token = default)
        {
            await _service.Remove(id, token);
        }

        public async Task Update(ComputerViewModel details, CancellationToken token = default)
        {
            await _service.Udpate(_mapper.Map<ComputerDto>(details), token);
        }
    }
}
