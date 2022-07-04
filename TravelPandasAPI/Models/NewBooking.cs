using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TravelPandasAPI.Models
{
	public class NewBooking
	{
		[JsonProperty("driver_id")]
		public string Driver_Id { get; set; }

		[JsonProperty("date")]
		public string date { get; set; }

		[JsonProperty("passenger_id")]
		public string Passenger_Id { get; set; }
	}
}

