using OrleansGrainInterfaces;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace OrleansGrains
{
    public class CustomerGrain : Orleans.Grain, ICustomerGrain
    {
        private Librarian Librarian;
        private List<Book> Books = new List<Book>();

        public Task CheckoutBook(string name)
        {
            var librarian = GrainFactory.GetGrain<ILibrarianGrain>(Librarian.LibrarianId);
            var book = librarian.CheckoutBook(name).Result;
            AddBook(book);

            return Task.CompletedTask;
        }

        private void AddBook(Book book)
        {
            Books.Add(book);
        }

        public Task<string> GetBooks()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendJoin(", ", Books.Select(b => b.Name));

            return Task.FromResult(stringBuilder.ToString());
        }

        public Task SetLibrarian(Librarian librarian)
        {
            Librarian = librarian;
            return Task.CompletedTask;
        }

        public Task<string> Command(string command)
        {
            switch (command)
            {
                case "checkout":
                    Console.WriteLine("What book would you like to check out?");
                    var bookName = Console.ReadLine();
                    CheckoutBook(bookName);
                    return Task.FromResult($"{bookName} was checked out!");
                case "list books":
                    return GetBooks();
                default:
                    return Task.FromResult("I don't know how to do that");
            }
        }
    }
}
