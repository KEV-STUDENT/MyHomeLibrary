using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyHomeLibFiles;
using MyDBModel;
using MyHomeLibCommon;

using System.Diagnostics;

namespace MyHomeLibBizLogic
{
    public class MyDBUpdater
    {
        private string fileDestination;
        private string fileSource;

        public MyDBUpdater()
        {
        }

        public MyDBUpdater(string file_SQLite, string file_ZIP)
        {
            this.fileDestination = file_SQLite;
            this.fileSource = file_ZIP;
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

            using (DBModel db = new DBModel(fileDestination))
            {
                foreach(var i in item.GetChilds_Items())
                {

                    Debug.WriteLine(i.Type);
                }
            }
            return false;
        }

        public int FillContextFromItemView(DBModel db, ITreeViewItem item)
        {
            int result = 0;            
            Book book = new Book();

            foreach (var i in item.GetChilds_Items())
            {
                TreeViewItem_Attribute attr = i as TreeViewItem_Attribute;

                if (attr != null && attr.Type == ItemType.Attribute)
                {
                    switch (attr.AttributeType) 
                    {
                        case AttributeType.Author:
                            AddAuthor2Book(attr, book, db);
                            break;
                        case AttributeType.Title:
                            book.Caption = attr.AttributeValue;
                            break;
                        case AttributeType.GenreFB2:
                            AddGenreFB2Book(attr, book, db);                            
                            break;
                        default:
                            break;
                    }                    
                    Debug.WriteLine("Name {0}  Value {1} ", attr.AttributeName, attr.AttributeValue);                   
                }
                else
                {
                    Debug.WriteLine(i.Type);
                }
            }
            db.Books.Add(book);
            result = db.SaveChanges();

            return result;
        }

        private void AddAuthor2Book(TreeViewItem_Attribute authorItem, Book book, DBModel db)
        {
            bool isAuthor = false;
            Author autorDB, author = new Author();
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
                autorDB = db.Authors.FirstOrDefault<Author>(
                    p =>  p.LastName == author.LastName &&
                    p.FirstName == author.FirstName &&
                    p.MiddleName == author.MiddleName
                    );

                if (autorDB == null)
                {
                    db.Authors.Add(author);
                }
                else
                {
                    author = autorDB;
                }
                book.Authors.Add(author);
            }
        }

        private void AddGenreFB2Book(TreeViewItem_Attribute genreItem, Book book, DBModel db)
        {
            ItemGenre genre;

            if(!Enum.TryParse<ItemGenre>(genreItem.AttributeValue, true, out genre))
            {
                genre = ItemGenre.none;
            }
            book.Genres.Add(db.Genres.Find(genre));
        }
    }
}
