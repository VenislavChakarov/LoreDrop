namespace LoreDrop.Data.Models;

public class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public virtual ICollection<Series> Series { get; set; } = new HashSet<Series>();
}