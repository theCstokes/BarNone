using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using BarNone.Shared.DomainModel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BarNone.TheRack.DomainModel
{
    [Table("User", Schema = "public")]
    public class User : DomainModel<User, UserDTO>
    {
        [Key]
        public override int ID { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        protected override UserDTO OnBuildDTO()
        {
            return new UserDTO
            {
                ID = ID,
                Name = Name,
                UserName = UserName,
                Password = Password
            };
        }

        protected override void OnPopulate(UserDTO dto, ConvertConfig config = null)
        {
            ID = dto.ID;
            Name = dto.Name;
            UserName = dto.UserName;
            Password = dto.Password;
        }

        //public override UserDTO BuildDTO()
        //{
        //    return new UserDTO
        //    {
        //        ID = ID,
        //        Name = Name,
        //        UserName = UserName,
        //        Password = Password
        //    };
        //}

        //public override void PopulateFromDTO(UserDTO dto)
        //{
        //    ID = dto.ID;
        //    Name = dto.Name;
        //    UserName = dto.UserName;
        //    Password = dto.Password;
        //}
    }
}
