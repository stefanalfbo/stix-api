﻿using System.Text.Json;
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

        // request token
        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "client",
            ClientSecret = "secret",
            Scope = "stixapi",
        });

        if (tokenResponse.IsError)
        {
            Console.WriteLine(tokenResponse.Error);
            Console.WriteLine(tokenResponse.ErrorDescription);
            return 1;
        }

        Console.WriteLine(tokenResponse.AccessToken);

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
            return;
        }

        var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
        Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
    }
}