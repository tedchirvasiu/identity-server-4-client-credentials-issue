using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace IS4CCRedirectUriIssue.Client {
    [Route("Issue")]
    public class IssueController : Controller {

        [HttpGet]
        public async Task<string> Index() {

            var disco = await DiscoveryClient.GetAsync("https://localhost:5001");
            if (disco.IsError) {
                return disco.Error;
            }

            var response = new StringBuilder();

            //Broken client
            response.AppendLine("Client Credentials + Authorization Code:");

            var brokenTokenClient = new TokenClient(disco.TokenEndpoint, "Broken-Client", "Broken-Client-Secret");
            var brokenTokenResponse = await brokenTokenClient.RequestClientCredentialsAsync("test-api");

            if (brokenTokenResponse.IsError) {
                response.AppendLine(brokenTokenResponse.Error);
            } else {
                response.AppendLine(brokenTokenResponse.Json.ToString());
            }

            response.AppendLine();
            response.AppendLine("Only Client Credentials:");

            //Working client, only difference being this one doesn't allow Authorization Code
            var workingTokenClient = new TokenClient(disco.TokenEndpoint, "Working-Client", "Working-Client-Secret");
            var workingTokenResponse = await workingTokenClient.RequestClientCredentialsAsync("test-api");

            if (workingTokenResponse.IsError) {
                response.AppendLine(workingTokenResponse.Error);
            } else {
                response.AppendLine(workingTokenResponse.Json.ToString());
            }

            return response.ToString(); ;
        }
    }
}
