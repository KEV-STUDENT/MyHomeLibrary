using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace MyDBModel
{
    [Table("KeyWords")]
    public class KeyWord
    {
        [Key]
        public int Key { get; set; }

        [Index("Word", IsUnique = true)]
        [Required]
        public string Word { get; set; }
    }
}
