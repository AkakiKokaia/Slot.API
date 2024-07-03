using Microsoft.AspNetCore.SignalR.Client;

namespace Project.Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Simulate a delay (for testing purposes)
            await Task.Delay(7000);

            // Replace this with your actual method of getting a token
            string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiJlMjczZjU1ZC1hYWQ1LTQ4YTAtOGU1Yy0yZWJlNmExMDdkMTMiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3MTk5MzU5MjcsImV4cCI6MTcyMDAyMjMyNywiaWF0IjoxNzE5OTM1OTI3LCJpc3MiOiJsb2NhbGhvc3QifQ.Hgn4wgKCs5PgKZ_4m3hxm5TBtC2o8hifbPrB-BrMdJY";

            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7145/slot-hub", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult("Bearer" + accessToken);
                })
                .Build();

            connection.On<string>("ReceiveMessage", message =>
            {
                Console.WriteLine("Received: " + message);
            });

            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connected to the hub.");

                // Send a message to the hub
                await connection.InvokeAsync("Isa");

                // Keep the console open
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to the hub: {ex.Message}");
            }
        }
    }
}
