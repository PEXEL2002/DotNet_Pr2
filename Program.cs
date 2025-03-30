using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Pr2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====================== Zadanie 1 ======================");
            DownlowdDataFromAPI dAPI = new DownlowdDataFromAPI();
            string userJson =  dAPI.GetData("https://dummyjson.com/users").Result;
            string todosJson = dAPI.GetData("https://dummyjson.com/todos").Result;
            Console.WriteLine("======= Users =======");
            var userDeserialize  = JsonSerializer.Deserialize<UserResponse>(userJson);
            Console.WriteLine(userDeserialize.ToString());
            Console.WriteLine("======= ToDos =======");
            var todosDeserialize = JsonSerializer.Deserialize<ToDoResponse>(todosJson);
            Console.WriteLine(todosDeserialize.ToString());

            Console.WriteLine("====================== Zadanie 2 ======================");
            using var db = new AppDbContext();

            // Pobierz użytkowników wraz z ich zadaniami
            var users = db.Users
                          .Include(u => u.ToDos)
                          .ToList();

            Console.WriteLine("======= Lista użytkowników i ich zadań =======");

            foreach (var user in users)
            {
                int doneCount = user.ToDos.Count(t => t.IsDone);
                int notDoneCount = user.ToDos.Count(t => !t.IsDone);

                Console.WriteLine($"{user.FirstName} {user.LastName} ({user.Age} lat)");
                Console.WriteLine($"  ✅ Zakończone zadania: {doneCount}");
                Console.WriteLine($"  ❌ Niezakończone zadania: {notDoneCount}");
                Console.WriteLine();
            }
        }
    }
}
