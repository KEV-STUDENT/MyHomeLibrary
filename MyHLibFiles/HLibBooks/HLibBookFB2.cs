using MyHomeLibCommon;
using System.Collections.Generic;

namespace MyHLibBooks
{
    public class HLibBookFB2 : HLibBook
    {
        protected IEnumerable<string> _keyWords;
        protected IEnumerable<HLibGenre> _genres;
        protected string _annotation;
        protected ItemEncoding _encoding;

        public virtual IEnumerable<string> KeyWords{ get { return _keyWords; } }
        public virtual IEnumerable<HLibGenre> Genres{ get { return _genres; } }
        public string Annotation { get { return _annotation; } }
        public ItemEncoding Encoding { get { return _encoding; } }

        public HLibBookFB2() : this(string.Empty, null, null, null, string.Empty, ItemEncoding.none) { }
        public HLibBookFB2(string title) : this(title, null, null, null, string.Empty, ItemEncoding.none) { }

        public HLibBookFB2(string title, IEnumerable<HLibAuthor> authors) :
            this(title, authors, null, null, string.Empty, ItemEncoding.none) { }
        public HLibBookFB2(
            string title,
            IEnumerable<HLibAuthor> authors,
            IEnumerable<string> keyWords) : this(title, authors, keyWords, null, string.Empty, ItemEncoding.none) { }

        public HLibBookFB2(
            string title,
            IEnumerable<HLibAuthor> authors,
            IEnumerable<string> keyWords,
            IEnumerable<HLibGenre> genres) : this(title, authors, keyWords, genres, string.Empty, ItemEncoding.none) { }

        public HLibBookFB2(
          string title,
          IEnumerable<HLibAuthor> authors,
          IEnumerable<string> keyWords,
          IEnumerable<HLibGenre> genres,
          string annotation) : this(title, authors, keyWords, genres, annotation, ItemEncoding.none) { }

        public HLibBookFB2(
            string title, 
            IEnumerable<HLibAuthor> authors,
            IEnumerable<string> keyWords,
            IEnumerable<HLibGenre> genres,
            string annotation,
            ItemEncoding encoding) : base(title, authors)
        {
            _annotation = annotation;
            _encoding = encoding;
            _keyWords = keyWords;
            _genres = genres;
        }
    }
}
