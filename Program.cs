using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Pr2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====================== Zadanie 1 ======================");
            Console.WriteLine("\t[ZAD 1] Wyświetlenie danych z API");
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
            Console.WriteLine("\t[ZAD 2] Pobranie danych z API i dodanie do bazy lub jeżeli są dane w bazie to przejście dalej");
            using var db = new AppDbContext();
            Console.WriteLine("======= Lista użytkowników z zakończonymi i niezakończonymi zadaniami =======");
            Console.WriteLine("\t[ZAD 2] Zapytanie do bazy danych");
            var users = db.Users
                .Include(u => u.ToDos)
                .ToList()
                .Where(u => u.ToDos.Any(t => !string.IsNullOrEmpty(t.Task)))
                .ToList();
            foreach (var user in users)
            {
                int doneCount = user.ToDos.Count(t => t.IsDone);
                int notDoneCount = user.ToDos.Count(t => !t.IsDone);
                Console.WriteLine($"{user.FirstName} {user.LastName} ({user.Age} lat)");
                Console.WriteLine($"   Zakończone zadania: {doneCount}");
                Console.WriteLine($"   Niezakończone zadania: {notDoneCount}");
                Console.WriteLine();
            }
            Console.WriteLine("======= Dodanie nowego użytkownika =======");
            Console.WriteLine("\t[ZAD 2] Dodawanie nowego użytkownika do bazy danych");
            Console.Write("Imię: ");
            string firstName = Console.ReadLine();
            Console.Write("Nazwisko: ");
            string lastName = Console.ReadLine();
            Console.Write("Wiek: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Płeć (np. male/female): ");
            string gender = Console.ReadLine();
            var newUser = new UsersEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Gender = gender
            };
            db.Users.Add(newUser);
            db.SaveChanges();
            Console.WriteLine("======= Lista użytkowników alfabetycznie =======");
            Console.WriteLine("\t[ZAD 2] Zapytanie z sortowaniem");
            var usersASC = db.Users
              .OrderBy(u => u.LastName)
              .ThenBy(u => u.FirstName)
              .ToList();
            foreach (var user in usersASC)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName} ({user.Age} lat, {user.Gender})");
            }
            Console.WriteLine("====================== Zadanie 3 ======================");
            Console.WriteLine("\t[ZAD 3] Realizowane w innym projekcie");
        }
    }
}
