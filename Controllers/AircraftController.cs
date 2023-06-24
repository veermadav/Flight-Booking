using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using User_Management.Helpers;

namespace User_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private readonly AuthenticationContext _authContext;
        private readonly IConfiguration _configuration;
       

        public AircraftController(AuthenticationContext authenticationContext, IConfiguration configuration)
        {
            _authContext = authenticationContext;
            _configuration = configuration;
          
        }
        [HttpPost("add_Aircraft")]
        public IActionResult AddAirCraft([FromBody] TblAircrafts userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            else
            {
                var user = _authContext.tblAircrafts.FirstOrDefault(x => x.AircraftCode == userObj.AircraftCode);
                if (user == null)
                {
                    
                    
                    _authContext.tblAircrafts.Add(userObj);
                    _authContext.SaveChanges();
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "AirCraft Added Successfully"
                    });
                }
                else
                {
                    return BadRequest(new { Message = "AirCraft already Exist!" });
                }
            }
        }

        [HttpPut("update_Aircraft")]
        public IActionResult UpdateAircraft([FromBody] TblAircrafts userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }

            var user = _authContext.tblAircrafts.FirstOrDefault(x => x.AircraftCode == userObj.AircraftCode);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Aircraft Not Found"
                });
            }
            else
            {
                // Update specific properties of the user object
                user.From = userObj.From;
                user.To = userObj.To;
                user.ArrivalTime = userObj.ArrivalTime;
                user.DepartureTime = userObj.DepartureTime;
                _authContext.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "AirCraftData Updated Successfully"
                });
            }
        }

            [HttpDelete("delete_AirCraftdata/{aircraftCode}")]
        public IActionResult DeleteAircraft(string aircraftCode)
        {
            var user = _authContext.tblAircrafts.Find(aircraftCode);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Aircraft Not Found"
                });
            }
            else
            {
                _authContext.Remove(user);
                _authContext.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "AircraftData Deleted"
                });
            }
        }

        [HttpGet("get_Aircraft_by_AircraftCode")]
        public IActionResult GetAirCraftByAirCraftCode(string aircraftCode)
        {
            var users = _authContext.tblAircrafts.AsNoTracking().FirstOrDefault(x => x.AircraftCode == aircraftCode);
            return Ok(new
            {
                StatusCode = 200,
                AircraftData = users
            });
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<TblAircrafts>> GetAlltblJobData()
        {
            return Ok(await _authContext.tblAircrafts.ToListAsync());
        }
      

    }
}

    