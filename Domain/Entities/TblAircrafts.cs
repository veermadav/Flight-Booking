
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TblAircrafts
    {
        [Key]
        public string AircraftCode { get; set; }
        public string FlightNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        
    }
}
