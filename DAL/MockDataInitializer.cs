using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public static class MockDataInitializer
    {
        private static int _nextCustomerId = 1;
        private static int _nextRoomTypeId = 1;
        private static int _nextRoomId = 1;
        private static int _nextBookingId = 1;

        public static List<Customer> InitializeCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    CustomerID = _nextCustomerId++,
                    CustomerFullName = "An Duong",
                    Telephone = "0901122334",
                    EmailAddress = "an.duong@example.org",
                    CustomerBirthday = new DateTime(1991, 2, 10),
                    CustomerStatus = 1,
                    Password = "MockPass1"
                },
                new Customer
                {
                    CustomerID = _nextCustomerId++,
                    CustomerFullName = "Bich Lan",
                    Telephone = "0912233445",
                    EmailAddress = "bich.lan@example.org",
                    CustomerBirthday = new DateTime(1987, 11, 5),
                    CustomerStatus = 1,
                    Password = "MockPass2"
                },
                new Customer
                {
                    CustomerID = _nextCustomerId++,
                    CustomerFullName = "Cuong Minh",
                    Telephone = "0923344556",
                    EmailAddress = "cuong.minh@example.org",
                    CustomerBirthday = new DateTime(1994, 6, 20),
                    CustomerStatus = 1,
                    Password = "MockPass3"
                },
                new Customer
                {
                    CustomerID = _nextCustomerId++,
                    CustomerFullName = "Dao Phuong",
                    Telephone = "0934455667",
                    EmailAddress = "dao.phuong@example.org",
                    CustomerBirthday = new DateTime(1984, 9, 12),
                    CustomerStatus = 1,
                    Password = "MockPass4"
                },
                new Customer
                {
                    CustomerID = _nextCustomerId++,
                    CustomerFullName = "Khanh Vy",
                    Telephone = "0945566778",
                    EmailAddress = "khanh.vy@example.org",
                    CustomerBirthday = new DateTime(1998, 4, 2),
                    CustomerStatus = 1,
                    Password = "MockPass5"
                }
            };
        }

        public static List<RoomType> InitializeRoomTypes()
        {
            return new List<RoomType>
            {
                new RoomType
                {
                    RoomTypeID = _nextRoomTypeId++,
                    RoomTypeName = "Classic",
                    TypeDescription = "Classic room with essential amenities",
                    TypeNote = "Entry level"
                },
                new RoomType
                {
                    RoomTypeID = _nextRoomTypeId++,
                    RoomTypeName = "Comfort",
                    TypeDescription = "Comfort room with added space",
                    TypeNote = "Popular choice"
                },
                new RoomType
                {
                    RoomTypeID = _nextRoomTypeId++,
                    RoomTypeName = "Premium",
                    TypeDescription = "Premium room with upgraded amenities",
                    TypeNote = "Higher comfort"
                },
                new RoomType
                {
                    RoomTypeID = _nextRoomTypeId++,
                    RoomTypeName = "FamilyPlus",
                    TypeDescription = "Spacious family room",
                    TypeNote = "Families"
                },
                new RoomType
                {
                    RoomTypeID = _nextRoomTypeId++,
                    RoomTypeName = "Business",
                    TypeDescription = "Business-focused room with workspace",
                    TypeNote = "Work travelers"
                }
            };
        }

        public static List<RoomInformation> InitializeRooms(List<RoomType> roomTypes)
        {
            var rooms = new List<RoomInformation>();
            var random = new Random();

            // Classic rooms
            for (int i = 1; i <= 10; i++)
            {
                rooms.Add(new RoomInformation
                {
                    RoomID = _nextRoomId++,
                    RoomNumber = $"C{i:D3}",
                    RoomDetailDescription = $"Classic room {i} with essential amenities",
                    RoomMaxCapacity = 2,
                    RoomTypeID = roomTypes.First(rt => rt.RoomTypeName == "Classic").RoomTypeID,
                    RoomStatus = 1,
                    RoomPricePerDay = 450000 + random.Next(0, 80000)
                });
            }

            // Comfort rooms
            for (int i = 1; i <= 8; i++)
            {
                rooms.Add(new RoomInformation
                {
                    RoomID = _nextRoomId++,
                    RoomNumber = $"CO{i:D3}",
                    RoomDetailDescription = $"Comfort room {i} with extra space",
                    RoomMaxCapacity = 3,
                    RoomTypeID = roomTypes.First(rt => rt.RoomTypeName == "Comfort").RoomTypeID,
                    RoomStatus = 1,
                    RoomPricePerDay = 700000 + random.Next(0, 150000)
                });
            }

            // Premium rooms
            for (int i = 1; i <= 5; i++)
            {
                rooms.Add(new RoomInformation
                {
                    RoomID = _nextRoomId++,
                    RoomNumber = $"P{i:D3}",
                    RoomDetailDescription = $"Premium room {i} with upgraded amenities",
                    RoomMaxCapacity = 3,
                    RoomTypeID = roomTypes.First(rt => rt.RoomTypeName == "Premium").RoomTypeID,
                    RoomStatus = 1,
                    RoomPricePerDay = 1400000 + random.Next(0, 300000)
                });
            }

            // FamilyPlus rooms
            for (int i = 1; i <= 6; i++)
            {
                rooms.Add(new RoomInformation
                {
                    RoomID = _nextRoomId++,
                    RoomNumber = $"FP{i:D3}",
                    RoomDetailDescription = $"FamilyPlus room {i} for groups",
                    RoomMaxCapacity = 5,
                    RoomTypeID = roomTypes.First(rt => rt.RoomTypeName == "FamilyPlus").RoomTypeID,
                    RoomStatus = 1,
                    RoomPricePerDay = 950000 + random.Next(0, 220000)
                });
            }

            // Business rooms
            for (int i = 1; i <= 4; i++)
            {
                rooms.Add(new RoomInformation
                {
                    RoomID = _nextRoomId++,
                    RoomNumber = $"B{i:D3}",
                    RoomDetailDescription = $"Business room {i} with workspace",
                    RoomMaxCapacity = 2,
                    RoomTypeID = roomTypes.First(rt => rt.RoomTypeName == "Business").RoomTypeID,
                    RoomStatus = 1,
                    RoomPricePerDay = 1100000 + random.Next(0, 250000)
                });
            }

            return rooms;
        }

        public static List<BookingReservation> InitializeBookings(List<Customer> customers, List<RoomInformation> rooms)
        {
            var bookings = new List<BookingReservation>();
            var random = new Random();

            for (int i = 0; i < 5; i++)
            {
                var customer = customers[random.Next(customers.Count)];
                var room = rooms[random.Next(rooms.Count)];
                var startDate = DateTime.Now.AddDays(random.Next(-20, 20));
                var nights = random.Next(1, 6);
                var endDate = startDate.AddDays(nights);

                var booking = new BookingReservation
                {
                    BookingReservationID = _nextBookingId++,
                    BookingDate = DateTime.Now.AddDays(random.Next(-40, -1)),
                    TotalPrice = room.RoomPricePerDay * (decimal)nights,
                    CustomerID = customer.CustomerID,
                    BookingStatus = 1
                };

                bookings.Add(booking);
            }

            return bookings;
        }

        public static List<BookingDetail> InitializeBookingDetails(List<BookingReservation> bookings, List<RoomInformation> rooms)
        {
            var bookingDetails = new List<BookingDetail>();
            var random = new Random();

            foreach (var booking in bookings)
            {
                var room = rooms[random.Next(rooms.Count)];
                var startDate = DateTime.Now.AddDays(random.Next(-20, 20));
                var nights = random.Next(1, 6);
                var endDate = startDate.AddDays(nights);

                bookingDetails.Add(new BookingDetail
                {
                    BookingReservationID = booking.BookingReservationID,
                    RoomID = room.RoomID,
                    StartDate = startDate,
                    EndDate = endDate,
                    ActualPrice = room.RoomPricePerDay * (decimal)nights
                });
            }

            return bookingDetails;
        }

        public static void ResetIds()
        {
            _nextCustomerId = 1;
            _nextRoomTypeId = 1;
            _nextRoomId = 1;
            _nextBookingId = 1;
        }
    }
}
