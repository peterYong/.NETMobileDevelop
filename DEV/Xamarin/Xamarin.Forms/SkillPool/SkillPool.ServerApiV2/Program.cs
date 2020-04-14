﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SkillPool.ServerApiV2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //WebHost.CreateDefaultBuilder(args)
        //    .UseStartup<Startup>();
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {


            var configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                                             .AddJsonFile("host.json")
                                             .Build();
            var url = configuration["urls"];

            return WebHost.CreateDefaultBuilder(args)
                   .UseUrls(url)
                   .UseStartup<Startup>();

        }
    }
}
