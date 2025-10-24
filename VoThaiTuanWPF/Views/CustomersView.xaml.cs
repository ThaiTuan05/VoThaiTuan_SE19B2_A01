using System.Windows.Controls;
using VoThaiTuanWPF.ViewModels;

namespace VoThaiTuanWPF.Views
{
    public partial class CustomersView : UserControl
    {
        public CustomersView()
        {
            InitializeComponent();
            Loaded += CustomersView_Loaded;
        }

        private void CustomersView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = DataContext as CustomersViewModel;
            if (vm == null) return;
            var pwd = this.FindName("PwdBox") as PasswordBox;
            if (pwd != null)
            {
                pwd.PasswordChanged += (s, a) =>
                {
                    if (vm.EditingCustomer != null)
                    {
                        vm.EditingCustomer.Password = pwd.Password;
                    }
                };
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = DataContext as CustomersViewModel;
            vm?.SearchCustomers();
        }
    }
}


