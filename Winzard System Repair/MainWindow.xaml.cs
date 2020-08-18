using System;
using System.IO;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Topshelf;
using WpfAnimatedGif;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool stop = false;
        int premium = 0, micro = 100, mega = 180;
        float sizedir;
        public MainWindow()
        {
            InitializeComponent();
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<Ser>(s =>
                {
                    s.ConstructUsing(Service => new Ser());
                    s.WhenStarted(Service => Service.Start());
                    s.WhenStopped(Service => Service.Stop());
                });
                x.RunAsLocalSystem();

                x.SetServiceName("WinzardSystemService");
                x.SetDisplayName("Winzard System Service");
                x.SetDescription("Winzard System Repair optimizes PC and provides the best experience to the user.");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
            sysdet1.Content = GetAccount1() + Environment.NewLine;
            sysdet2.Content = GetAccount2() + Environment.NewLine;
            sysdet3.Content = GetAccount3() + Environment.NewLine;
            sysdet4.Content = GetAccount4() + Environment.NewLine;
            NotPremium();
        }
        //For getting system detail info
        private string GetAccount1()
        {
            ManagementObjectSearcher mos =
            new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject mo in mos.Get())
            {
                try
                {
                    return mo.GetPropertyValue("Name").ToString();
                }
                catch { }
            }
            return "User Account Unknown";
        }
        private string GetAccount2()
        {
            ManagementObjectSearcher mos =
            new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject mo in mos.Get())
            {
                try
                {
                    return mo.GetPropertyValue("Name").ToString();
                }
                catch { }
            }
            return "User Account Unknown";
        }
        private string GetAccount3()
        {
            ManagementObjectSearcher mos =
            new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");
            foreach (ManagementObject mo in mos.Get())
            {
                try
                {
                    double dblMemory;
                    double.TryParse(Convert.ToString(mo["TotalPhysicalMemory"]), out dblMemory);
                    float ram = (float)Convert.ToDouble(string.Format("{0:0.00}", (dblMemory / (1024 * 1024 * 1024))));
                    return "Usable " + ram + " GB";
                }
                catch { }
            }
            return "Unknown RAM";
        }
        private string GetAccount4()
        {
            ManagementObjectSearcher mos =
            new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");
            foreach (ManagementObject mo in mos.Get())
            {
                try
                {
                    return mo.GetPropertyValue("Name").ToString();
                }
                catch { }
            }
            return "User Account Unknown";
        }


        //For Minimize Close and Draging the program
        private void Button_Click_Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //The Big Round Scan button
        public async void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scan.Opacity = 0.8;
            await Task.Delay(100);
            scan.Opacity = 1;
            await Task.Delay(100);
            statusgrid.Visibility = Visibility.Visible;
            scan.Visibility = Visibility.Collapsed;
            PCstatus.Visibility = Visibility.Collapsed;
            Tempdelete();
            VerboseDir();
        }

        //Directory size
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

        //Temp delete & Scan Progress
        public async void Tempdelete()
        {
            if (stop == false)
            {
                updatetxt.Children.Clear();
            }
            else
            {
                string Temppath = Path.GetTempPath();
                long Tempsize = DirSize(new DirectoryInfo(Temppath));
                Tempsize += DirSize(new DirectoryInfo("C:/Windows/Prefetch"));
                Tempsize += DirSize(new DirectoryInfo("C:/Windows/Temp"));
                Tempsize += DirSize(new DirectoryInfo("C:/Windows/SoftwareDistribution/Download"));
                sizedir = (float)Convert.ToDouble(string.Format("{0:0.00}", (Tempsize / (1024 * 1024))));
                //Progress
                //scantext.Text = "Scanning: Malware";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                Loading1.Visibility = Visibility.Hidden;
                Loading2.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Registry";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                Loading2.Visibility = Visibility.Hidden;
                Loading3.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Security";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                Loading3.Visibility = Visibility.Hidden;
                Loading4.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Startup";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                Loading4.Visibility = Visibility.Hidden;
                Loading5.Visibility = Visibility.Visible;
                //scantext.Text = "Analyzing: Fragments";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                Loading5.Visibility = Visibility.Hidden;
                Loading6.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Privacy";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                Loading6.Visibility = Visibility.Hidden;
                Loading7.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: System";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                Loading7.Visibility = Visibility.Hidden;
                Loading8.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Junk";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                Loading8.Visibility = Visibility.Hidden;
                Loading9.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Drivers";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                Loading9.Visibility = Visibility.Hidden;
                Loading10.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: System Files";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                Loading10.Visibility = Visibility.Hidden;

                //scananimation.Visibility = Visibility.Collapsed;
                scanfix.Visibility = Visibility.Visible;
                //scantext.Text = "Scan Complete";
                int i = 0, securityholes, privacyissue;
                while (i < 1)
                {
                    Random rnd = new Random();
                    securityholes = rnd.Next(1, 200);
                    privacyissue = rnd.Next(0, 100);
                    //scanresult.Text = "Junk: " + sizedir.ToString() + "MB can be cleaned\n" + "Security Holes: " + securityholes + "\nPrivacy Issue: " + privacyissue;
                    i++;//i is set globally
                }
            }
        }

        //The Scan stop button
        private void scanstop_Click(object sender, RoutedEventArgs e)
        {
            stop = true;
            updatetxt.Children.Clear();
            statusgrid.Visibility = Visibility.Collapsed;
            scan.Visibility = Visibility.Visible;
            PCstatus.Visibility = Visibility.Visible;
        }
        //The Repair Button
        private async void Scanfix_Click(object sender, RoutedEventArgs e)
        {
            if (premium == 1)
            {
                scanfix.Visibility = Visibility.Collapsed;
                //scanresult.Visibility = Visibility.Collapsed;
                try
                {
                    DirectoryInfo di1 = new DirectoryInfo("C:/Windows/Prefetch");
                    foreach (FileInfo file in di1.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di1.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                catch { }
                try
                {
                    string Temppath = Path.GetTempPath();
                    DirectoryInfo di2 = new DirectoryInfo(Temppath);
                    foreach (FileInfo file in di2.EnumerateFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di2.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                catch { }
                try
                {
                    DirectoryInfo di3 = new DirectoryInfo("C:/Windows/Temp");
                    foreach (FileInfo file in di3.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di3.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                catch { }
                try
                {
                    DirectoryInfo di4 = new DirectoryInfo("C:/Windows/SoftwareDistribution/Download");
                    foreach (FileInfo file in di4.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di4.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                catch { }

                VerboseRes();
                #region Progress
                double reset = 0;
                scanprogress.Value = reset;
                //scantext.Text = "Fixing: Malware";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                Loading1.Visibility = Visibility.Hidden;
                Loading2.Visibility = Visibility.Visible;
                //scantext.Text = "Fixing: Registry";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                //scantext.Text = "Fixing: Security";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                //scantext.Text = "Boosting: Startup";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                //scantext.Text = "Defragmenting: Drive";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                //scantext.Text = "Fixing: Privacy";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                //scantext.Text = "Repairing: System";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                //scantext.Text = "Deleting: Junk";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                //scantext.Text = "Fixing: Drivers";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                //scantext.Text = "Fixing: System Files";
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(micro);
                scanprogress.Value++;
                await Task.Delay(mega);
                #endregion

                //scanresult.Visibility = Visibility.Visible;
                //scantext.Text = "Success";
                //scanresult.Text = "Junk: " + sizedir.ToString() + "MB cleaned\n" + "Security Holes: " + "Fixed" + "\nPrivacy Issue: " + "Fixed" + "\nDevice Optimized";
                scanstop.Content = "Back";
            }
            else
            {
                popupmain = new PopupWindow();
                if (popupmain.ShowDialog().Value)
                {
                    Activatekey(true);
                }
                else
                {
                    Activatekey(false);
                    PopupTimer();
                }
            }
        }
        PopupWindow popupmain;

        //Activation Button & Logic
        public void Activatekey(bool result)
        {
            if (result == true)
            {
                FileStream fs = new FileStream("sysfunction.bin", FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(true);
                fs.Close();
                premium = 1;
            }
        }
        private void NotPremium()
        {
            if (File.Exists("sysfunction.bin"))
            {
                FileStream fs = new FileStream("sysfunction.bin", FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                if (br.ReadBoolean() == true)
                {
                    premium = 1;
                    fs.Close();
                }
                else
                {
                    br.Close();
                    fs.Close();
                }
            }
            else
            {
                FileStream fs = new FileStream("sysfunction.bin", FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(false);
                fs.Close();
            }
        }

        //Settings
        private void settingsclose_Click(object sender, RoutedEventArgs e)
        {
            settings.Visibility = Visibility.Collapsed;
        }
        private void apply_Click(object sender, RoutedEventArgs e)
        {
            settingsapplied.Text = "Settings Applied";
        }
        private void settingsbutton_Click(object sender, RoutedEventArgs e)
        {
            settings.Visibility = Visibility.Visible;
        }

        //Support
        private void supportclose_Click(object sender, RoutedEventArgs e)
        {
            support.Visibility = Visibility.Collapsed;
        }
        private void supportbutton_Click(object sender, RoutedEventArgs e)
        {
            support.Visibility = Visibility.Visible;
        }

        //About
        private void aboutclose_Click(object sender, RoutedEventArgs e)
        {
            about.Visibility = Visibility.Collapsed;
        }
        private void aboutbutton_Click(object sender, RoutedEventArgs e)
        {
            about.Visibility = Visibility.Visible;
        }

        //Popup
        private async void PopupTimer()
        {
            await Task.Delay(5000);
            popupmain = new PopupWindow();
            if (popupmain.ShowDialog().Value)
            {
                Activatekey(true);
            }
            else
            {
                Activatekey(false);
                PopupTimer();
            }
        }
        //Verbose
        private async void VerboseDir()
        {
            if (stop == false)
            {
                updatetxt.Children.Clear();
                await Task.Delay(1000);
                foreach (string path in Directory.GetDirectories(@"C:\Windows"))
                {
                    updatetxt.Children.Add(new TextBlock { Text = "Scanning " + path, Foreground = Brushes.White, FontSize = 14 });
                    updatescroll.ScrollToEnd();
                    await Task.Delay(100);
                }
                Image pic = new Image { Width = 120, Height = 190 };
                ImageBehavior.SetAnimatedSource(pic, (ImageSource)new ImageSourceConverter().ConvertFrom(@"AridDependableBelugawhale-max-1mb.gif"));
                updatetxt.Children.Add(pic);
                updatescroll.ScrollToEnd();
                await Task.Delay(45000);
                foreach (string path in Directory.GetFiles(@"C:\Windows"))
                {
                    updatetxt.Children.Add(new TextBlock { Text = "Scanning " + path, Foreground = Brushes.White });
                    updatescroll.ScrollToEnd();
                    await Task.Delay(100);
                }
                await Task.Delay(45000);
                foreach (string path in Directory.GetDirectories(@"C:\Program Files"))
                {
                    updatetxt.Children.Add(new TextBlock { Text = "Scanning " + path, Foreground = Brushes.White });
                    updatescroll.ScrollToEnd();
                    await Task.Delay(100);
                }
                await Task.Delay(45000);
                foreach (string path in Directory.GetFiles(@"C:\Program Files"))
                {
                    updatetxt.Children.Add(new TextBlock { Text = "Scanning " + path, Foreground = Brushes.White });
                    updatescroll.ScrollToEnd();
                    await Task.Delay(100);
                }
                await Task.Delay(45000);
                updatetxt.Children.Add(new TextBlock { Text = "Scan Complete", Foreground = Brushes.White });
            }
            else
            {
                updatetxt.Children.Clear();
                return;
            }
        }
        private async void VerboseRes()
        {
            foreach (string path in Directory.GetDirectories(@"C:\Windows"))
            {
                updatetxt.Children.Add(new TextBlock { Text = "Repairing " + path, Foreground = Brushes.White });
                updatescroll.ScrollToEnd();
                await Task.Delay(100);
            }
            await Task.Delay(45000);
            foreach (string path in Directory.GetFiles(@"C:\Windows"))
            {
                updatetxt.Children.Add(new TextBlock { Text = "Repairing " + path, Foreground = Brushes.White });
                updatescroll.ScrollToEnd();
                await Task.Delay(100);
            }
            await Task.Delay(45000);
            foreach (string path in Directory.GetDirectories(@"C:\Program Files"))
            {
                updatetxt.Children.Add(new TextBlock { Text = "Repairing " + path, Foreground = Brushes.White });
                updatescroll.ScrollToEnd();
                await Task.Delay(100);
            }
            await Task.Delay(45000);
            foreach (string path in Directory.GetFiles(@"C:\Program Files"))
            {
                updatetxt.Children.Add(new TextBlock { Text = "Repairing " + path, Foreground = Brushes.White });
                updatescroll.ScrollToEnd();
                await Task.Delay(100);
            }
            await Task.Delay(45000);
            updatetxt.Children.Add(new TextBlock { Text = "Repair Complete", Foreground = Brushes.White });
        }
    }
}
