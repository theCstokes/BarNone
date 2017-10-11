using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.TheRack.Core
{
    public abstract class MapEnum<TElement>
        where TElement : MapEnum<TElement>
    {
        #region Protected Static Read Only Field(s).
        private static int count = 0;
        private static readonly Dictionary<int, TElement> Types = new Dictionary<int, TElement>();
        #endregion

        #region Protected Constructor(s).
        public MapEnum()
        {
            count++;
            this.Value = count;
            Types[count] = Instance;
        }
        #endregion

        #region Protected Property(s).
        public int Value { get; private set; }
        #endregion

        #region Public Static Member(s).
        public static List<TElement> Values
        {
            get
            {
                return Types.Values.ToList();
            }
        }
        #endregion

        #region Public Static Implicit Operator(s).
        public static implicit operator int(MapEnum<TElement> type)
        {
            return type.Value;
        }

        public static implicit operator MapEnum<TElement>(int value)
        {
            return Types[value];
        }
        #endregion

        #region Protected Abstract Property(s).
        protected abstract TElement Instance { get; }
        #endregion
    }
}
