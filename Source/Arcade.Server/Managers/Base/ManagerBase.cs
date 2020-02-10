using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Managers
{
    public abstract class ManagerBase
    {
        private IConfigurationProvider _configurationProvider;
        private IMapper _mapper;
        private IServiceProvider _serviceProvider;
        private IMemoryCache _memoryCache;
        
        public IMapper Mapper => _mapper;
        public IConfigurationProvider ConfigurationProvider => _configurationProvider;
        public IServiceProvider ServiceProvider => _serviceProvider;
        public IMemoryCache MemoryCache => _memoryCache;


        public ManagerBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _memoryCache = serviceProvider.GetService<IMemoryCache>();
            _mapper = serviceProvider.GetService<IMapper>();
            _configurationProvider = serviceProvider.GetService<IConfigurationProvider>();
        }
    }
}
