using AddBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddBook.Repositories
{
    interface IBook
    {
        public string addBook(Books b);
        public Books Viewbook(string name);
        public List<Books> viewAllBooks();
        public List<Books> DelistedBooks();
      
        public string DeleteBook(string name);
        public string DelistBook(string name);
        public string EnlistBook(string name);
        public string EditBookDetails(string id, Books b);


    }
}
