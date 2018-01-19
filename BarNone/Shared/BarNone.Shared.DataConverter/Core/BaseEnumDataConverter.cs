using BarNone.Shared.Core;
using BarNone.Shared.DataConverter;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer.Core;
using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverters.Core
{
    /// <summary>
    /// Base enum data converter.
    /// </summary>
    /// <typeparam name="TType">The type of the type.</typeparam>
    /// <typeparam name="TTypeDTO">The type of the type dto.</typeparam>
    /// <typeparam name="TEType">The type of the e type.</typeparam>
    /// <seealso cref="BarNone.Shared.DataConverter.Core.BaseDataConverter{TType, TTypeDTO, BarNone.Shared.DataConverters.Converters}" />
    public class BaseEnumDataConverter<TType, TTypeDTO, TEType> : BaseDataConverter<TType, TTypeDTO, Converters>
        where TType : BaseEnumDomainModel<TType, TEType>, new()
        where TTypeDTO : BaseTypeDTO<TTypeDTO>, new()
        where TEType : struct 

    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEnumDataConverter{TType, TTypeDTO, TEType}"/> class.
        /// </summary>
        /// <param name="converterContext">The converter context.</param>
        /// <param name="context">The context.</param>
        public BaseEnumDataConverter(Converters converterContext, IDomainContext context) : base(converterContext, context)
        {
        }

        /// <summary>
        /// Creates data model.
        /// Called when [create data model].
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override TType OnCreateDataModel(TTypeDTO dto)
        {
            return new TType
            {
                ID = dto.ID,
                Name = dto.Name,
                Value = dto.Value
            };
        }

        /// <summary>
        /// Creates dto.
        /// Called when [create dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override TTypeDTO OnCreateDTO(TType data)
        {
            return new TTypeDTO
            {
                ID = data.ID,
                Name = data.Name,
                Value = data.Value
            };
        }
    }
}
