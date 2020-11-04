using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Winzard_System_Repair.RegistryScanner;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for PopupWindow2.xaml
    /// </summary>
    public partial class PopupWindow2 : Window
    {
        public BrowserWindow bwobj;
        bool result = false;
        public PopupWindow2()
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
            popupcrptfilenum.Text = ScannerFunctions.systemissuever.ToString();
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
            popupwindow2.Opacity = 0;
            await Task.Delay(1);
            popupwindow2.Opacity = 0.1;
            await Task.Delay(1);
            popupwindow2.Opacity = 0.2;
            await Task.Delay(1);
            popupwindow2.Opacity = 0.3;
            await Task.Delay(1);
            popupwindow2.Opacity = 0.4;
            await Task.Delay(1);
            popupwindow2.Opacity = 0.5;
            await Task.Delay(1);
            popupwindow2.Opacity = 0.6;
            await Task.Delay(1);
            popupwindow2.Opacity = 0.7;
            await Task.Delay(1);
            popupwindow2.Opacity = 0.8;
            await Task.Delay(1);
            popupwindow2.Opacity = 0.9;
            await Task.Delay(1);
            popupwindow2.Opacity = 1;
        }

        private void fixissue_Click(object sender, RoutedEventArgs e)
        {
            BrowserWindow bwObj = new BrowserWindow();
            result = bwObj.ShowDialog().Value;
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = result;
        }

        private void donotshowagain_Checked(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream("sysset.bin", FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(true);
            fs.Close();
        }
    }
}
