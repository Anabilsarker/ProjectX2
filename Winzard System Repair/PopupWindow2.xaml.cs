using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

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
            location();
        }

        private void popupclose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void location()
        {
            Random rnd = new Random();
            int corruptnum = rnd.Next(10, 100);
            popupcrptfilenum.Text = corruptnum.ToString();
            int screenheight = Screen.PrimaryScreen.Bounds.Height;
            int screenwidth = Screen.PrimaryScreen.Bounds.Width;
            popupwindow2.Top = screenheight - 350;
            popupwindow2.Left = screenwidth - 600;
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
    }
}
