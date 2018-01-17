using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.Shared.DomainModel
{
    [Table("Video", Schema = "public")]
    public class VideoRecord : IDomainModel<VideoRecord>, IOwnedDomainModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string Path { get; set; }

        [NotMapped]
        public byte[] Data { get; set; }
    }
}
