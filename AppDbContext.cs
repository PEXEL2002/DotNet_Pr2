using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Pr2
{
    internal class AppDbContext : DbContext
    {
        public DbSet<UsersEntity> Users { get; set; }
        public DbSet<ToDoEntity> ToDos { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
            EnsureDataLoaded().Wait();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlite("Data Source=app_database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<UsersEntity>()
                        .HasMany(u => u.ToDos)
                        .WithOne(t => t.User)
                        .HasForeignKey(t => t.UserId);
        }

        private async Task EnsureDataLoaded(){
            if (!Users.Any() && !ToDos.Any()){
                Console.WriteLine("[DB INIT] Pobieranie danych z API...");
                DownlowdDataFromAPI dAPI = new DownlowdDataFromAPI();
                string usersJson = await dAPI.GetData("https://dummyjson.com/users");
                string todosJson = await dAPI.GetData("https://dummyjson.com/todos");
                var users = JsonSerializer.Deserialize<UserResponse>(usersJson)?.users;
                var todos = JsonSerializer.Deserialize<ToDoResponse>(todosJson)?.todos;
                if (users != null){
                    var userEntities = users.Select(u => new UsersEntity{
                        Id = u.id,
                        FirstName = u.firstName,
                        LastName = u.lastName,
                        Age = u.age,
                        Gender = u.gender,
                        ToDos = new List<ToDoEntity>()
                    }).ToList();

                    Users.AddRange(userEntities);
                }
                if (todos != null){
                    var todoEntities = todos.Select(t => new ToDoEntity
                    {
                        Id = t.id,
                        Task = t.todo,
                        IsDone = t.completed,
                        UserId = t.userId
                    }).ToList();
                    ToDos.AddRange(todoEntities);
                }
                SaveChanges();
                Console.WriteLine("[DB INIT] Dane zostały zapisane do bazy.");
            }
            else{
                Console.WriteLine("[DB INIT] Dane pobrane z pliku.");
            }
        }
    }
}
