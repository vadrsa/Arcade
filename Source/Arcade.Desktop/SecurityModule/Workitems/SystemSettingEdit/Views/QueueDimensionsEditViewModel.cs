using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityModule.Workitems.SystemSettingEdit.Views
{
    public class QueueDimensionsEditViewModel : ObjectManagerDetailsViewModel<SystemSettingViewModel>
    {

        private int _width;
        public int Width
        {
            get { return _width; }
            set { 
                Set(ref _width, value, nameof(Width));
                CalculateValue();
            }
        }

        private int _height;
        public int Height
        {
            get { return _height; }
            set { 
                Set(ref _height, value, nameof(Height));
                CalculateValue();
            }
        }

        private void CalculateValue()
        {
            var newValue = Width + "x" + Height;
            if (Details.Value != newValue)
                Details.Value = newValue;
        }

        protected override void OnDetailsChanged()
        {
            base.OnDetailsChanged();
            if (String.IsNullOrEmpty(Details?.Value) || !Details.Value.Contains("x"))
            {
                Width = 0;
                Height = 0;
            }
            try
            {
                var dimensions = Details.Value.Split('x');
                Width = Int32.Parse(dimensions[0]);
                Height = Int32.Parse(dimensions[1]);
            }
            catch
            {
                Width = 0;
                Height = 0;
            }

        }
    }
}
