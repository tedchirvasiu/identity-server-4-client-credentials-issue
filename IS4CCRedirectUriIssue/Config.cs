using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS4CCRedirectUriIssue {
    public class Config {

        protected static ApiResource API_RESOURCE = new ApiResource("test-api", "My API");

        public static IEnumerable<ApiResource> GetApiResources() {
            return new List<ApiResource>
            {
                API_RESOURCE
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>();

        public static IEnumerable<Client> GetClients() {

            return new List<Client>
            {
                new Client {
                    ClientId = "Broken-Client",
                    AllowedGrantTypes = { GrantType.ClientCredentials, GrantType.AuthorizationCode },
                    ClientSecrets = { new Secret("Broken-Client-Secret".Sha256()) },
                    AllowedScopes = { API_RESOURCE.Name }
                },
                new Client {
                    ClientId = "Working-Client",
                    AllowedGrantTypes = { GrantType.ClientCredentials },
                    ClientSecrets = { new Secret("Working-Client-Secret".Sha256()) },
                    AllowedScopes = { API_RESOURCE.Name }
                }
            };
        }

        public static List<TestUser> GetTestUsers() => new List<TestUser>();

    }
}
