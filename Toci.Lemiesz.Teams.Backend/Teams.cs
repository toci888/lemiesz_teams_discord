using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Client;

class Program
{
    static async Task Main(string[] args)
    {
        // Konfiguracja aplikacji
        string clientId = "YOUR_CLIENT_ID";
        string tenantId = "YOUR_TENANT_ID";
        string clientSecret = "YOUR_CLIENT_SECRET";

        // Uwierzytelnianie przy użyciu MSAL
        var confidentialClient = ConfidentialClientApplicationBuilder
            .Create(clientId)
            .WithTenantId(tenantId)
            .WithClientSecret(clientSecret)
            .Build();

        string[] scopes = { "https://graph.microsoft.com/.default" };
        var authResult = await confidentialClient.AcquireTokenForClient(scopes).ExecuteAsync();

        // Inicjalizacja Graph Client
        var graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(requestMessage =>
        {
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResult.AccessToken);
            return Task.CompletedTask;
        }));

        // Pobranie użytkowników z Microsoft Teams
        var users = await graphClient.Users.Request().GetAsync();

        foreach (var user in users)
        {
            Console.WriteLine($"User: {user.DisplayName}, Email: {user.Mail}");
        }
    }
}
