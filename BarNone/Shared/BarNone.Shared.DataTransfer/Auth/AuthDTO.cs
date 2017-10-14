using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.Shared.DataTransfer.Auth
{
    public class AuthDTO
    {
        public string Access_Token { get; set; }

        public int Expires_In { get; set; }
    }
}
