using System;
using System.Collections.Generic;

#nullable disable

namespace Unsplash.Models
{
    public partial class Photo
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IdUser { get; set; }

        public virtual User IdUserNavigation { get; set; }
    }
}
