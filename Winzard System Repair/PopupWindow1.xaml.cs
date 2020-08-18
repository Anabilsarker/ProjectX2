using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for PopupWindow1.xaml
    /// </summary>
    public partial class PopupWindow1 : Window
    {
        public PopupWindow1()
        {
            InitializeComponent();
            location();
        }

        private void popupclose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void location()
        {
            Random rnd = new Random();
            int malware = rnd.Next(1, 20);
            MalwareThreats.Text = malware.ToString();
            int holes = rnd.Next(1, 20);
            SecurityHoles.Text = holes.ToString();
            int corruptsysfiles = rnd.Next(1, 100);
            CorruptedSystemFiles.Text = corruptsysfiles.ToString();
            int driver = rnd.Next(1, 20);
            OutdatedDrivers.Text = driver.ToString();
            int junk = rnd.Next(1, 10);
            JunkFiles.Text = junk.ToString() + "GB";
            int brknreg = rnd.Next(1, 2000);
            BrokenRegistryEntries.Text = brknreg.ToString();
            int startup = rnd.Next(1, 20);
            Startupoptimizations.Text = startup.ToString();
            int privacy = rnd.Next(1, 700);
            PrivacyTraces.Text = privacy.ToString();
            int frgfiles = rnd.Next(1, 10);
            FragmentedFiles.Text = frgfiles.ToString() + "%";
            int sysopt = rnd.Next(1, 30);
            Systemoptimizations.Text = sysopt.ToString();
            int screenheight = Screen.PrimaryScreen.Bounds.Height;
            int screenwidth = Screen.PrimaryScreen.Bounds.Width;
            popupwindow1.Top = screenheight - 700;
            popupwindow1.Left = screenwidth - 300;
            popupwindow1.Opacity = 0;
            await Task.Delay(20);
            popupwindow1.Opacity = 0.25;
            await Task.Delay(20);
            popupwindow1.Opacity = 0.50;
            await Task.Delay(20);
            popupwindow1.Opacity = 0.75;
            await Task.Delay(20);
            popupwindow1.Opacity = 1;
        }
    }
}
