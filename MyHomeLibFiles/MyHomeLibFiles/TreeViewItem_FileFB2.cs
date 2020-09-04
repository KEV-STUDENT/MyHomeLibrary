using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Threading.Tasks;

namespace MyHomeLibFiles
{    
    public class TreeViewItem_FileFB2 : TreeViewItem_File
    {
        private readonly object locker = new object();
        private ZipArchive zipArchive = null;
        private XmlDocument xmlDoc = null;

        public TreeViewItem_FileFB2(string str) : base(str)
        {
        }

        public TreeViewItem_FileFB2(string zip, string file) : base(zip, file)
        {
        }

        public TreeViewItem_FileFB2(string zip, string file, ZipArchive archive) : base(zip, file)
        {
            if (archive != null && archive.Mode == ZipArchiveMode.Read)
            {
                zipArchive = archive;
            }
        }

        public TreeViewItem_FileFB2(string zip, string file, XmlDocument xDoc) : base(zip, file)
        {
            xmlDoc = xDoc;
        }

        private XmlDocument GetXmlDocument()
        {
            if (xmlDoc != null)
                return xmlDoc;

            var xDoc = new XmlDocument();
            //lock (locker) {
                if (type == ItemType.InZip)
                {
                    if (zipArchive == null)
                    {
                        using (ZipArchive archive = ZipFile.Open(path, ZipArchiveMode.Read))
                        {
                            xDoc = LoadFromArchive(archive);
                        }
                    }
                    else
                    {
                        xDoc = LoadFromArchive(zipArchive);

                    }
                }
                else
                {
                    xDoc.Load(path);
                }
            //}

            xmlDoc = xDoc;
            return xDoc;
        }

        private XmlDocument LoadFromArchive(ZipArchive zip)
        {
            string str = "";
            var xDoc = new XmlDocument();
            ZipArchiveEntry entry = zip.GetEntry(name);
            using (Stream st = entry.Open())
            {
                try
                {
                    using (StreamReader reader = new StreamReader(st))
                    {
                        str = reader.ReadToEnd();
                    }                    
                }
                catch (Exception e)
                {
                    State = ItemState.Error;
                    //Debug.WriteLine(e.Message);
                    //Debug.WriteLine(name);
                }
            }

            if (State != ItemState.Error)
            {
                xDoc.Load(str);
            }
            return xDoc;
        }

        private void GetXRoot(out XmlElement xRoot, out XmlNamespaceManager namespaceManager)
        {
            try
            {
                var xDoc = GetXmlDocument();
                xRoot = xDoc.DocumentElement;
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
    }
}