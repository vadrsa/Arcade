using Arcade.Management.Services;
using Arcade.Management.Views;
using Infrastructure.Constants;
using Kernel.Configuration;
using SecurityModule.Features.Login;
using System.Collections.Generic;

namespace Arcade.Management
{
    public class ArcadeManagemantApplication : Project
    {
        public ArcadeManagemantApplication()
        {
            this.Configure<RegionOptions>(r =>
            {
                r.GetModalWindow = () => new ModalRegionPopup(this);
                r.RegionNames = new List<string>()
                {
                    KnownRegions.Ribbon,
                    KnownRegions.Content,
                    KnownRegions.MainMenu,
                };
            });
        }

        public override void RegisterFeatures(IFeatureRegister featureRegister)
        {
            base.RegisterFeatures(featureRegister);
            featureRegister.Register(new LoginFeature(() => new AuthenticationService()));
        }
    }
}
