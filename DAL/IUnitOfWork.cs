using Models;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IRoomRepository Rooms { get; }
        IRoomTypeRepository RoomTypes { get; }
        IRepository<BookingReservation> Bookings { get; }
        IRepository<BookingDetail> BookingDetails { get; }

        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
