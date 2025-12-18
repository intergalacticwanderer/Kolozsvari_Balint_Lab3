using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kolozsvari_Balint_Lab2.Data;
using Kolozsvari_Balint_Lab2.Models;

namespace Kolozsvari_Balint_Lab2.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var authors = await _context.Authors.ToListAsync();
            return View(authors);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .Include(a => a.Books)               // load related books
                .ThenInclude(b => b.Genre)           // load book genres (optional)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }
    }
}