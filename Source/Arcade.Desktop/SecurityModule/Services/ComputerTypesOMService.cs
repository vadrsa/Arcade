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

namespace SecurityModule.Services
{
    public class ComputerTypesOMService : IObjectManagementService<ComputerTypeViewModel, ComputerTypeViewModel, string>
    {

        private ComputerTypesService _service;
        private IMapper _mapper;

        public ComputerTypesOMService(ComputerTypesService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        public async Task<string> Add(ComputerTypeViewModel details, CancellationToken token = default)
        {
            return (await _service.Add(_mapper.Map<ComputerTypeDto>(details), token)).Id;
        }

        public async Task<List<ComputerTypeViewModel>> GetAll(CancellationToken token = default)
        {
            return _mapper.Map<List<ComputerTypeViewModel>>(await _service.GetAll(token));
        }

        public async Task<ComputerTypeViewModel> GetForUploadByID(string id, CancellationToken token = default)
        {
            return _mapper.Map<ComputerTypeViewModel>(await _service.GetById(id, token));
        }

        public async Task Remove(string id, CancellationToken token = default)
        {
            await _service.Remove(id, token);
        }

        public async Task Update(ComputerTypeViewModel details, CancellationToken token = default)
        {
            await _service.Udpate(_mapper.Map<ComputerTypeDto>(details), token);
        }
    }
}
