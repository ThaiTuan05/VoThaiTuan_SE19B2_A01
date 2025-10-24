using Models;

namespace DAL
{
    public class DatabaseService
    {
        private static DatabaseService? _instance;
        private static readonly object _lock = new object();

        public InMemoryDatabase Database { get; private set; }
        public UnitOfWork UnitOfWork { get; private set; }

        private DatabaseService()
        {
            Database = InMemoryDatabase.Instance;
            UnitOfWork = new UnitOfWork(Database);
        }

        public static DatabaseService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DatabaseService();
                        }
                    }
                }
                return _instance;
            }
        }
        public IEnumerable<Customer> Customers => Database.Customers;
        public IEnumerable<RoomType> RoomTypes => Database.RoomTypes;
        public IEnumerable<RoomInformation> Rooms => Database.Rooms;
        public IEnumerable<BookingReservation> Bookings => Database.Bookings;
        public IEnumerable<BookingDetail> BookingDetails => Database.BookingDetails;
        public IEnumerable<RoomInformation> RoomInformations => Database.Rooms;
        public IEnumerable<BookingReservation> BookingReservations => Database.Bookings;

        public async Task<int> SaveChangesAsync()
        {
            await Task.CompletedTask;
            return 1; 
        }

        public int SaveChanges()
        {
            return 1; 
        }
    }
}

