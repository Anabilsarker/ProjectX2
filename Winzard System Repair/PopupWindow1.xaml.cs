using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for PopupWindow1.xaml
    /// </summary>
    public partial class PopupWindow1 : Window
    {
        public BrowserWindow bwobj;
        bool result = false;
        public PopupWindow1()
        {
            InitializeComponent();
            RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
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
            int corruptsysfiles = rnd.Next(20, 100);
            CorruptedSystemFiles.Text = corruptsysfiles.ToString();
            int driver = rnd.Next(1, 20);
            OutdatedDrivers.Text = driver.ToString();
            int junk = rnd.Next(1, 10);
            JunkFiles.Text = junk.ToString() + "GB";
            int brknreg = rnd.Next(100, 2000);
            BrokenRegistryEntries.Text = brknreg.ToString();
            int startup = rnd.Next(1, 20);
            Startupoptimizations.Text = startup.ToString();
            int privacy = rnd.Next(100, 700);
            PrivacyTraces.Text = privacy.ToString();
            int frgfiles = rnd.Next(1, 10);
            FragmentedFiles.Text = frgfiles.ToString() + "%";
            int sysopt = rnd.Next(10, 30);
            Systemoptimizations.Text = sysopt.ToString();
            /*int screenheight = Screen.PrimaryScreen.Bounds.Height;
            int screenwidth = Screen.PrimaryScreen.Bounds.Width;
            popupwindow1.Top = screenheight - 700;
            popupwindow1.Left = screenwidth - 300;*/
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
            popupwindow1.Opacity = 0;
            await Task.Delay(1);
            popupwindow1.Opacity = 0.1;
            await Task.Delay(1);
            popupwindow1.Opacity = 0.2;
            await Task.Delay(1);
            popupwindow1.Opacity = 0.3;
            await Task.Delay(1);
            popupwindow1.Opacity = 0.4;
            await Task.Delay(1);
            popupwindow1.Opacity = 0.5;
            await Task.Delay(1);
            popupwindow1.Opacity = 0.6;
            await Task.Delay(1);
            popupwindow1.Opacity = 0.7;
            await Task.Delay(1);
            popupwindow1.Opacity = 0.8;
            await Task.Delay(1);
            popupwindow1.Opacity = 0.9;
            await Task.Delay(1);
            popupwindow1.Opacity = 1;
        }

        private void popupfixclean_Click(object sender, RoutedEventArgs e)
        {
            BrowserWindow bwObj = new BrowserWindow();
            result = bwObj.ShowDialog().Value;
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = result;
        }
    }
}
