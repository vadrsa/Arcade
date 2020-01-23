﻿using GamesModule.Workitems;
using Infrastructure.Prism;
using Prism.Ioc;

namespace Modules
{
    public class GamesModule : Module
    {
        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            CurrentContextService.LaunchWorkItem<GamesDisplayWorkitem>();
        }
    }
}