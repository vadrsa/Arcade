using System.Windows.Controls;

namespace SecurityModule.Workitems.Login.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            OnDataContextChanged();
            DataContextChanged += (o,e) => OnDataContextChanged();
            password.Password = "_Maruse2010";
        }

        private void OnDataContextChanged()
        {
            if(DataContext != null && DataContext is LoginViewModel)
            {
                ((LoginViewModel)DataContext).PasswordBox = password;
            }
        }
    }
}
