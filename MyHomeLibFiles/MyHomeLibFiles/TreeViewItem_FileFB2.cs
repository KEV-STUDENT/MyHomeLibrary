using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Threading.Tasks;
using System.Security.Policy;
using System.Dynamic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using MyHomeLibCommon;
using MyDBModel;

namespace MyHomeLibFiles
{
    public class TreeViewItem_FileFB2 : TreeViewItem_File
    {
        private readonly object locker = new object();
        private Ionic.Zip.ZipFile zipArchive = null;
        private XmlDocument xmlDoc = null;
        private byte[] byte4book;
        private ItemEncoding encoding = ItemEncoding.none;
        public TreeViewItem_FileFB2(string str) : base(str)
        {
        }

        public TreeViewItem_FileFB2(string zip, string file) : base(zip, file)
        {
        }

        public TreeViewItem_FileFB2(string zip, string file, Ionic.Zip.ZipFile archive) : base(zip, file)
        {
            if (archive != null)
            {
                zipArchive = archive;
            }
        }

        public TreeViewItem_FileFB2(string zip, string file, XmlDocument xDoc) : base(zip, file)
        {
            xmlDoc = xDoc;
        }

        public TreeViewItem_FileFB2(string zip, string file, byte[] array) : base(zip, file)
        {
            Byte4Book = array;
           
        }

        public byte[] Byte4Book
        {
            get { return byte4book; }
            set
            {
                string file = "                                                     ";
                Encoding encoding = Encoding.Default;

                byte4book = value;
                if (byte4book.Length > 37)
                {
                    if ((byte4book[30] == 85 || byte4book[30] == 117) &&
                        (byte4book[31] == 84 || byte4book[31] == 116) &&
                        (byte4book[32] == 70 || byte4book[32] == 102) &&
                        byte4book[33] == 45 && byte4book[34] == 56 && byte4book[35] == 34 &&
                        byte4book[36] == 63 && byte4book[37] == 62)
                    {
                        encoding = Encoding.UTF8;
                        this.encoding = ItemEncoding.utf8;
                    }
                    else
                    {
                        encoding = Encoding.GetEncoding("Windows-1251");
                        this.encoding = ItemEncoding.win1251;
                    }
                }

                try
                {
                    xmlDoc = new XmlDocument();
                    file = encoding.GetString(byte4book, 0, byte4book.Length);
                    byte4book = new byte[0];
                    //Debug.WriteLine("{0}   {1}   {2}    {3}", 
                    //    file.IndexOf("<body>"), 
                    //    file.IndexOf("</body>"), 
                    //    this.encoding,
                    //    this.Name);
                    int start = file.IndexOf("<? xml");
                    int end = file.IndexOf("<body>");

                    if (start < 0 || end < 0)
                    {
                        State = ItemState.Error;

                    }
                    else
                    {
                        file = file.Substring(start, end) + "</FictionBook>";
                        //+ "<body>" +
                        //file.Substring(file.IndexOf("</body>"));

                        // Debug.WriteLine(file);

                        xmlDoc.LoadXml(file);

                    }
                }
                catch (Exception e)
                {                    
                    Debug.WriteLine("===========");
                    Debug.WriteLine(Path);
                    Debug.WriteLine(file.Substring(0, 20));
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(e.StackTrace);
                    State = ItemState.Error;
                }
            }
        }

        public ItemEncoding Encoding4File
        {
            get { return encoding; }
        }
        private void GetXmlDocument()
        {
            if (xmlDoc != null)
                return;
            
            if (type == ItemType.InZip)
            {
                if (zipArchive == null)
                {
                    using (Ionic.Zip.ZipFile archive = Ionic.Zip.ZipFile.Read(path))
                    {
                        LoadFromArchive(archive);
                    }
                }
                else
                {
                    LoadFromArchive(zipArchive);
                }
            }
            else
            {
                LoadFromFB2(path);
            }
        }

