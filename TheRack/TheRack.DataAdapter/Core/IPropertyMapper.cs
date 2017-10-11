using System;
using System.Collections.Generic;
using System.Text;

namespace TheRack.DataAdapter.Core
{
    public interface IPropertyMapper
    {
        dynamic ToDTO(dynamic value);

        dynamic ToDomainModel(dynamic value);
    }
}
