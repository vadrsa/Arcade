using Common.Core;
using Common.Enums;
using Common.ResponseHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core
{
    public abstract class ApiControllerBase : ControllerBase
    {
        private IServiceProvider _serviceProvider;
        protected IServiceProvider ServiceProvider { get { return _serviceProvider; } }

        public ApiControllerBase(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
        
    }
}
