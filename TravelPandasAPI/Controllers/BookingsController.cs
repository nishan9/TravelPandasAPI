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
            User passenger = context.Users.Where(x => x.Auth0_Id == User.FindFirst(ClaimTypes.NameIdentifier).Value).FirstOrDefault();
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
                List<User> testPassengers = booking.InboundPassengers;
                testPassengers.Add(passenger);
                booking.InboundPassengers = testPassengers; 
            }

            context.SaveChanges();
            return new OkObjectResult(booking.InboundPassengers);
        }

        [HttpPost("Outbound")]
        public IActionResult AddOutboundBooking([FromBody] NewBooking reqBooking)
        {
            User driver = context.Users.Where(x => x.Auth0_Id == reqBooking.Driver_Id).FirstOrDefault();
            User passenger = context.Users.Where(x => x.Auth0_Id == User.FindFirst(ClaimTypes.NameIdentifier).Value).FirstOrDefault();
            Booking booking = context.Bookings.Include(x => x.InboundPassengers).Include(x => x.OutboundPassengers).Where(x => x.Driver_Id == reqBooking.Driver_Id).Where(x => x.Date == reqBooking.date).FirstOrDefault();

            if (booking == null)
            {
                List<User> newpassengers = new List<User>();
                newpassengers.Add(passenger);
                Booking newbooking = new Booking
                {
                    Date = reqBooking.date,
                    Driver_Id = reqBooking.Driver_Id,
                    InboundPassengers = new List<User>(), 
                    OutboundPassengers =  newpassengers
                };
                context.Bookings.Add(newbooking);
                context.SaveChanges();
                return new OkObjectResult(newbooking.OutboundPassengers);
            }
            else
            {
                List<User> testPassengers = booking.OutboundPassengers;
                testPassengers.Add(passenger);
                booking.OutboundPassengers = testPassengers;
            }

            context.SaveChanges();
            return new OkObjectResult(booking.OutboundPassengers);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllAddresses([FromBody] string days)
        {
            List<string> daysBody = SplitString(days);
            List<AddressRadius> results = new List<AddressRadius>();
            foreach (User user in context.Users)
            {

                List<string> dayList = SplitString(user.Days);
                var FilteredList = dayList.Intersect(daysBody, StringComparer.OrdinalIgnoreCase);

                if (FilteredList.Count() >= 1)
                {
                    results.Add(new AddressRadius { Address = user.Address, Radius = user.Radius });
                }

            }
            return new OkObjectResult(results);
        }

        private List<string> SplitString(string days)
        {
            int k = 0;
            double j = 3;
            return days.ToLookup(x => Math.Floor(k++ / j)).Select(y => new String(y.ToArray())).ToList();
        }

        [HttpGet("{address}")]
        public IActionResult GetDriverInfo(string address)
        {   
            User driver = context.Users.Where(x => x.Address == address).FirstOrDefault();
            List<Booking> bookings = context.Bookings.Include(x => x.InboundPassengers).Include(x => x.OutboundPassengers).Where(x => x.Driver_Id == driver.Auth0_Id).ToList();

            List<string> fullyBooked = new List<string>(); 
            foreach (Booking booking in bookings)
            {
                if (booking.InboundPassengers.Count() >= driver.Capacity && booking.OutboundPassengers.Count() >= driver.Capacity)
                {
                    fullyBooked.Add(booking.Date); 
                }
            }

            DriverBookings data;
            if (driver != null)
            {
                data = new DriverBookings
                {
                    fullyBooked = fullyBooked,
                    driver = driver
                };
            }
            else
            {
                data = new DriverBookings(); 
            }
            return new OkObjectResult(data); 
        }

        [HttpPost("GetBookings/{driver}")]
        public IActionResult GetBookings([FromBody] string date, string driver)
        {
            Booking booking = context.Bookings.Include(x => x.InboundPassengers).Include(x => x.OutboundPassengers).Where(x => x.Driver_Id == driver).Where(x => x.Date == date).FirstOrDefault();
            if (booking == null)
            {
                return new NotFoundResult();
            }
            else
            {
                return new OkObjectResult(booking);
            }
        }

    }
}

