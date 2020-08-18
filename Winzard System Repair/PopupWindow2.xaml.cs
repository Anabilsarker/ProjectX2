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
            int corruptnum = rnd.Next(1, 100);
            popupcrptfilenum.Text = corruptnum.ToString();
            int screenheight = Screen.PrimaryScreen.Bounds.Height;
            int screenwidth = Screen.PrimaryScreen.Bounds.Width;
            popupwindow2.Top = screenheight - 350;
            popupwindow2.Left = screenwidth - 600;
            popupwindow2.Opacity = 0;
            await Task.Delay(20);
            popupwindow2.Opacity = 0.25;
            await Task.Delay(20);
            popupwindow2.Opacity = 0.50;
            await Task.Delay(20);
            popupwindow2.Opacity = 0.75;
            await Task.Delay(20);
            popupwindow2.Opacity = 1;
        }

        private void fixissue_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
