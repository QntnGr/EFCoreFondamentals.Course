using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublisherDomain;

namespace PublisherData;

public class PubContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    private static readonly object _fileLock = new object();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
          "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDatabase"
        ).LogTo(message =>
        {
            lock (_fileLock)
            {
                File.AppendAllText("EFCoreLog.txt", message + Environment.NewLine);
            }
        },
        new[] { DbLoggerCategory.Database.Name },
        LogLevel.Information);
        //.EnableSensitiveDataLogging();//to add in debug, display parameters in query
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(
                     new Author { AuthorId = 1, FirstName = "Rhoda", LastName = "Lerman" });

        var authorList = new Author[]{
                new Author {AuthorId = 2, FirstName = "Ruth", LastName = "Ozeki" },
                new Author {AuthorId = 3, FirstName = "Sofia", LastName = "Segovia" },
                new Author {AuthorId = 4, FirstName = "Ursula K.", LastName = "LeGuin" },
                new Author {AuthorId = 5, FirstName = "Hugh", LastName = "Howey" },
                new Author {AuthorId = 6, FirstName = "Isabelle", LastName = "Allende" }
            };
        modelBuilder.Entity<Author>().HasData(authorList);

        var someBooks = new Book[]{
           new Book {BookId = 1, AuthorId=1, Title = "In God's Ear",
               PublishDate= new DateOnly(1989,3,1) },
           new Book {BookId = 2, AuthorId=2, Title = "A Tale For the Time Being",
               PublishDate = new DateOnly(2013,12,31) },
           new Book {BookId = 3, AuthorId=3, Title = "The Left Hand of Darkness",
               PublishDate=new DateOnly(1969,3,1)},
    };
        modelBuilder.Entity<Book>().HasData(someBooks);
    }
}