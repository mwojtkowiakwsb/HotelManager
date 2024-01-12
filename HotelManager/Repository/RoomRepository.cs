using HotelManager.Data;
using HotelManager.Enums;
using HotelManager.Model;
using HotelManager.Rooms;

namespace HotelManager.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelContext _hotelContext;

        public RoomRepository(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }

        public void Add(RoomInfo entity)
        {
            _hotelContext.Rooms.Add(entity);
            _hotelContext.SaveChanges();
        }

        public void AddRange(IList<RoomInfo> rooms)
        {
            _hotelContext.Rooms.AddRange(rooms);
            _hotelContext.SaveChanges();
        }

        public void AddRangeUnique(IList<RoomInfo> rooms)
        {
            var areRoomsExist = IsRangeExist(rooms);
            if (!areRoomsExist)
            {
                AddRange(rooms);
            }
        }

        public void UpdateBasedOnList(IList<IRoom> roomsListWithNewProperties)
        {
            roomsListWithNewProperties.ToList().ForEach(newRoom =>
            {
                var roomWithId = GetRoomById(newRoom.RoomId);
                roomWithId.IsAvailable = newRoom.IsAvailable;
                roomWithId.StartDate = newRoom.StartDate;
                roomWithId.EndDate = newRoom.EndDate;
            });
            _hotelContext.SaveChanges();
        }

        public IList<RoomInfo> GetAll()
        {
            return _hotelContext.Rooms.ToList();
        }

        public IList<RoomInfo> GetRoomsWithType(RoomType roomType)
        {
            return _hotelContext.Rooms.ToList().Where(room => room.Type == roomType.ToString()).ToList();
        }

        public void Remove(int id)
        {
            var singleRoom = _hotelContext.Rooms.ToList().Find(room => room.Id == id);
            if (singleRoom != null)
            {
                _hotelContext.Remove(singleRoom);
                _hotelContext.SaveChanges();
            }
        }

        public RoomInfo GetRoomById(int id)
        {
            var foundRoom = _hotelContext.Rooms.ToList().Find(oldRoom => oldRoom.Id == id);
            if (foundRoom != null)
            {
                return foundRoom;
            }
            throw new ArgumentException($"Pokój o ID {id} nie istnieje");
        }

        private bool IsRangeExist(IList<RoomInfo> rooms)
        {
            return rooms.ToList().All(r => IsExist(r.Description));
        }

        private bool IsExist(string description)
        {
            var foundRoom = _hotelContext.Rooms.ToList().Find(oldRoom => oldRoom.Description == description);
            if (foundRoom != null )
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
