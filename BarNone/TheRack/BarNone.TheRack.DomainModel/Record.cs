using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.TheRack.DomainModel
{
    [Table("Record", Schema = "public")]
    public class Record : BaseDomainModel<User>
    {
        [Key]
        public override int ID { get; set; }

        public string Name { get; set; }

        public int RecordCollectionID { get; set; }

        [ForeignKey("RecordCollectionID")]
        public RecordCollection RecordCollection { get; set; }
    }
}
