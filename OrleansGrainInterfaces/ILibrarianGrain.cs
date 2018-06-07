using Orleans;
using System.Threading.Tasks;

namespace OrleansGrainInterfaces
{
    public interface ILibrarianGrain : IGrainWithIntegerKey
    {
        Task AddBook(Book book);
        Task<string> GetBooks();
        Task<Book> CheckoutBook(string name);
        Task<bool> CheckinBook(Book book);
    }
}
