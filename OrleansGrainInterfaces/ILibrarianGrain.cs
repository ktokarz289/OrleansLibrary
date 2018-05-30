using Orleans;
using System.Threading.Tasks;

namespace OrleansGrainInterfaces
{
    public interface ILibrarianGrain : IGrainWithIntegerKey
    {
        Task<string> GetBooks();
        Task<Book> CheckoutBook(string name);
    }
}
