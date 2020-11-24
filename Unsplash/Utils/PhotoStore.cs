using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Unsplash.Utils
{
    public class PhotoStore
    {
    
        public static async Task<ImageUploadResult> UploadPhoto(string imgPath)
        {
            string CLOUD_NAME = Environment.GetEnvironmentVariable("CLOUD_NAME");
            string API_KEY = Environment.GetEnvironmentVariable("API_KEY");
            string API_SECRET = Environment.GetEnvironmentVariable("API_SECRET");

            Account account = new Account(CLOUD_NAME,API_KEY,API_SECRET);
            Cloudinary cloudinary = new Cloudinary(account);
            Console.WriteLine(imgPath);
            var updateParams = new ImageUploadParams(){ File = new FileDescription(imgPath) };
            Console.WriteLine(updateParams);
            Task<ImageUploadResult> imageUploadTask = cloudinary.UploadAsync(updateParams);
            var uploadResult = await imageUploadTask;

            return uploadResult;

    }


        

    }
}
