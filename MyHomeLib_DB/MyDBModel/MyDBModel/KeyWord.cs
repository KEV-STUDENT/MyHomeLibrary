using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace MyDBModel
{
    [Table("KeyWords")]
    public class KeyWord : IComparable
    {
        [Key]
        public int Key { get; set; }

        [Index("Word", IsUnique = true)]
        [Required]
        public string Word { get; set; }

        public ICollection<Book> Books { get; set; }

        public KeyWord()
        {
            Books = new List<Book>();
        }

        public int CompareTo(object obj)
        {
            KeyWord kw = obj as KeyWord;
            int res = -1;
            if (kw == null)
            {
                throw new ArgumentException("Wrong type");
            }

            res = Word.CompareTo(kw.Word);

            return res;
        }
    }
}
