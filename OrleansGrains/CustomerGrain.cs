using OrleansGrainInterfaces;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace OrleansGrains
{
    public class CustomerGrain : Orleans.Grain, ICustomerGrain
    {
        List<Book> Books = new List<Book>();

        public Task CheckoutBook(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetBooks()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendJoin(", ", Books.Select(b => b.Name));

            return Task.FromResult(stringBuilder.ToString());
        }
    }
}
