using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class SingleModel : PageModel
    {


        private string imagesDir;
        [BindProperty(SupportsGet = true)]
        public string Image { get; set; } = string.Empty;

        public SingleModel(IWebHostEnvironment environment)
        {
            imagesDir = Path.Combine(environment.WebRootPath, "images");
        }
        public ActionResult OnGet()
        {
            if (System.IO.File.Exists(Path.Combine(imagesDir, Image)))
            {
                return Page();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
