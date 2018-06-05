using Orleans;
using Orleans.Configuration;
using OrleansGrainInterfaces;
using System;

namespace OrleansLibraryClient
{
    class Program
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
        }
    }
}
