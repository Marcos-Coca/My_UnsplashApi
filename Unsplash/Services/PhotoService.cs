using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unsplash.Models;

namespace Unsplash.Services
{
    public class PhotoService : IPhotoService
    {
        public Photo GetPhoto(int photoId)
        {
            Photo photo;
            using(UnsplashContext db =  new UnsplashContext())
            {
                photo = db.Photos select(new Photo)
            }
            return photo;
        }
        public List<Photo> GetAllPhotos(int userId)
        {
            throw new NotImplementedException();
        }
        public void DeletePhoto(int photoId)
        {
            throw new NotImplementedException();
        }

        public List<Photo> FindPhotos(int userId, string label)
        {
            throw new NotImplementedException();
        }
        
    }
}
