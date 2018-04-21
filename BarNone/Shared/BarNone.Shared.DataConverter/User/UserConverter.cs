using BarNone.Shared.Core;
using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverters
{
    /// <summary>
    /// User converter.
    /// </summary>
    /// <seealso cref="BarNone.Shared.DataConverter.Core.BaseDataConverter{BarNone.Shared.DomainModel.User, BarNone.Shared.DataTransfer.UserDTO, BarNone.Shared.DataConverters.Converters}" />
    public class UserConverter : BaseDataConverter<User, UserDTO, Converters>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserConverter"/> class.
        /// </summary>
        /// <param name="converters">The converters.</param>
        /// <param name="context">The context.</param>
        public UserConverter(Converters converters, IDomainContext context) : base(converters, context)
        {
        }

        /// <summary>
        /// Create data model.
        /// Called when [create data model].
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override User OnCreateDataModel(UserDTO dto)
        {
            return new User
            {
                ID = dto.ID,
                Name = dto.Name,
                //Password = dto.Password,
                UserName = dto.UserName
            };
        }

        /// <summary>
        /// Create dto.
        /// Called when [create dto].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override UserDTO OnCreateDTO(User data)
        {
            return new UserDTO
            {
                ID = data.ID,
                Name = data.Name,
                //Password = data.Password,
                UserName = data.UserName
            };
        }
    }
}
