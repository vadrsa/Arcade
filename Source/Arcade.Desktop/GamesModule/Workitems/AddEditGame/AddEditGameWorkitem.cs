using Arcade.ViewModels;
using GamesModule.Services;
using GamesModule.Workitems.AddEditGame.Views;
using Infrastructure.ObjectManagement;
using Infrastructure.Utils;
using Kernel.Workitems;
using Prism.Commands;
using Prism.Ioc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GamesModule.Workitems.AddEditGame
{
    class AddEditGameWorkitem : ObjectManagerDetailsWorkitem<EditGameView, GameViewModel, GameUploadViewModel, string>
    {
        protected override IObjectManagementService<GameViewModel, GameUploadViewModel, string> ObjectManagementService => Container.Resolve<GamesOMService>();

        public AddEditGameWorkitem(IContainerExtension container) : base(container)
        {
        }


        public override string WorkItemName => "Edit Game";


        protected override void RegisterCommands(ICommandContainer container)
        {
            base.RegisterCommands(container);
            container.Register(AddEditGame.Constants.Commands.BrowseImage, BrowseImageCommand);
        }

        private DelegateCommand _browseImageCommand;

        public DelegateCommand BrowseImageCommand
        {
            get
            {
                if (_browseImageCommand == null)
                    _browseImageCommand = new DelegateCommand(BrowseImage);
                return _browseImageCommand;
            }
        }

        private void BrowseImage()
        {
            var path = FileHelper.LoadImageFile();
            try
            {
                var bitamp = new Bitmap(path);
                var memStream = new MemoryStream();
                bitamp.Save(memStream, ImageFormat.Jpeg);
                ViewModel.Details.Image = memStream.ToArray();
                ViewModel.Details.ImagePath = path;
            }
            catch
            {

            }
        }
    }
}
