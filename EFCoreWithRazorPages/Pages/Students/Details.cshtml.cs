using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using praticando_efcore_with_razor_pages.Models;
using System.Threading.Tasks;

namespace praticando_efcore_with_razor_pages.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly praticando_efcore_with_razor_pages.Data.EFCoreWithRazorPagesContext _context;

        public DetailsModel(praticando_efcore_with_razor_pages.Data.EFCoreWithRazorPagesContext context)
            => _context = context;

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                NotFound();

            Student = await _context.Students.FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null)
                NotFound();

            return Page();
        }
    }
}
