using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBooking.Models.ViewModel
{
    public class SearchReservationVM
    {
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public bool SearchDate { get; set; }
        [Display(Name = "Reservation Date")]
        [DataType(DataType.Date)]
        public DateTime DateFilter { get; set; } = DateTime.Now;

        public IEnumerable<Reservation> SearchResult { get; set; }
    }
        
}
