using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Pr2
{
    public class ToDoEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Task { get; set; }
        public bool IsDone { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public UsersEntity User { get; set; }
    }

    public class UsersEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public List<ToDoEntity> ToDos { get; set; }
    }
}
