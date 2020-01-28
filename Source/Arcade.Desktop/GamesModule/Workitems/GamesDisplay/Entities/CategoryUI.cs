using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GamesModule.Workitems.GamesDisplay.Entities
{
    public class CategoryUI
    {
        public string Name { get; set; }
        public string Background
        {
            set
            {
                if (String.IsNullOrEmpty(value)) return;

                BackgroundBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(value));
            }
        }

        public string Border
        {
            set
            {
                if (String.IsNullOrEmpty(value)) return;
                BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(value));
            }
        }
        public Brush BackgroundBrush { get; private set; }
        public Brush BorderBrush { get; private set; }
    }
}
