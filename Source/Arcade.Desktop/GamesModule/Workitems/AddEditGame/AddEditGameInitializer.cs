using Arcade.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesModule.Workitems.AddEditGame
{
    class AddEditGameInitializer
    {
        public bool IsAdding { get; set; } = true;
        public GameUploadViewModel Game { get; set; }
    }
}
