using BarNone.Shared.DataConverter.Core;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataConverters
{
    public class UserConverter : BaseDataConverter<User, UserDTO, Converters>
    {
        public UserConverter(Converters converters) : base(converters)
        {
        }

        public override User OnCreateDataModel(UserDTO dto)
        {
            return new User
            {
                ID = dto.ID,
                Name = dto.Name,
                Password = dto.Password,
                UserName = dto.UserName
            };
        }

        public override UserDTO OnCreateDTO(User data)
        {
            return new UserDTO
            {
                ID = data.ID,
                Name = data.Name,
                Password = data.Password,
                UserName = data.UserName
            };
        }
    }
}
