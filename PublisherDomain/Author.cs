using System.ComponentModel.DataAnnotations;

namespace PublisherDomain;

public class Author
{
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public string Description { get; set; } = string.Empty;

}
