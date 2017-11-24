using BarNone.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarNone.DataLift.DomainModel.Core
{
    public interface IDataModel<TDataModel> : ITrackable<TDataModel>
        where TDataModel : IDataModel<TDataModel>, new()
    {
    }
}
