using Newtonsoft.Json;
using Orleans;
using OrleansGrainInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OrleansLibraryServer
{
    public class LibrarySetup
    {
        private IClusterClient client;

        public LibrarySetup(IClusterClient client)
        {
            this.client = client;
        }

        public async Task Configure(string filename)
        {
            var rand = new Random();

            using (var jsonStream = new JsonTextReader(File.OpenText(filename)))
            {
                var deserializer = new JsonSerializer();
                var data = deserializer.Deserialize<BookList>(jsonStream);
                var librarian = GetLibrarian();

                foreach (var book in data.Books)
                {
                    await librarian.AddBook(book);
                }
            }
        }

        private ILibrarianGrain GetLibrarian()
        {
            var librarian = client.GetGrain<ILibrarianGrain>(1);
            return librarian;
        }
    }
}
