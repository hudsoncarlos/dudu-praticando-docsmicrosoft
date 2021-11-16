using EFCoreWithRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWithRazorPages.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly EFCoreWithRazorPages.Data.EFCoreWithRazorPagesContext _context;

        public EditModel(EFCoreWithRazorPages.Data.EFCoreWithRazorPagesContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                Page();

            _context.Attach(Student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(Student.ID))
                    NotFound();
                else
                    throw;
            }

            return RedirectToPage("./Index");
        }

        private bool StudentExists(int id)
            => _context.Students.Any(e => e.ID == id);
    }
}
