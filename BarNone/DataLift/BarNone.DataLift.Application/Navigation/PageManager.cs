namespace BarNone.DataLift.UI.Nav
{
    /// <summary>
    /// Class to manage the control of which parent UserControl is present
    /// </summary>
    public static class PageManager
    {
        /// <summary>
        /// Window being controlled
        /// </summary>
        internal static MainWindow Window { private get; set; }

        /// <summary>
        /// Switches the page to <paramref name="newPage"/>
        /// </summary>
        /// <param name="newPage">Page to switch to</param>
        public static void SwitchPage(UIPages newPage)
        {
            Window.Navigate(newPage);
        }
    }
}
