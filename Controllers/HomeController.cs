using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MuncipalityManagementSystem.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult Contact()
		{
			return View();
		}

		/// <summary>
		/// Handles and displays errors that occur during request processing.
		/// </summary>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
			_logger.LogError("Error occurred with Request ID: {RequestId}", requestId);

			ViewData["RequestId"] = requestId;
			ViewData["ErrorMessage"] = "An error occurred while processing your request. Please try again or contact support if the problem persists.";

			return View();
		}
	}
}
