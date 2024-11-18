using BtkTodo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace BtkTodo.Db
{
    public class TodoDbContext : DbContext
    {
        public DbSet<DbTodoEntry> Entries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BtkTodoAppDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            string baseDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\btkakademi";
            
            if (!Directory.Exists(baseDir))
                Directory.CreateDirectory(baseDir);

            optionsBuilder.UseSqlite($"Data Source={baseDir}\\todolist.db");
        }
    }
}
