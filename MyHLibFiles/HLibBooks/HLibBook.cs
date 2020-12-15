using MyHomeLibCommon;
using System.Collections.Generic;

namespace MyHLibBooks
{
    public abstract class HLibBook : IData
    {
        private string _title;
        private IEnumerable<HLibAuthor> _authors;

        public virtual string Title 
        {
            get { return _title; }
        }

        public virtual IEnumerable<HLibAuthor> Authors
        {
            get { return _authors; }
        }

        public HLibBook(string title) : this(title, null) { }
        public HLibBook(string title, IEnumerable<HLibAuthor> authors)
        {        
            _title = title;
            _authors = authors;
        }
    }
}