using System;
using System.Collections.Generic;
using System.Linq;
using Unsplash.Models;
using Unsplash.Models.ViewModels;

namespace Unsplash.Services
{
    public class PhotoService : IPhotoService

    {
        public PhotoViewModel GetPhoto(int photoId)
        {
            PhotoViewModel photo;
            using(UnsplashContext db = new UnsplashContext())
            {
                photo = (from d in db.Photos 
                        select new PhotoViewModel{
                            Id = d.Id,
                            Url = d.Url,
                            CreatedAt = d.CreatedAt,
                            Label = d.Label
                }).FirstOrDefault();
            }
            return photo;
        }
        public List<PhotoViewModel> GetAllPhotos(int userId)
        {
            List<PhotoViewModel> photos;
            using(UnsplashContext db = new UnsplashContext())
            {
                photos = (from d in db.Photos
                          where userId == d.IdUser
                          select new PhotoViewModel
                          {
                              Id = d.Id,
                              Url = d.Url,
                              CreatedAt = d.CreatedAt,
                              Label = d.Label
                          }).ToList();
            }
            return photos;
        }
        public void DeletePhoto(int photoId)
        {
            using (UnsplashContext db = new UnsplashContext())
            {
                Photo photo = db.Photos.Find(photoId);
                db.Photos.Remove(photo);
                db.SaveChanges();
            }
        }

        public List<PhotoViewModel> FindPhotos(int userId, string label)
        {
            List<PhotoViewModel> photos;

            using(UnsplashContext db = new UnsplashContext())
            {
                photos = (from d in db.Photos
                          where d.IdUser == userId && d.Label.Contains(label)
                          select new PhotoViewModel
                          {
                              Id = d.Id,
                              Url = d.Url,
                              CreatedAt = d.CreatedAt,
                              Label = d.Label
                          }).ToList();
               
            }

            return photos;
        }

        public void CreatePhoto(Photo photo)
        {
            using(UnsplashContext db = new UnsplashContext())
            {
                db.Photos.Add(photo);
                db.SaveChanges();
            }
        }
    }
}
