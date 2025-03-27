namespace DotNet_Pr2
{
    public class UserClass
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string gender { get; set; }

        public override string ToString()
        {
            return $"UserId:{id} \t| FirstName: {firstName} \t| LastName: {lastName} \t| Age: {age} \t| gender:{gender}";
        }
    }

    public class UserResponse
    {
        public List<UserClass> users { get; set; }

        public override string ToString()
        {
            string result = "";
            foreach (var user in users) {
                result += user.ToString();
                result += Environment.NewLine;
            }
            return result ;
        }
    }
}
