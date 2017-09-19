using TheRack.DataAccessAdapter;
using TheRack.DataTransfer.UserDTO;
using TheRack.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheRack.DataAccessAdapter.DataAccessActions;

namespace TheRack.DataAccessAdapter
{
    public class UserAdapter : BaseAdapter<UserDTO, User>
    {
        public override string SchemaName
        {
            get
            {
                return "public";
            }
        }

        public override string TableName
        {
            get
            {
                return "User";
            }
        }

        public override MapToDomainModel<UserDTO, User> ToDomainModel
        {
            get
            {
                return (dto) =>
                {

                    return new User
                    {
                        ID = dto.ID,
                        Name = dto.Name,
                        UserName = dto.UserName,
                        AccountID = dto.AccountID,
                        Password = dto.Password
                    };
                };
            }
        }

        public override MapToDTO<User, UserDTO> ToDTO
        {
            get
            {
                return (user) =>
                {

                    return new UserDTO
                    {
                        ID = user.ID,
                        Name = user.Name,
                        UserName = user.UserName,
                        AccountID = user.AccountID,
                        Password = user.Password
                    };
                };
            }
        }

        public override Dictionary<string, ReadAction<UserDTO>> ReadMap
        {
            get
            {
                return new Dictionary<string, ReadAction<UserDTO>>
                {
                    [UserColumnData.NAME] = dto => dto.Name,
                    [UserColumnData.USER_NAME] = dto => dto.UserName,
                    [UserColumnData.ACCOUNT_ID] = dto => dto.AccountID,
                    [UserColumnData.PASSWORD] = dto => dto.Password
                };
            }
        }

        public override Dictionary<string, WriteAction<User>> WriteMap
        {
            get
            {
                return new Dictionary<string, WriteAction<User>>
                {
                    [UserColumnData.NAME] = (model, value) => model.Name = value,
                    [UserColumnData.USER_NAME] = (model, value) => model.UserName = value,
                    [UserColumnData.ACCOUNT_ID] = (model, value) => model.AccountID = value,
                    [UserColumnData.PASSWORD] = (model, value) => model.Password = value
                };
            }
        }
    }
}
