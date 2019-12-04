using System.Windows;

namespace WpfMtvUk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Injections.Startup();
        }
    }
}
