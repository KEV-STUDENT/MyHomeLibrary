using Ionic.Zip;

namespace MyHLibFiles
{
    public class HLibFileFB2 : HLibFile
    {
        public HLibFileFB2(string path, string name) : base(path, name)
        {
        }

        public HLibFileFB2(HLibFileZIP zip, ZipEntry entry) : base(zip, entry)
        {
        }

        public override IData GetDataFromFile()
        {
            return new HLibBookFB2();
        }
    }
}