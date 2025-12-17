using System.Collections.Generic;

namespace Kolozsvari_Balint_Lab2.Models
{
    public class Authors
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // optional back-reference for EF navigation
        public ICollection<Book>? Books { get; set; }
    }
}