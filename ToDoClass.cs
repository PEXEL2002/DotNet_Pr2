using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Pr2
{
    class ToDoClass
    {
        public int id { get; set; }
        public string todo { get; set; }
        public bool completed { get; set; }
        public int userId { get; set; }

        public override string ToString()
        {
            return $"ID: {id} \t | Completed: {completed} \t | UserID: {userId} \t | ToDo: {todo}";
        }
    }

    class ToDoResponse
    {
        public List<ToDoClass> todos { get; set; }
        public override string ToString()
        {
            string result = "";
            foreach (var todo in todos)
            {
                result += todo.ToString();
                result += Environment.NewLine;
            }
            return result;
        }
    }
}
