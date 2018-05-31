using Orleans;
using System.Threading.Tasks;

namespace OrleansGrainInterfaces
{
    public interface ICustomerGrain : IGrainWithIntegerKey
    {
        Task<string> GetBooks();
        Task CheckoutBook(string name);
        Task SetLibrarian(Librarian librarian);
        Task<string> Command(string command);
    }
}
