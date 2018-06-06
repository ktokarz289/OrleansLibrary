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

        public Task<Book> CheckoutBook(string name)
        {
            var librarian = GrainFactory.GetGrain<ILibrarianGrain>(Librarian.LibrarianId);
            var book = librarian.CheckoutBook(name).Result;

            if (book != null)
            {
                AddBook(book);
            }

            return Task.FromResult(book);
        }

        private void AddBook(Book book)
        {
            Books.Add(book);
        }

        public Task<string> GetBooks()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendJoin(", ", Books.Select(b => b.Name));

            if (stringBuilder.ToString() == "")
            {
                stringBuilder.Append("I have no books checked out!");
            }

            return Task.FromResult(stringBuilder.ToString());
        }

        public Task SetLibrarian(Librarian librarian)
        {
            Librarian = librarian;
            return Task.CompletedTask;
        }

        public Task<string> Command(string args)
        {
            var commands = args.Split("-");
            switch (commands[0].Trim())
            {
                case "checkout":
                    var bookName = commands[1].Trim();
                    var book = CheckoutBook(bookName);

                    if (book != null)
                    {
                        return Task.FromResult($"{bookName} was checked out!");
                    }
                    else
                    {
                        return Task.FromResult("The library doesn't have that book");
                    }
                case "list books":
                    return GetBooks();
                case "quit":
                    return Task.FromResult("");
                default:
                    return Task.FromResult("I don't know how to do that");
            }
        }
    }
}
