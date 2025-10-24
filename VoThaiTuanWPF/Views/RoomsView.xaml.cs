using System.Windows.Controls;
using VoThaiTuanWPF.ViewModels;

namespace VoThaiTuanWPF.Views
{
    public partial class RoomsView : UserControl
    {
        public RoomsView()
        {
            InitializeComponent();
            Loaded += RoomsView_Loaded;
        }

        private void RoomsView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = DataContext as RoomsViewModel;
            if (vm == null) return;
            
            // Load room types when view loads
            vm.LoadRoomTypes();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = DataContext as RoomsViewModel;
            vm?.SearchRooms();
        }
    }
}


