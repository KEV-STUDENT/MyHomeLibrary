using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyHomeLibFiles;
using MyDBModel;
using MyHomeLibCommon;

using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Xml;
using System.Collections.Concurrent;

using System.Data.Entity.Validation;

namespace MyHomeLibBizLogic
{
    public class MyDBUpdater
    {
        private string fileDestination;
        private string fileSource;
        private bool addDuplicated;

        private readonly object locker = new object();

        public MyDBUpdater()
        {
        }

        public MyDBUpdater(bool addDupBook)
        {
            this.addDuplicated = addDupBook;
        }

        public MyDBUpdater(string file_SQLite, string file_ZIP, bool addDupBook)
        {
            this.fileDestination = file_SQLite;
            this.fileSource = file_ZIP;
            this.addDuplicated = addDupBook;
        }

        public MyDBUpdater(string file_SQLite, string file_ZIP):this(file_SQLite, file_ZIP, false)
        {
        }

        public string FileSource
        {
            get {return fileSource; }
            set { fileSource = value; }
        }

        public string FileDestination
        {
            get {return fileDestination; }
            set {fileDestination = value; }
        }

        public bool ProcessUpdate()
        {
            if(string.IsNullOrEmpty(fileDestination))
            {
                throw new ArgumentOutOfRangeException("fileDestination", "Wrong database name");
            }

            if(string.IsNullOrEmpty(fileSource))
            {
                throw new ArgumentOutOfRangeException("fileSource", "Wrong source file name");
            }

            ITreeViewItem item = TreeItemsFactory.GetItem(fileSource);
            if(item.State == ItemState.Error)
            {
                throw new ArgumentOutOfRangeException("fileSource", "Wrong source file name");
            }

            bool result = false;
            string libPathTest = "Data Source=" + fileDestination + "; version = 3 ";

            using (DBModel db = new DBModel(libPathTest))
            {
                switch (item.Type)
                {
                    case ItemType.Zip:
                        result = true;                        
                        FillContextFromFileZipAsync(db, (TreeViewItem_FileZIP)item);
                        break;
                    case ItemType.File:
                        throw new NotImplementedException();
                        break;
                    case ItemType.Directory:
                        throw new NotImplementedException();
                        break;
                }

                if (result)
                {
                    //Debug.WriteLine("Books {0}", db.Books.Local.Count());
                    //foreach (var b in db.Books.Local)
                    //{
                    //    Debug.WriteLine(b.Caption);
                    //}
                    try
                    {
                        //foreach(var k in db.KeyWords.Local)
                        //{
                        //    Debug.WriteLine(k.Word);
                        //}
                        //Debug.WriteLine(db.Authors.Local.Count());
                        //Debug.WriteLine(db.KeyWords.Local.Count());

                        //var kw = from k in db.KeyWords.Local
                        //            group k by k.Word into k1
                        //            select new { Wors = k1.Key, Cnt = k1.Count() };

                        //Debug.WriteLine(kw.Count());

                        //foreach (var k in kw)
                        //{
                        //    Debug.WriteLine("{0} : {1}", k.Wors, k.Cnt);
                        //}
                        ////db.SaveChangesAsync();
                        //Debug.WriteLine("-=Save=-");
                        db.SaveChangesAsync();
                    }
                    //catch(Exception e)
                    //{
                    //    Debug.WriteLine(" 2 :" + e.Message);
                    //}
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                            }
                        }
                    }
                }
            }
            return result;
            ;
        }


        //private void FillContextFromFile(DBModel db, TreeViewItem_FileZIP item)
        //{
        //    using (FileStream zipToOpen = new FileStream(item.Path, FileMode.Open))
        //    {
        //        using (ZipArchive zipArchive =
        //            new ZipArchive(FileStream.Synchronized((Stream)zipToOpen), ZipArchiveMode.Read))
        //        {
        //            XmlDocument xDoc;
        //            string t;
        //            foreach (var entry in zipArchive.Entries)
        //            {
        //                try
        //                {
        //                    xDoc = new XmlDocument();
        //                    t = ReadFileFromZip(entry);
        //                    xDoc.LoadXml(t);
        //                    FillContextFromItemView(db, new TreeViewItem_FileFB2(item.Path, entry.Name, xDoc), entry);
        //                }
        //                catch (Exception e)
        //                {
        //                    Debug.WriteLine("1  " + e.Message);                            
        //                }
        //            }
        //        }
        //    }
        //}
        private void FillContextFromFileZipAsync(DBModel db, TreeViewItem_FileZIP item)
        {
            db.Books.Include("KeyWords").Include("Authors").Include("Genres").ToList();

            using (FileStream zipToOpen = new FileStream(item.Path, FileMode.Open))
            {
                using (ZipArchive zipArchive =
                    new ZipArchive(FileStream.Synchronized((Stream)zipToOpen), ZipArchiveMode.Read))
                {
                    ParallelOptions po = new ParallelOptions
                    {
                        MaxDegreeOfParallelism =
                        Environment.ProcessorCount > 2 ? Environment.ProcessorCount - 1 : 1
                    };

                    Parallel.ForEach(zipArchive.Entries, po,
                       (entry) =>
                       {
                           try
                           {
                               var xDoc = new XmlDocument();
                               xDoc.LoadXml(ReadFileFromZip(entry));
                               var itemFB2 = new TreeViewItem_FileFB2(item.Path, entry.Name, xDoc);

                               lock (locker)
                               {
                                   FillContextFromItemView(db, itemFB2, entry);
                               }
                           }
                           catch (Exception e)
                           {
                               Debug.WriteLine("1  " + e.Message);
                           }
                       });
                }
            }
        }

        private string ReadFileFromZip(ZipArchiveEntry x)
        {
            using (StreamReader reader = new StreamReader(Stream.Synchronized(x.Open()), Encoding.GetEncoding(1251)))
            {
                return StreamReader.Synchronized(reader).ReadToEnd();
            }
        }

        private bool FillContextFromFile(DBModel db, TreeViewItem_FilesUnion item)
        {
            bool result = true;
            foreach (var i in item.GetChilds_Items())
            {
                try
                {
                    FillContextFromItemView(db, i);
                }
                catch(Exception e) 
                {
                    //Debug.WriteLine("   Error    " + i.Name);
                    Debug.WriteLine(e.Message);
                   // throw e;
                }
            }
            return result;
        }

        public int FillContextFromItemView(DBModel db, ITreeViewItem item, ZipArchiveEntry entry=null)
        {
            int result = 0;
            bool addBook;
            TreeViewItem_File file;

            var book = AddDataToTables(db, item, out file);

            CheckBooksAuthors(db, book);
            CheckBooksKeyWords(db, book);

            if (addDuplicated)
            {
                addBook = true;
            }
            else
            {
                addBook = CheckBookInDB(db, book);
            }

            if (addBook)
            {
                if (entry == null)
                {
                    book.BookFile = file.GetFile4Book();
                }
                else
                {
                    book.BookFile = file.GetFile4Book(entry);
                }
                db.Books.Local.Add(book);
            }

            return result;
        }

        private Book AddDataToTables(DBModel db, ITreeViewItem item, out TreeViewItem_File file)
        {
            file = item as TreeViewItem_File;
            if (file == null)
            {
                throw new ArgumentException("Invalid item");
            }
           
            TreeViewItem_Attribute attr;
            Book book = new Book() { Caption = "<Unknown>"};

            var list = item.GetChilds_Items();
            foreach (var i in item.GetChilds_Items())
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
                            book.Caption = attr.AttributeValue;
                            break;
                        case AttributeType.GenreFB2:
                            AddGenreFB2Book(attr, book, db);
                            break;
                        case AttributeType.Genre:
                            AddKeyWords2Book(attr, book);
                            break;
                        default:
                            break;
                    }
                }
            }

            return book;
        }

        private void CheckBooksKeyWords(DBModel db, Book book)
        {
            KeyWord keyWordDB;
            List<KeyWord> list = new List<KeyWord>();
            bool updateList = false;

            foreach (KeyWord keyWord in book.KeyWords)
            {
                keyWordDB = db.KeyWords.Local.FirstOrDefault<KeyWord>(
                    p => p.Word.ToUpper() == keyWord.Word.ToUpper()
                    );
                if(keyWordDB == null)
                {                  
                    db.KeyWords.Add(keyWord);
                    keyWordDB = keyWord;
                }
                else 
                {
                    updateList = true;
                }
                list.Add(keyWordDB);
            }

            if (list.Count() > 0)
            {
                if (updateList)
                {
                    book.KeyWords.Clear();
                    foreach (var k in list)
                    {
                        book.KeyWords.Add(k);
                    }
                }
            }
        }

        private void CheckBooksAuthors(DBModel db, Book book)
        {
            Author authorDB;
            List<Author> list = new List<Author>();
            bool updateList = false;

            foreach(Author author in book.Authors)
            {
                authorDB = db.Authors.Local.FirstOrDefault<Author>(
                    p => p.LastName == author.LastName &&
                    p.FirstName == author.FirstName &&
                    p.MiddleName == author.MiddleName
                    );

                if (authorDB == null)
                {
                    db.Authors.Local.Add(author);
                    authorDB = author;
                }
                else 
                {
                    updateList = true; 
                }
                list.Add(authorDB);
            }

            if(updateList)
            {
                book.Authors.Clear();
                foreach(var a in list)
                {
                    book.Authors.Add(a);
                }
            }
        }

        private bool CompareDbSets<T>(List<T> db1, List<T> db2)
        {
            db2.Sort();
            return db1.SequenceEqual(db2);
        }
        private bool CheckBookInDB(DBModel db, Book book)
        {
            List<Genre> listgb, g;
            List<Author> listab;

            listgb = book.Genres.ToList();
            listgb.Sort();

            listab = book.Authors.ToList();
            listab.Sort();

            var res = from books in db.Books.Local
                      where books.Caption == book.Caption &&
                      CompareDbSets(listgb, books.Genres.ToList()) &&
                      CompareDbSets(listab, books.Authors.ToList())
                      select books;

            return (res.Count() == 0);
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

            if (isAuthor)
            {
                book.Authors.Add(author);
            }
        }

        private void AddGenreFB2Book(TreeViewItem_Attribute genreItem, Book book, DBModel db)
        {
            ItemGenre genre;

            if (!Enum.TryParse<ItemGenre>(genreItem.AttributeValue, true, out genre))
            {
                if (!Enum.TryParse<ItemGenre>("none", true, out genre))
                {
                    return;
                }
            }

            Genre genre4Book = db.Genres.Find(genre);

            if (genre4Book != null)
            {
                book.Genres.Add(genre4Book);
            }
        }

        private void AddKeyWords2Book(TreeViewItem_Attribute attr, Book book)
        {
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
                book.KeyWords.Add(new KeyWord() { Word = word });
            }
        }
    }
}
