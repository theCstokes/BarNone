using BarNone.TheRack.DataAdapter.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.DataAdapter
{
    public class UserPropertyMap
    {
        public static readonly BasePropertyEnum Name = new BasePropertyEnum("Name", () => new BasicPropertyMapper());

        public static readonly BasePropertyEnum UserName = new BasePropertyEnum("UserName", () => new BasicPropertyMapper());

        public static readonly BasePropertyEnum Password = new BasePropertyEnum("Password", () => new BasicPropertyMapper());

        //public readonly BasePropertyEnum SubFolders = new BasePropertyEnum(new DetailCollectionPropertyMapper<LiftFolderDTO, LiftFolder>(new LiftFolderDataAdapter()));

        //public readonly BasePropertyEnum Lifts = new BasePropertyEnum(new DetailCollectionPropertyMapper<LiftDTO, Lift>(new LiftDataAdapter()));

        //public const string Name = "Name";

        //public const string UserName = "UserName";

        //public const string Password = "Password";
    }
}
