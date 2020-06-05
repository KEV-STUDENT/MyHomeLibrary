using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDBModel
{
    [Table("Authors")]
    public class Author
    {
        [Key]
        public int Key { get; set; }

        [Index("Name", IsUnique = true, Order = 1)]
        [Index("LastName")]
        [Required]
        public string LastName { get; set; }
        [Index("Name", IsUnique = true, Order = 2)]
        [Index("FirstName")]
        [Required]
        public string FirstName { get; set; }
        public string FirstName111 { get; set; }
        //public string FirstName1_1 { get; set; }
    }
}
