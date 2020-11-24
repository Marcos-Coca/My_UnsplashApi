using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unsplash.Services;
using Unsplash.Models.ViewModels;
using Unsplash.Models;
using Unsplash.Models.Request;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Unsplash.Utils;

namespace Unsplash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private IPhotoService _photoService;
        private IWebHostEnvironment _webHostEnvironment;

        public PhotoController(IPhotoService photoService, IWebHostEnvironment webHostEnvironment)
        {
            this._photoService = photoService;
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetAllPhotos(int userId,[FromQuery]string label)
        {

            List<PhotoViewModel> photos;

            if (string.IsNullOrEmpty(label))
                photos = _photoService.GetAllPhotos(userId);

            else
                photos = _photoService.FindPhotos(userId, label);

            return Ok(photos);
        }

        [HttpGet("{photoId}")]
        public IActionResult GetPhoto(int photoId)
        {
            PhotoViewModel photo = _photoService.GetPhoto(photoId);
            return Ok(photo);
        }

        [HttpPost]

        public async Task<IActionResult> CreatePhoto([FromForm]PhotoRequest model)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
           
            using (FileStream fileStream = new FileStream(filePath,FileMode.Create))
            {
                 model.Photo.CopyTo(fileStream);

            }
            var result = await PhotoStore.UploadPhoto(filePath);
            System.IO.File.Delete(filePath);

            Photo oPhoto = new Photo { IdUser = 1, Label = model.Label, Url = filePath };
            _photoService.CreatePhoto(oPhoto);
            return Created("", new {result });
            
        }

        [HttpDelete("{photoId}")]
        public IActionResult DeletePhoto(int photoId)
        {
            try
            {
                _photoService.DeletePhoto(photoId);
                return Ok();
            }
            catch(ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
