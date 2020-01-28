using Arcade.ViewModels;
using GamesModule.Services;
using GamesModule.Workitems.GameDetails;
using GamesModule.Workitems.GamesDisplay.Entities;
using Infrastructure.Api;
using Infrastructure.Mvvm;
using Infrastructure.Utils;
using Prism.Commands;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamesModule.Workitems.GamesDisplay.Views
{
    public class GamesDisplayViewModel : WorkitemViewModel
    {
        private const string ALL_CATEGORIES_NAME = "All";

        private static List<Tuple<string, string>> CategoryColors = new List<Tuple<string, string>>() {
            new Tuple<string, string>("#FFCB3636", "#FFCB3636"),
            new Tuple<string, string>("#FF1697E0", "#FF1697E0"),
            new Tuple<string, string>("#FF02FF3B", "#FF3AB74B"),
        };


        public GamesDisplayViewModel()
        {
            Load();
        }

        private DelegateCommand<string> _openGameCommand;
        public DelegateCommand<string> OpenGameCommand
        {
            get
            {
                if(_openGameCommand == null)
                    _openGameCommand = new DelegateCommand<string>(OpenGame);
                return _openGameCommand;
            }
        }

        private DelegateCommand<string> _filterByCategoryCommand;

        public DelegateCommand<string> FilterByCategoryCommand
        {

            get
            {
                if (_filterByCategoryCommand == null)
                    _filterByCategoryCommand = new DelegateCommand<string>(FilterByCategory);
                return _filterByCategoryCommand;
            }
        }

        private void FilterByCategory(string category)
        {
            if (ALL_CATEGORIES_NAME == category)
            {
                Games.ClearFilter();
            }
            else
            {
                Games.Filter(g => g.Category == category);
            }
        }

        private void OpenGame(string id)
        {
            ((GamesDisplayWorkitem)Workitem).OpenDetails(id);
        }

        private ObservableCollection<CategoryUI> _categories;
        public ObservableCollection<CategoryUI> Categories
        {
            get => _categories;
            set => Set(ref _categories, value, nameof(Categories));
        }

        private FilteredObservableCollection<GameViewModel> _games;
        public FilteredObservableCollection<GameViewModel> Games
        {
            get => _games;
            set => Set(ref _games, value, nameof(Games));
        }

        protected override async Task DoLoad(CancellationToken token)
        {
            var games = await new GamesService().GetAll(token);
            Games = Mapper.Map<FilteredObservableCollection<GameViewModel>>(games);
            int i = 0;
            Categories = new ObservableCollection<CategoryUI>(Games.GroupBy(g=> g.Category).Select(g => {
                var color = CategoryColors[i++ % CategoryColors.Count];
                return new CategoryUI { Name = g.Key, Border = color.Item2, Background = color.Item1 };
            }).ToList());
            var c = CategoryColors[i++ % CategoryColors.Count];
            Categories.Insert(0, new CategoryUI { Name = ALL_CATEGORIES_NAME, Border = c.Item2, Background = c.Item1 });
        }
    }
}
