using TheRack.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DomainModel
{
    public class User : BaseDomainModel<User>
    {
        public override int ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int AccountID { get; set; }
        public string Password { get; set; }
    }
}
