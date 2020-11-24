using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unsplash.Models.Request
{
    public class PhotoRequest
    {
        [Required]
        [StringLength(50)]
        public string Label { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
