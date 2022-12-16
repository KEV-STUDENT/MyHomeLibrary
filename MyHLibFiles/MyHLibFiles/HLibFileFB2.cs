using Ionic.Zip;
using MyHLibBooks;
using MyHomeLibCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyHLibFiles
{
    public class HLibFileFB2 : HLibFile, IHLibFileWithData
    {
        private Stream stream = null;
        private XmlDocument xmlDoc = null;
        private ItemEncoding encoding = ItemEncoding.none;

        public HLibFileFB2(string path, string name) : base(path, name)
        {
        }

        public HLibFileFB2(HLibFileZIP zip, ZipEntry entry) : base(zip, entry)
        {
        }

        public HLibFileFB2(HLibFileZIP zip, ZipEntry entry, bool exclusive) : base(zip, entry, exclusive)
        {
        }

        public async Task<IData> GetDataFromFileAsync()
        {
            return await Task<IData>.Factory.StartNew(GetDataFromFile);
        }
        public IData GetDataFromFile()
        {
            XmlElement xRoot;
            XmlNamespaceManager namespaceManager;

            GetXRoot(out xRoot, out namespaceManager);

            if (xRoot == null || namespaceManager == null)
            {
                throw new ExceptionDataNotLoaded(Path, Name);
            }
            //string attribute;
            string title = GetInnerTextFromNode(xRoot.SelectSingleNode("//fb:description/fb:title-info/fb:book-title[1]", namespaceManager));
            IEnumerable<string> keyWords = GetKeyWords(xRoot, namespaceManager);
            IEnumerable<HLibAuthor> authors = GetAuthors(xRoot, namespaceManager);
            IEnumerable<HLibGenre> genres = GetGenres(xRoot, namespaceManager);
            string annotation = GetInnerTextFromNode(xRoot.SelectSingleNode("//fb:description/fb:title-info/fb:annotation[1]", namespaceManager));

            return new HLibBookFB2(title, authors, keyWords, genres, annotation, encoding);
        }

        private IEnumerable<HLibGenre> GetGenres(XmlElement xRoot, XmlNamespaceManager namespaceManager)
        {
            string genreName;
            ItemGenre genre;

            var nodeList = xRoot.SelectNodes("//fb:description/fb:title-info/fb:genre", namespaceManager);

            if (nodeList.Count > 0)
            {
                foreach (XmlNode genreType in nodeList)
                {
                    genreName = GetInnerTextFromNode(genreType);

                    if (!Enum.TryParse<ItemGenre>(genreName, true, out genre))
                    {
                        if (!Enum.TryParse<ItemGenre>("none", true, out genre))
                        {
                            continue;
                        }
                    }
                    yield return new HLibGenre(genre, genreName);
                }
            }
            yield break;
        }

        private IEnumerable<HLibAuthor> GetAuthors(XmlElement xRoot, XmlNamespaceManager namespaceManager)
        {
            string lastName, firstName, middleName;
            var nodeList = xRoot.SelectNodes("//fb:description/fb:title-info/fb:author", namespaceManager);

            if (nodeList.Count <= 0)
            {
                yield break;
            }

            foreach (XmlNode author in nodeList)
            {
                lastName = GetInnerTextFromNode(author.SelectSingleNode("./fb:last-name", namespaceManager));
                firstName = GetInnerTextFromNode(author.SelectSingleNode("./fb:first-name", namespaceManager));
                middleName = GetInnerTextFromNode(author.SelectSingleNode("./fb:middle-name", namespaceManager));

                yield return new HLibAuthor(lastName, firstName, middleName);
            }
        }

        private IEnumerable<string> GetKeyWords(XmlElement xRoot, XmlNamespaceManager namespaceManager)
        {
            XmlNode keyWords = xRoot.SelectSingleNode("//fb:description/fb:title-info/fb:keywords[1]", namespaceManager);
            string result = GetInnerTextFromNode(keyWords);


            if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
            {
                yield break;
            }

            string[] words = result.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                if (string.IsNullOrEmpty(word) || string.IsNullOrWhiteSpace(word))
                {
                    continue;
                }

                yield return word;
            }
            yield break;
        }

        private string GetInnerTextFromNode(XmlNode node)
        {
            if (node == null)
                return string.Empty;

            return node.InnerText;
        }

        private void GetXRoot(out XmlElement xRoot, out XmlNamespaceManager namespaceManager)
        {
            try
            {
                GetXmlDocument();
                xRoot = xmlDoc.DocumentElement;
                namespaceManager = new XmlNamespaceManager(new NameTable());
                namespaceManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.0");
            }
            catch (Exception e)
            {
                xRoot = null;
                namespaceManager = null;
                throw e;
            }
        }

        private void GetXmlDocument()
        {
            if (xmlDoc != null)
                return;

            byte[] byte4book;
            try
            {
                using (var ms = new MemoryStream())
                {
                    OpenFile();
                    Stream.Synchronized(stream).CopyTo(ms);
                    CloseFile();
                    byte4book = ms.ToArray();
                }
                LoadDocument(byte4book);
            }
            catch(ExceptionDataNotLoaded eData)
            {
                throw eData;
            }
            catch(ExceptionAccess eAccess)
            {
                throw eAccess;
            }
            catch
            {
                throw new ExceptionDataNotLoaded(Path, Name);
            }
        }

        private void LoadDocument(byte[] byte4book)
        {
             
            Encoding encoding = GetEncoding(byte4book);
            string file = encoding.GetString(byte4book, 0, byte4book.Length);
            int start = file.IndexOf("<? xml");
            if(start <= 0)
            {
                start = file.IndexOf("<?xml");
            }
            int end = file.IndexOf("<body>");

            if (start < 0 || end < 0)
            {
                throw new ExceptionDataNotLoaded(Path, Name);
            }
            else
            {
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(file.Substring(start, end) + "</FictionBook>");
            }
        }

        private Encoding GetEncoding(byte[] byte4book)
        {
            Encoding encoding = HLibFactory.GetEncoding(byte4book);
            if (encoding == Encoding.UTF8)
            {
                this.encoding = ItemEncoding.utf8;
            }
            else
            {
                this.encoding = ItemEncoding.win1251;
            }
            return encoding;
        }

        public override void CloseFile()
        {
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
            if(InArchive && _exclusive && _zip!=null)
            {
                _zip.Dispose();
            }
        }

        public override void OpenFile()
        {
            if (stream == null)
            {
                try
                {
                    if (InArchive)
                    {
                        stream = Zip_Entry.OpenReader();
                    }
                    else
                    {
                        stream = File.OpenRead(FullName);
                    }
                }
                catch
                {
                    stream = null;
                    throw new ExceptionAccess(Path, Name);
                }
            }
        }        
    }
}