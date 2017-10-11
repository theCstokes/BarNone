using System;
using System.Collections.Generic;
using System.Text;

namespace TheRack.DomainModel
{
    public abstract class BaseDomainModel<TDomainModel>
        where TDomainModel : new()
    {
        public abstract int ID { get; set; }
    }
}
