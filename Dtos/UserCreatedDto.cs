namespace KweetService.Dtos
{
    public class UserCreatedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public int ExternalId { get; set; }
    }
}