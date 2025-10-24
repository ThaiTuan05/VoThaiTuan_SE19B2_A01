using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using VoThaiTuanWPF.Commands; // Gi? nguy�n namespace Commands c?a b?n
using VoThaiTuanWPF.Views;

namespace VoThaiTuanWPF.ViewModels
{
    // Gi? s? ViewModelBase c?a b?n d� implement INotifyPropertyChanged
    public class MainWindowViewModel : ViewModelBase
    {
        // === B?T �?U: TH�M C�C THU?C T�NH CHO GIAO DI?N M?I ===

        private bool _isCustomersViewSelected;
        private bool _isRoomsViewSelected;
        private bool _isBookingsViewSelected;
        private bool _isReportViewSelected;

        private string _currentViewTitle = string.Empty;
        private object? _currentView;

        public bool IsCustomersViewSelected
        {
            get => _isCustomersViewSelected;
            set { _isCustomersViewSelected = value; OnPropertyChanged(); }
        }
        public bool IsRoomsViewSelected
        {
            get => _isRoomsViewSelected;
            set { _isRoomsViewSelected = value; OnPropertyChanged(); }
        }
        public bool IsBookingsViewSelected
        {
            get => _isBookingsViewSelected;
            set { _isBookingsViewSelected = value; OnPropertyChanged(); }
        }
        public bool IsReportViewSelected
        {
            get => _isReportViewSelected;
            set { _isReportViewSelected = value; OnPropertyChanged(); }
        }
        public string CurrentViewTitle
        {
            get => _currentViewTitle;
            set { _currentViewTitle = value; OnPropertyChanged(); }
        }
        public object? CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }
        public MainWindowViewModel()
        {
            CustomerButtonCommand = new RelayCommand(_ => SelectView("Customers"));
            RoomButtonCommand = new RelayCommand(_ => SelectView("Rooms"));
            BookingButtonCommand = new RelayCommand(_ => SelectView("Bookings"));
            ReportButtonCommand = new RelayCommand(_ => SelectView("Report"));
            LogoutButtonCommand = new RelayCommand(Logout);
            SelectView("Customers");
        }

        public ICommand CustomerButtonCommand { get; }
        public ICommand RoomButtonCommand { get; }
        public ICommand BookingButtonCommand { get; }
        public ICommand ReportButtonCommand { get; }
        public ICommand LogoutButtonCommand { get; }

        private void Logout()
        {
            OnLogout?.Invoke();
        }

        public event Action? OnLogout;
        private void SelectView(string viewName)
        {
            // C?p nh?t c�c thu?c t�nh bool
            IsCustomersViewSelected = (viewName == "Customers");
            IsRoomsViewSelected = (viewName == "Rooms");
            IsBookingsViewSelected = (viewName == "Bookings");
            IsReportViewSelected = (viewName == "Report");

            // C?p nh?t ti�u d?
            if (viewName == "Customers") CurrentViewTitle = "Customer Management";
            if (viewName == "Rooms") CurrentViewTitle = "Room Management";
            if (viewName == "Bookings") CurrentViewTitle = "Booking Management";
            if (viewName == "Report") CurrentViewTitle = "Statistics Report";

            // Set current view model (inline rendering in MainWindow)
            if (viewName == "Customers") CurrentView = new CustomersViewModel();
            if (viewName == "Rooms") CurrentView = new RoomsViewModel();
            if (viewName == "Bookings") CurrentView = new BookingsView();
            if (viewName == "Report") CurrentView = new ReportView();
        }
    }

    // Simple placeholder VM for not-yet-implemented views
    public class ViewModelPlaceholder : ViewModelBase
    {
        private string _message;
        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public ViewModelPlaceholder(string message)
        {
            _message = message;
        }
    }
}

