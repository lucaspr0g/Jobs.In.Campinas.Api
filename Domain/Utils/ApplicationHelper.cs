namespace Domain.Utils
{
    public static class ApplicationHelper
    {
        public static string GenerateGuid() => Guid.NewGuid().ToString();
    }
}