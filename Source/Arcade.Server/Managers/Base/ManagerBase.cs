using Common.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Common.Services;

namespace Managers.Base
{
    public class ManagerBase
    {
        private IServiceProvider serviceProvider;
        private IHttpContextAccessor httpContextAccessor;
        private IMemoryCache memoryCache;
        private IOptions<RequestLocalizationOptions> localizationOptions;
        private INotificationService notificationService;

        protected IOptions<RequestLocalizationOptions> LocalizationOptions
        {
            get
            {
                return localizationOptions;
            }
        }

        protected INotificationService Notification
        {
            get
            {
                return notificationService;
            }
        }

        protected IServiceProvider ServiceProvider
        {
            get
            {
                return serviceProvider;
            }
        }


        protected HttpContext HttpContext
        {
            get
            {
                return httpContextAccessor.HttpContext;
            }
        }


        protected CultureInfo Culture
        {
            get
            {
                return HttpContext.GetCulture();
            }
        }


        protected IMemoryCache MemoryCache
        {
            get
            {
                return memoryCache;
            }
        }

        public ManagerBase(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            this.memoryCache = serviceProvider.GetService<IMemoryCache>();
            this.localizationOptions = serviceProvider.GetService<IOptions<RequestLocalizationOptions>>();
            this.notificationService = serviceProvider.GetService<INotificationService>();
        }
    }
}
