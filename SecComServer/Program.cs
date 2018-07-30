﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SecComServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:7777")
                .UseStartup<Startup>()
                .Build();
    }
}
