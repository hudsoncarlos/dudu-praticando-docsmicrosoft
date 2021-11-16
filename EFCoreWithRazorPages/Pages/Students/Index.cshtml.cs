using EFCoreWithRazorPages.Data;
using EFCoreWithRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWithRazorPages.Pages.Students
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

        public IList<Student> Student { get;set; }

        public async Task OnGetAsync()
            => Student = await _context.Students.Take(
                _mvcOptions.MaxModelBindingCollectionSize).ToListAsync();
    }
}
