namespace Domain.Entities
{
    public sealed class UserDto
    {
        public UserDto(string id, string name, string email)
        {
            Id = id;
            Name = name;    
            Email = email;
        }

        public string Id { get; } = string.Empty;

        public string Name { get; } = string.Empty;

        public string Email { get; } = string.Empty;
    }
}