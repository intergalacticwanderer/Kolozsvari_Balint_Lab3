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
                // Seed Authors if none exist
                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(
                        new Authors { FirstName = "Jane", LastName = "Austen" },
                        new Authors { FirstName = "Mark", LastName = "Twain" },
                        new Authors { FirstName = "George", LastName = "Orwell" }
                    );

                    context.SaveChanges();
                }

                // Seed Genres if none exist
                if (!context.Genre.Any())
                {
                    context.Genre.AddRange(
                        new Genre { Name = "Roman" },
                        new Genre { Name = "Nuvela" },
                        new Genre { Name = "Poezie" }
                    );

                    context.SaveChanges();
                }

                // Seed Customers if none exist
                if (!context.Customer.Any())
                {
                    context.Customer.AddRange(
                        new Customer { Name = "Popescu Marcela", Adress = "Str. Plopilor, nr. 24", BirthDate = DateTime.Parse("1979-09-01") },
                        new Customer { Name = "Mihailescu Cornel", Adress = "Str. Bucuresti, nr. 45, ap. 2", BirthDate = DateTime.Parse("1969-07-08") }
                    );

                    context.SaveChanges();
                }

                // Seed Books if none exist
                if (!context.Book.Any())
                {
                    context.Book.AddRange(
                        new Book { Title = "Baltagul", Price = Decimal.Parse("22") },
                        new Book { Title = "Enigma Otiliei", Price = Decimal.Parse("18") },
                        new Book { Title = "Maytrei", Price = Decimal.Parse("27") }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}