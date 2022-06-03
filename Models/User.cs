using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KweetService.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExternalID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Kweet> Kweets { get; set; } = new List<Kweet>();
     }
}