using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using praticando_efcore_with_razor_pages.Models;
using System.Threading.Tasks;

namespace praticando_efcore_with_razor_pages.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly praticando_efcore_with_razor_pages.Data.EFCoreWithRazorPagesContext _context;

        public CreateModel(praticando_efcore_with_razor_pages.Data.EFCoreWithRazorPagesContext context)
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
