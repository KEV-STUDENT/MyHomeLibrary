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

            var nodeList = xRoot.SelectNodes("//fb:description/fb:title-info/fb:author", namespaceManager);
            if (nodeList.Count > 0)
            {
                attribute = "Author:";
                foreach (XmlNode author in nodeList)
                {
                    attribute = string.Format("{0} {1} {2} {3},", attribute,
                        GetInnerTextFromNode(author.SelectSingleNode("//fb:last-name", namespaceManager)),
                        GetInnerTextFromNode(author.SelectSingleNode("//fb:first-name", namespaceManager)),
                        GetInnerTextFromNode(author.SelectSingleNode("//fb:middle-name", namespaceManager)));
                }
                yield return attribute.TrimEnd(',');
            }

            XmlNode book = xRoot.SelectSingleNode("//fb:description/fb:title-info/fb:book-title[1]", namespaceManager);
            yield return string.Format("Title: {0}", GetInnerTextFromNode(book));

            book = xRoot.SelectSingleNode("//fb:description/fb:title-info/fb:keywords[1]", namespaceManager);
            yield return string.Format("Genre: {0}", GetInnerTextFromNode(book));

            book = xRoot.SelectSingleNode("//fb:description/fb:title-info/fb:annotation[1]", namespaceManager);
            yield return string.Format("Annotation: {0}", GetInnerTextFromNode(book));

            nodeList = xRoot.SelectNodes("//fb:description/fb:title-info/fb:genre", namespaceManager);
            if (nodeList.Count > 0)
            {
                attribute = "Genre[fb2] :";
                foreach (XmlNode genreType in nodeList)
                {
                    attribute = string.Format("{0} {1},", attribute, GetInnerTextFromNode(genreType));
                }
                yield return attribute.TrimEnd(',');
            }
        }

        private string GetInnerTextFromNode(XmlNode node)
        {
            if (node == null)
                return string.Empty;

            return node.InnerText;
        }
    }
}