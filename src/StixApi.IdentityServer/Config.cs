using Duende.IdentityServer.Models;

namespace StixApi.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope(name: "create:vuln", displayName: "create:vuln"),
                new ApiScope(name: "read:vuln", displayName: "read:vuln"),
                new ApiScope(name: "update:vuln", displayName: "update:vuln"),
                new ApiScope(name: "delete:vuln", displayName: "delete:vuln"),
            };

    public static IEnumerable<Client> Clients =>
    new Client[]

    {
        new Client
        {
            ClientId = "admin",

            // no interactive user, use the clientid/secret for authentication
            AllowedGrantTypes = GrantTypes.ClientCredentials,

            // secret for authentication
            ClientSecrets =
            {
                new Secret("admin-password".Sha256())
            },

            AllowedScopes = { "read:vuln", "create:vuln", "update:vuln", "delete:vuln" },
        }
    };
}