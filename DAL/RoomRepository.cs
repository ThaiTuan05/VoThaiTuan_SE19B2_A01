using Models;

namespace DAL
{
    public class RoomRepository : Repository<RoomInformation>, IRoomRepository
    {
        public RoomRepository(InMemoryDatabase database) : base(database)
        {
        }

        public IEnumerable<RoomInformation> GetActiveRooms()
        {
            var rooms = _dbSet.Where(r => r.RoomStatus == 1).ToList();
            var roomTypes = _database.RoomTypes.ToList();
            
            return rooms.Select(r => new RoomInformation
            {
                RoomID = r.RoomID,
                RoomNumber = r.RoomNumber,
                RoomDetailDescription = r.RoomDetailDescription,
                RoomMaxCapacity = r.RoomMaxCapacity,
                RoomPricePerDay = r.RoomPricePerDay,
                RoomTypeID = r.RoomTypeID,
                RoomStatus = r.RoomStatus,
                RoomType = roomTypes.FirstOrDefault(rt => rt.RoomTypeID == r.RoomTypeID)
            }).ToList();
        }

        public IEnumerable<RoomInformation> GetRoomsByType(int roomTypeId)
        {
            var rooms = _dbSet.Where(r => r.RoomTypeID == roomTypeId && r.RoomStatus == 1).ToList();
            var roomTypes = _database.RoomTypes.ToList();
            
            return rooms.Select(r => new RoomInformation
            {
                RoomID = r.RoomID,
                RoomNumber = r.RoomNumber,
                RoomDetailDescription = r.RoomDetailDescription,
                RoomMaxCapacity = r.RoomMaxCapacity,
                RoomPricePerDay = r.RoomPricePerDay,
                RoomTypeID = r.RoomTypeID,
                RoomStatus = r.RoomStatus,
                RoomType = roomTypes.FirstOrDefault(rt => rt.RoomTypeID == r.RoomTypeID)
            }).ToList();
        }

        public async Task<IEnumerable<RoomInformation>> GetActiveRoomsAsync()
        {
            await Task.CompletedTask;
            return GetActiveRooms();
        }
    }
}
