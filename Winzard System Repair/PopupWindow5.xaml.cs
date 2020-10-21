using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for PopupWindow5.xaml
    /// </summary>
    public partial class PopupWindow5 : Window
    {
        public PopupWindow5()
        {
            InitializeComponent();
            RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
            location();
        }

        private void location()
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
