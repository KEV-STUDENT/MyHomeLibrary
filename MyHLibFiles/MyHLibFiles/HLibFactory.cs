using System;
using System.IO;
using System.Linq;

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
                else
                {
                    file = new byte[6];
                    fileStream = new FileStream(fullName, FileMode.Open);
                    fileStream.Read(file, 0, 6);
                    fileStream.Close();
                    if (Enumerable.SequenceEqual(file, fb2Signature))
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                }
            }
            catch (NotImplementedException e)
            {
                throw e;
            }
            catch (NotSupportedException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ExceptionAccess(path, name);
            }
        }
    }
}