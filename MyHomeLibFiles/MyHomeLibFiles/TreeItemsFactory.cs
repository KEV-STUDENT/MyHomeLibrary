using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.IO.Compression;

namespace MyHomeLibFiles
{
    public class TreeItemsFactory
    {
        static readonly byte[] zipSignature = { 0x50, 0x4B, 0x03, 0x04 };
        static readonly byte[] fb2Signature = { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20 };

        public static ITreeViewItem GetItem(string str)
        {
            if(!Directory.Exists(str) && !File.Exists(str))
            {
                return new TreeViewItem_Attribute(str);
            }

            FileAttributes attr = File.GetAttributes(str);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                return new TreeViewItem_Directory(str);
            }

            ITreeViewItem item;
            try
            {
                byte[] file = new byte[4];
               
                FileStream fileStream = new FileStream(str, FileMode.Open);
                fileStream.Read(file, 0, 4);
                fileStream.Close();

                if (Enumerable.SequenceEqual(file, zipSignature))
                {
                    item = new TreeViewItem_FileZIP(str);
                }
                else
                {
                    file = new byte[6];
                    fileStream = new FileStream(str, FileMode.Open);
                    fileStream.Read(file, 0, 6);
                    fileStream.Close();

                    if (Enumerable.SequenceEqual(file, fb2Signature))
                    {
                        item = new TreeViewItem_FileFB2(str);
                    }
                    else
                    {
                        item = new TreeViewItem_File(str);
                    }
                }
            }
            catch
            {
                item = new TreeViewItem_File(str);
                item.State = ItemState.Error;
            }
            return item;
        }

        public static ITreeViewItem GetItem()
        {
            return new TreeViewItem_Empty();
        }

        public static ITreeViewItem GetItem(string zip, string file)
        {
            bool isFB2;
            ITreeViewItem item;

            isFB2 = file.Substring(file.Length - 3).Equals("fb2", StringComparison.OrdinalIgnoreCase);
            /*using (ZipArchive archive = ZipFile.Open(zip, ZipArchiveMode.Read))
            {
                ZipArchiveEntry entry = archive.GetEntry(file);
                using (Stream st = entry.Open())
                {
                    byte[] bt = new byte[6];
                    st.Read(bt, 0, 6);
                    isFB2 = Enumerable.SequenceEqual(bt, fb2Signature);
                }
            }*/

            if (isFB2)
                item = new TreeViewItem_FileFB2(zip, file);
            else
                item = new TreeViewItem_File(zip, file);

            return item;    
        }
    }
}