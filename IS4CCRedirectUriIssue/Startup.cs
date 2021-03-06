﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IS4CCRedirectUriIssue {
    public class Startup {

        public void ConfigureServices(IServiceCollection services) {
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddTestUsers(Config.GetTestUsers())
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryIdentityResources(Config.GetIdentityResources())
                    .AddInMemoryClients(Config.GetClients());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}
