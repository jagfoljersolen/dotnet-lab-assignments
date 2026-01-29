using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ImageMagick;

namespace MyApp.Namespace
{
    public class UploadModel : PageModel
    {
        [BindProperty]
        public IFormFile? Upload { get; set; }
        private string imagesDir;
        private MagickImage watermark;

        public UploadModel(IWebHostEnvironment environment)
        {
            imagesDir = Path.Combine(environment.WebRootPath, "images");
            watermark = new MagickImage("watermark.png");
            // przezroczystosc znaku wodnego
            watermark.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 4);

        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            const long maxBytes = 1 * 1024 * 1024;

            if (Upload != null && Upload.Length <= maxBytes)
            {
                string extension = ".jpg";
                switch (Upload.ContentType)
                {
                    case "image/png":
                        extension = ".png";
                        break;
                    case "image/gif":
                        extension = ".gif";
                        break;
                }

                var fileName =
                    Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) +
                extension;
                var outPath = Path.Combine(imagesDir, fileName);

                await using var inputStream = Upload.OpenReadStream();
                using var ms = new MemoryStream();
                await inputStream.CopyToAsync(ms);
                ms.Position = 0;

                using var image = new MagickImage(ms);
                
                // narysowanie znaku wodnego
                image.Composite(watermark, Gravity.Southeast, CompositeOperator.Over);

                using var resultBuffer = new MemoryStream();
                image.Write(resultBuffer);
                resultBuffer.Position = 0;

                await using var outputStream = new FileStream(
                    outPath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 64 * 1024,
                    options: FileOptions.Asynchronous | FileOptions.SequentialScan
                );
                await resultBuffer.CopyToAsync(outputStream);

            }
            else
            {
                ModelState.AddModelError("Upload", "Plik jest za duży (max. 1 MB)");
                return Page();
            }
             return RedirectToPage("Index");

        }
    }
}  