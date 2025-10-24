using Models;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InMemoryDatabase _database;
        private ICustomerRepository? _customers;
        private IRoomRepository? _rooms;
        private IRoomTypeRepository? _roomTypes;
        private IRepository<BookingReservation>? _bookings;
        private IRepository<BookingDetail>? _bookingDetails;

        public UnitOfWork(InMemoryDatabase database)
        {
            _database = database;
        }

        public ICustomerRepository Customers => 
            _customers ??= new CustomerRepository(_database);

        public IRoomRepository Rooms => 
            _rooms ??= new RoomRepository(_database);

        public IRoomTypeRepository RoomTypes => 
            _roomTypes ??= new RoomTypeRepository(_database);

        public IRepository<BookingReservation> Bookings => 
            _bookings ??= new Repository<BookingReservation>(_database);

        public IRepository<BookingDetail> BookingDetails => 
            _bookingDetails ??= new Repository<BookingDetail>(_database);

        public async Task<int> SaveChangesAsync()
        {
            await Task.CompletedTask;
            return 1; // Simulate successful save
        }

        public int SaveChanges()
        {
            return 1; // Simulate successful save
        }

        public void Dispose()
        {
            // No-op for in-memory database
        }
    }
}
