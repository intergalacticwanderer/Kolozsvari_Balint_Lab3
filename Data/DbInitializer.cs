using Microsoft.EntityFrameworkCore;
using Kolozsvari_Balint_Lab2.Models;


namespace Kolozsvari_Balint_Lab2.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext
            (serviceProvider.GetRequiredService
            <DbContextOptions<LibraryContext>>()))
            {
                // Remove unwanted authors if present
                var unwantedLastNames = new[] { "Austen", "Twain", "Orwell" };
                var toRemove = context.Authors
                    .Where(a => unwantedLastNames.Contains(a.LastName))
                    .ToList();
                if (toRemove.Any())
                {
                    context.Authors.RemoveRange(toRemove);
                    context.SaveChanges();
                }

                // Upsert desired authors
                var desiredAuthors = new[]
                {
                    new Authors { FirstName = "Mihail", LastName = "Sadoveanu" },
                    new Authors { FirstName = "Goerge", LastName = "Calinescu" },
                    new Authors { FirstName = "Mircea", LastName = "Eliade" }
                };

                foreach (var a in desiredAuthors)
                {
                    if (!context.Authors.Any(x => x.FirstName == a.FirstName && x.LastName == a.LastName))
                    {
                        context.Authors.Add(a);
                    }
                }
                context.SaveChanges();

                // Upsert Genres
                var desiredGenres = new[] { "Roman", "Nuvela", "Poezie" };
                foreach (var g in desiredGenres)
                {
                    if (!context.Genre.Any(x => x.Name == g))
                        context.Genre.Add(new Genre { Name = g });
                }
                context.SaveChanges();

                // Upsert Customers
                if (!context.Customer.Any(c => c.Name == "Popescu Marcela"))
                {
                    context.Customer.AddRange(
                        new Customer { Name = "Popescu Marcela", Adress = "Str. Plopilor, nr. 24", BirthDate = DateTime.Parse("1979-09-01") },
                        new Customer { Name = "Mihailescu Cornel", Adress = "Str. Bucuresti, nr. 45, ap. 2", BirthDate = DateTime.Parse("1969-07-08") }
                    );
                    context.SaveChanges();
                }

                // Add Books only if they don't already exist (and set AuthorsID when possible)
                if (!context.Book.Any(b => b.Title == "Baltagul"))
                {
                    var sadoveanu = context.Authors.FirstOrDefault(a => a.LastName == "Sadoveanu");
                    var calinescu = context.Authors.FirstOrDefault(a => a.LastName == "Calinescu");
                    var eliade = context.Authors.FirstOrDefault(a => a.LastName == "Eliade");

                    context.Book.AddRange(
                        new Book { Title = "Baltagul", Price = Decimal.Parse("22"), AuthorsID = sadoveanu?.ID },
                        new Book { Title = "Enigma Otiliei", Price = Decimal.Parse("18"), AuthorsID = calinescu?.ID },
                        new Book { Title = "Maytrei", Price = Decimal.Parse("27"), AuthorsID = eliade?.ID }
                    );
                    context.SaveChanges();
                }
                // ensure AuthorsID set for seeded books (safe extra step)
                context.Database.ExecuteSqlRaw("UPDATE Book SET AuthorsID = (SELECT TOP(1) ID FROM Authors WHERE LastName = 'Sadoveanu') WHERE Title = 'Baltagul'");
                context.Database.ExecuteSqlRaw("UPDATE Book SET AuthorsID = (SELECT TOP(1) ID FROM Authors WHERE LastName = 'Calinescu') WHERE Title = 'Enigma Otiliei'");
                context.Database.ExecuteSqlRaw("UPDATE Book SET AuthorsID = (SELECT TOP(1) ID FROM Authors WHERE LastName = 'Eliade') WHERE Title = 'Maytrei'");
            }
        }
    }
}