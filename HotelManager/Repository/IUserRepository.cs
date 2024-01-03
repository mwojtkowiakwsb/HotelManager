using HotelManager.Model;

namespace HotelManager.Repository
{
    public interface IUserRepository
    {
        public void AddRangeUnique(IList<User> users);
        public void Add(User user);
        public User GetByEmailAndPassword(string email, string password);
        public void Create(User user);
        public IList<User> GetAll();
        public User GetById(int id);    
        public void Update(User user);
    }
}
