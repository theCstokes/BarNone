using System;
using System.Collections.Generic;
using System.Text;
using TheRack.DataAdapter.Core;
using TheRack.DataTransfer;
using TheRack.DomainModel;
using static TheRack.DataAdapter.Core.DataAdapterActions;

namespace TheRack.DataAdapter
{
    public class UserDataAdapter : BaseDataAdapter<UserDTO, User>
    {
        public override Dictionary<BasePropertyEnum, DTORead<UserDTO>> DTOReadMap { get; }
            = new Dictionary<BasePropertyEnum, DTORead<UserDTO>>
            {
                [BasePropertyEnum.ID] = (dto) => dto.ID,
                [UserPropertyMap.Name] = (dto) => dto.Name,
                [UserPropertyMap.UserName] = (dto) => dto.UserName,
                [UserPropertyMap.Password] = (dto) => dto.Password
            };

        public override Dictionary<BasePropertyEnum, DTOWrite<UserDTO>> DTOWriteMap { get; }
             = new Dictionary<BasePropertyEnum, DTOWrite<UserDTO>>
             {
                 [BasePropertyEnum.ID] = (dto, value) => dto.ID = value,
                 [UserPropertyMap.Name] = (dto, value) => dto.Name = value,
                 [UserPropertyMap.UserName] = (dto, value) => dto.UserName = value,
                 [UserPropertyMap.Password] = (dto, value) => dto.Password = value
             };

        public override Dictionary<BasePropertyEnum, DomainModelRead<User>> DomainModelReadMap { get; }
            = new Dictionary<BasePropertyEnum, DomainModelRead<User>>
            {
                [BasePropertyEnum.ID] = (entity) => entity.ID,
                [UserPropertyMap.Name] = (entity) => entity.Name,
                [UserPropertyMap.UserName] = (entity) => entity.UserName,
                [UserPropertyMap.Password] = (entity) => entity.Password
            };

        public override Dictionary<BasePropertyEnum, DomainModelWrite<User>> DomainModelWriteMap { get; }
            = new Dictionary<BasePropertyEnum, DomainModelWrite<User>>
            {
                [BasePropertyEnum.ID] = (entity, value) => entity.ID = value,
                [UserPropertyMap.Name] = (entity, value) => entity.Name = value,
                [UserPropertyMap.UserName] = (entity, value) => entity.UserName = value,
                [UserPropertyMap.Password] = (entity, value) => entity.Password = value
            };
    }
}
