using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace MyDBModel
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int Key { get; set; }

        [Index("Caption", IsUnique = true)]
        [Required]
        public string Caption { get; set; }

        public ICollection<Author> Authors{ get; set; }
        public ICollection<KeyWord> KeyWords{ get; set; }

        public Book()
        {
            Authors = new List<Author>();
            KeyWords = new List<KeyWord>();
        }
    }
}
