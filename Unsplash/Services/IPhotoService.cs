using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unsplash.Models;

namespace Unsplash.Services
{
    interface IPhotoService
    {
        List<Photo> GetAllPhotos(int userId);
        Photo GetPhoto(int photoId);
        void DeletePhoto(int photoId);
        List<Photo> FindPhotos(int userId, string label);

    }
}
