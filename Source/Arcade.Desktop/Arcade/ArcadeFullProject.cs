using Arcade.Behaviours;
using Arcade.Services;
using Arcade.Views;
using Infrastructure.Constants;
using Kernel.Configuration;
using SecurityModule.Features;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Arcade
{
    public class ArcadeFullProject : Project
    {
        public ArcadeFullProject()
        {
            Configure<RegionOptions>(r =>
            {
                r.GetWindow = () => new MainWindow();
                r.GetModalWindow = () => new DialogHostWindow(this);
                r.RegionNames = new List<string>()
                {
                    KnownRegions.Content,
                    KnownRegions.MainMenu,
                };
            })
            .Configure<WorkitemBehaviorOptions>(o => o.Attach(new WorkitemOneOfTypeLaunchBehaviour()));
        }


        public override string AppDataFolderName => "ArcadeAdmin";

        public override void RegisterFeatures(IFeatureRegister featureRegister)
        {
            base.RegisterFeatures(featureRegister);
            featureRegister.Register(new GamesModule.Features.GamesEditFeature());
            featureRegister.Register(new GamesModule.Features.GameDisplayFeature());
            featureRegister.Register(new StaffModule.Features.ManageSessionsFeature());
            featureRegister.Register(new StaffModule.Features.StaffEditFeature());
            featureRegister.Register(new ConfigurationFeature());
            featureRegister.Register(new LoginFeature(() => new AuthenticationService()));
        }
    }
}
