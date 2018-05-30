using Orleans.Concurrency;

namespace OrleansGrainInterfaces
{
    [Immutable]
    public class Book
    {
        public string Isbn { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
    }
}
