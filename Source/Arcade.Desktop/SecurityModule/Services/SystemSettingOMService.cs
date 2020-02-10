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
    class SystemSettingOMService : IObjectManagementService<SystemSettingViewModel, SystemSettingViewModel, int>
    {
        private SystemSettingService _service;
        private IMapper _mapper;

        public SystemSettingOMService(SystemSettingService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        public Task<int> Add(SystemSettingViewModel details, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SystemSettingViewModel>> GetAll(CancellationToken token = default)
        {
            return _mapper.Map<List<SystemSettingViewModel>>(await _service.GetAll(token));
        }

        public async Task<SystemSettingViewModel> GetForUploadByID(int id, CancellationToken token = default)
        {
            return _mapper.Map<SystemSettingViewModel>(await _service.GetById(id, token));
        }

        public Task Remove(int id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public async Task Update(SystemSettingViewModel details, CancellationToken token = default)
        {
            await _service.Udpate(_mapper.Map<SystemSettingDto>(details));
        }
    }
}
