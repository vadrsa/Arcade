using Arcade.ViewModels;
using GamesModule.Services;
using Infrastructure.Mvvm;
using Infrastructure.Utils;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using SharedEntities;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GamesModule.Workitems.AddEditGame.Views
{
    public class EditGameViewModel : WorkitemViewModel
    {
        private GameUploadViewModel _game;

        public GameUploadViewModel Game
        {
            get { return _game; }
            set { Set(ref _game, value, nameof(Game)); }
        }

        public bool NeedReload { get; set; }
    }
}
