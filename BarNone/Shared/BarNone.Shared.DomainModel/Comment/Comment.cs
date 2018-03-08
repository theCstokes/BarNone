using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.Shared.DomainModel
{
    [Table("Comment", Schema = "public")]
    public class Comment : IDomainModel<Comment>, IOwnedDomainModel
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }

        public int LiftID { get; set; }

        public DateTime TimeSent { get; set; }

        public string Text { get; set; }

        [ForeignKey("LiftID")]
        public Lift Lift { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
