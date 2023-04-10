using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class User
    {
        public long userId { get; set; }
        [MaxLength(100)]
        [Required]
        public string userName { get; set; }
        [MaxLength(100)]
        public string password { get; set; }
        [MaxLength(100)]
        public string token { get; set; }
        [MaxLength(100)]
        [Required]
        public string role { get; set; }
        [MaxLength(100)]
        [Required]
        public string email { get; set; }
    }
}
