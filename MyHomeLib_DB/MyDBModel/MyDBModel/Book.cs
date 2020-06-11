﻿using System.ComponentModel.DataAnnotations;
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

        public ICollection<Author> Author { get; set; }

        public Book()
        {
            Author = new List<Author>();
        }
    }
}
