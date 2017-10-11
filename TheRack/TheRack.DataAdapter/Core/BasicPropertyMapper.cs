using System;
using System.Collections.Generic;
using System.Text;

namespace TheRack.DataAdapter.Core
{
    public class BasicPropertyMapper : IPropertyMapper
    {
        public dynamic ToDTO(dynamic value)
        {
            return value;
        }

        public dynamic ToDomainModel(dynamic value)
        {
            return value;
        }
    }
}
