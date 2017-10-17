using BarNone.Shared.DataTransfer;
using BarNone.Shared.DataTransfer.Core;
using BarNone.TheRack.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarNone.DataLift.APIRequest
{
    public class DataManager
    {
        #region Endpoint Type(s).
        public static Endpoint<UserDTO> Users { get; } =
            new Endpoint<UserDTO>("User");

        public static Endpoint<LiftDTO> Lifts { get; } =
            new Endpoint<LiftDTO>("Lift");
        #endregion
    }
}
