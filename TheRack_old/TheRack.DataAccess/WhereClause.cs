using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess
{
    public class WhereClause
    {
        #region Public Constructor(s).
        public WhereClause()
        {

        }
        #endregion

        #region Public Property(s).
        public string Name { get; set; }
        public string Value { get; set; }
        public WhereOperation Operation { get; set; }
        public WhereOperation Then { get; set; }
        #endregion
    }
}
