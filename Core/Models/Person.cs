using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Person : BaseEntity
    {
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string FullName { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string Title { get; set; }

        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile PhotoFile { get; set; }
    }
}
