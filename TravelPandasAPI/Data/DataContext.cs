using Microsoft.EntityFrameworkCore;
using TravelPandasAPI.Data.Tables;

namespace TravelPandasAPI.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options){ }
		public DbSet<User> Users { get; set; }
		public DbSet<Booking> Bookings { get; set; }
	}
}

