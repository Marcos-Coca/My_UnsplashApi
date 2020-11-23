using System;
using System.Collections.Generic;

#nullable disable

namespace Unsplash.Models
{
    public partial class User
    {
        public User()
        {
            Photos = new HashSet<Photo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
