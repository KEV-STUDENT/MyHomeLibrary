using MyHomeLibCommon;

namespace MyHLibBooks
{
    public struct HLibGenre
    {
        private ItemGenre _genre;
        private string _genreName;

        public HLibGenre(ItemGenre genre, string genreName)
        {
            _genre = genre;
            _genreName = genreName;
        }

        public ItemGenre Genre
        {
            get { return _genre; }
        }

        public string GenreName
        {
            get { return _genreName; }
        }
    }
}