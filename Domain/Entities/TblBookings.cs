using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    
    public class TblBookings
    {
        [Key]
        public string AircraftCode { get; set; }
        public string UserName { get; set; }
        public string SeatNo { get; set; }
       
    }
}
