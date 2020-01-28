using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure
{
    public interface IHasVisibility
    {
        Visibility Visibility { get; }
        event EventHandler<EventArgs> VisibilityChanged;

    }
}
