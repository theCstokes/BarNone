using System;
using System.Collections.Generic;
using System.Text;
using TheRack.DataAdapter.Core;
using TheRack.DataTransfer.Lift;
using TheRack.DomainModel;

namespace TheRack.DataAdapter
{
    public class LiftPropertyEnum
    {
        public static readonly BasePropertyEnum Name = new BasePropertyEnum("Name", () => new BasicPropertyMapper());

        public static readonly BasePropertyEnum ParentID = new BasePropertyEnum("ParentID", () => new BasicPropertyMapper());

        public static readonly BasePropertyEnum Parent = new BasePropertyEnum("Parent", () => new DetailPropertyMapper<LiftFolderDTO, LiftFolder>(new LiftFolderDataAdapter()));

        //public readonly BasePropertyEnum Lifts = new BasePropertyEnum(new DetailCollectionPropertyMapper<LiftDTO, Lift>(new LiftDataAdapter()));

        //public const string Name = "Name";

        //public const string ParentID = "ParentID";

        //public const string Parent = "Parent";
    }
}
