namespace Domain.Entities
{
    public sealed record JobDto(string Id,
        string Title, 
        string Description, 
        string Requirements,
        string Contact,
        string Location,
        decimal? Salary,
        int Positions, 
        string CreatedOn, 
        string Time,
        string Status);
}