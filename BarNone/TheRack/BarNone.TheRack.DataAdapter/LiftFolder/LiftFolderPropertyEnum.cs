using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAdapter.Core;
using BarNone.TheRack.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.TheRack.DataAdapter
{
    public class LiftFolderPropertyEnum
    {
        public static readonly BasePropertyEnum Name = new BasePropertyEnum("Name", () => new BasicPropertyMapper());

        public static readonly BasePropertyEnum ParentID = new BasePropertyEnum("ParentID", () => new BasicPropertyMapper());

        public static readonly BasePropertyEnum Parent = new BasePropertyEnum("Parent", () => new DetailPropertyMapper<LiftFolderDTO, LiftFolder>(new LiftFolderDataAdapter()));

        public static readonly BasePropertyEnum SubFolders = new BasePropertyEnum("SubFolders", () => new DetailCollectionPropertyMapper<LiftFolderDTO, LiftFolder>(new LiftFolderDataAdapter()));

        public static readonly BasePropertyEnum Lifts = new BasePropertyEnum("Lifts", () => new DetailCollectionPropertyMapper<LiftDTO, Lift>(new LiftDataAdapter()));

        //public const string Name = "Name";

        //public const string ParentID = "ParentID";

        //public const string SubFolders = "SubFolders";

        //public const string Lifts = "Lifts";
    }
}
