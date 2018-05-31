using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansGrainInterfaces
{
    [Immutable]
    public class Librarian
    {
        public int LibrarianId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
