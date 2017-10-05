using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TheRack.DomainModel
{
    [Table("Account", Schema = "public")]
    public class Account : BaseDomainModel<Account>
    {
        [Key]
        public override int ID { get; set; }

        public Double Balance { get; set; }
    }
}
