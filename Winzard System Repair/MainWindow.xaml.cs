using System;
using System.IO;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media;
using Winzard_System_Repair.RegistryScanner;
using System.Threading;
using Winzard_System_Repair;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NotifyIcon nIcon = new NotifyIcon();
        PopupWindow1 Popupsub;
        PopupWindow2 Popupsub2;
        PopupWindow3 popupmain;
        PopupWindow4 popupmain1;
        PopupWindow5 popupmain2;
        Thread t;
        int premium = 0, notification = 0, mega = 2000, totalcachenum = 0, malwarever = 0, filever = 0, systemissuever = 0, close = 0;
        float sizedir = 0;
        public MainWindow()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
            RegistryKey reg1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            Assembly curAssembly = Assembly.GetExecutingAssembly();
            /*reg1.SetValue(curAssembly.GetName().Name, curAssembly.Location);*/
            reg1.SetValue(curAssembly.GetName().Name, System.Windows.Forms.Application.ExecutablePath.ToString());
            sysdet1.Content = GetAccount1() + Environment.NewLine;
            sysdet2.Content = GetAccount2() + Environment.NewLine;
            sysdet3.Content = GetAccount3() + Environment.NewLine;
            sysdet4.Content = GetAccount4() + Environment.NewLine;
            NotPremium();
            NotificationOnOff();
            afternumber();
            Popups3();
        }
        //Corrupt Sysfiles
        public async Task DiskCleanerAsync()
        {
            try
            {
                await JunkCleaner();
                await PrivacyCleaner();
            }
            catch(Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }
        public async Task FixSystemIssueAsync()
        {
            try
            {
                await RegistryFix();
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }
        //Defragment
        private async Task Fragments()
        {
            try
            {
                if (close == 1) { return; };
                load.Children.Add(new TextBlock { Text = "Checking:0%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                if (close == 1) { return; };
                await Task.Delay(1000);
                if (close == 1) { return; };
                load.Children.Add(new TextBlock { Text = "Checking:10%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                if (close == 1) { return; };
                Process proc = new Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.Arguments = "/c \"defrag c: /A\" && exit";
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                if (close == 1) { return; };
                await Task.Run(() => proc.WaitForExit());
                if (close == 1) { return; };
                await Task.Delay(1000);
                if (close == 1) { return; };
                load.Children.Add(new TextBlock { Text = "Checking:20%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                if (close == 1) { return; };
                await Task.Delay(1000);
                if (close == 1) { return; };
                load.Children.Add(new TextBlock { Text = "Checking:30%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                if (close == 1) { return; };
                await Task.Delay(1000);
                if (close == 1) { return; };
                load.Children.Add(new TextBlock { Text = "Checking:40%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                if (close == 1) { return; };
                await Task.Delay(1000);
                if (close == 1) { return; };
                load.Children.Add(new TextBlock { Text = "Checking:50%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                if (close == 1) { return; };
                await Task.Delay(1000);
                if (close == 1) { return; };
                load.Children.Add(new TextBlock { Text = "Checking:60%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                if (close == 1) { return; };
                await Task.Delay(1000);
                if (close == 1) { return; };
                load.Children.Add(new TextBlock { Text = "Checking:70%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                if (close == 1) { return; };
                await Task.Delay(1000);
                if (close == 1) { return; };
                load.Children.Add(new TextBlock { Text = "Checking:80%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                if (close == 1) { return; };
                await Task.Delay(1000);
                if (close == 1) { return; };
                load.Children.Add(new TextBlock { Text = "Checking:90%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                if (close == 1) { return; };
                await Task.Delay(1000);
                if (close == 1) { return; };
                load.Children.Add(new TextBlock { Text = "Checking:100%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                if (close == 1) { return; };
                await Task.Delay(1000);
                if (close == 1) { return; };
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }

        }
        private async Task DiskDefragmentation()
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.Arguments = "/c \"defrag c: /U /V\" && exit";
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                await Task.Run(() => proc.WaitForExit());
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }
        //startup Optimization
        private async Task startupoptimization()
        {
            try
            {
                if (close == 1) { return; };
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                string regvalue = reg.ValueCount.ToString();
                RegistryKey reg1 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                string regvalue1 = reg1.ValueCount.ToString();
                Startupoptimizations.Text = regvalue + "Items";
                string[] startupitems = reg.GetValueNames();
                for (int i = 0; i < reg.GetValueNames().Length; i++)
                {
                    if (close == 1) { return; };
                    load.Children.Add(new TextBlock { Text = startupitems[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    await Task.Delay(7);
                    if (close == 1) { return; };
                }
                if (close == 1) { return; };
                string[] startupitems1 = reg1.GetValueNames();
                for (int i = 0; i < reg.GetValueNames().Length; i++)
                {
                    if (close == 1) { return; };
                    load.Children.Add(new TextBlock { Text = startupitems1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    await Task.Delay(7);
                    if (close == 1) { return; };
                }
                if (close == 1) { return; };
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }
        private void startupoptimizationfix()
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                reg.DeleteSubKeyTree("Computer\\HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
                RegistryKey reg1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Assembly curAssembly = Assembly.GetExecutingAssembly();
                /*reg1.SetValue(curAssembly.GetName().Name, curAssembly.Location);*/
                reg1.SetValue(curAssembly.GetName().Name, System.Windows.Forms.Application.ExecutablePath.ToString());
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }
        //display number
        private void afternumber()
        {
            /*Random rnd = new Random();
            int malware = rnd.Next(1, 20);
            MalwareThreats.Text = malware.ToString();
            int holes = rnd.Next(1, 20);
            SecurityHoles.Text = holes.ToString();
            int corruptsysfiles = rnd.Next(1, 10);
            CorruptedSystemFiles.Text = corruptsysfiles.ToString();
            int driver = rnd.Next(1, 10);
            OutdatedDrivers.Text = driver.ToString();
            int brknreg = rnd.Next(1, 10);
            BrokenRegistryEntries.Text = brknreg.ToString();
            int startup = rnd.Next(1, 20);
            Startupoptimizations.Text = startup.ToString();
            int privacy = rnd.Next(1, 10);
            PrivacyTraces.Text = privacy.ToString();*/
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
            WindowState = WindowState.Minimized;
        }
        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            BaseUI.Visibility = Visibility.Collapsed;
            nIcon.Icon = new Icon("shield.ico");
            nIcon.Visible = true;
            ShowInTaskbar = false;
            nIcon.DoubleClick += NIcon_DoubleClick;
        }

        private void NIcon_DoubleClick(object sender, EventArgs e)
        {
            BaseUI.Visibility = Visibility.Visible;
            ShowInTaskbar = true;
            nIcon.Visible = false;
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
            if (close == 1) { await Task.Delay(1000); };
            close = 0;
            statusgrid.Visibility = Visibility.Visible;
            scan.Visibility = Visibility.Collapsed;
            PCstatus.Visibility = Visibility.Collapsed;
            OtherSoftware.Visibility = Visibility.Collapsed;
            onetimefix.Visibility = Visibility.Collapsed;
            //t = new Thread (Tempdelete);
            //t.Start();
            Tempdelete();
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
        public async Task Registrycleanfix()
        {
            try
            {
                Task.Run(() => ScannerFunctions.FixProblems());
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }
        public async Task RegistryFix()
        {
            if (close == 1) { return; };
            try
            {
                if (close == 1) { return; };
                Task.Run(() => ScannerFunctions.StartScanning());
                for (int i = 0; i < 500; i++)
                {
                    if (close == 1) { return; };
                    load.Children.Add(new TextBlock { Text = ScannerFunctions.CurrentScannedObject, Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    if (close == 1) { return; };
                    await Task.Delay(7);
                    if (close == 1) { return; };
                }
                if (close == 1) { return; };
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }
        public async Task MalwareCleaner()
        {
            try
            {
                await JunkCleaner();
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }
        public async Task PrivacyCleaner()
        {
            if (close == 1) { return; };
            try
            {
                if (close == 1) { return; };
                string ICacheFirefoxpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Mozilla\\Firefox\\Profiles\\";
                string ICacheChromepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Cache\\";
                string ICacheChromepath1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Application Cache\\Cache\\";
                if(Directory.Exists(ICacheChromepath))
                {
                    string[] chromedetails = Directory.GetFiles(ICacheChromepath);
                    for (int i = 0; i < 200; i++)
                    {
                        if (close == 1) { return; };
                        load.Children.Add(new TextBlock { Text = chromedetails[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                        regdetails.ScrollToEnd();
                        if (close == 1) { return; };
                        await Task.Delay(7);
                        if (close == 1) { return; };
                    }
                }
                if(Directory.Exists(ICacheChromepath1))
                {
                    string[] chromedetails1 = Directory.GetFiles(ICacheChromepath1);
                    for (int i = 0; i < 200; i++)
                    {
                        if (close == 1) { return; };
                        load.Children.Add(new TextBlock { Text = chromedetails1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                        regdetails.ScrollToEnd();
                        if (close == 1) { return; };
                        await Task.Delay(7);
                        if (close == 1) { return; };
                    }
                }
                if(Directory.Exists(ICacheFirefoxpath))
                {
                    string[] firefoxdetails = Directory.GetFiles(ICacheFirefoxpath, "*.*", SearchOption.AllDirectories);
                    for (int i = 0; i < 200; i++)
                    {
                        if (close == 1) { return; };
                        load.Children.Add(new TextBlock { Text = firefoxdetails[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                        regdetails.ScrollToEnd();
                        if (close == 1) { return; };
                        await Task.Delay(7);
                        if (close == 1) { return; };
                    }
                }
                if (close == 1) { return; };
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }
        public async Task JunkCleaner()
        {
            if (close == 1) { return; };
            try
            {
                if (close == 1) { return; };
                string[] Junk1 = Directory.GetFiles(Path.GetTempPath());
                string[] Junk2 = Directory.GetFiles("C:\\Windows\\Prefetch\\");
                string[] Junk3 = Directory.GetFiles("C:\\Windows\\Temp\\");
                string[] Junk4 = Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\");
                for (int i = 0; i < Directory.GetFiles(Path.GetTempPath()).Length; i++)
                {
                    if (close == 1) { return; };
                    load.Children.Add(new TextBlock { Text = Junk1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    if (close == 1) { return; };
                    await Task.Delay(7);
                    if (close == 1) { return; };
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Prefetch\\").Length; i++)
                {
                    if (close == 1) { return; };
                    load.Children.Add(new TextBlock { Text = Junk2[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    if (close == 1) { return; };
                    await Task.Delay(7);
                    if (close == 1) { return; };
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Temp\\").Length; i++)
                {
                    if (close == 1) { return; };
                    load.Children.Add(new TextBlock { Text = Junk3[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    if (close == 1) { return; };
                    await Task.Delay(7);
                    if (close == 1) { return; };
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\").Length; i++)
                {
                    if (close == 1) { return; };
                    load.Children.Add(new TextBlock { Text = Junk4[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    if (close == 1) { return; };
                    await Task.Delay(7);
                    if (close == 1) { return; };
                }
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace);
            }
        }
        public async Task RepairFilesAsync()
        {
            try
            {
                await JunkCleaner();
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }
        //Temp delete & Scan Progress
        public async void Tempdelete()
        {
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
                    if(Directory.Exists(ICacheExpolrerpath))
                    {
                        totalcachenum += Directory.GetFiles(ICacheExpolrerpath).Length;
                    }
                    if (Directory.Exists(ICacheChromepath))
                    {
                        totalcachenum += Directory.GetFiles(ICacheChromepath).Length;
                    }
                    /*if (Directory.Exists(ICacheChromepath1))
                    {
                        totalcachenum += Directory.GetFiles(ICacheChromepath1).Length;
                    }*/
                    if (Directory.Exists(ICacheFirefoxpath))
                    {
                        totalcachenum += Directory.GetFiles(ICacheFirefoxpath, "*.*", SearchOption.AllDirectories).Length;
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        if(Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\", "*.*", SearchOption.AllDirectories).Length > 0)
                        {
                            systemissuever = i + 1;
                        }
                        else
                        {
                            systemissuever = 0;
                        }
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        if (Directory.GetFiles("C:\\Windows\\Temp\\", ".", SearchOption.AllDirectories).Length > 0)
                        {
                            filever = i + 1;
                        }
                        else
                        {
                            filever = 0;
                        }
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        if (Directory.GetFiles("C:\\Windows\\Prefetch\\").Length > 0)
                        {
                            malwarever = i + 1;
                        }
                        else
                        {
                            malwarever = 0;
                        }
                    }
                }
                catch (Exception e)
                {
                    //System.Windows.MessageBox.Show(e.Message);
                }
                //Progress
                //scantext.Text = "Scanning: Malware";
                Statuspanel.Text = "PC Gold Optimizer and System Repair is currently collecting information from your computer for\nscanning purpose. Please wait until the scanning is complete";
                Loading1.Visibility = Visibility.Visible;
                Status.Text = "Scanning: Junk";
                if(close == 1) { return; };
                await JunkCleaner();
                if (close == 1) { return; };
                regdetails.Visibility = Visibility.Collapsed;
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
                //await Task.Delay(12000);
                Loading1.Visibility = Visibility.Hidden;
                Loading2.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Registry";
                regdetails.Visibility = Visibility.Visible;
                Status.Text = "Scanning: Privacy";
                if (close == 1) { return; };
                await PrivacyCleaner();
                if (close == 1) { return; };
                regdetails.Visibility = Visibility.Collapsed;
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
                //await Task.Delay(15000);
                Loading2.Visibility = Visibility.Hidden;
                Loading3.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Security";
                regdetails.Visibility = Visibility.Visible;
                Status.Text = "Scanning: Registry";
                if (close == 1) { return; };
                await RegistryFix();
                if (close == 1) { return; };
                regdetails.Visibility = Visibility.Collapsed;
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
                //await Task.Delay(20000);
                Loading3.Visibility = Visibility.Hidden;
                Loading4.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Startup";
                regdetails.Visibility = Visibility.Visible;
                Status.Text = "Scanning: Malware";
                if (close == 1) { return; };
                await MalwareCleaner();
                if (close == 1) { return; };
                regdetails.Visibility = Visibility.Collapsed;
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
                //await Task.Delay(31000);
                Loading4.Visibility = Visibility.Hidden;
                Loading5.Visibility = Visibility.Visible;
                //scantext.Text = "Analyzing: Fragments";
                regdetails.Visibility = Visibility.Visible;
                Status.Text = "Scanning: Fragments";
                if (close == 1) { return; };
                await Fragments();
                if (close == 1) { return; };
                regdetails.Visibility = Visibility.Collapsed;
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
                //await Task.Delay(17000);
                Loading5.Visibility = Visibility.Hidden;
                Loading6.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Privacy";
                regdetails.Visibility = Visibility.Visible;
                Status.Text = "Scanning: Startup";
                if (close == 1) { return; };
                await startupoptimization();
                if (close == 1) { return; };
                regdetails.Visibility = Visibility.Collapsed;
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
                //await Task.Delay(23000);
                Loading6.Visibility = Visibility.Hidden;
                Loading7.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: System";
                regdetails.Visibility = Visibility.Visible;
                Status.Text = "Scanning: Repair Files";
                if (close == 1) { return; };
                await RepairFilesAsync();
                if (close == 1) { return; };
                regdetails.Visibility = Visibility.Collapsed;
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
                //await Task.Delay(7000);
                Loading7.Visibility = Visibility.Hidden;
                Loading8.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Junk";
                regdetails.Visibility = Visibility.Visible;
                Status.Text = "Scanning: Disk Cleaner";
                if (close == 1) { return; };
                await DiskCleanerAsync();
                if (close == 1) { return; };
                regdetails.Visibility = Visibility.Collapsed;
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
                //await Task.Delay(15000);
                Loading8.Visibility = Visibility.Hidden;
                Loading9.Visibility = Visibility.Visible;
                //scantext.Text = "Scanning: Drivers";
                regdetails.Visibility = Visibility.Visible;
                Status.Text = "Scanning: Fix System Issue";
                if (close == 1) { return; };
                await FixSystemIssueAsync();
                if (close == 1) { return; };
                regdetails.Visibility = Visibility.Collapsed;
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
                //await Task.Delay(mega);
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
                //await Task.Delay(mega);
                Loading10.Visibility = Visibility.Hidden;
                if (close == 1) { return; };
                //load.Visibility = Visibility.Collapsed;
                Random rnd = new Random();
                int defrag = rnd.Next(0, 9);
                defragmentation.Text = defrag.ToString() + "%";
                MalwareThreats.Text = malwarever.ToString();
                repairfile.Text = filever.ToString();
                systemissue.Text = systemissuever.ToString();
                diskclean.Text = sizedir.ToString() + totalcachenum.ToString();
                JunkFiles.Text = sizedir.ToString() + "MB";
                PrivacyFiles.Text = totalcachenum.ToString() + "Items";
                registrydetails.Text = RegistryItems.RegistryProblemNumbers().ToString() + "Items";
                scanfix.Visibility = Visibility.Visible;
                onetimefix.Visibility = Visibility.Visible;
                resultpanel.Visibility = Visibility.Visible;
                resultpanel2.Visibility = Visibility.Visible;
                resultdetails.Visibility = Visibility.Visible;
                Popups();
                if (close == 1) { return; };
            }
        }

        //The Scan stop button
        private async void scanstop_Click(object sender, RoutedEventArgs e)
        {
            if (close == 1) { return; };
            close = 1;
            await Task.Delay(4000);
            statusgrid.Visibility = Visibility.Collapsed;
            scan.Visibility = Visibility.Visible;
            PCstatus.Visibility = Visibility.Visible;
            OtherSoftware.Visibility = Visibility.Visible;
            //loadvid.LoadedBehavior = MediaState.Stop;
            //load.Visibility = Visibility.Collapsed;
            resultpanel3.Visibility = Visibility.Collapsed;
            resultpanel4.Visibility = Visibility.Collapsed;
            Loading1.Visibility = Visibility.Hidden;
            Loading2.Visibility = Visibility.Hidden;
            Loading3.Visibility = Visibility.Hidden;
            Loading4.Visibility = Visibility.Hidden;
            Loading5.Visibility = Visibility.Hidden;
            Loading6.Visibility = Visibility.Hidden;
            Loading7.Visibility = Visibility.Hidden;
            Loading8.Visibility = Visibility.Hidden;
            Loading9.Visibility = Visibility.Hidden;
            Loading10.Visibility = Visibility.Hidden;
        }
        //The Repair Button
        public async void Scanfix_Click(object sender, RoutedEventArgs e)
        {
            if (premium == 1)
            {
                scanfix.Visibility = Visibility.Collapsed;
                onetimefix.Visibility = Visibility.Collapsed;
                resultpanel.Visibility = Visibility.Collapsed;
                resultpanel2.Visibility = Visibility.Collapsed;
                resultdetails.Visibility = Visibility.Collapsed;
                //load.Visibility = Visibility.Collapsed;
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
                try
                {
                    DirectoryInfo di5 = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Cache\\");
                    foreach (FileInfo file in di5.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di5.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                catch { }
                try
                {
                    DirectoryInfo di6 = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Mozilla\\Firefox\\Profiles\\");
                    foreach (FileInfo file in di6.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di6.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                catch { }
                startupoptimizationfix();
                #region Progress
                double reset = 0;
                scanprogress.Value = reset;
                Loading1.Visibility = Visibility.Visible;
                Statuspanel.Text = "Fixing: Junk";
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
                await Task.Delay(mega);
                Loading1.Visibility = Visibility.Hidden;
                Loading2.Visibility = Visibility.Visible;
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
                await Task.Delay(mega);
                Loading2.Visibility = Visibility.Hidden;
                Loading3.Visibility = Visibility.Visible;
                //Statuspanel.Text = "Fixing: Registry";
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
                await Task.Delay(mega);
                Loading3.Visibility = Visibility.Hidden;
                Loading4.Visibility = Visibility.Visible;
                Statuspanel.Text = "Fixing: Startup";
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
                await Task.Delay(mega);
                Loading4.Visibility = Visibility.Hidden;
                Loading5.Visibility = Visibility.Visible;
                //Statuspanel.Text = "Optimizing: Driver";
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
                await Task.Delay(mega);
                Loading5.Visibility = Visibility.Hidden;
                Loading6.Visibility = Visibility.Visible;
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
                await Task.Delay(mega);
                Loading6.Visibility = Visibility.Hidden;
                Loading7.Visibility = Visibility.Visible;
                Statuspanel.Text = "Defragmenting";
                DiskDefragmentation();
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
                await Task.Delay(mega);
                Loading7.Visibility = Visibility.Hidden;
                Loading8.Visibility = Visibility.Visible;
                Statuspanel.Text = "Fixing: Security Holes";
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
                await Task.Delay(mega);
                Loading8.Visibility = Visibility.Hidden;
                Loading9.Visibility = Visibility.Visible;
                Statuspanel.Text = "Optimizing: System";
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
                await Task.Delay(mega);
                Loading9.Visibility = Visibility.Hidden;
                Loading10.Visibility = Visibility.Visible;
                Statuspanel.Text = "Optimizing: System";
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
                Statuspanel.Text = "Repairing Files";
                await Task.Delay(mega);
                Loading10.Visibility = Visibility.Hidden;
                scanprogress.Value = reset;
                #endregion
                resultpanel3.Visibility = Visibility.Visible;
                resultpanel4.Visibility = Visibility.Visible;
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
                    RepairfirstTime();
                }
                else
                {
                    Activatekey(false);
                    //PopupTimer();
                }
            }
        }
        public async void RepairfirstTime()
        {
            scanfix.Visibility = Visibility.Collapsed;
            resultpanel.Visibility = Visibility.Collapsed;
            resultpanel2.Visibility = Visibility.Collapsed;
            resultdetails.Visibility = Visibility.Collapsed;
            //load.Visibility = Visibility.Collapsed;
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
            try
            {
                DirectoryInfo di5 = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Cache\\");
                foreach (FileInfo file in di5.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di5.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch { }
            try
            {
                DirectoryInfo di6 = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Mozilla\\Firefox\\Profiles\\");
                foreach (FileInfo file in di6.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di6.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch { }
            startupoptimizationfix();
            #region Progress
            double reset = 0;
            scanprogress.Value = reset;
            Loading1.Visibility = Visibility.Visible;
            Statuspanel.Text = "Fixing: Junk";
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
            await Task.Delay(mega);
            Loading1.Visibility = Visibility.Hidden;
            Loading2.Visibility = Visibility.Visible;
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
            await Task.Delay(mega);
            Loading2.Visibility = Visibility.Hidden;
            Loading3.Visibility = Visibility.Visible;
            //Statuspanel.Text = "Fixing: Registry";
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
            await Task.Delay(mega);
            Loading3.Visibility = Visibility.Hidden;
            Loading4.Visibility = Visibility.Visible;
            Statuspanel.Text = "Fixing: Startup";
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
            await Task.Delay(mega);
            Loading4.Visibility = Visibility.Hidden;
            Loading5.Visibility = Visibility.Visible;
            //Statuspanel.Text = "Optimizing: Driver";
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
            await Task.Delay(mega);
            Loading5.Visibility = Visibility.Hidden;
            Loading6.Visibility = Visibility.Visible;
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
            await Task.Delay(mega);
            Loading6.Visibility = Visibility.Hidden;
            Loading7.Visibility = Visibility.Visible;
            Statuspanel.Text = "Defragmenting";
            await DiskDefragmentation();
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
            await Task.Delay(mega);
            Loading7.Visibility = Visibility.Hidden;
            Loading8.Visibility = Visibility.Visible;
            //Statuspanel.Text = "Fixing: Security Holes";
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
            await Task.Delay(mega);
            Loading8.Visibility = Visibility.Hidden;
            Loading9.Visibility = Visibility.Visible;
            Statuspanel.Text = "Optimizing: Registry";
            await Registrycleanfix();
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
            await Task.Delay(mega);
            Loading9.Visibility = Visibility.Hidden;
            Loading10.Visibility = Visibility.Visible;
            //Statuspanel.Text = "Optimizing: System";
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
            Statuspanel.Text = "Repairing Files";
            await Task.Delay(mega);
            Loading10.Visibility = Visibility.Hidden;
            scanprogress.Value = reset;
            #endregion
            resultpanel3.Visibility = Visibility.Visible;
            resultpanel4.Visibility = Visibility.Visible;
            //scanresult.Visibility = Visibility.Visible;
            Statuspanel.Text = "Success";
            //scanresult.Text = "Junk: " + sizedir.ToString() + "MB cleaned\n" + "Security Holes: " + "Fixed" + "\nPrivacy Issue: " + "Fixed" + "\nDevice Optimized";
            scanstop.Content = "Back";
            popupmain2 = new PopupWindow5();
            popupmain2.Show();
        }

        //Activation Button & Logic
        public void Activatekey(bool result)
        {
            if (result == true)
            {
                FileStream fs = new FileStream("sysfunction.bin", FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(true);
                fs.Close();
                bw.Close();
                premium = 1;
            }
        }
        public void NotPremium()
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
        private void EULA_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://thealliancetech.com/end-user-license-agreement/");
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
        /*private async void PopupTimer()
        {
            await Task.Delay(300000);
            popupmain = new PopupWindow3();
            if (popupmain.ShowDialog().Value)
            {
                Activatekey(true);
                RepairfirstTime();
            }
            else
            {
                Activatekey(false);
                PopupTimer();
            }
        }*/

        
        private async void Popups()
        {
            if (premium == 0 && notification == 0)
            {
                await Task.Delay(120000);
                Popupsub = new PopupWindow1();
                if (Popupsub.ShowDialog().Value)
                {
                    Activatekey(true);
                    RepairfirstTime();
                }
                else
                {
                    Activatekey(false);
                    await Task.Delay(400000);
                    Popups1();
                }
            }
            else
            {
                return;
            }
        }

        private async void Popups1()
        {
            if (premium == 0 && notification == 0)
            {
                await Task.Delay(120000);
                Popupsub2 = new PopupWindow2();
                if (Popupsub2.ShowDialog().Value)
                {
                    Activatekey(true);
                    RepairfirstTime();
                }
                else
                {
                    Activatekey(false);
                    Popups2();
                }
            }
            else
            {
                return;
            }

        }
        private async void Popups2()
        {
            if (premium == 0 && notification == 0)
            {
                await Task.Delay(120000);
                popupmain1 = new PopupWindow4();
                if (popupmain1.ShowDialog().Value)
                {
                    Activatekey(true);
                    RepairfirstTime();
                }
                else
                {
                    Activatekey(false);
                    Popups();
                }
            }
            else
            {
                return;
            }
        }
        private async void Popups3()
        {
            if (premium == 1)
            {
                await Task.Delay(5000000);
                popupmain2 = new PopupWindow5();
                popupmain2.Show();
                Popups3();
            }
            else
            {
                await Task.Delay(60000);
                Popups3();
            }
        }

        private void Turnoffnotifications_Checked(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream("sysset.bin", FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(true);
            fs.Close();
        }
        public void NotificationOnOff()
        {
            if (System.IO.File.Exists("sysset.bin"))
            {
                FileStream fs = new FileStream("sysset.bin", FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                if (br.ReadBoolean() == true)
                {
                    notification = 1;
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
                FileStream fs = new FileStream("sysset.bin", FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(false);
                fs.Close();
            }
        }

        private void onetimefix_Click(object sender, RoutedEventArgs e)
        {
            OneTimeFix();
        }

        public void OneTimeFix()
        {
            var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();

            if (RunningProcessPaths.Contains("firefox.exe"))
            {
                //firefox is running
                System.Windows.MessageBox.Show("Please Close your browser to continue");
                return;
            }
            if (RunningProcessPaths.Contains("chrome.exe"))
            {
                //Google Chrome is running
                System.Windows.MessageBox.Show("Please Close your browser to continue");
                return;
            }
            if (System.IO.File.Exists("sysdone.bin"))
            {
                FileStream fs = new FileStream("sysdone.bin", FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                if (br.ReadBoolean() == true)
                {
                    br.Close();
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(false);
                    bw.Close();
                    fs.Close();
                    OneTimeRepair();
                }
                else
                {
                    br.Close();
                    fs.Close();
                    popupmain = new PopupWindow3();
                    if (popupmain.ShowDialog().Value)
                    {
                        Activatekey(true);
                        RepairfirstTime();
                    }
                    else
                    {
                        Activatekey(false);
                        //PopupTimer();
                    }
                }
            }
            else
            {
                FileStream fs = new FileStream("sysdone.bin", FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(false);
                bw.Close();
                fs.Close();
                OneTimeRepair();
            }
        }
        public async void OneTimeRepair()
        {
            try
            {
                scanfix.Visibility = Visibility.Collapsed;
                onetimefix.Visibility = Visibility.Collapsed;
                resultpanel.Visibility = Visibility.Collapsed;
                resultpanel2.Visibility = Visibility.Collapsed;
                resultdetails.Visibility = Visibility.Collapsed;
                //load.Visibility = Visibility.Collapsed;
                //scanresult.Visibility = Visibility.Collapsed;
                try
                {
                    DirectoryInfo di1 = new DirectoryInfo("C:/Windows/Prefetch");
                    foreach (FileInfo file in di1.GetFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch { }
                    }
                    foreach (DirectoryInfo dir in di1.GetDirectories())
                    {
                        try
                        {
                            dir.Delete(true);
                        }
                        catch { }
                    }
                }
                catch { }
                try
                {
                    string Temppath = Path.GetTempPath();
                    DirectoryInfo di2 = new DirectoryInfo(Temppath);
                    foreach (FileInfo file in di2.EnumerateFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch { }
                    }
                    foreach (DirectoryInfo dir in di2.GetDirectories())
                    {
                        try
                        {
                            dir.Delete(true);
                        }
                        catch { }
                    }
                }
                catch { }
                try
                {
                    DirectoryInfo di3 = new DirectoryInfo("C:/Windows/Temp");
                    foreach (FileInfo file in di3.GetFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch { }
                    }
                    foreach (DirectoryInfo dir in di3.GetDirectories())
                    {
                        try
                        {
                            dir.Delete(true);
                        }
                        catch { }
                    }
                }
                catch { }
                try
                {
                    DirectoryInfo di4 = new DirectoryInfo("C:/Windows/SoftwareDistribution/Download");
                    foreach (FileInfo file in di4.GetFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch { }
                    }
                    foreach (DirectoryInfo dir in di4.GetDirectories())
                    {
                        try
                        {
                            dir.Delete(true);
                        }
                        catch { }
                    }
                }
                catch { }
                try
                {
                    DirectoryInfo di5 = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Cache\\");
                    foreach (FileInfo file in di5.GetFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch { }
                    }
                    foreach (DirectoryInfo dir in di5.GetDirectories())
                    {
                        try
                        {
                            dir.Delete(true);
                        }
                        catch { }
                    }
                }
                catch { }
                try
                {
                    DirectoryInfo di6 = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Mozilla\\Firefox\\Profiles\\");
                    foreach (FileInfo file in di6.GetFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch { }
                    }
                    foreach (DirectoryInfo dir in di6.GetDirectories())
                    {
                        try
                        {
                            dir.Delete(true);
                        }
                        catch { }
                    }
                }
                catch { }
                try
                {
                    DirectoryInfo di6 = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Local\\Microsoft\\Edge\\User Data\\Default\\");
                    foreach (FileInfo file in di6.GetFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch { }
                    }
                    foreach (DirectoryInfo dir in di6.GetDirectories())
                    {
                        try
                        {
                            dir.Delete(true);
                        }
                        catch { }
                    }
                }
                catch { }
                startupoptimizationfix();
                #region Progress
                double reset = 0;
                scanprogress.Value = reset;
                Loading1.Visibility = Visibility.Visible;
                Statuspanel.Text = "Fixing: Junk";
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
                await Task.Delay(mega);
                Loading1.Visibility = Visibility.Hidden;
                Loading2.Visibility = Visibility.Visible;
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
                await Task.Delay(mega);
                Loading2.Visibility = Visibility.Hidden;
                Loading3.Visibility = Visibility.Visible;
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
                await Task.Delay(mega);
                Loading3.Visibility = Visibility.Hidden;
                Loading4.Visibility = Visibility.Visible;
                Statuspanel.Text = "Fixing: Startup";
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
                await Task.Delay(mega);
                Loading4.Visibility = Visibility.Hidden;
                Loading5.Visibility = Visibility.Visible;
                Statuspanel.Text = "Optimizing: Driver";
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
                await Task.Delay(mega);
                Loading5.Visibility = Visibility.Hidden;
                Loading6.Visibility = Visibility.Visible;
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
                await Task.Delay(mega);
                Loading6.Visibility = Visibility.Hidden;
                Loading7.Visibility = Visibility.Visible;
                Statuspanel.Text = "Defragmenting";
                await DiskDefragmentation();
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
                await Task.Delay(mega);
                Loading7.Visibility = Visibility.Hidden;
                Loading8.Visibility = Visibility.Visible;
                Statuspanel.Text = "Fixing: Security Holes";
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
                await Task.Delay(mega);
                Loading8.Visibility = Visibility.Hidden;
                Loading9.Visibility = Visibility.Visible;
                Statuspanel.Text = "Optimizing: System";
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
                await Task.Delay(mega);
                Loading9.Visibility = Visibility.Hidden;
                Loading10.Visibility = Visibility.Visible;
                Statuspanel.Text = "Optimizing: System";
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
                Statuspanel.Text = "Repairing Files";
                await Task.Delay(mega);
                Loading10.Visibility = Visibility.Hidden;
                scanprogress.Value = reset;
                #endregion
                resultpanel3.Visibility = Visibility.Visible;
                resultpanel4.Visibility = Visibility.Visible;
                //scanresult.Visibility = Visibility.Visible;
                Statuspanel.Text = "Success";
                //scanresult.Text = "Junk: " + sizedir.ToString() + "MB cleaned\n" + "Security Holes: " + "Fixed" + "\nPrivacy Issue: " + "Fixed" + "\nDevice Optimized";
                scanstop.Content = "Back";
            }
            catch ( Exception e)
            {
                System.Windows.MessageBox.Show(e.Message + "\n" + e.Source);
            }
        }

        private void junkdet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] Junk1 = Directory.GetFiles(Path.GetTempPath());
                string[] Junk2 = Directory.GetFiles("C:\\Windows\\Prefetch\\");
                string[] Junk3 = Directory.GetFiles("C:\\Windows\\Temp\\");
                string[] Junk4 = Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\");
                for (int i = 0; i < Directory.GetFiles(Path.GetTempPath()).Length; i++)
                {
                    resdet2.Children.Add(new TextBlock { Text = Junk1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Prefetch\\").Length; i++)
                {
                    resdet2.Children.Add(new TextBlock { Text = Junk2[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Temp\\").Length; i++)
                {
                    resdet2.Children.Add(new TextBlock { Text = Junk3[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\").Length; i++)
                {
                    resdet2.Children.Add(new TextBlock { Text = Junk4[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace);
            }
            //resdet2.Text = "Junk Files: " + sizedir.ToString() + "MB";
            resultdetails1.Visibility = Visibility.Visible;
            resdet2.Visibility = Visibility.Visible;
        }

        private void regdet_Click(object sender, RoutedEventArgs e)
        {
            resultdetails1.Visibility = Visibility.Visible;
            resdet6.Visibility = Visibility.Visible;
            try
            {
                Task.Run(() => ScannerFunctions.StartScanning());
                for (int i = 0; i < 500; i++)
                {
                    resdet6.Children.Add(new TextBlock { Text = ScannerFunctions.CurrentScannedObject, Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
            }
            catch { }
        }

        private void privacydet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ICacheFirefoxpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Mozilla\\Firefox\\Profiles\\";
                string ICacheChromepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Cache\\";
                //string ICacheChromepath1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Application Cache\\Cache\\";
                if (Directory.Exists(ICacheChromepath))
                {
                    string[] chromedetails = Directory.GetFiles(ICacheChromepath);
                    for (int i = 0; i < 200; i++)
                    {
                        resdet3.Children.Add(new TextBlock { Text = chromedetails[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                    }
                }
                /*if (Directory.Exists(ICacheChromepath1))
                {
                    string[] chromedetails1 = Directory.GetFiles(ICacheChromepath1);
                    for (int i = 0; i < 200; i++)
                    {
                        resdet3.Children.Add(new TextBlock { Text = chromedetails1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                    }
                }*/
                if (Directory.Exists(ICacheFirefoxpath))
                {
                    string[] firefoxdetails = Directory.GetFiles(ICacheFirefoxpath, "*.*", SearchOption.AllDirectories);
                    for (int i = 0; i < 200; i++)
                    {
                        resdet3.Children.Add(new TextBlock { Text = firefoxdetails[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 9 });
                    }
                }
            }
            catch (Exception )
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
            //resdet3.Text = "Privacy: " + TotalICache.ToString() + "MB";
            resultdetails1.Visibility = Visibility.Visible;
            resdet3.Visibility = Visibility.Visible;
        }
        private void malwaredet_Click(object sender, RoutedEventArgs e)
        {
            string[] Junk2 = Directory.GetFiles("C:\\Windows\\Prefetch\\");
            for (int i = 0; i < 2; i++)
            {
                resdet1.Children.Add(new TextBlock { Text = Junk2[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
            }
            /*string Temppath = Path.GetTempPath() + "sysvent.dll";
            string Tenppath1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "erwts.dll";
            string Temppath2 = Path.GetTempPath() + "crrptrr.dll";
            string Tenppath3 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "ruop.dll";
            string Temppath4 = Path.GetTempPath() + "systemred.dll";
            string Tenppath5 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "windrr.dll";
            string Temppath6 = Path.GetTempPath() + "tyope.dll";
            string Tenppath7 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "undll.dll";
            string Temppath8 = Path.GetTempPath() + "vans.dll";
            resdet1.Text = "Detected Items" + "\n" + "\n" + Temppath + "\n" + Tenppath1 + "\n" + Temppath2 + "\n" + Tenppath3 + "\n" + Temppath4 + "\n" + Tenppath5 + "\n" + Temppath6 + "\n" + Tenppath7 + "\n" + Temppath8;*/
            resultdetails1.Visibility = Visibility.Visible;
            resdet1.Visibility = Visibility.Visible;
        }

        private void startupdet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                string[] startupitems = reg.GetValueNames();
                for (int i = 0; i < reg.GetValueNames().Length; i++)
                {
                    resdet4.Children.Add(new TextBlock { Text = startupitems[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
            resultdetails1.Visibility = Visibility.Visible;
            resdet4.Visibility = Visibility.Visible;
        }

        private void filerepair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] Junk3 = Directory.GetFiles("C:\\Windows\\Temp\\",".",SearchOption.AllDirectories);
                for (int i = 0; i < 2; i++)
                {
                    resdet5.Children.Add(new TextBlock { Text = Junk3[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
                resultdetails1.Visibility = Visibility.Visible;
                resdet5.Visibility = Visibility.Visible;
            }
            catch { }
        }

        private void fragments_Click(object sender, RoutedEventArgs e)
        {

        }

        private void diskcln_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] Junk1 = Directory.GetFiles(Path.GetTempPath());
                string[] Junk2 = Directory.GetFiles("C:\\Windows\\Prefetch\\");
                string[] Junk3 = Directory.GetFiles("C:\\Windows\\Temp\\");
                string[] Junk4 = Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\");
                for (int i = 0; i < Directory.GetFiles(Path.GetTempPath()).Length; i++)
                {
                    resdet8.Children.Add(new TextBlock { Text = Junk1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Prefetch\\").Length; i++)
                {
                    resdet8.Children.Add(new TextBlock { Text = Junk2[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Temp\\").Length; i++)
                {
                    resdet8.Children.Add(new TextBlock { Text = Junk3[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\").Length; i++)
                {
                    resdet8.Children.Add(new TextBlock { Text = Junk4[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace);
            }
            try
            {
                string ICacheFirefoxpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Mozilla\\Firefox\\Profiles\\";
                string ICacheChromepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Cache\\";
                string ICacheChromepath1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Application Cache\\Cache\\";
                if (Directory.Exists(ICacheChromepath))
                {
                    string[] chromedetails = Directory.GetFiles(ICacheChromepath);
                    for (int i = 0; i < 200; i++)
                    {
                        resdet8.Children.Add(new TextBlock { Text = chromedetails[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                    }
                }
                if (Directory.Exists(ICacheChromepath1))
                {
                    string[] chromedetails1 = Directory.GetFiles(ICacheChromepath1);
                    for (int i = 0; i < 200; i++)
                    {
                        resdet8.Children.Add(new TextBlock { Text = chromedetails1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                    }
                }
                if (Directory.Exists(ICacheFirefoxpath))
                {
                    string[] firefoxdetails = Directory.GetFiles(ICacheFirefoxpath, "*.*", SearchOption.AllDirectories);
                    for (int i = 0; i < 200; i++)
                    {
                        resdet8.Children.Add(new TextBlock { Text = firefoxdetails[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 9 });
                    }
                }
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
            resultdetails1.Visibility = Visibility.Visible;
            resdet8.Visibility = Visibility.Visible;
        }

        private void systemissu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] Junk4 = Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\", "*.*", SearchOption.AllDirectories);
                for (int i = 0; i < 2; i++)
                {
                    resdet9.Children.Add(new TextBlock { Text = Junk4[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
            }
            catch(Exception f)
            {
                //System.Windows.MessageBox.Show(f.Message + "\n" + f.StackTrace + "\n" + f.Source);
            }
            resultdetails1.Visibility = Visibility.Visible;
            resdet9.Visibility = Visibility.Visible;
        }

        private void resdetclose_Click(object sender, RoutedEventArgs e)
        {
            resultdetails1.Visibility = Visibility.Collapsed;
            resdet1.Visibility = Visibility.Collapsed;
            resdet2.Visibility = Visibility.Collapsed;
            resdet3.Visibility = Visibility.Collapsed;
            resdet4.Visibility = Visibility.Collapsed;
            resdet5.Visibility = Visibility.Collapsed;
            resdet6.Visibility = Visibility.Collapsed;
            resdet7.Visibility = Visibility.Collapsed;
            resdet8.Visibility = Visibility.Collapsed;
            resdet9.Visibility = Visibility.Collapsed;
            resdet1.Children.Clear();
            resdet2.Children.Clear();
            resdet3.Children.Clear();
            resdet4.Children.Clear();
            resdet5.Children.Clear();
            resdet6.Children.Clear();
            resdet7.Children.Clear();
            resdet8.Children.Clear();
            resdet9.Children.Clear();
        }

        private void RegistryCleaner_Click(object sender, RoutedEventArgs e)
        {
            if (premium == 0)
            {
                popupmain = new PopupWindow3();
                if (popupmain.ShowDialog().Value)
                {
                    Activatekey(true);
                }
                else
                {
                    Activatekey(false);
                }
            }
            else
            {
                Process.Start("Registry Cleaner.exe");
            }
        }
        private void EasyBackup_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("EasyBackup.exe");
            /*if (premium == 0)
            {
                popupmain = new PopupWindow3();
                if (popupmain.ShowDialog().Value)
                {
                    Activatekey(true);
                }
                else
                {
                    Activatekey(false);
                }
            }
            else
            {
                
            }*/
        }
    }
}
