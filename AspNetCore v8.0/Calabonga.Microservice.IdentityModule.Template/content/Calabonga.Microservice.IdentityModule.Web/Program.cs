﻿// --------------------------------------------------------------------
// Name: Template for Micro service on ASP.NET Core API with
// OpenIddict (OAuth2.0)
// Author: Calabonga © 2005-2024 Calabonga SOFT
// Version: 8.0.1
// Based on: .NET 8.0.x
// Created Date: 2024-02-01
// Updated Date: 2024-02-05
// --------------------------------------------------------------------
// Contacts
// --------------------------------------------------------------------
// Donate: https://boosty.to/calabonga/donate
// Blog: https://www.calabonga.net
// GitHub: https://github.com/Calabonga/Microservice-Template
// Boosty: https://boosty.to/calabonga
// YouTube: https://youtube.com/sergeicalabonga
// DZen: https://dzen.ru/calabonga
// --------------------------------------------------------------------
// Description:
// --------------------------------------------------------------------
// Minimal API for NET8 used with Definitions.
// This template implements Web API and OpenIddict (OAuth2.0).
// functionality. Also, support two type of Authentication:
// Cookie and Bearer
// --------------------------------------------------------------------

using Calabonga.AspNetCore.AppDefinitions;
using Serilog;
using Serilog.Events;

try
{
    // configure logger (Serilog)
    Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

    // created builder
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    // adding definitions for application
    builder.AddDefinitions(typeof(Program));

    // create application
    var app = builder.Build();

    // using definition for application
    app.UseDefinitions();

    // using Serilog request logging
    app.UseSerilogRequestLogging();

    // start application
    app.Run();

    return 0;
}
catch (Exception ex)
{
    var type = ex.GetType().Name;
    if (type.Equals("HostAbortedException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Unhandled exception");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}