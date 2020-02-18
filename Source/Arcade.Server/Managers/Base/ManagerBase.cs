using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;

namespace Managers
{
    public abstract class ManagerBase
    {
        private IConfigurationProvider _configurationProvider;
        private IMapper _mapper;
        private IServiceProvider _serviceProvider;
        private IMemoryCache _memoryCache;
        private IHttpContextAccessor _httpContextAccessor;

        public IMapper Mapper => _mapper;
        public IConfigurationProvider ConfigurationProvider => _configurationProvider;
        public IServiceProvider ServiceProvider => _serviceProvider;
        public IMemoryCache MemoryCache => _memoryCache;
        public IHttpContextAccessor HttpContextAccessor => _httpContextAccessor;


        public ManagerBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _memoryCache = serviceProvider.GetService<IMemoryCache>();
            _mapper = serviceProvider.GetService<IMapper>();
            _configurationProvider = serviceProvider.GetService<IConfigurationProvider>();
            _httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
