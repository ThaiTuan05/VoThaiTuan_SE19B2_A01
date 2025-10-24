using System.Collections.ObjectModel;
using System.Windows.Input;
using BLL;
using Models;
using VoThaiTuanWPF.Commands;

namespace VoThaiTuanWPF.ViewModels
{
    public class CustomersViewModel : ViewModelBase
    {
        private readonly CustomerService _customerService;
        private ObservableCollection<Customer> _customers;
        private Customer? _selectedCustomer;
        private string _searchText = string.Empty;
        private string _statusText = string.Empty;
        private bool _isEditorOpen;
        private string _editorTitle = string.Empty;
        private Customer _editingCustomer = new Customer();

        public CustomersViewModel()
        {
            _customerService = new CustomerService();
            _customers = new ObservableCollection<Customer>();
            
            LoadCustomersCommand = new RelayCommand(LoadCustomers);
            AddCustomerCommand = new RelayCommand(AddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(EditCustomer);
            DeleteCustomerCommand = new RelayCommand<Customer>(DeleteCustomer);
            RefreshCommand = new RelayCommand(LoadCustomers);
            SearchCommand = new RelayCommand(SearchCustomers);
            SaveCustomerCommand = new RelayCommand(async _ => await SaveCustomerAsync());
            CancelEditCommand = new RelayCommand(_ => CloseEditor());
            
            LoadCustomers();
        }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetProperty(ref _selectedCustomer, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        public bool IsEditorOpen
        {
            get => _isEditorOpen;
            set => SetProperty(ref _isEditorOpen, value);
        }

        public string EditorTitle
        {
            get => _editorTitle;
            set => SetProperty(ref _editorTitle, value);
        }

        public Customer EditingCustomer
        {
            get => _editingCustomer;
            set => SetProperty(ref _editingCustomer, value);
        }

        public ICommand LoadCustomersCommand { get; }
        public ICommand AddCustomerCommand { get; }
        public ICommand EditCustomerCommand { get; }
        public ICommand DeleteCustomerCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand SaveCustomerCommand { get; }
        public ICommand CancelEditCommand { get; }

        private void LoadCustomers()
        {
            try
            {
                var customers = _customerService.GetAll().ToList();
                Customers = new ObservableCollection<Customer>(customers);
                StatusText = $"Total customers: {customers.Count}";
            }
            catch (Exception ex)
            {
                StatusText = $"Error loading customers: {ex.Message}";
            }
        }

        private void AddCustomer()
        {
            EditingCustomer = new Customer
            {
                CustomerStatus = 1,
                CustomerBirthday = DateTime.Now
            };
            EditorTitle = "Add Customer";
            IsEditorOpen = true;
        }

        private void EditCustomer(Customer? customer)
        {
            if (customer == null) return;
            EditingCustomer = new Customer
            {
                CustomerID = customer.CustomerID,
                CustomerFullName = customer.CustomerFullName,
                EmailAddress = customer.EmailAddress,
                Telephone = customer.Telephone,
                CustomerBirthday = customer.CustomerBirthday,
                Password = customer.Password,
                CustomerStatus = customer.CustomerStatus
            };
            EditorTitle = "Edit Customer";
            IsEditorOpen = true;
        }

        private async void DeleteCustomer(Customer? customer)
        {
            if (customer == null) return;

            var result = System.Windows.MessageBox.Show(
                $"Are you sure you want to delete customer {customer.CustomerFullName}?",
                "Confirm Delete",
                System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Question);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                try
                {
                    bool success = await _customerService.DeleteAsync(customer.CustomerID);
                    if (success)
                    {
                        LoadCustomers();
                        StatusText = "Customer deleted successfully.";
                    }
                    else
                    {
                        StatusText = "Failed to delete customer.";
                    }
                }
                catch (Exception ex)
                {
                    StatusText = $"Error deleting customer: {ex.Message}";
                }
            }
        }

        public void SearchCustomers()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    LoadCustomers();
                }
                else
                {
                    var filteredCustomers = _customerService.Search(SearchText).ToList();
                    Customers = new ObservableCollection<Customer>(filteredCustomers);
                    StatusText = $"Found {filteredCustomers.Count} customers";
                }
            }
            catch (Exception ex)
            {
                StatusText = $"Error searching customers: {ex.Message}";
            }
        }

        private async Task SaveCustomerAsync()
        {
            try
            {
                bool success;
                if (EditingCustomer.CustomerID == 0)
                {
                    success = await _customerService.AddAsync(EditingCustomer);
                }
                else
                {
                    success = await _customerService.UpdateAsync(EditingCustomer);
                }

                if (success)
                {
                    LoadCustomers();
                    CloseEditor();
                    StatusText = "Customer saved successfully.";
                }
                else
                {
                    StatusText = "Failed to save customer.";
                }
            }
            catch (Exception ex)
            {
                StatusText = $"Error saving customer: {ex.Message}";
            }
        }

        private void CloseEditor()
        {
            IsEditorOpen = false;
        }
    }
}
