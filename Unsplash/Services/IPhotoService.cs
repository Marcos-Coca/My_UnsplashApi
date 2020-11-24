using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unsplash.Models.ViewModels;
using Unsplash.Models;

namespace Unsplash.Services
{
    public interface IPhotoService
    {
        List<PhotoViewModel> GetAllPhotos(int userId);
        PhotoViewModel GetPhoto(int photoId);
        void CreatePhoto(Photo photo);
        void DeletePhoto(int photoId);
        List<PhotoViewModel> FindPhotos(int userId, string label);

    }
}
