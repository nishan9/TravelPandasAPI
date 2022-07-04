using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TravelPandasAPI.Data.Tables
{
	public class User
	{

		[Key]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("career_stage")]
		public string Career_Stage { get; set; }

		[JsonProperty("auth0_id")]
		public string? Auth0_Id { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }

		[JsonProperty("isDriver")]
		public bool IsDriver { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("radius")]
		public string Radius { get; set; }

		[JsonProperty("days")]
		public string Days { get; set; }

		[JsonProperty("outboundTime")]
		public string OutboundTime { get; set; }

		[JsonProperty("inboundTime")]
		public string InboundTime { get; set; }

		[JsonProperty("capacity")]
		public int Capacity { get; set; }
	}
}

