using Microsoft.AspNetCore.Mvc;

using RestaurantBooking.Models;
using RestaurantBooking.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantBookingWeb.Controllers
{
    public class SearchController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private static HttpClient client = new HttpClient();

        private string apiPath = "";

        public SearchController(IConfiguration configuration, IWebHostEnvironment webHostingEnv)
        {
            _webHostEnvironment = webHostingEnv;
            _configuration = configuration;

            apiPath = _configuration["ReservationAPI"];
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            SearchReservationVM searchVM = new SearchReservationVM();

            HttpResponseMessage response = await client.GetAsync(apiPath);

            if (response.IsSuccessStatusCode)
            {
                searchVM.SearchResult = await response.Content.ReadFromJsonAsync<IEnumerable<Reservation>>();
            }

            return View(searchVM);
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> IndexPOST(SearchReservationVM searchVM)
        {
            HttpResponseMessage response = null;

            if (searchVM.SearchDate)
            {
                response = await client.GetAsync(apiPath + "bydate/" + searchVM.DateFilter.ToString("dd-MM-yyyy"));
            }
            else
            {
                response = await client.GetAsync(apiPath + "byphone/" + searchVM.PhoneNumber);
            }

            if (response.IsSuccessStatusCode)
            {
                searchVM.SearchResult = await response.Content.ReadFromJsonAsync<IEnumerable<Reservation>>();
            }

            return View(searchVM);
        }

        

       
    }

}
