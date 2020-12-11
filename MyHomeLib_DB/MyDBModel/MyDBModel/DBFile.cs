using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
using System.Linq;
using MyHomeLibCommon;

namespace MyDBModel
{
    [Table("DBFiles")]
    public class DBFile : IComparable
    {
        [Key]
        public int Key { get; set; }

        [Index("Path", IsUnique = true, IsClustered = true)]
        [Required]
        public string Path{ get; set; }
        
        public ICollection<Book> Books { get; set; }
        public DBFile()
        {
            Books = new List<Book>();
        }

        public int CompareTo(object obj)
        {
            DBFile f= obj as DBFile;
            int res = -1;
            if (f == null)
            {
                throw new ArgumentException("Wrong type");
            }

            res = Path.CompareTo(f.Path);
            return res;
        }
    }
}
