using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace MyHomeLibFiles
{
    public class TreeViewItem_FileFB2 : TreeViewItem_File
    {
        public TreeViewItem_FileFB2(string str) : base(str)
        {
        }

        public TreeViewItem_FileFB2(string zip, string file) : base(zip, file)
        {
        }

        private XmlDocument GetXmlDocument()
        {
            var xDoc = new XmlDocument();
            if (type == ItemType.InZip)
            {
                using (ZipArchive archive = ZipFile.Open(path, ZipArchiveMode.Read))
                {
                    ZipArchiveEntry entry = archive.GetEntry(name);
                    using (Stream st = entry.Open())
                    {
                        byte[] bt = new byte[6];
                        xDoc.Load(st);
                    }
                }
            }
            else
            {
                xDoc.Load(path);
            }
            return xDoc;
        }

        public override IEnumerable<string> GetChilds()
        { 
            var xDoc = GetXmlDocument();
            var xRoot = xDoc.DocumentElement;
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.0");
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
            var xDoc = GetXmlDocument();
            var xRoot = xDoc.DocumentElement;
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            
            namespaceManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.0");

            string lastName, firstName, middleName;
            TreeViewItem_Attribute authorItem;

            var nodeList = xRoot.SelectNodes("//fb:description/fb:title-info/fb:author", namespaceManager);
            if (nodeList.Count > 0)
            {
                foreach (XmlNode author in nodeList)
                {
                    lastName = GetInnerTextFromNode(author.SelectSingleNode("./fb:last-name", namespaceManager));
                    firstName = GetInnerTextFromNode(author.SelectSingleNode("./fb:first-name", namespaceManager));
                    middleName = GetInnerTextFromNode(author.SelectSingleNode("./fb:middle-name", namespaceManager));

                    authorItem = new TreeViewItem_Attribute(string.Format("{0} {1} {2}",
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