//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace BarNone.Shared.Core
//{
//    /// <summary>
//    /// Base map enum for 'Java' type enums.
//    /// </summary>
//    /// <typeparam name="TElement">The type of the element.</typeparam>
//    public abstract class MapEnum<TElement>
//        where TElement : MapEnum<TElement>
//    {
//        #region Protected Static Read Only Field(s).        
//        /// <summary>
//        /// The count of elements.
//        /// </summary>
//        private static int count = 0;

//        /// <summary>
//        /// The types mapping.
//        /// </summary>
//        private static readonly Dictionary<int, TElement> Types = new Dictionary<int, TElement>();
//        #endregion

//        #region Protected Constructor(s).        
//        /// <summary>
//        /// Initializes a new instance of the <see cref="MapEnum{TElement}"/> class.
//        /// </summary>
//        public MapEnum()
//        {
//            count++;
//            this.Value = count;
//            Types[count] = Instance;
//        }
//        #endregion

//        #region Protected Property(s).        
//        /// <summary>
//        /// Gets the value.
//        /// </summary>
//        /// <value>
//        /// The value.
//        /// </value>
//        public int Value { get; private set; }
//        #endregion

//        #region Public Static Member(s).        
//        /// <summary>
//        /// Gets the values.
//        /// </summary>
//        /// <value>
//        /// The values.
//        /// </value>
//        public static List<TElement> Values
//        {
//            get
//            {
//                return Types.Values.ToList();
//            }
//        }
//        #endregion

//        #region Public Static Implicit Operator(s).        
//        /// <summary>
//        /// Performs an implicit conversion from <see cref="MapEnum{TElement}"/> to <see cref="System.Int32"/>.
//        /// </summary>
//        /// <param name="type">The type.</param>
//        /// <returns>
//        /// The result of the conversion.
//        /// </returns>
//        public static implicit operator int(MapEnum<TElement> type)
//        {
//            return type.Value;
//        }

//        /// <summary>
//        /// Performs an implicit conversion from <see cref="System.Int32"/> to <see cref="MapEnum{TElement}"/>.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns>
//        /// The result of the conversion.
//        /// </returns>
//        public static implicit operator MapEnum<TElement>(int value)
//        {
//            return Types[value];
//        }
//        #endregion

//        #region Protected Abstract Property(s).        
//        /// <summary>
//        /// Gets the instance.
//        /// </summary>
//        /// <value>
//        /// The instance.
//        /// </value>
//        protected abstract TElement Instance { get; }
//        #endregion
//    }
//}
