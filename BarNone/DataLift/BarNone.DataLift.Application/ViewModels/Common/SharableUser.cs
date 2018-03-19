namespace BarNone.DataLift.UI.ViewModels.Common
{
    public class SharableUser
    {
        public string Name
        {
            get; set;
        }
        public string UserName
        {
            get; set;
        }

        public string Code
        {
            get
            {
                return UserName[0].ToString();
            }

            private set { }
        }

        public bool IsSeletcted
        {
            get; set;
        }

        public int ID
        {
            get; set;
        }

        public SharableUser()
        {
            IsSeletcted = false;
        }
    }
}
