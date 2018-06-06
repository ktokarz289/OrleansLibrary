using Orleans;
using Orleans.Configuration;
using OrleansGrainInterfaces;
using System;

namespace OrleansLibraryClient
{
    static class Program
    {
        static void Main(string[] args)
        {
            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "development";
                    options.ServiceId = "OrleansLibrary";
                })
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ILibrarianGrain).Assembly).WithReferences())
                .Build();

            client.Connect().Wait();

            var librarian = new Librarian
            {
                LibrarianId = 1,
                FirstName = "Ruth",
                LastName = "Brad"
            };

            var customer = client.GetGrain<ICustomerGrain>(1);
            customer.SetLibrarian(librarian);

            try
            {
                Console.WriteLine("What do you want to do?");
                var action = Console.ReadLine();
                var result = customer.Command(action).Result;
                Console.WriteLine(result);

                while (result != "")
                {
                    action = Console.ReadLine();
                    result = customer.Command(action).Result;
                    Console.WriteLine(result);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            finally
            {
                Console.WriteLine("Thanks, come again!");
            }

        }
    }
}
