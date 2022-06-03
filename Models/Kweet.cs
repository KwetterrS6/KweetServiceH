using System.ComponentModel.DataAnnotations;

namespace KweetService.Models
{
    public class Kweet
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Datetime { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public User User {get; set;}
    }
}