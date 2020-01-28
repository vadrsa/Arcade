using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Managers
{
    public abstract class ManagerBase<T>
        where T : DbContext
    {
        private IConfigurationProvider _configurationProvider;
        private IMapper _mapper;
        private IServiceProvider _serviceProvider;
        private T _context;
        private IMemoryCache _memoryCache;
        
        public IMapper Mapper => _mapper;
        public IConfigurationProvider ConfigurationProvider => _configurationProvider;
        public T Context => _context;
        public IServiceProvider ServiceProvider => _serviceProvider;
        public IMemoryCache MemoryCache => _memoryCache;


        public ManagerBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _context = serviceProvider.GetService<T>();
            _memoryCache = serviceProvider.GetService<IMemoryCache>();
            _mapper = serviceProvider.GetService<IMapper>();
            _configurationProvider = serviceProvider.GetService<IConfigurationProvider>();
        }
    }
}
