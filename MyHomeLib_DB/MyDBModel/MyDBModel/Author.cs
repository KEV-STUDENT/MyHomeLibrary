using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace MyDBModel
{
    [Table("Authors")]
    public class Author : IComparable
    {
        [Key]
        public int Key { get; set; }

        [Index("FullName", IsUnique = true, Order = 1)]
        [Index("LastName")]
        public string LastName { get; set; }

        [Index("FullName", IsUnique = true, Order = 2)]
        [Index("FirstName")]
        public string FirstName { get; set; }

        [Index("FullName", IsUnique = true, Order = 3)]
        public string MiddleName{ get; set; }

        [Index("FullName", IsUnique = true, Order = 4)]
        public string NickName { get; set; }
        public string EMail { get; set; }


        public ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }

        public int CompareTo(object obj)
        {
            Author a = obj as Author;
            int res = -1;
            if(a == null)
            {
                throw new ArgumentException("Wrong type");
            }

            res = LastName.CompareTo(a.LastName);
            if (res == 0)
            {
                res = FirstName.CompareTo(a.FirstName);
            }

            if (res == 0)
            {
                res = MiddleName.CompareTo(a.MiddleName);
            }

            return res;
        }
    }
}
