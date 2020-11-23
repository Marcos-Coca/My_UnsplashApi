using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unsplash.Models.ViewModels
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
