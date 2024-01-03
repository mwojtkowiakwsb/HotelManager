using HotelManager.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Data
{
    public class HotelContext : DbContext
    {
        public DbSet<RoomInfo> Rooms { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }
}
