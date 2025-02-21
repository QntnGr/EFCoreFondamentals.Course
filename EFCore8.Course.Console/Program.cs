using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

using var context = new PubContext();
{
    context.Database.EnsureCreated();
}

//InsertNewAuthorWithBook();
void InsertNewAuthorWithBook()
{
    var author = new Author { FirstName = "Lynda", LastName = "Rutledge" };
    author.Books.Add(new Book
    {
        Title = "West With Giraffes",
        PublishDate = new DateOnly(2021, 2, 1)
    });
    context.Authors.Add(author);
    context.SaveChanges();
}

void AddAuthorWithBook()
{
    var author = new Author { FirstName = "Julie", LastName = "Lerman" };
    author.Books.Add(new Book
    {
        Title = "Programming Entity Framework",
        PublishDate = new DateOnly(2009, 1, 1),
        AuthorId = author.AuthorId,
    });
    author.Books.Add(new Book
    {
        Title = "Programming Entity Framework 2nd Ed",
        PublishDate = new DateOnly(2010, 8, 1),
        AuthorId = author.AuthorId,
    });
    context.Authors.Add(author);
    context.SaveChanges();
}

string GetAuthorsWithBooks()
{
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
    var duplicateAuthors = context.Authors
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
            context.Authors.Remove(duplicateAuthor.First());
            duplicateAuthor.Remove(duplicateAuthor.First());
            context.SaveChanges();
        }
    }
}

string GetAuthors()
{
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
