using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeodoraGetsova.Core.Data;
using TeodoraGetsova.Core.Entities;

namespace TeodoraGetsova.Controllers
{
    public class AdminEventController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _iwebhost;

        public AdminEventController(ApplicationDbContext dbContext, IWebHostEnvironment iwebhost)
        {
            this._dbContext = dbContext;
            this._iwebhost = iwebhost;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertPicture(IFormFile file) {

            string imageExt = Path.GetExtension(file.FileName);

            if (imageExt == ".jpg" || imageExt == ".gif")
            {
                var saveImagePath = Path.Combine(_iwebhost.WebRootPath, "eventimages", file.FileName);
                var stream = new FileStream(saveImagePath, FileMode.Create);
                await file.CopyToAsync(stream);               

                var image = new EventImage();
                image.Name = file.FileName;
                image.ImagePath = saveImagePath;

                await _dbContext.Images.AddAsync(image);
                await _dbContext.SaveChangesAsync();

                return Ok(image.ImagePath);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
