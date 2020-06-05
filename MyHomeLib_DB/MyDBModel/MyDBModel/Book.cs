using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
