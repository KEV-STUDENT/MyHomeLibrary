using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using MyHomeLibCommon;

namespace MyDBModel
{
    [Table ("Genres")]
    public class Genre
    {
        [Key]
        public ItemGenre Key { get; set; }
        public string Code { get; set; }
        public ICollection<Book> Books { get; set; }

        public Genre()
        {
            Books = new List<Book>();
        }
    }
}
