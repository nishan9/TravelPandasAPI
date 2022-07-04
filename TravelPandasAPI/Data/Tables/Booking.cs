using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TravelPandasAPI.Data.Tables
{
	public class Booking
	{
		[Key]
		public int Id { get; set; }

		[JsonProperty("date")]
		public string Date { get; set;  }

		[JsonProperty("driver_id")]
		public string Driver_Id { get; set; }

		[JsonProperty("inboundPassengers")]
		public List<User> InboundPassengers { get; set; }

		[JsonProperty("outboundPassengers")]
		public List<User> OutboundPassengers { get; set; }
	}
}

