using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using praticando_efcore_with_razor_pages.Data;
using praticando_efcore_with_razor_pages.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace praticando_efcore_with_razor_pages.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly EFCoreWithRazorPagesContext _context;
        private readonly MvcOptions _mvcOptions;

        public IndexModel(EFCoreWithRazorPagesContext context, IOptions<MvcOptions> mvcOptions)
        {
            _context = context;
            _mvcOptions = mvcOptions.Value;
        }

        public IList<Student> Student { get; set; }

        public async Task OnGetAsync()
            => Student = await _context.Students.Take(
                _mvcOptions.MaxModelBindingCollectionSize).ToListAsync();
    }
}
