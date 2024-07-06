using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;
using System.Text;
using Slot.Application.Features.Slot.Query.DataModels;

namespace Slot.TestConsole;

internal class Program
{
    private static async Task<string> LoginAndGetTokenAsync(string username, string password)
    {
        var loginUrl = "https://localhost:7145/api/v1/Account/SignIn";

        var httpClient = new HttpClient();

        var loginData = new
        {
            Username = username,
            Password = password
        };

        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(loginUrl, content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(responseContent);
            if (jsonDocument.RootElement.TryGetProperty("data", out JsonElement dataElement) &&
                dataElement.TryGetProperty("token", out JsonElement tokenElement))
            {
                return tokenElement.GetString();
            }
        }
        else
        {
            Console.WriteLine("Login failed.");
        }

        return null;
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        string username = "Administrator";
        string password = "Test1234*";

        string token = await LoginAndGetTokenAsync(username, password);

        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Failed to obtain token.");
            return;
        }

        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7145/slot-hub", options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(token);
            })
            .Build();

        connection.On<string>("ReceiveMessage", message =>
        {
            Console.WriteLine("Received: " + message);
        });

        connection.On<SpinResultDTO>("ReceiveSpinResult", result =>
        {
            Console.WriteLine($"Spin Result: Bet: {result.BetAmount}, Win: {result.WinAmount}, Current Balance: {result.CurrentBalance}, Result: {result.SlotResult}, Transaction Type: {result.TransactionType}");
        });

        try
        {
            await connection.StartAsync();
            Console.WriteLine("Connected to the hub.");

            while (true)
            {
                Console.WriteLine("Enter bet amount:");
                if (decimal.TryParse(Console.ReadLine(), out decimal betAmount))
                {
                    await connection.InvokeAsync("Spin", betAmount);
                }
                else
                {
                    Console.WriteLine("Invalid bet amount.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to the hub: {ex.Message}");
        }
    }
}