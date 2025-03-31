using Microsoft.AspNetCore.Mvc;
using MuncipalityManagementSystem.Data;
using System.Linq;

namespace MuncipalityManagementSystem.Controllers
{
	public class CitizenController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CitizenController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var citizens = _context.Citizens.ToList(); // Fetch all citizens
			return View(citizens);
		}
	}
}
