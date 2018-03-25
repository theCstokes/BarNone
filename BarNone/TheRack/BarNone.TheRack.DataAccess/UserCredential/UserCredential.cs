using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.TheRack.DataAccess
{
    [Table("UserCredential", Schema = "public")]
    class UserCredential
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string Salt { get; set; }

        public string Password { get; set; }
    }
}
