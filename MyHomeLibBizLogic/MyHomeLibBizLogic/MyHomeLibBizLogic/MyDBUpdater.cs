using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyHomeLibFiles;
using MyDBModel;

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
                    switch (attr.AttributeName) 
                    {
                        case "Author":
                            AddAuthor2Book(attr.AttributeValue, book);
                            break;
                        case "Title":
                            book.Caption = attr.AttributeValue;
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

        private void AddAuthor2Book(string authorName, Book book)
        {
            Author author = new Author();
            string[] name;

            name = authorName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (name.Length > 0)
                author.LastName = name[0];

            if (name.Length > 1)
                author.FirstName = name[1];

            if (name.Length > 2)
            {
                for (int j = 2; j < name.Length; j++)
                {
                    author.MiddleName = string.Concat(author.MiddleName, " ", name[j]);
                }
            }

            if (name.Length > 2)
            {
                for (int j = 2; j < name.Length; j++)
                {
                    author.MiddleName = string.Concat(author.MiddleName, " ", name[j]);
                }
            }

            if (name.Length > 2)
            {
                for (int j = 2; j < name.Length; j++)
                {
                    author.MiddleName = string.Concat(author.MiddleName, " ", name[j]);
                }
            }


            book.Author.Add(author);
        }
    }
}
