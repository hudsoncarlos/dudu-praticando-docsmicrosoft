using EFCoreWithRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EFCoreWithRazorPages.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly EFCoreWithRazorPages.Data.EFCoreWithRazorPagesContext _context;

        public CreateModel(EFCoreWithRazorPages.Data.EFCoreWithRazorPagesContext context)
            => _context = context;

        public IActionResult OnGet()
            => Page();

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                Page();

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
