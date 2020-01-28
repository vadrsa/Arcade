using GamesModule.Services;
using GamesModule.Workitems.AddEditGame.Views;
using Infrastructure.Constants;
using Infrastructure.Mvvm;
using Infrastructure.Utils;
using Infrastructure.Workitems;
using Kernel;
using Kernel.Workitems;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Ioc;
using SharedEntities;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GamesModule.Workitems.AddEditGame
{
    class AddEditGameWorkitem : WorkitemWpfBase, ISupportsInitialization
    {
        private AddEditGameInitializer _data;
        private EditGameViewModel viewModel;

        public AddEditGameWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Edit Game";

        public void Initialize(object data)
        {
            if (data is AddEditGameInitializer)
            {
                _data = ((AddEditGameInitializer)data);
            }
            else
                throw new ArgumentException();
        }

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            var view = container.Register<EditGameView>(new EditGameView(), KnownRegions.Content);
            viewModel = (EditGameViewModel)view.DataContext;
            viewModel.Game = _data.Game;
            IsAdding = _data.IsAdding;

        }

        protected override void RegisterCommands(ICommandContainer container)
        {
            base.RegisterCommands(container);
            container.Register(AddEditGame.Constants.Commands.BrowseImage, BrowseImageCommand);
            container.Register(AddEditGame.Constants.Commands.SaveGame, SaveCommand);
        }


        public bool IsAdding { get; set; }

        private AsyncCommand _saveCommand;

        public AsyncCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new AsyncCommand(Save);
                return _saveCommand;
            }
        }

        private async Task Save()
        {
            await viewModel.LoadCustom(DoSave);
        }

        private async Task DoSave(CancellationToken token)
        {
            if (IsAdding)
            {
                var game = await new GamesService().Add(Mapper.Map<GameUploadDto>(viewModel.Game), token);
                Communication.OnNext(new WorkitemEventArgs(this, true));
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                await new GamesService().Udpate(Mapper.Map<GameUploadDto>(viewModel.Game), token);
                Communication.OnNext(new WorkitemEventArgs(this, true));
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
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
                viewModel.Game.Image = memStream.ToArray();
                viewModel.Game.ImagePath = path;
            }
            catch
            {

            }
        }
    }
}
