using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.AccountDto
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Surname { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password),Compare("Password")]
        public string PasswordConfirmed { get; set; }
    }
}
