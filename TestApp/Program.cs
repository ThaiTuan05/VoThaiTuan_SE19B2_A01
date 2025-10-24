using DAL;
using BLL;
using Models;

Console.WriteLine("Testing In-Memory Database Functionality...");

// Test Customer Service
var customerService = new CustomerService();
var customers = customerService.GetAll();
Console.WriteLine($"Found {customers.Count()} customers");

// Test Room Service
var roomService = new RoomService();
var rooms = roomService.GetAll();
Console.WriteLine($"Found {rooms.Count()} rooms");

var roomTypes = roomService.GetAllRoomTypes();
Console.WriteLine($"Found {roomTypes.Count()} room types");

// Test Auth Service
var authService = new AuthService();
var admin = authService.Authenticate("admin@FUMiniHotelSystem.com", "@@abc123@@");
Console.WriteLine($"Admin login: {(admin != null ? "Success" : "Failed")}");

// Test Booking Service
var bookingService = new BookingService();
var bookings = bookingService.GetAll();
Console.WriteLine($"Found {bookings.Count()} bookings");

// Test Report Service
var reportService = new ReportService();
var summary = reportService.GetSummaryReport(DateTime.Now.AddDays(-30), DateTime.Now);
Console.WriteLine($"Summary Report - Total Revenue: {summary.TotalRevenue:C}, Total Bookings: {summary.TotalBookings}");

Console.WriteLine("All tests completed successfully!");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