        private void LoadFromFB2(string path)
        {
            using (Stream st = File.OpenRead(path))
            {
                LoadFromStream(st);
            }
        }

        private void LoadFromArchive(Ionic.Zip.ZipFile zip)
        {
            var entry = zip[name];
            using (Stream st = entry.OpenReader())
            {
                LoadFromStream(st);
            }
        }

        private void LoadFromStream(Stream st)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    Stream.Synchronized(st).CopyTo(ms);
                    Byte4Book = ms.ToArray();
                    byte4book = new byte[0];
                }
            }
            catch (Exception e)
            {
                State = ItemState.Error;
            }
        }

        private void GetXRoot(out XmlElement xRoot, out XmlNamespaceManager namespaceManager)
        {
            try
            {
                GetXmlDocument();
                xRoot = xmlDoc.DocumentElement;
 //Debug.WriteLine(xRoot.GetAttribute("encoding"));
                namespaceManager = new XmlNamespaceManager(new NameTable());
                namespaceManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.0");
            }
            catch (Exception e)
            {
                xRoot = null;
                namespaceManager = null;
                State = ItemState.Error;
            }
        }

        public override IEnumerable<string> GetChilds()
        {
            XmlElement xRoot;
            XmlNamespaceManager namespaceManager;
 //Debug.WriteLine("Read 5  - 1");
            GetXRoot(out xRoot, out namespaceManager);
 //Debug.WriteLine("Read 5  - 2");
            if (xRoot == null || namespaceManager == null)
            { 
                yield break;
            }

            string attribute;
            XmlNode book = xRoot.SelectSingleNode("//fb:description/fb:title-info/fb:book-title[1]", namespaceManager);
            yield return string.Format("Title: {0}", GetInnerTextFromNode(book));

            book = xRoot.SelectSingleNode("//fb:description/fb:title-info/fb:keywords[1]", namespaceManager);
            yield return string.Format("Genre: {0}", GetInnerTextFromNode(book));

            book = xRoot.SelectSingleNode("//fb:description/fb:title-info/fb:annotation[1]", namespaceManager);
            yield return string.Format("Annotation: {0}", GetInnerTextFromNode(book));

            var nodeList = xRoot.SelectNodes("//fb:description/fb:title-info/fb:genre", namespaceManager);
            if (nodeList.Count > 0)
            {
                attribute = "Genre[fb2]:";
                foreach (XmlNode genreType in nodeList)
                {
                    yield return string.Format("{0} {1}", attribute, GetInnerTextFromNode(genreType));
                }                
            }
        }

        public IEnumerable<TreeViewItem_Attribute> GetAuthors()
        {
            XmlElement xRoot;
            XmlNamespaceManager namespaceManager;

            GetXRoot(out xRoot, out namespaceManager);
            if (xRoot == null || namespaceManager == null)
            {
                yield break;
            }

            string lastName, firstName, middleName;
            var nodeList = xRoot.SelectNodes("//fb:description/fb:title-info/fb:author", namespaceManager);

            if (nodeList.Count > 0)
            {
                foreach (XmlNode author in nodeList)
                {
                    lastName = GetInnerTextFromNode(author.SelectSingleNode("./fb:last-name", namespaceManager));
                    firstName = GetInnerTextFromNode(author.SelectSingleNode("./fb:first-name", namespaceManager));
                    middleName = GetInnerTextFromNode(author.SelectSingleNode("./fb:middle-name", namespaceManager));

                    var authorItem = new TreeViewItem_Attribute(string.Format("{0} {1} {2}",
                        lastName, firstName, middleName), AttributeType.Author);

                    authorItem.AddChild(new TreeViewItem_Attribute(lastName, AttributeType.LastName));
                    authorItem.AddChild(new TreeViewItem_Attribute(firstName, AttributeType.FirstName));
                    authorItem.AddChild(new TreeViewItem_Attribute(middleName, AttributeType.MiddleName));

                    yield return authorItem;
                }
            }        
        }

        public override List<ITreeViewItem> GetChilds_Items()
        {
            List<ITreeViewItem> list = new List<ITreeViewItem>();
            foreach (string item in GetChilds())
            {
                list.Add(new TreeViewItem_Attribute(item));
            }
            foreach(var author in GetAuthors())
            {
                list.Add(author);
            }
            return list;
        }


        private string GetInnerTextFromNode(XmlNode node)
        {
            if (node == null)
                return string.Empty;

            return node.InnerText;
        }


        public override List<Book> GetChilds_Books()
        {
            List<Book> books = new List<Book>();
            
            books.Add(GetBook());

            return books;
        }

        public Book GetBook()
        {
            TreeViewItem_Attribute attr;
            string caption;
            Book book = new Book() { Caption = "<Unknown>", EntityName = name};

            foreach (var i in GetChilds_Items())
            {
                attr = i as TreeViewItem_Attribute;

                if (attr != null && attr.Type == ItemType.Attribute)
                {
                    switch (attr.AttributeType)
                    {
                        case AttributeType.Author:
                            AddAuthor2Book(attr, book);
                            break;
                        case AttributeType.Title:
                            caption = attr.AttributeValue.Trim();
                            if (string.IsNullOrEmpty(caption) != true)
                            {
                                book.Caption = caption;
                            }
                            break;
                        case AttributeType.GenreFB2:
                            AddGenreFB2Book(attr, book);
                            break;
                        case AttributeType.Genre:
                            AddKeyWords2Book(attr, book);
                            break;
                        default:
                            break;
                    }
                }
            }

            book.Encoding = Encoding4File;

            return book;
        }

        private void AddAuthor2Book(TreeViewItem_Attribute authorItem, Book book)
        {

            bool isAuthor = false;
            Author author = new Author();
            TreeViewItem_Attribute attr;

            foreach (var info in authorItem.GetChilds_Items())
            {
                attr = info as TreeViewItem_Attribute;
                if (attr != null)
                {
                    switch (attr.AttributeType)
                    {
                        case AttributeType.FirstName:
                            author.FirstName = attr.AttributeValue;
                            isAuthor = true;
                            break;
                        case AttributeType.LastName:
                            author.LastName = attr.AttributeValue;
                            isAuthor = true;
                            break;
                        case AttributeType.MiddleName:
                            author.MiddleName = attr.AttributeValue;
                            isAuthor = true;
                            break;
                        case AttributeType.NickName:
                            author.NickName = attr.AttributeValue;
                            isAuthor = true;
                            break;
                        case AttributeType.EMail:
                            author.EMail = attr.AttributeValue;
                            isAuthor = true;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!isAuthor || book.Authors.Contains(author))
                return;

            book.Authors.Add(author);
        }

        private void AddGenreFB2Book(TreeViewItem_Attribute genreItem, Book book)
        {
            ItemGenre genre;
            Genre genre4Book;

            if (!Enum.TryParse<ItemGenre>(genreItem.AttributeValue, true, out genre))
            {
                if (!Enum.TryParse<ItemGenre>("none", true, out genre))
                {
                    return;
                }
            }

            genre4Book = new Genre() { Key = genre, Code = genreItem.AttributeValue };
            if (book.Genres.Contains(genre4Book))
                return;

            book.Genres.Add(genre4Book);
        }

        private void AddKeyWords2Book(TreeViewItem_Attribute attr, Book book)
        {
            KeyWord keyWord;

            if (string.IsNullOrEmpty(attr.AttributeValue) || string.IsNullOrWhiteSpace(attr.AttributeValue))
            {
                return;
            }

            string[] words = attr.AttributeValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                if (string.IsNullOrEmpty(word) || string.IsNullOrWhiteSpace(word))
                {
                    continue;
                }

                keyWord = new KeyWord() { Word = word };
                if (book.KeyWords.Contains(keyWord))
                {
                    continue;
                }

                book.KeyWords.Add(keyWord);
            }
        }
    }
}
