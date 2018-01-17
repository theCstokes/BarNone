using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.DomainModel.Core
{
    public interface IOwnedDomainModel
    {
        int UserID { get; set; }
    }
}
