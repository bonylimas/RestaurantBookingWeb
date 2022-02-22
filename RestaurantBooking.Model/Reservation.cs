using RestaurantBooking.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBooking.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Table Number")]
        public int TableNumber { get; set; } = 0; // This will be automatically assigned
        [Required]
        [Display(Name = "Guest Name")]
        public string GuestName { get; set; }
        [Required]
        [Display(Name = "Guest Phone")]
        public string GuestPhone { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Reservation Date/Time")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]

        public DateTime ReservationTime { get; set; } = DateTime.Now;
        [Required]
        [Range(30, 360)]
        public int DurationMinute { get; set; } = 60; // business Requirement
       


    }
}
