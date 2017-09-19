using TheRack.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DomainModel
{
    [Model("Public.SharpSight", "User")]
    public class User : BaseDomainModel<User>
    {
        [Key]
        public override int ID { get; set; }

        [Property]
        public string Name { get; set; }
        public string UserName { get; set; }
        public int AccountID { get; set; }
        public string Password { get; set; }
    }
}
