using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}

//GetAuthors();
//AddAuthor();
//GetAuthors();
//AddAuthorWithBook();
//GetAuthorsWithBooks();
//DeleteDupicate();

void AddAuthorWithBook()
{
    var author = new Author { FirstName = "Julie", LastName = "Lerman" };
    author.Books.Add(new Book
    {
        Title = "Programming Entity Framework",
        PublishDate = new DateOnly(2009, 1, 1),
        Author = author,
    });
    author.Books.Add(new Book
    {
        Title = "Programming Entity Framework 2nd Ed",
        PublishDate = new DateOnly(2010, 8, 1),
        Author = author,
    });
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}
string GetAuthorsWithBooks()
{
    using var context = new PubContext();
    var authors = context.Authors.Include(a => a.Books).ToList();
    var authorsWithBooks = string.Empty;
    foreach (var author in authors)
    {
        authorsWithBooks += $"\nthe writer {author.FirstName} {author.LastName} wrote :";
        foreach (var book in author.Books)
        {
            authorsWithBooks += $"\n    .{book.Title}";
        }
    }
    return authorsWithBooks;
}

void AddAuthor()
{
    var author = new Author { FirstName = "Josie", LastName = "Newf" };
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void DeleteDupicate()
{
    using var _context = new PubContext();
    var duplicateAuthors = _context.Authors
        .GroupBy(aut => new { aut.LastName, aut.FirstName})
        .Select(g => new
        {
            Key1 = g.Key.FirstName,
            Key2 = g.Key.LastName,
            Items = g.ToList()
        })
        .ToList();

    foreach (var duplicateAuthor in duplicateAuthors.Select( da=>da.Items).Where(da => da.Count > 1))
    {
        while(duplicateAuthor.Count != 1)
        {
            _context.Authors.Remove(duplicateAuthor.First());
            duplicateAuthor.Remove(duplicateAuthor.First());
            _context.SaveChanges();
        }
    }
}

string GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.ToList();
    var listAUthors = string.Empty;
    foreach (var author in authors)
    {
        listAUthors += $"{author.FirstName} {author.LastName} \n";
    }
    return listAUthors;
}
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/", () => GetAuthorsWithBooks());

app.Run();
