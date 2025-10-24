using System.Collections.ObjectModel;
using System.Windows.Input;
using BLL;
using Models;
using VoThaiTuanWPF.Commands;

namespace VoThaiTuanWPF.ViewModels
{
    public class RoomsViewModel : ViewModelBase
    {
        private readonly RoomService _roomService;
        private ObservableCollection<RoomInformation> _rooms;
        private ObservableCollection<RoomType> _roomTypes;
        private RoomInformation? _selectedRoom;
        private string _searchText = string.Empty;
        private string _statusText = string.Empty;
        private bool _isEditorOpen;
        private string _editorTitle = string.Empty;
        private RoomInformation _editingRoom = new RoomInformation();

        public RoomsViewModel()
        {
            _roomService = new RoomService();
            _rooms = new ObservableCollection<RoomInformation>();
            _roomTypes = new ObservableCollection<RoomType>();
            
            LoadRoomsCommand = new RelayCommand(LoadRooms);
            AddRoomCommand = new RelayCommand(AddRoom);
            EditRoomCommand = new RelayCommand<RoomInformation>(EditRoom);
            DeleteRoomCommand = new RelayCommand<RoomInformation>(DeleteRoom);
            RefreshCommand = new RelayCommand(LoadRooms);
            SearchCommand = new RelayCommand(SearchRooms);
            SaveRoomCommand = new RelayCommand(async _ => await SaveRoomAsync());
            CancelEditCommand = new RelayCommand(_ => CloseEditor());
            
            LoadRooms();
            LoadRoomTypes();
        }

        public ObservableCollection<RoomInformation> Rooms
        {
            get => _rooms;
            set => SetProperty(ref _rooms, value);
        }

        public ObservableCollection<RoomType> RoomTypes
        {
            get => _roomTypes;
            set => SetProperty(ref _roomTypes, value);
        }

        public RoomInformation? SelectedRoom
        {
            get => _selectedRoom;
            set => SetProperty(ref _selectedRoom, value);
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

        public RoomInformation EditingRoom
        {
            get => _editingRoom;
            set => SetProperty(ref _editingRoom, value);
        }

        public ICommand LoadRoomsCommand { get; }
        public ICommand AddRoomCommand { get; }
        public ICommand EditRoomCommand { get; }
        public ICommand DeleteRoomCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand SaveRoomCommand { get; }
        public ICommand CancelEditCommand { get; }

        private void LoadRooms()
        {
            try
            {
                var rooms = _roomService.GetAll().ToList();
                Rooms = new ObservableCollection<RoomInformation>(rooms);
                StatusText = $"Total rooms: {rooms.Count}";
            }
            catch (Exception ex)
            {
                StatusText = $"Error loading rooms: {ex.Message}";
            }
        }

        public void LoadRoomTypes()
        {
            try
            {
                var roomTypes = _roomService.GetAllRoomTypes().ToList();
                RoomTypes = new ObservableCollection<RoomType>(roomTypes);
            }
            catch (Exception ex)
            {
                StatusText = $"Error loading room types: {ex.Message}";
            }
        }

        private void AddRoom()
        {
            // Ensure room types are loaded before opening editor
            LoadRoomTypes();
            
            EditingRoom = new RoomInformation
            {
                RoomStatus = 1
            };
            EditorTitle = "Add Room";
            IsEditorOpen = true;
        }

        private void EditRoom(RoomInformation? room)
        {
            if (room == null) return;
            
            // Ensure room types are loaded before opening editor
            LoadRoomTypes();
            
            EditingRoom = new RoomInformation
            {
                RoomID = room.RoomID,
                RoomNumber = room.RoomNumber,
                RoomDetailDescription = room.RoomDetailDescription,
                RoomMaxCapacity = room.RoomMaxCapacity,
                RoomPricePerDay = room.RoomPricePerDay,
                RoomTypeID = room.RoomTypeID,
                RoomStatus = room.RoomStatus
            };
            EditorTitle = "Edit Room";
            IsEditorOpen = true;
        }

        private async void DeleteRoom(RoomInformation? room)
        {
            if (room == null) return;

            var result = System.Windows.MessageBox.Show(
                $"Are you sure you want to delete room {room.RoomNumber}?",
                "Confirm Delete",
                System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Question);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                try
                {
                    bool success = await _roomService.DeleteAsync(room.RoomID);
                    if (success)
                    {
                        LoadRooms();
                        StatusText = "Room deleted successfully.";
                    }
                    else
                    {
                        StatusText = "Failed to delete room.";
                    }
                }
                catch (Exception ex)
                {
                    StatusText = $"Error deleting room: {ex.Message}";
                }
            }
        }

        public void SearchRooms()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    LoadRooms();
                }
                else
                {
                    var filteredRooms = _roomService.Search(SearchText).ToList();
                    Rooms = new ObservableCollection<RoomInformation>(filteredRooms);
                    StatusText = $"Found {filteredRooms.Count} rooms";
                }
            }
            catch (Exception ex)
            {
                StatusText = $"Error searching rooms: {ex.Message}";
            }
        }

        private async Task SaveRoomAsync()
        {
            try
            {
                bool success;
                if (EditingRoom.RoomID == 0)
                {
                    success = await _roomService.AddAsync(EditingRoom);
                }
                else
                {
                    success = await _roomService.UpdateAsync(EditingRoom);
                }

                if (success)
                {
                    LoadRooms();
                    CloseEditor();
                    StatusText = "Room saved successfully.";
                }
                else
                {
                    StatusText = "Failed to save room.";
                }
            }
            catch (Exception ex)
            {
                StatusText = $"Error saving room: {ex.Message}";
            }
        }

        private void CloseEditor()
        {
            IsEditorOpen = false;
        }
    }
}
