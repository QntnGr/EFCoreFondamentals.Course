﻿using System.ComponentModel.DataAnnotations;

namespace PublisherDomain;

public class Author
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public List<Book> Books { get; set; } = new List<Book>();

}
