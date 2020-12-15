using System;
using System.IO;
using System.Linq;
using Ionic.Zip;

namespace MyHLibFiles
{
    public static class HLibFactory
    {
        static readonly byte[] zipSignature = { 0x50, 0x4B, 0x03, 0x04 };
        static readonly byte[] fb2Signature = { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20 };

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

        public static HLibDiscItem GetDiskItem(HLibFileZIP zip, ZipEntry entry)
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
                using (var st = entry.OpenReader())
                {
                    byte[] bt = new byte[6];
                    st.Read(bt, 0, 6);
                    if (Enumerable.SequenceEqual(bt, fb2Signature))
                    {
                        return new HLibFileFB2(zip, entry);
                    }
                }                
            }
            throw new NotSupportedException();
        }
    }
}