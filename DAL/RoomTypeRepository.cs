using Models;

namespace DAL
{
    public class RoomTypeRepository : Repository<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(InMemoryDatabase database) : base(database)
        {
        }
    }
}
