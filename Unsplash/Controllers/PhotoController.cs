using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Unsplash.Services;
using Unsplash.Models.ViewModels;
using Unsplash.Models;
using Unsplash.Models.Request;
using Microsoft.AspNetCore.Hosting;
using Unsplash.Utils;
using Microsoft.Extensions.Configuration;

namespace Unsplash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private IPhotoService _photoService;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;

        public PhotoController(
            IPhotoService photoService, 
            IWebHostEnvironment webHostEnvironment,
            IConfiguration config
            )
        {
            this._photoService = photoService;
            this._webHostEnvironment = webHostEnvironment;
            this._config = config;
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

        public  IActionResult CreatePhoto([FromForm]PhotoRequest model)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string filePath = PhotoStore.SavePhoto(model.Photo, webRootPath);
            var result =  PhotoStore.UploadPhoto(filePath,_config);
            PhotoStore.DeletePhoto(filePath);
    
            Photo oPhoto = new Photo { 
                IdUser = 1,
                Label = model.Label, 
                Url = result.SecureUrl.ToString() 
            };
            _photoService.CreatePhoto(oPhoto);
            return Created("Photo Added Successfully",new { });
            
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
