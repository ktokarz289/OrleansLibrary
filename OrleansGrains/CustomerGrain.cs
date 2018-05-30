using OrleansGrainInterfaces;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace OrleansGrains
{
    public class CustomerGrain : Orleans.Grain, ICustomerGrain
    {
        private ILibrarianGrain Librarian;
        private List<Book> Books = new List<Book>();

        public Task CheckoutBook(string name)
        {
            var book = Librarian.CheckoutBook(name).Result;
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

        public Task SetLibrarian(ILibrarianGrain librarian)
        {
            Librarian = librarian;
            return Task.CompletedTask;
        }
    }
}
