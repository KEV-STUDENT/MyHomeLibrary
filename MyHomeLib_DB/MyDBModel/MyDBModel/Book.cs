using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
using System.Linq;
using MyHomeLibCommon;

namespace MyDBModel
{
    [Table("Books")]
    public class Book : IComparable
    {
        [Key]
        public int Key { get; set; }

        [Index("Caption")]
        [Required]
        public string Caption { get; set; }
        [Required]
        public string EntityName { get; set; }
        
        public ItemEncoding Encoding { get; set; }

        public ICollection<Author> Authors{ get; set; }
        public ICollection<KeyWord> KeyWords{ get; set; }
        public ICollection<Genre> Genres { get; set; }
        public Book()
        {
            Authors = new List<Author>();
            KeyWords = new List<KeyWord>();
            Genres = new List<Genre>();
        }

        private int ComapreList<T>(ICollection<T> list1, ICollection<T> list2) where T:IComparable
        {
            int res = 0;
            int cnt1 = list1.Count();
            int cnt2 = list2.Count();

            if (cnt1 == 0 && cnt2 > 0)
            {
                res = -1;
            }
            else if (cnt1 > 0 && cnt2 == 0)
            {
                res = 1;
            }
            else if (cnt1 == 0 && cnt2 == 0)
            {
                res = 0;
            }
            else
            {
                var a1 = list1.ToList();
                a1.Sort();
                var a2 = list2.ToList();
                a2.Sort();
                for (int i = 0; i < Math.Min(cnt1, cnt2); i++)
                {
                    res = a1[i].CompareTo(a2[i]);
                    if (res != 0)
                    {
                        break;
                    }
                }

                if (res == 0 && cnt1 != cnt2)
                {
                    res = (cnt1 < cnt2 ? -1 : 1);
                }
            }
            return res;
        }
        public int CompareTo(object obj)
        {
            Book b = obj as Book;
            int res = -1;
            if (b == null)
            {
                throw new ArgumentException("Wrong type");
            }

            res = Caption.CompareTo(b.Caption);
            if (res == 0)
            {
                res = ComapreList<Author>(Authors, b.Authors);
            }

            if(res==0)
            {
                res = ComapreList<KeyWord>(KeyWords, b.KeyWords);
            }

            if (res == 0)
            {
                res = ComapreList<Genre>(Genres, b.Genres);
            }
            return res;
        }
    }
}
