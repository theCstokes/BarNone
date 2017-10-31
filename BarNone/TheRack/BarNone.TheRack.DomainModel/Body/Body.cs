using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.TheRack.DomainModel
{
    [Table("Body", Schema = "public")]
    public class Body : BaseDomainModel<Body, BodyDataDTO>
    {
        [Key]
        public override int ID { get; set; }

        public Joint Joint { get; set; }

        public override BodyDataDTO BuildDTO()
        {
            throw new NotImplementedException();
        }

        public override void PopulateFromDTO(BodyDataDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
