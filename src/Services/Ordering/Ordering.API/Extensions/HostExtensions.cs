﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Ordering.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder, int? retry = 0) where TContext: DbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope()) 
            {
                var serviceProvider= scope.ServiceProvider; 
                var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
                var context = serviceProvider.GetService<TContext>();

                try
                { 
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    InvokeSeeder(seeder, context, serviceProvider);

                    logger.LogInformation("Migrated  database associated with context {DbContextName}", typeof(TContext).Name);

                }
                catch(SqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryForAvailability);
                    }
                }


            }
            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, 
                                                   TContext context, 
                                                   IServiceProvider serviceProvider) 
                                                   where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, serviceProvider);
        }
    }
}