using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;

namespace MaterialDesignMessageBox
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    internal partial class MaterialDesignMessageBoxWindow : Window
    {
        private const PrimaryColor _defaultColor = PrimaryColor.Blue;
        
        internal string Caption
        {
            get
            {
                return Title;
            }
            set
            {
                Title = value;
            }
        }

        internal string Message
        {
            get
            {
                return TextBlock_Message.Text;
            }
            set
            {
                TextBlock_Message.Text = value;
            }
        }

        public MessageBoxResult Result { get; set; }

        internal MaterialDesignMessageBoxWindow(Window owner, string message, string caption, MessageBoxButton button, PackIconKind? icon, PrimaryColor? primaryColor, ResourceDictionary primaryColorStyle)
        {
            InitializeComponent();

            // Apply the built in material design primary color styles
            var primaryColorDictionary = new ResourceDictionary();
            if (primaryColor.HasValue)
            {
                primaryColorDictionary.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor." + primaryColor + ".xaml");
            }
            else if(primaryColorStyle != null)
            {
                primaryColorDictionary = primaryColorStyle;
            }
            else
            {
                primaryColorDictionary.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor." + _defaultColor + ".xaml");
            }
            Resources.MergedDictionaries.Add(primaryColorDictionary);

            // Ensure parent window is shown when the message box tries to show up
            if (owner != null)
            {
                Owner = owner;
                owner.Focus();
                owner.Activate();
            }

            Message = message;
            Caption = caption ?? string.Empty;
            DisplayImage(icon);
            DisplayButtons(button);
        }

        private void DisplayButtons(MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.OKCancel:
                    // Hide all but OK, Cancel
                    Button_OK.Visibility = Visibility.Visible;
                    Button_OK.Focus();
                    Button_Cancel.Visibility = Visibility.Visible;

                    Button_Yes.Visibility = Visibility.Collapsed;
                    Button_No.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNo:
                    // Hide all but Yes, No
                    Button_Yes.Visibility = Visibility.Visible;
                    Button_Yes.Focus();
                    Button_No.Visibility = Visibility.Visible;

                    Button_OK.Visibility = Visibility.Collapsed;
                    Button_Cancel.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNoCancel:
                    // Hide only OK
                    Button_Yes.Visibility = Visibility.Visible;
                    Button_Yes.Focus();
                    Button_No.Visibility = Visibility.Visible;
                    Button_Cancel.Visibility = Visibility.Visible;

                    Button_OK.Visibility = Visibility.Collapsed;
                    break;
                default:
                    // Hide all but OK
                    Button_OK.Visibility = Visibility.Visible;
                    Button_OK.Focus();

                    Button_Yes.Visibility = Visibility.Collapsed;
                    Button_No.Visibility = Visibility.Collapsed;
                    Button_Cancel.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void DisplayImage(PackIconKind? icon)
        {
            if (!icon.HasValue)
            {
                Image_MessageBox.Kind = PackIconKind.Null;
                Image_MessageBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                Image_MessageBox.Kind = icon.Value;
                Image_MessageBox.Visibility = Visibility.Visible;
            }
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            Close();
        }

        private void Button_Yes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            Close();
        }

        private void Button_No_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            Close();
        }    
    }
}
