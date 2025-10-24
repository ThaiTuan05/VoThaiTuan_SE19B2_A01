using Models;

namespace DAL
{
    public class InMemoryDatabase
    {
        private static InMemoryDatabase? _instance;
        private static readonly object _lock = new object();
        public List<Customer> Customers { get; private set; }
        public List<RoomType> RoomTypes { get; private set; }
        public List<RoomInformation> Rooms { get; private set; }
        public List<BookingReservation> Bookings { get; private set; }
        public List<BookingDetail> BookingDetails { get; private set; }

        private InMemoryDatabase()
        {
            RoomTypes = MockDataInitializer.InitializeRoomTypes();
            Customers = MockDataInitializer.InitializeCustomers();
            Rooms = MockDataInitializer.InitializeRooms(RoomTypes);
            Bookings = MockDataInitializer.InitializeBookings(Customers, Rooms);
            BookingDetails = MockDataInitializer.InitializeBookingDetails(Bookings, Rooms);
            SetupNavigationProperties();
        }

        public static InMemoryDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new InMemoryDatabase();
                        }
                    }
                }
                return _instance;
            }
        }

        private void SetupNavigationProperties()
        {
            // Setup Customer -> Bookings navigation
            foreach (var customer in Customers)
            {
                customer.BookingReservations = Bookings.Where(b => b.CustomerID == customer.CustomerID).ToList();
            }

            // Setup RoomType -> Rooms navigation
            foreach (var roomType in RoomTypes)
            {
                roomType.RoomInformations = Rooms.Where(r => r.RoomTypeID == roomType.RoomTypeID).ToList();
            }

            // Setup Room -> RoomType navigation
            foreach (var room in Rooms)
            {
                room.RoomType = RoomTypes.FirstOrDefault(rt => rt.RoomTypeID == room.RoomTypeID)!;
            }

            // Setup Booking -> Customer navigation
            foreach (var booking in Bookings)
            {
                booking.Customer = Customers.FirstOrDefault(c => c.CustomerID == booking.CustomerID)!;
            }

            // Setup Booking -> BookingDetails navigation
            foreach (var booking in Bookings)
            {
                booking.BookingDetails = BookingDetails.Where(bd => bd.BookingReservationID == booking.BookingReservationID).ToList();
            }

            // Setup BookingDetail -> Booking navigation
            foreach (var bookingDetail in BookingDetails)
            {
                bookingDetail.BookingReservation = Bookings.FirstOrDefault(b => b.BookingReservationID == bookingDetail.BookingReservationID)!;
            }

            // Setup BookingDetail -> Room navigation
            foreach (var bookingDetail in BookingDetails)
            {
                bookingDetail.RoomInformation = Rooms.FirstOrDefault(r => r.RoomID == bookingDetail.RoomID)!;
            }

            // Setup Room -> BookingDetails navigation
            foreach (var room in Rooms)
            {
                room.BookingDetails = BookingDetails.Where(bd => bd.RoomID == room.RoomID).ToList();
            }
        }

        public void ResetData()
        {
            MockDataInitializer.ResetIds();
            RoomTypes = MockDataInitializer.InitializeRoomTypes();
            Customers = MockDataInitializer.InitializeCustomers();
            Rooms = MockDataInitializer.InitializeRooms(RoomTypes);
            Bookings = MockDataInitializer.InitializeBookings(Customers, Rooms);
            BookingDetails = MockDataInitializer.InitializeBookingDetails(Bookings, Rooms);
            SetupNavigationProperties();
        }

        public int GetNextCustomerId()
        {
            return Customers.Count > 0 ? Customers.Max(c => c.CustomerID) + 1 : 1;
        }

        public int GetNextRoomTypeId()
        {
            return RoomTypes.Count > 0 ? RoomTypes.Max(rt => rt.RoomTypeID) + 1 : 1;
        }

        public int GetNextRoomId()
        {
            return Rooms.Count > 0 ? Rooms.Max(r => r.RoomID) + 1 : 1;
        }

        public int GetNextBookingId()
        {
            return Bookings.Count > 0 ? Bookings.Max(b => b.BookingReservationID) + 1 : 1;
        }
    }
}