using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unsplash.Models.ViewModels;

namespace Unsplash.Services
{
    interface IPhotoService
    {
        List<PhotoViewModel> GetAllPhotos(int userId);
        PhotoViewModel GetPhoto(int photoId);
        void DeletePhoto(int photoId);
        List<PhotoViewModel> FindPhotos(int userId, string label);

    }
}
