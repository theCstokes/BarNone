using BarNone.Shared.DataTransfer.Core;

namespace BarNone.Shared.DataTransfer
{
    public class UserDTO : BaseDTO<UserDTO>
    {
        public override int ID { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
