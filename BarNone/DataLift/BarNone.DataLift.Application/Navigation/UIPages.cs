using BarNone.DataLift.UI.Views;
using System.Windows.Controls;

namespace BarNone.DataLift.UI.Nav
{
    /// <summary>
    /// Class containing static onstances of views to keep page management fast
    /// </summary>
    public class UIPages
    {
        /// <summary>
        /// Reference to the login page
        /// </summary>
        public static readonly UIPages LoginView = new UIPages(new LoginScreen());
        /// <summary>
        /// Reference to the registration page
        /// </summary>
        public static readonly UIPages RegistrationView = new UIPages(new RegistrationScreen());
        /// <summary>
        /// Reference to the control holder page
        /// </summary>
        public static readonly UIPages ControlHolderView = new UIPages(new ControlHolderScreen());

        /// <summary>
        /// Reference to the login page
        /// </summary>
        public static readonly UIPages DataRecorderView = new UIPages(new DataRecorderScreen());
        /// <summary>
        /// Reference to the registration page
        /// </summary>
        public static readonly UIPages EditLiftView = new UIPages(new EditLiftsScreen());
        /// <summary>
        /// Reference to the control holder page
        /// </summary>
        public static readonly UIPages SaveLiftView = new UIPages(new SaveLiftScreen());

        /// <summary>
        /// UserControl reference for the UIPage instance
        /// </summary>
        public UserControl Page { get; }

        /// <summary>
        /// Creates a new UIPage referenced to the singleton of <paramref name="page"/>
        /// </summary>
        /// <param name="page">Usercontrol pointed to by this</param>
        private UIPages(UserControl page)
        {
            Page = page;
        }
    }
}
