using Arcade.Behaviours;
using Arcade.Services;
using Arcade.Views;
using Infrastructure.Constants;
using Kernel.Configuration;
using SecurityModule.Features.Login;
using System.Collections.Generic;

namespace Arcade
{
    public class ArcadeProject : Project
    {
        public ArcadeProject()
        {
            Configure<RegionOptions>(r =>
            {
                r.GetModalWindow = () => new DialogHostWindow(this);
                r.RegionNames = new List<string>()
                {
                    KnownRegions.Content,
                    KnownRegions.MainMenu,
                };
            })
            .Configure<WorkitemBehaviorOptions>(o => o.Attach(new WorkitemOneOfTypeLaunchBehaviour()));
        }
        public override void RegisterFeatures(IFeatureRegister featureRegister)
        {
            base.RegisterFeatures(featureRegister);
            featureRegister.Register(new LoginFeature(() => new AuthenticationService()));
        }
    }
}
