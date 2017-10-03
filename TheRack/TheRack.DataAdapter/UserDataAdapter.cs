using System;
using System.Collections.Generic;
using System.Text;
using TheRack.DataTransfer;
using TheRack.DomainModel;
using static TheRack.DataAdapter.DataAdapterActions;

namespace TheRack.DataAdapter
{
    class UserDataProperty
    {
        public const string ID = "ID";
        public const string Name = "Name";
        public const string UserName = "UserName";
        public const string Password = "Password";
    }

    public class UserDataAdapter : BaseDataAdapter<UserDTO, User>
    {
        public override Dictionary<string, DTORead<UserDTO>> DTOReadMap { get; }
            = new Dictionary<string, DTORead<UserDTO>>
            {
                [UserDataProperty.ID] = (dto) => dto.ID,
                [UserDataProperty.Name] = (dto) => dto.Name,
                [UserDataProperty.UserName] = (dto) => dto.UserName,
                [UserDataProperty.Password] = (dto) => dto.Password
            };

        public override Dictionary<string, DTOWrite<UserDTO>> DTOWriteMap { get; }
             = new Dictionary<string, DTOWrite<UserDTO>>
             {
                 [UserDataProperty.ID] = (dto, value) => dto.ID = value,
                 [UserDataProperty.Name] = (dto, value) => dto.Name = value,
                 [UserDataProperty.UserName] = (dto, value) => dto.UserName = value,
                 [UserDataProperty.Password] = (dto, value) => dto.Password = value
             };

        public override Dictionary<string, DomainModelRead<User>> DomainModelReadMap { get; }
            = new Dictionary<string, DomainModelRead<User>>
            {
                [UserDataProperty.ID] = (entity) => entity.ID,
                [UserDataProperty.Name] = (entity) => entity.Name,
                [UserDataProperty.UserName] = (entity) => entity.UserName,
                [UserDataProperty.Password] = (entity) => entity.Password
            };

        public override Dictionary<string, DomainModelWrite<User>> DomainModelWriteMap { get; }
            = new Dictionary<string, DomainModelWrite<User>>
            {
                [UserDataProperty.ID] = (entity, value) => entity.ID = value,
                [UserDataProperty.Name] = (entity, value) => entity.Name = value,
                [UserDataProperty.UserName] = (entity, value) => entity.UserName = value,
                [UserDataProperty.Password] = (entity, value) => entity.Password = value
            };
    }
}
