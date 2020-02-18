using Arcade.Behaviours;
using Arcade.Views;
using Infrastructure.Constants;
using Kernel.Configuration;
using QueueModule.Features;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Arcade
{
    public class ArcadeQueueProject : Project
    {
        public ArcadeQueueProject()
        {
            Configure<RegionOptions>(r =>
            {
                r.GetWindow = () => new QueueWindow();
                r.GetModalWindow = () => new DialogHostWindow(this);
                r.RegionNames = new List<string>()
                {
                    KnownRegions.Content
                };
            })
            .Configure<WorkitemBehaviorOptions>(o => o.Attach(new WorkitemOneOfTypeLaunchBehaviour()));
        }

        public override string AppDataFolderName => "ArcadeQueue";

        public override void RegisterFeatures(IFeatureRegister featureRegister)
        {
            base.RegisterFeatures(featureRegister);
            featureRegister.Register(new QueueFeature());
        }
    }
}
