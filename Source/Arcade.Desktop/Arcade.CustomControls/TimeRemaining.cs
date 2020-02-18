using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Arcade.CustomControls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Arcade.CustomControls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Arcade.CustomControls;assembly=Arcade.CustomControls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:Session/>
    ///
    /// </summary>
    public class TimeRemaining : Control
    {
        static TimeRemaining()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimeRemaining), new FrameworkPropertyMetadata(typeof(TimeRemaining)));
        }

        public DispatcherTimer Timer { get; set; }

        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(string), typeof(TimeRemaining), new PropertyMetadata("{0}"));

        public DateTime FinishTime
        {
            get { return (DateTime)GetValue(FinishTimeProperty); }
            set { SetValue(FinishTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FinishTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FinishTimeProperty =
            DependencyProperty.Register("FinishTime", typeof(DateTime), typeof(TimeRemaining), new PropertyMetadata(DateTime.MinValue, FinishTimeChanged));

        private static void FinishTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = ((TimeRemaining)d);
            DateTime date = (DateTime)e.NewValue;
            obj.RemainingTimespan = default;
            obj.RemainingTime = null;
            if (date > DateTime.Now)
            {
                obj.Timer?.Stop();
                obj.RemainingTimespan = date.Subtract(DateTime.Now);
                obj.Timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    obj.RemainingTimespan = obj.FinishTime.Subtract(DateTime.Now);
                    if (obj.RemainingTimespan.Ticks < 0)
                    {
                        obj.RemainingTimespan = default;
                        obj.Timer.Stop();
                        if (obj.TimeElapsed != null && obj.TimeElapsed.CanExecute(null))
                            obj.TimeElapsed.Execute(null);
                    }
                }, Application.Current.Dispatcher);
                obj.Timer.Start();
            }
        }

        public ICommand TimeElapsed
        {
            get { return (ICommand)GetValue(TimeElapsedProperty); }
            set { SetValue(TimeElapsedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeElapsed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeElapsedProperty =
            DependencyProperty.Register("TimeElapsed", typeof(ICommand), typeof(TimeRemaining), new PropertyMetadata(null));

        private TimeSpan _remainingTimespan;
        public TimeSpan RemainingTimespan
        {
            get => _remainingTimespan;
            set
            {
                _remainingTimespan = value;
                if (_remainingTimespan == default)
                {
                    RemainingTime = null;
                    return;
                }
                string hourPart = "";
                string minutePart = "";
                int hours = (int)System.Math.Truncate(_remainingTimespan.TotalHours);
                int minutes = (int)Math.Ceiling(_remainingTimespan.Subtract(new TimeSpan(hours, 0, 0)).TotalMinutes);
                if (hours > 1)
                    hourPart = $"{hours} hours";
                else if (hours == 1)
                    hourPart = $"1 hour";
                else
                    hourPart = "";
                if (minutes > 1)
                    minutePart = $"{minutes} minutes";
                else
                    minutePart = $"1 minute";
                RemainingTime = String.Format(Format, $"{hourPart} {minutePart}");

            }
        }

        public string RemainingTime
        {
            get { return (string)GetValue(RemainingTimeProperty); }
            set { SetValue(RemainingTimeProperty, value); }
        }

        public static readonly DependencyProperty RemainingTimeProperty =
            DependencyProperty.Register("RemainingTime", typeof(string), typeof(TimeRemaining), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    }
}
