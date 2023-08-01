namespace Domain.Entities
{
    public sealed record JobDto(string Id, 
        string Title, 
        string Description, 
        int Positions, 
        string CreatedOn, 
        string Time);
}