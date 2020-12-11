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
using System.Xml.Linq;
using System.Collections.Concurrent;

using System.Data.Entity.Validation;
using Ionic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace MyHomeLibBizLogic
{
    public class MyDBUpdater
    {
        private string fileDestination;
        private string fileSource;
        private bool addDuplicated;
        private bool checkUnknown4Duplicated;

        private readonly object lockerZip = new object();
        //private readonly object locker = new object();

        public MyDBUpdater()
        {
        }

        public MyDBUpdater(bool addDupBook, bool checkUnknown4Duplicated = false)
        {
            this.addDuplicated = addDupBook;
            this.checkUnknown4Duplicated = checkUnknown4Duplicated;
        }

        public MyDBUpdater(string file_SQLite, string file_Source, bool addDupBook, bool checkUnknown4Duplicated = false)
        {
            this.fileDestination = file_SQLite;
            this.fileSource = file_Source;
            this.addDuplicated = addDupBook;
            this.checkUnknown4Duplicated = checkUnknown4Duplicated;
        }

        public MyDBUpdater(string file_SQLite, string file_Source) :this(file_SQLite, file_Source, false, false)
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

        //public bool ProcessUpdate2()
        //{
        //    ITreeViewItem item;
        //    try
        //    {
        //        item = GetInitItem();
        //    }catch(ArgumentOutOfRangeException e)
        //    {
        //        throw e;
        //        //return false;
        //    }

        //    Debug.WriteLine(item.Name);

        //    IEnumerable<TreeViewItem_FileFB2> listFB2 = GetListFB2(item);
        //    if (listFB2.Count() == 0)
        //    {
        //        return true;
        //    }

        //    Debug.WriteLine(listFB2.Count());

        //    string libPathTest = "Data Source=" + fileDestination + "; version = 3 ";
        //    using (DBModel db = new DBModel(libPathTest))
        //    {
        //        db.Genres.Load();               

        //        //foreach (var fb2 in listFB2)
        //        //{
        //        //    ProcessUpdateFB2(fb2, db);
        //        //}

        //        ParallelOptions po = new ParallelOptions
        //        {
        //            MaxDegreeOfParallelism =
        //               Environment.ProcessorCount > 2 ? Environment.ProcessorCount - 1 : 1
        //        };

        //        Parallel.ForEach(listFB2, po, entry =>
        //        {
        //            ProcessUpdateFB2(entry, db);
        //        });
        //        Debug.WriteLine("! End !");
        //    }
        //    return true;
        //}

        //public bool ProcessUpdate3()
        //{
        //    ITreeViewItem item;
        //    try
        //    {
        //        item = GetInitItem();
        //    }
        //    catch (ArgumentOutOfRangeException e)
        //    {
        //        throw e;
        //        //return false;
        //    }

        //    Debug.WriteLine(item.Name);

        //    List<Book> listBooks = item.GetChilds_Books();
        //    if (listBooks.Count() == 0)
        //    {
        //        return true;
        //    }

        //    Debug.WriteLine(listBooks.Count());

        //    string libPathTest = "Data Source=" + fileDestination + "; version = 3 ";
        //    using (DBModel db = new DBModel(libPathTest))
        //    {
        //        db.Genres.Load();

        //        foreach (var fb2 in listBooks)
        //        {
        //            //ProcessUpdateFB2(fb2, db);
        //            //ProcessUpdateBook(fb2, db);
        //        }

        //        //ParallelOptions po = new ParallelOptions
        //        //{
        //        //    MaxDegreeOfParallelism =
        //        //       Environment.ProcessorCount > 2 ? Environment.ProcessorCount - 1 : 1
        //        //};

        //        //Parallel.ForEach(listBooks, po, entry =>
        //        //{
        //        //    ProcessUpdateBook(entry, db);
        //        //});
        //        //Debug.WriteLine("! End !");
        //    }
        //    return true;
        //}

        public bool ProcessUpdate()
        {
            ITreeViewItem item;
            try
            {
                item = GetInitItem();
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
               // return false;
            }

            Debug.WriteLine("Init Item : {0} ", item.Name);

            List<DBFile> listFiles = item.GetChilds_Files();
            
            Debug.WriteLine(listFiles.Count);
            
            if (listFiles.Count() == 0)
            {
                return true;
            }

            Debug.WriteLine(listFiles.Count());

            string libPathTest = "Data Source=" + fileDestination + "; version = 3 ";
            using (DBSQLiteModel db = new DBSQLiteModel(libPathTest))
            {
                db.Genres.Load();

                foreach (var file in listFiles)
                {
                    //ProcessUpdateFB2(fb2, db);
                    //ProcessUpdateBook(fb2, db);
                    ProcessUpdateFile(file, db);
                }

                //ParallelOptions po = new ParallelOptions
                //{
                //    MaxDegreeOfParallelism =
                //       Environment.ProcessorCount > 2 ? Environment.ProcessorCount - 1 : 1
                //};

                //Parallel.ForEach(listBooks, po, entry =>
                //{
                //    ProcessUpdateBook(entry, db);
                //});
                //Debug.WriteLine("! End !");
            }
            return true;
        }

        private void ProcessUpdateFile(DBFile file, DBSQLiteModel db)
        {
            CheckFile(db, file);

            db.SaveChanges();
        }

        private void CheckFile(DBSQLiteModel db, DBFile file)
        {
            Debug.WriteLine("File :" + file.Path);

            List<Book> books = new List<Book>();
            bool addBook = false;
                        
            foreach(Book book in file.Books)
            {
                if (CheckBook(db, book))
                {
                    db.Books.Add(book);
                    books.Add(book);
                    db.SaveChanges();
                }
            }
                        
            if (books.Count() > 0)
            {
                file.Books.Clear();

                var fileDb = db.DBFiles.FirstOrDefault<DBFile>(
                        p => p.Path.CompareTo(file.Path) == 0
                       );

                if (fileDb == null)
                {
                    db.DBFiles.Add(file);
                    fileDb = file;
                }

                foreach (var b in books)
                {
                    fileDb.Books.Add(b);
                }
            }
        }

        private bool CheckBook(DBSQLiteModel db, Book book)
        {
            bool addBook;
            if (book == null)
            {
                return false;
            }

            CheckBooksGenres(db, book);
            addBook = CheckBooksAuthors(db, book) | CheckBooksKeyWords(db, book) | addDuplicated;
            addBook = addBook || (!checkUnknown4Duplicated && book.Caption.CompareTo("<Unknown>") == 0) || CheckBookInDB(db, book);

            return addBook;
        }

        //private void ProcessUpdateBook(Book book, DBModel db)
        //{
        //    bool addBook;

        //    if (book == null)
        //    {
        //        Debug.WriteLine("Failed");
        //        return;
        //    }

        //    CheckBooksGenres(db, book);
        //    addBook = CheckBooksAuthors(db, book) | CheckBooksKeyWords(db, book) | addDuplicated;
        //    addBook = (!checkUnknown4Duplicated && book.Caption.CompareTo("<Unknown>") == 0) || CheckBookInDB(db, book);

        //    if (addBook)
        //    {
        //        addBook2DB(db, book);
        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch (DbEntityValidationException ex)
        //        {
        //            Debug.WriteLine(string.IsNullOrEmpty(book.Caption.Trim()));
        //            Debug.WriteLine(" {0} - {1} - {2}", book.Caption, book.Caption.CompareTo("<Unknown>"), addBook);

        //            foreach (var entityValidationErrors in ex.EntityValidationErrors)
        //            {
        //                foreach (var validationError in entityValidationErrors.ValidationErrors)
        //                {
        //                    Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
        //                }
        //            }

        //            throw ex;
        //        }
        //    }
        //}

        //private void addBook2DB(DBModel db, Book book)
        //{
        //    db.Books.Add(book);
        //}

        //private void ProcessUpdateFB2(TreeViewItem_FileFB2 fb2, DBModel db)
        //{
        //    bool addBook;
        //    Book book = GetBook(fb2);

        //    if (book == null)
        //    {
        //        Debug.WriteLine(fb2.Name + " : Failed");
        //        return;
        //    }

        //    lock (lockerZip)
        //    {
        //        CheckBooksGenres(db, book);
        //        addBook = CheckBooksAuthors(db, book) | CheckBooksKeyWords(db, book) | CheckBookInDB(db, book);
        //        //addBook = addBook || CheckBookInDB(db, book);
        //        addBook = (!checkUnknown4Duplicated && book.Caption.CompareTo("<Unknown>") == 0) || CheckBookInDB(db, book);
        //    //Debug.WriteLine("Book: {0} - {1}", book.Caption, addBook);

        //        if (addBook && fb2.State != ItemState.Error)
        //        {
        //            //Debug.WriteLine("Save");
        //            //book.BookFile = fb2.Byte4Book;
        //            book.Encoding = fb2.Encoding4File;
        //            db.Books.Add(book);
                   
        //            try
        //            {
        //                db.SaveChanges();
        //            }
        //            catch (DbEntityValidationException ex)
        //            {
        //                Debug.WriteLine(string.IsNullOrEmpty(book.Caption.Trim()));
        //                Debug.WriteLine(" {0} - {1} - {2}", book.Caption, book.Caption.CompareTo("<Unknown>"), addBook);

        //                foreach (var entityValidationErrors in ex.EntityValidationErrors)
        //                {
        //                    foreach (var validationError in entityValidationErrors.ValidationErrors)
        //                    {
        //                        Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
        //                    }
        //                }

        //                throw ex;
        //            }

        //        }
        //    }
        //}

        private Book GetBook(TreeViewItem_FileFB2 fb2)
        {
            TreeViewItem_Attribute attr;
            string caption;
            Book book = new Book() { Caption = "<Unknown>" };

            foreach (var i in fb2.GetChilds_Items())
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

            book.Encoding = fb2.Encoding4File;


            return book;
        }

        private IEnumerable<TreeViewItem_FileFB2> GetListFB2(ITreeViewItem item)
        {
            if(item.Type == ItemType.Zip || item.Type == ItemType.FilesUnion || item.Type == ItemType.Directory)
            {
                foreach (var itemChild in item.GetChilds_Items())
                {
                    foreach (var fb2 in GetListFB2(itemChild))
                    {
                        yield return fb2;
                    }
                }
            }else if(item.Type == ItemType.File || item.Type == ItemType.InZip)
            {
                if (item is TreeViewItem_FileFB2 fb2File)
                {
                    
                    yield return fb2File;
                }
            }

            yield break;
        }

        private IEnumerable<Book> GetListBooks(ITreeViewItem item)
        {
            if (item.Type == ItemType.Zip || item.Type == ItemType.FilesUnion || item.Type == ItemType.Directory)
            {
                foreach (var itemChild in item.GetChilds_Items())
                {
                    foreach (var book in GetListBooks(itemChild))
                    {
                        yield return book;
                    }
                }
            }
            else if (item.Type == ItemType.File || item.Type == ItemType.InZip)
            {
                if (item is TreeViewItem_FileFB2 fb2File)
                {

                    //Debug.WriteLine("Save");
                    //book.BookFile = fb2.Byte4Book;

                    yield return GetBook(fb2File);
                }
            }

            yield break;
        }


        private ITreeViewItem GetInitItem()
        {
            ITreeViewItem item;

            if (string.IsNullOrEmpty(fileDestination))
            {
                throw new ArgumentOutOfRangeException("fileDestination", "Wrong database name");
            }

            if (string.IsNullOrEmpty(fileSource))
            {
                throw new ArgumentOutOfRangeException("fileSource", "Wrong source file name");
            }

            item = TreeItemsFactory.GetItem(fileSource);

            if (item.State == ItemState.Error)
            {
                throw new ArgumentOutOfRangeException("fileSource", "Wrong source file name");
            }

            return item;
        }

        private bool CheckBooksKeyWords(DBSQLiteModel db, Book book)
        {
            KeyWord keyWordDB;
            List<KeyWord> list = new List<KeyWord>();
            bool updateList = false, addedItems = false;

            foreach (KeyWord keyWord in book.KeyWords)
            {
                //lock (lockerKeyWord)
               // lock (lockerZip)
                //{
                    keyWordDB = db.KeyWords.FirstOrDefault<KeyWord>(
                         p => p.Word.CompareTo(keyWord.Word) == 0
                        );

                    if (keyWordDB == null)
                    {
                        db.KeyWords.Add(keyWord);
                        keyWordDB = keyWord;
                        addedItems = true;
                    }
                    else
                    {
                        updateList = true;
                    }
                //}
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

            return addedItems;
        }

        private bool CheckBooksAuthors(DBSQLiteModel db, Book book)
        {
            Author authorDB;
            List<Author> list = new List<Author>();
            bool updateList = false, addedItems = false;

            foreach(Author author in book.Authors)
            {
                //lock (lockerAuthors)
                //lock (lockerZip)
                //{
                    authorDB = db.Authors.FirstOrDefault<Author>(
                        p => p.LastName.CompareTo(author.LastName) == 0 &&
                             p.FirstName.CompareTo(author.FirstName) == 0 &&
                             p.MiddleName.CompareTo(author.MiddleName) == 0);

                    if (authorDB == null)
                    {
                        db.Authors.Add(author);
                        authorDB = author;
                        addedItems = true;
                    }
                    else
                    {
                        updateList = true;
                    }
                //}
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
            return addedItems;
        }

        private void CheckBooksGenres(DBSQLiteModel db, Book book)
        {
            Genre genreDB;
            List<Genre> list = new List<Genre>();

            foreach (Genre genre in book.Genres)
            {
                //lock (lockerGenre)
                //lock (lockerZip)
                //{
                    genreDB = db.Genres.FirstOrDefault<Genre>(
                         p => p.Key == genre.Key
                        );
                //}
                if (genreDB != null)
                {
                    list.Add(genreDB);
                }
            }

            book.Genres.Clear();
            if (list.Count() > 0)
            {
                foreach (var g in list)
                {
                    book.Genres.Add(g);
                }
            }
        }

        private bool CompareDbSets<T>(List<T> db1, List<T> db2)
        {
            db2.Sort();
            return db1.SequenceEqual(db2);
        }
        private bool CheckBookInDB(DBSQLiteModel db, Book book)
        {
            List<Genre> listgb;
            List<Author> listab;
            List<Book> bookList;

            listgb = book.Genres.ToList();
            listgb.Sort();

            listab = book.Authors.ToList();
            listab.Sort();

            //lock(lockerBook)
            //lock (lockerZip)
            //{
                bookList = db.Books
                        .Include(b => b.Authors)
                        .Include(b => b.Genres)
                        .Where(b => b.Caption == book.Caption)
                        .ToList();
            //}
            if (bookList.Count() == 0)
                return true;


            var res = from b in bookList
                      where b.Caption == book.Caption &&
                      CompareDbSets(listgb, b.Genres.ToList()) &&
                      CompareDbSets(listab, b.Authors.ToList())
                      select b.Caption;

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
