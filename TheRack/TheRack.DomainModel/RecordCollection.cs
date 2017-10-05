using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TheRack.DomainModel
{
    [Table("RecordCollection", Schema = "public")]
    public class RecordCollection : BaseDomainModel<User>
    {
        [Key]
        public override int ID { get; set; }

        public string Name { get; set; }

        public List<Record> Records { get; set; }
    }
}
