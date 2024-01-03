using HotelManager.Data;
using HotelManager.Model;

namespace HotelManager.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly HotelContext _hotelContext;

        public UserRepository(HotelContext hotelContext) 
        { 
            _hotelContext = hotelContext;
        }

        public void Add(User user)
        {
            IsExist(user.Email);
            _hotelContext.Users.Add(user);
            _hotelContext.SaveChanges();
        }

        public void AddRangeUnique(IList<User> users)
        {
            var areUsersExist = users.ToList().All(u => IsExist(u.Email));
            if (!areUsersExist)
            {
                _hotelContext.Users.AddRange(users);
                _hotelContext.SaveChanges();
            }
        }

        public void Create(User user)
        {
            _hotelContext.Users.Add(user);
            _hotelContext.SaveChanges();
        }

        public IList<User> GetAll()
        {
            return _hotelContext.Users.ToList();
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            var foundUser = _hotelContext.Users.ToList().Find(user => user.Email == email && password == user.Password);
            if (foundUser == null) 
            {
                throw new ArgumentException("Użytkownik o podanym emailu i haśle nie istnieje");
            }
            return foundUser;
        }

        public User GetById(int id)
        {
            var foundUser = _hotelContext.Users.ToList().Find(user => user.Id == id);
            if (foundUser == null) 
            {
                throw new ArgumentException($"Użytkownik o id {id} nie istnieje");
            }
            return foundUser;
        }

        public void Update(User newUser)
        {
            var foundUser = GetById(newUser.Id);
            foundUser.Email = newUser.Email;
            foundUser.Password = newUser.Password;  
            foundUser.isAdmin = newUser.isAdmin;
            foundUser.Balance = newUser.Balance;
            _hotelContext.SaveChanges();
        }

        private bool IsExist(string email)
        {
           var user = _hotelContext.Users?.ToList().Find(user => user.Email == email);
           if (user != null)
           {
                return true;
           }
            return false;
        }
    }
}
