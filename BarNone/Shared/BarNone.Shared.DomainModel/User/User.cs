using BarNone.Shared.DomainModel.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace BarNone.Shared.DomainModel
{
    [Table("User", Schema = "public")]
    public class User : IDomainModel<User>
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
