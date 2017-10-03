using System;
using System.Collections.Generic;
using System.Text;

namespace TheRack.DataTransfer
{
    public class UserDTO : BaseDTO<UserDTO>
    {
        public override int ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
