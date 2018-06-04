using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansGrainInterfaces;
using OrleansGrains;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace OrleansLibraryServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string mapFileName = Path.Combine(path, "books.json");

            var silo = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "development";
                    options.ServiceId = "OrleansLibrary";
                })
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(LibrarianGrain).Assembly).WithReferences())
                .Build();

            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "development";
                    options.ServiceId = "OrleansLibrary";
                })
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ILibrarianGrain).Assembly).WithReferences())
                .Build();

            RunAsync(silo, client, mapFileName).Wait();

            Console.ReadLine();

            StopAsync(silo, client).Wait();
        }

        static async Task RunAsync(ISiloHost silo, IClusterClient client, string mapFileName)
        {
            await silo.StartAsync();
            await client.Connect();
            var librarySetup = new LibrarySetup(client);
            await librarySetup.Configure("books.json");
            Console.WriteLine("Setup complete");
        }

        static async Task StopAsync(ISiloHost silo, IClusterClient client)
        {
            await client.Close();
            await silo.StopAsync();
        }
    }
}
