using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Unsplash.Utils
{
    public class PhotoStore
    {
    
        public static ImageUploadResult UploadPhoto(string imgPath, IConfiguration config)
        {
            string CLOUD_NAME = config["CLOUD_NAME"];
            string API_KEY = config["API_KEY"];
            string API_SECRET = config["API_SECRET"];

            Account account = new Account(CLOUD_NAME,API_KEY,API_SECRET);
            Cloudinary cloudinary = new Cloudinary(account);

            var updateParams = new ImageUploadParams(){ File = new FileDescription(imgPath) };
            ImageUploadResult imageUploadTask = cloudinary.Upload(updateParams);

            return imageUploadTask;
        }

        public static string SavePhoto(IFormFile photo,string webRootPath)
        {
            string uploadsFolder = Path.Combine(webRootPath, "uploads");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }

            return filePath;
        }

        public static void DeletePhoto(string imgPath)
        {
            System.IO.File.Delete(imgPath);
        }

    }
}
