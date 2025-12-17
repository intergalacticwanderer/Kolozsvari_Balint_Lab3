using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Kolozsvari_Balint_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public int? GenreID { get; set; }
        public Genre? Genre { get; set; }

        // foreign key and navigation property toward Models.Authors.cs
        public int? AuthorsID { get; set; }
        public Authors? Authors { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
