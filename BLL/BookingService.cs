using DAL;
using Models;

namespace BLL
{
    public class BookingService
    {
        private readonly DatabaseService db = DatabaseService.Instance;

        public IEnumerable<BookingReservation> GetAll() => db.BookingReservations;

        public BookingReservation? GetById(int id) => db.BookingReservations.FirstOrDefault(b => b.BookingReservationID == id);

        public async Task<bool> AddAsync(BookingReservation booking)
        {
            try
            {
                // Generate new BookingReservationID
                booking.BookingReservationID = db.Database.GetNextBookingId();
                
                db.Database.Bookings.Add(booking);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(BookingReservation booking)
        {
            try
            {
                var existing = db.Database.Bookings.FirstOrDefault(b => b.BookingReservationID == booking.BookingReservationID);
                if (existing == null) return false;
                
                existing.BookingDate = booking.BookingDate;
                existing.TotalPrice = booking.TotalPrice;
                existing.CustomerID = booking.CustomerID;
                existing.BookingStatus = booking.BookingStatus;
                
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var booking = db.Database.Bookings.FirstOrDefault(b => b.BookingReservationID == id);
                if (booking == null) return false;
                
                db.Database.Bookings.Remove(booking);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddBookingDetailAsync(BookingDetail detail)
        {
            try
            {
                db.Database.BookingDetails.Add(detail);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteBookingDetailAsync(int bookingReservationId, int roomId)
        {
            try
            {
                var detail = db.Database.BookingDetails.FirstOrDefault(bd => 
                    bd.BookingReservationID == bookingReservationId && bd.RoomID == roomId);
                if (detail == null) return false;
                
                db.Database.BookingDetails.Remove(detail);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
