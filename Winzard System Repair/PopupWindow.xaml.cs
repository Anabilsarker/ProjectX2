using System.Windows;
using System.Timers;
using System.Threading;
using System.Diagnostics;
using System;
using System.Windows.Media;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : Window
    {
        public BrowserWindow bwobj;
        bool result = false;
        public PopupWindow()
        {
            InitializeComponent();
            RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
        }

        private void paybutton_Click(object sender, RoutedEventArgs e)
        {
            BrowserWindow bwObj = new BrowserWindow();
            result = bwObj.ShowDialog().Value;
            Close();
        }

        private void popupclose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = result;
        }
    }
}
