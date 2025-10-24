using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VoThaiTuanWPF.ViewModels;

namespace VoThaiTuanWPF
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();  
            
            // Set up event handlers for ViewModel events
            var viewModel = (LoginViewModel)DataContext;
            viewModel.OnAdminLoginSuccess += () => AdminLoginSuccess();
            viewModel.OnCustomerLoginSuccess += (customer) => CustomerLoginSuccess(customer);
            viewModel.OnRegisterRequested += () => OpenRegisterWindow();
            
            // Set focus to email textbox
            Loaded += (s, e) => FocusManager.SetFocusedElement(this, this.FindName("EmailTextBox") as TextBox);
            
            // Handle PasswordBox changes
            PasswordBox.PasswordChanged += (s, e) => viewModel.Password = PasswordBox.Password;
        }

        private void AdminLoginSuccess()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void CustomerLoginSuccess(Models.Customer customer)
        {
            var customerDashboard = new CustomerDashboardWindow(customer);
            customerDashboard.Show();
            this.Close();
        }

        private void OpenRegisterWindow()
        {
            var registerWindow = new CustomerRegisterWindow();
            registerWindow.ShowDialog();
        }
    }
}
