using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TheRack.DomainModel
{
    [Table("User", Schema = "public")]
    public class User : BaseDomainModel<User>
    {
        [Key]
        public override int ID { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        [ForeignKey("Account")]
        public int? AccountID { get; set; }

        public Account Account { get; set; }
    }
}
