using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace MyDBModel
{
    [Table("Authors")]
    public class Author
    {
        [Key]
        public int Key { get; set; }

        [Index("FullName", IsUnique = true, Order = 1)]
        [Index("LastName")]
        [Required]
        public string LastName { get; set; }

        [Index("FullName", IsUnique = true, Order = 2)]
        [Index("FirstName")]
        [Required]
        public string FirstName { get; set; }

        [Index("FullName", IsUnique = true, Order = 3)]
        public string MiddleName{ get; set; }

        public string NickName { get; set; }
        public string EMail { get; set; }


        public ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
    }
}
