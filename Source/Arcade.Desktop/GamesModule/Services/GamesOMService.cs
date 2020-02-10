using Arcade.ViewModels;
using AutoMapper;
using Infrastructure.ObjectManagement;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GamesModule.Services
{
    public class GamesOMService : IObjectManagementService<GameViewModel, GameUploadViewModel, string>
    {
        private GamesService _service;
        private IMapper _mapper;

        public GamesOMService(GamesService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        public async Task<string> Add(GameUploadViewModel details, CancellationToken token = default)
        {
            return (await _service.Add(_mapper.Map<GameUploadDto>(details), token)).Id;
        }

        public async Task<List<GameViewModel>> GetAll(CancellationToken token = default)
        {
            return _mapper.Map<List<GameViewModel>>(await _service.GetAll(token));
        }

        public async Task<GameUploadViewModel> GetForUploadByID(string id, CancellationToken token = default)
        {
            return _mapper.Map<GameUploadViewModel>(await _service.GetForUpload(id, token));
        }

        public async Task Remove(string id, CancellationToken token = default)
        {
            await _service.Remove(id, token);
        }

        public async Task Update(GameUploadViewModel details, CancellationToken token = default)
        {
            await _service.Udpate(_mapper.Map<GameUploadDto>(details), token);
        }
    }
}
