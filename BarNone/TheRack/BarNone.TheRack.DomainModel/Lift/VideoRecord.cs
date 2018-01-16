using BarNone.TheRack.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.TheRack.DomainModel
{
    [Table("Video", Schema = "public")]
    public class VideoRecord : IDomainModel<LiftFolder>, IOwnedDomainModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string Path { get; set; }
    }
}
