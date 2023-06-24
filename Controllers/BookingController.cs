using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System;

namespace User_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly AuthenticationContext _authContext;
        private readonly IConfiguration _configuration;
        private static readonly Random random = new Random();

        public BookingController(AuthenticationContext authenticationContext, IConfiguration configuration)
        {
            _authContext = authenticationContext;
            _configuration = configuration;

        }
        [HttpPost("add_BookingData")]
        public IActionResult AddBookingData([FromBody] TblBookings userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            else
            {
                var user = _authContext.tblBookings.FirstOrDefault(x => x.AircraftCode == userObj.AircraftCode);

                if (user != null && user.SeatNo == userObj.SeatNo)
                {
                    return BadRequest(new { Message = "Already Booked!" });
                }

                _authContext.tblBookings.Add(userObj);
                _authContext.SaveChanges();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Your Booking Added Successfully"
                });

            }
        }
        [HttpGet("get_booking_by_UserName")]
        public IActionResult GetBookingByUserName(string userName)
        {
            var bookings = _authContext.tblBookings.AsNoTracking().Where(x => x.UserName == userName).ToList();
            var aircraftCodes = bookings.Select(x => x.AircraftCode).ToList();
            var aircraft = _authContext.tblAircrafts.AsNoTracking().Where(x => aircraftCodes.Contains(x.AircraftCode)).ToList();

            var combinedData = from booking in bookings
                               join a in aircraft on booking.AircraftCode equals a.AircraftCode into joinedData
                               from a in joinedData.DefaultIfEmpty()
                               select new
                               {
                                   booking.AircraftCode,
                                   booking.UserName,
                                   booking.SeatNo,
                                   From = a?.From,
                                   To = a?.To,
                                   ArrivalTime = a?.ArrivalTime,
                                   DepartureTime = a?.DepartureTime,
                                   FlightNumber = a?.FlightNumber
                               };

            return Ok(new
            {
                StatusCode = 200,
                BookingData = combinedData
            });
        }

        [HttpGet("get_booking_by_aircraftCode")]
        public IActionResult GetBookingByAircraftcode(string aircraftCode)
        {
            var users = _authContext.tblBookings.AsNoTracking().Where(x => x.AircraftCode == aircraftCode).ToList();
            return Ok(new
            {
                StatusCode = 200,
                BookingData = users
            });
        }

        //[HttpDelete("delete_MyBookingdata")]
        //public IActionResult DeleteBooking([FromBody] TblBookings userObj)
        //{
        //    var user = _authContext.tblBookings.FirstOrDefault(x => x.AircraftCode == userObj.AircraftCode);

        //    if (user != null && user.SeatNo == userObj.SeatNo)
        //    {
        //        _authContext.Remove(user);
        //        _authContext.SaveChanges();
        //        return Ok(new
        //        {
        //            StatusCode = 200,
        //            Message = "Your Booking Cancelled!"
        //        });
        //    }

        //    else
        //    {
        //        return BadRequest(new { Message = "Booking Not Found!" });
        //    }
        //}

        [HttpGet("get_all_Bookings")]
        public IActionResult GetBookingDetails()
        {
            var bookingDetails = from booking in _authContext.tblBookings.AsNoTracking()
                                 join aircraft in _authContext.tblAircrafts.AsNoTracking() on booking.AircraftCode equals aircraft.AircraftCode
                                 select new
                                 {
                                     booking.AircraftCode,
                                     booking.UserName,
                                     booking.SeatNo,
                                     aircraft.From,
                                     aircraft.To,
                                     aircraft.ArrivalTime,
                                     aircraft.DepartureTime,
                                     aircraft.FlightNumber
                                 };

            return Ok(new
            {
                StatusCode = 200,
                BookingData = bookingDetails
            });
        }
        [HttpDelete("delete_MyBookingdata")]
        public IActionResult DeleteBooking(BookingModel bookingObj)
        {
            try
            {
                var booking = _authContext.tblBookings.FirstOrDefault(x =>
                    x.AircraftCode == bookingObj.AircraftCode &&
                    x.UserName == bookingObj.UserName &&
                    x.SeatNo == bookingObj.SeatNo
                );

                if (booking != null)
                {
                    _authContext.Remove(booking);
                    _authContext.SaveChanges();
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Booking deleted!"
                    });
                }
                else
                {
                    return BadRequest(new { Message = "Booking not found!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the booking." });
            }
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<TblAircrafts>> GetAlltblJobData()
        {
            return Ok(await _authContext.tblAircrafts.ToListAsync());
        }
    }
}
