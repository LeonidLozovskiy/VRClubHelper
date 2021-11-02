using System;
using System.Linq;
using CorgiVR.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CorgiVR.Repository
{
    public static class CustomMigrations
    {
        public static void Migrate(IServiceProvider ServiceProvider)
        {
            var context = (EfContext)ServiceProvider.GetRequiredService(typeof(EfContext));
            
            while (true)
            {
                try
                {
                    var lastMigration = context.Migrations.OrderBy(x => x.Id).LastOrDefault();

                    if (lastMigration?.Id == 1)
                    {
                        context.Database.ExecuteSqlRaw("CREATE TABLE \"ClientKnowledgeSources\" (\"Id\"	INTEGER,\"Name\"	TEXT,\"Count\"	INTEGER,\"CreateDateTime\"	TEXT,\"UpdateDateTime\"	TEXT,PRIMARY KEY(\"Id\" AUTOINCREMENT))");

                        context.Migrations.Add(new Migration
                                               {
                                                   Id = 2,
                                                   Name = "Add ClientKnowledgeSource"
                                               });
                        context.SaveChanges();
                    }

                    if (lastMigration?.Id == 2)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    if (e.Message == "SQLite Error 1: 'no such table: Migrations'.")
                    {
                        context.Database.ExecuteSqlRaw("CREATE TABLE \"Migrations\" (\"Id\"	INTEGER UNIQUE,\"Name\"	TEXT,PRIMARY KEY(\"Id\" AUTOINCREMENT))");

                        context.Migrations.Add(new Migration
                                               {
                                                   Id = 1,
                                                   Name = "Init"
                                               });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}