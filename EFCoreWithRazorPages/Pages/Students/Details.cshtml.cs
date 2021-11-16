using EFCoreWithRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EFCoreWithRazorPages.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly EFCoreWithRazorPages.Data.EFCoreWithRazorPagesContext _context;

        public DetailsModel(EFCoreWithRazorPages.Data.EFCoreWithRazorPagesContext context)
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
