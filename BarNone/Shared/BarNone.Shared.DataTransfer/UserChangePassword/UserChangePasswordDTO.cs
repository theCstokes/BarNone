using BarNone.Shared.DataTransfer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer
{
    public class UserChangePasswordDTO: BaseDTO<UserChangePasswordDTO>
    {
        public override int ID { get; set; }
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string RetypePassword { get; set; }
       
    }
}
