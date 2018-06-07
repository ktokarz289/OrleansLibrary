using Orleans;
using System.Threading.Tasks;

namespace OrleansGrainInterfaces
{
    public interface ICustomerGrain : IGrainWithIntegerKey
    {
        Task<string> GetBooks();
        Task<Book> CheckoutBook(string name);
        Task SetLibrarian(Librarian librarian);
        Task<string> Command(string args);
    }
}
