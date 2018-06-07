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

        public async Task<Book> CheckoutBook(string name)
        {
            var librarian = GrainFactory.GetGrain<ILibrarianGrain>(Librarian.LibrarianId);
            var book = await librarian.CheckoutBook(name);

            if (book != null)
            {
                AddBook(book);
            }

            return book;
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

        public async Task<string> Command(string args)
        {
            var commands = args.Split("-");
            switch (commands[0].Trim())
            {
                case "checkout":
                    var bookName = commands[1].Trim();
                    var book = await CheckoutBook(bookName).ConfigureAwait(false);

                    if (book != null)
                    {
                        return $"{book.Name} was checked out!";
                    }
                    else
                    {
                        return "The library doesn't have that book";
                    }
                case "list books":
                    return await GetBooks().ConfigureAwait(false);
                case "quit":
                    return "";
                default:
                    return "I don't know how to do that";
            }
        }
    }
}
