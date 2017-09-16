using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.DataAccess
{
    public class DomainParameters
    {
        private const string ACCOUNT_ID = "accountID";
        public int AccountID { get; set; }

        public static readonly Dictionary<string, Action<DomainParameters, string>> DataMap
            = new Dictionary<string, Action<DomainParameters, string>>
            {
                [ACCOUNT_ID] = (domainParams, value) => domainParams.AccountID = Convert.ToInt32(value)
            };
    }
}
