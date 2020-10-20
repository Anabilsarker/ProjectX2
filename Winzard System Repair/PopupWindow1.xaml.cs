using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Winzard_System_Repair;
using Winzard_System_Repair.RegistryScanner;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for PopupWindow1.xaml
    /// </summary>
    public partial class PopupWindow1 : Window
    {
        public static int regissuenum = ScannerFunctions.arrBadRegistryKeys.Problems;
        float sizedir = 0;
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
        public long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
        private async void location()
        {
            try
            {
                string Temppath = Path.GetTempPath();
                string ICacheChromepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Cache\\";
                string ICacheFirefoxpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Mozilla\\Firefox\\Profiles\\";
                string ICacheExpolrerpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Local\\Microsoft\\Edge\\User Data\\Default\\";
                //string ICacheChromepath1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Application Cache\\Cache\\";
                double Tempsize = 0;
                Tempsize = DirSize(new DirectoryInfo(Temppath));
                Tempsize += DirSize(new DirectoryInfo("C:/Windows/Prefetch"));
                Tempsize += DirSize(new DirectoryInfo("C:/Windows/Temp"));
                Tempsize += DirSize(new DirectoryInfo("C:/Windows/SoftwareDistribution/Download"));
                //Tempsize += DirSize(new DirectoryInfo(ICacheChromepath));
                //Tempsize += DirSize(new DirectoryInfo(ICacheFirefoxpath));
                //Tempsize += DirSize(new DirectoryInfo(ICacheExpolrerpath));
                sizedir = (float)Convert.ToDouble(string.Format("{0:0.00}", (Tempsize / (1024 * 1024))));
                
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                string regvalue = reg.ValueCount.ToString();
                Startupoptimizations.Text = regvalue;
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
            MalwareThreats.Text = ScannerFunctions.malwarlist.Count.ToString();
            CorruptedSystemFiles.Text = ScannerFunctions.filever.ToString();
            JunkFiles.Text = sizedir.ToString() + "MB";
            BrokenRegistryEntries.Text = ScannerFunctions.mynum.ToString() + "Items";
            PrivacyTraces.Text = ScannerFunctions.totalcachenum.ToString();
            SystemIssue.Text = ScannerFunctions.systemissuever.ToString(); ;
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
