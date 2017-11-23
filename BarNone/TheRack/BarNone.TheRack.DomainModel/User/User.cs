using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DomainModel.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace BarNone.TheRack.DomainModel
{
    [Table("User", Schema = "public")]
    public class User : IDomainModel<User>
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public static void CreateFromDTO(UserDTO dto, ConvertConfig config)
        {
            throw new NotImplementedException();
        }

        //protected override UserDTO OnBuildDTO()
        //{
        //    return new UserDTO
        //    {
        //        ID = ID,
        //        Name = Name,
        //        UserName = UserName,
        //        Password = Password
        //    };
        //}

        //protected override void OnPopulate(UserDTO dto, ConvertConfig config = null)
        //{
        //    ID = dto.ID;
        //    Name = dto.Name;
        //    UserName = dto.UserName;
        //    Password = dto.Password;
        //}
    }
}
