using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.Core
{
    public abstract class Mapper<TElement> where TElement : Mapper<TElement>
    {
        #region Protected Static Read Only Field(s).
        protected static readonly Dictionary<int, TElement> Types = new Dictionary<int, TElement>();
        #endregion

        #region Protected Constructor(s).
        public Mapper(int value)
        {
            this.value = value;
            Types[value] = Instance;
        }
        #endregion

        #region Protected Property(s).
        protected int value { get; private set; }
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
        public static implicit operator Mapper<TElement>(TElement type)
        {
            return type.value;
        }

        public static implicit operator Mapper<TElement>(int value)
        {
            return Types[value];
        }
        #endregion

        #region Protected Abstract Property(s).
        protected abstract TElement Instance { get; }
        #endregion
    }
}
