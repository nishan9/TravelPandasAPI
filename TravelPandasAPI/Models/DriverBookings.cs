using System;
using TravelPandasAPI.Data.Tables;

namespace TravelPandasAPI.Models
{
	public class DriverBookings
	{
		public User driver { get; set;  }

		public List<string> fullyBooked { get; set; }
		
	}
}

