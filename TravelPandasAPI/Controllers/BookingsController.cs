using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelPandasAPI.Data;
using TravelPandasAPI.Data.Tables;
using Microsoft.EntityFrameworkCore;
using TravelPandasAPI.Models;
using System.Security.Claims;
using Newtonsoft.Json;

namespace TravelPandasAPI.Controllers
{
    [Route("[controller]")]
    public class BookingsController : Controller
    {
        private DataContext context;

        public BookingsController(DataContext context)
        {
            this.context = context;
        }


        [HttpPost("Inbound")]
        public IActionResult AddInboundBooking([FromBody] NewBooking reqBooking)
        {

            User driver = context.Users.Where(x => x.Auth0_Id == reqBooking.Driver_Id).FirstOrDefault();
            User passenger = context.Users.Where(x => x.Auth0_Id == reqBooking.Passenger_Id).FirstOrDefault();

            Booking booking = context.Bookings.Include(x => x.InboundPassengers).Include(x => x.OutboundPassengers).Where(x => x.Driver_Id == reqBooking.Driver_Id).Where(x => x.Date == reqBooking.date).FirstOrDefault();

            if (booking == null)
            {
                List<User> newpassengers = new List<User>();
                newpassengers.Add(passenger);
                Booking newbooking = new Booking
                {
                    Date = reqBooking.date,
                    Driver_Id = reqBooking.Driver_Id,
                    InboundPassengers = newpassengers,
                    OutboundPassengers = new List<User>()
                };
                context.Bookings.Add(newbooking);
                context.SaveChanges();
                return new OkObjectResult(newbooking.InboundPassengers);
            }
            else
            {
                booking.InboundPassengers.Add(passenger);
            }

            context.SaveChanges();
            return new OkObjectResult(booking.InboundPassengers);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            List<AddressRadius> results = new List<AddressRadius>(); 

            foreach (User user in context.Users)
            {
                results.Add(new AddressRadius { Address = user.Address, Radius = user.Radius});
            }
            return new OkObjectResult(results); 
        }

    }
}

