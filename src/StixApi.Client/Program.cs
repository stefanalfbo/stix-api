using System.Text.Json;
using Duende.IdentityModel.Client;

namespace StixApi.Client;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var client = new HttpClient();

        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
        if (disco.IsError)
        {
            Console.WriteLine(disco.Error);
            Console.WriteLine(disco.Exception);
            return 1;
        }

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "admin",
            ClientSecret = "admin-password",
            Scope = "read:vuln create:vuln update:vuln delete:vuln",
        });

        if (tokenResponse.IsError)
        {
            Console.WriteLine(tokenResponse.Error);
            Console.WriteLine(tokenResponse.ErrorDescription);
            return 1;
        }

        Console.WriteLine(tokenResponse.AccessToken);

        CreateVulnerability(tokenResponse.AccessToken!).GetAwaiter().GetResult();
        ListVulnerabilities(tokenResponse.AccessToken!).GetAwaiter().GetResult();

        return 0;
    }

    static async Task ListVulnerabilities(string accessToken)
    {
        var apiClient = new HttpClient();
        apiClient.SetBearerToken(accessToken);

        var response = await apiClient.GetAsync("https://localhost:7195/v1/api/vulnerabilities");
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            return;
        }

        var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
        Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
    }

    static async Task GetVulnerability(string accessToken, string id)
    {
        var apiClient = new HttpClient();
        apiClient.SetBearerToken(accessToken);

        var response = await apiClient.GetAsync($"https://localhost:7195/v1/api/vulnerabilities/{id}");
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            return;
        }

        var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
        Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
    }


    static async Task DeleteVulnerability(string accessToken, string id)
    {
        var apiClient = new HttpClient();
        apiClient.SetBearerToken(accessToken);

        var response = await apiClient.DeleteAsync($"https://localhost:7195/v1/api/vulnerabilities/{id}");
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            return;
        }

        Console.WriteLine("Vulnerability deleted successfully.");
    }

    static async Task CreateVulnerability(string accessToken)
    {
        var apiClient = new HttpClient();
        apiClient.SetBearerToken(accessToken);

        var vulnerability = new
        {
            id = $"vulnerability--{Guid.NewGuid()}",
            spec_version = "2.1",
            type = "vulnerability",
            name = "Test Vulnerability",
            description = "This is a test vulnerability.",
            created = DateTime.UtcNow.ToString("o"),
            modified = DateTime.UtcNow.ToString("o"),
            labels = new[] { "test", "vulnerability" }
        };

        var content = new StringContent(JsonSerializer.Serialize(vulnerability), System.Text.Encoding.UTF8, "application/json");
        var response = await apiClient.PostAsync("https://localhost:7195/v1/api/vulnerabilities", content);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            return;
        }

        Console.WriteLine("Vulnerability created successfully.");
    }

    static async Task UpdateVulnerability(string accessToken, string id)
    {
        var apiClient = new HttpClient();
        apiClient.SetBearerToken(accessToken);

        var vulnerability = new
        {
            id = id,
            type = "vulnerability",
            name = "Updated Vulnerability",
            description = "This is an updated test vulnerability.",
            modified = DateTime.UtcNow.ToString("o"),
            labels = new[] { "updated", "vulnerability" }
        };

        var content = new StringContent(JsonSerializer.Serialize(vulnerability), System.Text.Encoding.UTF8, "application/json");
        var response = await apiClient.PutAsync($"https://localhost:7195/v1/api/vulnerabilities/{id}", content);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            return;
        }

        Console.WriteLine("Vulnerability updated successfully.");
    }
}