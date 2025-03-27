using System.Text.Json;

namespace DotNet_Pr2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DownlowdDataFromAPI dAPI = new DownlowdDataFromAPI();
            string userJson =  dAPI.GetData("https://dummyjson.com/users").Result;
            string todosJson = dAPI.GetData("https://dummyjson.com/todos").Result;
            Console.WriteLine("======= Users =======");
            var userDeserialize  = JsonSerializer.Deserialize<UserResponse>(userJson);
            Console.WriteLine(userDeserialize.ToString());
            Console.WriteLine("======= ToDos =======");
            var todosDeserialize = JsonSerializer.Deserialize<ToDoResponse>(todosJson);
            Console.WriteLine(todosDeserialize.ToString());
        }
    }
}
