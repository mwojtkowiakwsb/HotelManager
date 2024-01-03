

using HotelManager.Enums;
using HotelManager.Model;
using HotelManager.Rooms;

namespace HotelManager.Repository
{
    public interface IRoomRepository
    {
        public void Add(RoomInfo entity);
        public void AddRange(IList<RoomInfo> entity);
        public void AddRangeUnique(IList<RoomInfo> entity);
        public IList<RoomInfo> GetRoomsWithType(RoomType roomType);
        public IList<RoomInfo> GetAll();
        public RoomInfo GetRoomById(int id);
        public void UpdateBasedOnList(IList<IRoom> roomsListWithNewProperties);
        public void Remove(int id);
    }
}
