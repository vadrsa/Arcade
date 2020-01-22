using Arcade.Views;
using Infrastructure.Constants;
using Kernel.Configuration;
using System.Collections.Generic;

namespace Arcade
{
    public class ArcadeProject : Project
    {
        public ArcadeProject()
        {
            this.Configure<RegionOptions>(r =>
            {
                r.GetModalWindow = () => new ModalRegionPopup(this);
                r.RegionNames = new List<string>()
                {
                    KnownRegions.Content,
                    KnownRegions.MainMenu,
                };
            });
        }
    }
}
