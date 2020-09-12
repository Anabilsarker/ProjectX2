using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using WPFUI;

namespace Winzard_System_Repair
{
    /// <summary>
    /// Interaction logic for PopupWindow4.xaml
    /// </summary>
    public partial class PopupWindow4 : Window
    {
        public BrowserWindow bwobj;
        bool result = false;
        public PopupWindow4()
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
            /*int screenheight = Screen.PrimaryScreen.Bounds.Height;
            int screenwidth = Screen.PrimaryScreen.Bounds.Width;
            popupwindow4.Top = screenheight - 450;
            popupwindow4.Left = screenwidth - 800;*/
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
            popupwindow4.Opacity = 0;
            await Task.Delay(1);
            popupwindow4.Opacity = 0.1;
            await Task.Delay(1);
            popupwindow4.Opacity = 0.2;
            await Task.Delay(1);
            popupwindow4.Opacity = 0.3;
            await Task.Delay(1);
            popupwindow4.Opacity = 0.4;
            await Task.Delay(1);
            popupwindow4.Opacity = 0.5;
            await Task.Delay(1);
            popupwindow4.Opacity = 0.6;
            await Task.Delay(1);
            popupwindow4.Opacity = 0.7;
            await Task.Delay(1);
            popupwindow4.Opacity = 0.8;
            await Task.Delay(1);
            popupwindow4.Opacity = 0.9;
            await Task.Delay(1);
            popupwindow4.Opacity = 1;
        }

        private void upgradenow_Click(object sender, RoutedEventArgs e)
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
