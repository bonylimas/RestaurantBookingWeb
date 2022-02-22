using Microsoft.AspNetCore.Mvc;

using RestaurantBooking.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantBookingWeb.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration _configuration;
        private static HttpClient client = new HttpClient();

        private string apiPath = "";
        public ReservationController(IConfiguration configuration, IWebHostEnvironment _env)
        {
            webHostEnvironment = _env;
            _configuration = configuration;

            apiPath = _configuration["ReservationAPI"];
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Reservation> reservations = null;

            HttpResponseMessage response =  await client.GetAsync(apiPath);

            if (response.IsSuccessStatusCode)
            {
                reservations = await response.Content.ReadFromJsonAsync<IEnumerable<Reservation>>();
            }


            return View(reservations);
        }

        //GET
        public async Task<IActionResult> Upsert(int? id)
        {
            Reservation reservation = new();

            if (id == null || id == 0)
            {
                //Create Reservation
                return View(reservation);
            }
            else
            {
                //Edit Reservation
                HttpResponseMessage response = await client.GetAsync(apiPath + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    reservation = await response.Content.ReadFromJsonAsync<Reservation>();
                }
                else
                {
                    TempData["error"] = "Error getting this reservation.";

                    return RedirectToAction("Index");
                }
            }
            return View(reservation);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Reservation reservation)
        {

            if (ModelState.IsValid)
            {

                // Create New
                if (reservation.Id == 0)
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(apiPath, reservation);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                        TempData["success"] = "Reservation created successfully.";
                    else
                    {
                        TempData["error"] = "Error creating reservation.";
                        return View(reservation);
                    }
                }
                else
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(apiPath + reservation.Id, reservation);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                        TempData["success"] = "Reservation updated successfully.";
                    else
                    {
                        TempData["error"] = "Error updating reservation.";
                        return View(reservation);
                    }
                }

                return RedirectToAction("Index");
            }

            return View(reservation);
            
        }

        // GET
        // This is to Confirm
        public async Task<IActionResult> Delete(int? id)
        {
            Reservation reservation = new();

            HttpResponseMessage response = await client.GetAsync(apiPath + id.ToString());

            if (response.IsSuccessStatusCode)
            {
                reservation = await response.Content.ReadFromJsonAsync<Reservation>();

                return View(reservation);
            }

            return NotFound();

        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            // Consume API
            HttpResponseMessage response = await client.DeleteAsync(apiPath + id.ToString());

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Reservation cancelled successfully.";
            }
            else
            {
                TempData["error"] = "Error cancelling reservation.";
            }

            return RedirectToAction("Index");
            
        }

        
    }

}
