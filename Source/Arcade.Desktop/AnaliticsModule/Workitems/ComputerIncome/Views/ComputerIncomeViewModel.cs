using Arcade.ViewModels;
using Infrastructure.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaliticsModule.Workitems.ComputerIncome.Views
{
    class ComputerIncomeViewModel : WorkitemViewModel<ComputerIncomeWorkitem>
    {

        public ComputerIncomeViewModel()
        {
            List = new FilteredObservableCollection<ComputerAnaliticsViewModel>() { 
                new ComputerAnaliticsViewModel { 
                    Type = new ComputerTypeViewModel{ Name = "test type"},
                    Number = 2,
                    Earnings = 200
                }
            };
        }

        private FilteredObservableCollection<ComputerAnaliticsViewModel> items;
        public FilteredObservableCollection<ComputerAnaliticsViewModel> List
        {
            get => items;
            set => Set(ref items, value, nameof(List));
        }
    }
}
