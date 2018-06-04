using OrleansGrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansGrains
{
    public class LibrarianGrain : Orleans.Grain, ILibrarianGrain
    {
        List<Book> LibraryBooks = new List<Book>();

        public Task AddBook(Book book)
        {
            LibraryBooks.Add(book);
            return Task.CompletedTask;
        }

        public Task<Book> CheckoutBook(string name)
        {
            var book = LibraryBooks.Where(lb => lb.Name == name).First();

            if (LibraryBooks.Any(lb => lb.Name == name))
            {
                LibraryBooks.Remove(book);
            }

            return Task.FromResult(book);
        }

        public Task<string> GetBooks()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendJoin(", ", LibraryBooks.Select(b => b.Name));

            return Task.FromResult(stringBuilder.ToString());
        }
    }
}
