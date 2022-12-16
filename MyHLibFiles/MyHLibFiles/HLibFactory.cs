using System;
using System.IO;
using System.Linq;
using Ionic.Zip;
using System.Diagnostics;
using System.Text;

namespace MyHLibFiles
{
    public static class HLibFactory
    {
        static readonly byte[] zipSignature = { 0x50, 0x4B, 0x03, 0x04 };
        static readonly byte[] fb2Signature = { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20 };
        static readonly object locker = new object();

        public static HLibDiscItem GetDiskItem(string path, string name)
        {
            string fullName = Path.Combine(path, name);

            if (!Directory.Exists(fullName) && !File.Exists(fullName))
            {
                throw new ExceptionPath(path, name);
            }

            FileAttributes attr = File.GetAttributes(fullName);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                return new HLibDirectory(path, name);
            }

            try
            {
                byte[] file = new byte[4];

                FileStream fileStream = new FileStream(fullName, FileMode.Open);
                fileStream.Read(file, 0, 4);
                fileStream.Close();

                if (Enumerable.SequenceEqual(file, zipSignature))
                {
                    return new HLibFileZIP(path, name);
                }

                file = new byte[6];
                fileStream = new FileStream(fullName, FileMode.Open);
                fileStream.Read(file, 0, 6);
                fileStream.Close();
                if (Enumerable.SequenceEqual(file, fb2Signature))
                {
                    return new HLibFileFB2(path, name);
                }

                throw new NotSupportedException();
            }
            catch (NotImplementedException e)
            {
                throw e;
            }
            catch (NotSupportedException e)
            {
                throw e;
            }
            catch
            {
                throw new ExceptionAccess(path, name);
            }
        }

        public static HLibDiscItem GetDiskItem(string path, string name, bool inArchive)
        {
            if (inArchive)
            {
                FileInfo fileInfo = new FileInfo(path);
                HLibFileZIP zip = (HLibFileZIP)GetDiskItem(fileInfo.DirectoryName, fileInfo.Name);
                ZipEntry entry = zip.GetEntryByName(name);
                return GetDiskItem(zip, entry, true);
            }    
            else
            {
                return GetDiskItem(path, name);
            }
        }

        public static HLibDiscItem GetDiskItem(HLibFileZIP zip, ZipEntry entry)
        {
            return GetDiskItem(zip, entry, false);
        }

        public static HLibDiscItem GetDiskItem(HLibFileZIP zip, ZipEntry entry, bool excl)
        {
            if (zip == null || entry == null)
            {
                throw new ArgumentNullException();
            }

            if (!File.Exists(zip.FullName))
            {
                throw new ExceptionPath(zip.Path, zip.Name);
            }

            if (entry.IsDirectory)
            {
                return new HLibDirectory(zip, entry);
            }
            else
            {

                string file = "";
                using (var st = entry.OpenReader())
                {
                    byte[] bt = new byte[6];
                    st.Read(bt, 0, 6);
                    Encoding encoding = GetEncoding(bt);
                    file = encoding.GetString(bt, 0, bt.Length);
                }


                    if (file.ToLower().Trim(' ').Substring(1, 4) == "?xml")
                    {
                        return new HLibFileFB2(zip, entry, excl);
                    }

                    Debug.WriteLine("|" + file.ToLower().Trim(' ').Substring(1, 5) + "|");
                    Debug.WriteLine(file.ToLower().Trim(' ').Substring(1, 5) == "<?xml");
                    Debug.WriteLine(file.ToLower().Trim(' ').Substring(1, 6) == "<? xml");
                    Debug.WriteLine(String.Compare(file.Trim(' ').Substring(1, 5), "<?xml",
                       true, new System.Globalization.CultureInfo("en-US")));
                    //StringComparison.OrdinalIgnoreCase));
                    Debug.WriteLine("{0}  -  {1}", file.Trim(' ').Substring(1, 5).Length, "<?xml".Length);
                
            }
            throw new NotSupportedException();
        }

        public static Encoding GetEncoding(byte[] byte4book)
        {
            Encoding encoding = Encoding.Default;
            if (byte4book.Length > 37)
            {
                if ((byte4book[30] == 85 || byte4book[30] == 117) &&
                    (byte4book[31] == 84 || byte4book[31] == 116) &&
                    (byte4book[32] == 70 || byte4book[32] == 102) &&
                    byte4book[33] == 45 && byte4book[34] == 56 && byte4book[35] == 34 &&
                    byte4book[36] == 63 && byte4book[37] == 62)
                {
                    encoding = Encoding.UTF8;
                }
                else
                {
                    encoding = Encoding.GetEncoding("Windows-1251");
                }
            }
            return encoding;
        }
    }
}