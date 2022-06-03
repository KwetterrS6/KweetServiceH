using System.ComponentModel.DataAnnotations;

namespace KweetService.Dtos
{
    public class KweetCreateDto
    {
        [Required]
        public string Datetime { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}