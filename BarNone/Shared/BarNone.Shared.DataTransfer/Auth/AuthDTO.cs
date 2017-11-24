namespace BarNone.Shared.DataTransfer.Auth
{
    public class AuthDTO
    {
        public string Access_Token { get; set; }

        public int Expires_In { get; set; }

        public bool Authorized { get; set; }
    }
}
