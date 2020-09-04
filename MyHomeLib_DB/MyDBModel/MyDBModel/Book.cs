using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.IO;

namespace MyDBModel
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int Key { get; set; }

        [Index("Caption")]
        [Required]
        public string Caption { get; set; }
        //public Stream BookFile { get; set; }
        public byte[] BookFile { get; set; }

        public ICollection<Author> Authors{ get; set; }
        public ICollection<KeyWord> KeyWords{ get; set; }
        public ICollection<Genre> Genres { get; set; }
        public Book()
        {
            Authors = new List<Author>();
            KeyWords = new List<KeyWord>();
            Genres = new List<Genre>();
        }
    }
}
