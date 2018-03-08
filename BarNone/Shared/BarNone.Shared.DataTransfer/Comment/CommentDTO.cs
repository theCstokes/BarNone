using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer
{
    public class CommentDetailDTO : BaseDetailDTO<CommentDetailDTO>
    {
        public LiftDTO Lift { get; set; }

        public UserDTO SentUser { get; set; }
    }

    public class CommentDTO : BaseParentDTO<CommentDTO, CommentDetailDTO>
    { 
        public override int ID { get; set; }

        public int SentUserID { get; set; }

        public int LiftID { get; set; }

        public DateTime TimeSent { get; set; }
        
        public string Text { get; set; }
    }
}
