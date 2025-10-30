using CoolLibrary.Domain.Entities;
using CoolLibrary.Domain.Enums;
using CoolLibrary.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CoolLibrary.Infrastructure.Data;

/// <summary>
/// Entity Framework DbContext for the Library Management System
/// </summary>
public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }

    // DbSets for all entities
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Fine> Fines { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all entity configurations
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new LoanConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        modelBuilder.ApplyConfiguration(new FineConfiguration());
        modelBuilder.ApplyConfiguration(new BookAuthorConfiguration());
        modelBuilder.ApplyConfiguration(new BookGenreConfiguration());

        // Seed initial data
        SeedData(modelBuilder);
    }

    /// <summary>
    /// Seeds initial data for testing and development
    /// </summary>
    private void SeedData(ModelBuilder modelBuilder)
    {
        // Use static dates to avoid model changes on each build
        var baseDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        // Seed Authors
        modelBuilder.Entity<Author>().HasData(
            new Author
            {
                AuthorId = 1,
                FirstName = "George",
                LastName = "Orwell",
                Biography = "English novelist and essayist, journalist and critic",
                BirthDate = new DateTime(1903, 6, 25),
                Nationality = "British",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Author
            {
                AuthorId = 2,
                FirstName = "Jane",
                LastName = "Austen",
                Biography = "English novelist known primarily for her six major novels",
                BirthDate = new DateTime(1775, 12, 16),
                Nationality = "British",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Author
            {
                AuthorId = 3,
                FirstName = "J.K.",
                LastName = "Rowling",
                Biography = "British author, best known for the Harry Potter series",
                BirthDate = new DateTime(1965, 7, 31),
                Nationality = "British",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            }
        );

        // Seed Genres
        modelBuilder.Entity<Genre>().HasData(
            new Genre
            {
                GenreId = 1,
                Name = "Fiction",
                Description = "Literary works of imaginative narration",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Genre
            {
                GenreId = 2,
                Name = "Fantasy",
                Description = "Fiction involving magical or supernatural elements",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Genre
            {
                GenreId = 3,
                Name = "Classic Literature",
                Description = "Literature that has stood the test of time",
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            }
        );

        // Seed Books
        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                BookId = 1,
                ISBN = "978-0-452-28423-4",
                Title = "1984",
                Description = "A dystopian social science fiction novel and cautionary tale",
                PublicationDate = new DateTime(1949, 6, 8),
                Publisher = "Secker & Warburg",
                PageCount = 328,
                Language = "English",
                AvailableCopies = 3,
                TotalCopies = 5,
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Book
            {
                BookId = 2,
                ISBN = "978-0-14-143951-8",
                Title = "Pride and Prejudice",
                Description = "A romantic novel of manners",
                PublicationDate = new DateTime(1813, 1, 28),
                Publisher = "T. Egerton",
                PageCount = 432,
                Language = "English",
                AvailableCopies = 2,
                TotalCopies = 3,
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Book
            {
                BookId = 3,
                ISBN = "978-0-439-70818-8",
                Title = "Harry Potter and the Philosopher's Stone",
                Description = "The first novel in the Harry Potter series",
                PublicationDate = new DateTime(1997, 6, 26),
                Publisher = "Bloomsbury",
                PageCount = 223,
                Language = "English",
                AvailableCopies = 4,
                TotalCopies = 4,
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Book
            {
                BookId = 4,
                ISBN = "978-0-452-28424-1",
                Title = "Animal Farm",
                Description = "An allegorical novella about farm animals",
                PublicationDate = new DateTime(1945, 8, 17),
                Publisher = "Secker & Warburg",
                PageCount = 95,
                Language = "English",
                AvailableCopies = 1,
                TotalCopies = 2,
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Book
            {
                BookId = 5,
                ISBN = "978-0-14-143952-5",
                Title = "Emma",
                Description = "A novel about youthful hubris and romantic misunderstandings",
                PublicationDate = new DateTime(1815, 12, 23),
                Publisher = "John Murray",
                PageCount = 474,
                Language = "English",
                AvailableCopies = 3,
                TotalCopies = 3,
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            }
        );

        // Seed Customers
        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                CustomerId = 1,
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@email.com",
                Phone = "+1-555-0101",
                Address = "123 Main Street",
                City = "New York",
                PostalCode = "10001",
                MembershipDate = baseDate.AddMonths(-6),
                MembershipStatus = MembershipStatus.Active,
                MaxBooksAllowed = 5,
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            },
            new Customer
            {
                CustomerId = 2,
                FirstName = "Emily",
                LastName = "Johnson",
                Email = "emily.johnson@email.com",
                Phone = "+1-555-0102",
                Address = "456 Oak Avenue",
                City = "Los Angeles",
                PostalCode = "90210",
                MembershipDate = baseDate.AddMonths(-3),
                MembershipStatus = MembershipStatus.Active,
                MaxBooksAllowed = 3,
                CreatedAt = baseDate,
                UpdatedAt = baseDate
            }
        );

        // Seed Book-Author relationships
        modelBuilder.Entity<BookAuthor>().HasData(
            new BookAuthor { BookId = 1, AuthorId = 1, AuthorOrder = 1 }, // 1984 by George Orwell
            new BookAuthor { BookId = 2, AuthorId = 2, AuthorOrder = 1 }, // Pride and Prejudice by Jane Austen
            new BookAuthor { BookId = 3, AuthorId = 3, AuthorOrder = 1 }, // Harry Potter by J.K. Rowling
            new BookAuthor { BookId = 4, AuthorId = 1, AuthorOrder = 1 }, // Animal Farm by George Orwell
            new BookAuthor { BookId = 5, AuthorId = 2, AuthorOrder = 1 }  // Emma by Jane Austen
        );

        // Seed Book-Genre relationships
        modelBuilder.Entity<BookGenre>().HasData(
            new BookGenre { BookId = 1, GenreId = 1 }, // 1984 - Fiction
            new BookGenre { BookId = 1, GenreId = 3 }, // 1984 - Classic Literature
            new BookGenre { BookId = 2, GenreId = 1 }, // Pride and Prejudice - Fiction
            new BookGenre { BookId = 2, GenreId = 3 }, // Pride and Prejudice - Classic Literature
            new BookGenre { BookId = 3, GenreId = 1 }, // Harry Potter - Fiction
            new BookGenre { BookId = 3, GenreId = 2 }, // Harry Potter - Fantasy
            new BookGenre { BookId = 4, GenreId = 1 }, // Animal Farm - Fiction
            new BookGenre { BookId = 4, GenreId = 3 }, // Animal Farm - Classic Literature
            new BookGenre { BookId = 5, GenreId = 1 }, // Emma - Fiction
            new BookGenre { BookId = 5, GenreId = 3 }  // Emma - Classic Literature
        );
    }

    /// <summary>
    /// Override SaveChanges to automatically update UpdatedAt timestamps
    /// </summary>
    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    /// <summary>
    /// Override SaveChangesAsync to automatically update UpdatedAt timestamps
    /// </summary>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates the UpdatedAt property for modified entities
    /// </summary>
    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified)
            .Where(e => e.Entity.GetType().GetProperty("UpdatedAt") != null);

        foreach (var entry in entries)
        {
            entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
        }
    }
}