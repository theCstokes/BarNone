using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TheRack.DomainModel
{
    [Table("User", Schema = "public")]
    public class User
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
