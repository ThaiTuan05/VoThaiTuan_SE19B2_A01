using System.Windows.Input;
using BLL;
using Models;
using VoThaiTuanWPF.Commands;

namespace VoThaiTuanWPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly AuthService _authService;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isLoading = false;

        public LoginViewModel()
        {
            _authService = new AuthService();
            
            LoginCommand = new RelayCommand(_ => Login(), _ => CanLogin(null));
            RegisterCommand = new RelayCommand(_ => Register());
        }

        public string Email
        {
            get => _email;
            set
            {
                if (SetProperty(ref _email, value))
                {
                    LoginCommand.CanExecute(null);
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    LoginCommand.CanExecute(null);
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private bool CanLogin(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(Email) && 
                   !string.IsNullOrWhiteSpace(Password) && 
                   !IsLoading;
        }

        private void Login()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Please enter both email and password.";
                    return;
                }

                var customer = _authService.Authenticate(Email, Password);
                if (customer != null)
                {
                    // Auto-route based on account type: CustomerID == 0 => Admin
                    if (customer.CustomerID == 0)
                    {
                        OnAdminLoginSuccess?.Invoke();
                    }
                    else
                    {
                        OnCustomerLoginSuccess?.Invoke(customer);
                    }
                }
                else
                {
                    ErrorMessage = "Invalid email or password. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Login error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void Register()
        {
            OnRegisterRequested?.Invoke();
        }

        // Events for view to handle
        public event Action? OnAdminLoginSuccess;
        public event Action<Customer>? OnCustomerLoginSuccess;
        public event Action? OnRegisterRequested;
    }
}
