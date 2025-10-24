using System.Windows;
using VoThaiTuanWPF.ViewModels;

namespace VoThaiTuanWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Set up event handler for logout only
            var viewModel = (MainWindowViewModel)DataContext;
            viewModel.OnLogout += () => Logout();
        }

        private void Logout()
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
