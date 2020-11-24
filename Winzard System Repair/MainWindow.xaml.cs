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
using System.Collections.ObjectModel;
using ServiceStack;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NotifyIcon nIcon = new NotifyIcon();
        Process malscan, regscan, Defrag, privacyclean, junkclean, fragmentscan;
        EventLog eventLog;
        PopupWindow1 Popupsub;
        PopupWindow2 Popupsub2;
        PopupWindow3 popupmain;
        PopupWindow4 popupmain1;
        PopupWindow5 popupmain2;
        CancellationToken ct = CancellationToken.None;
        int premium = 0, mega = 2000, MalwareNumbers = 0;
        float sizedir = 0, sizedir1 = 0;
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
            ScannerFunctions.NotificationOnOff();
            Popups3();
        }
        //Corrupt Sysfiles
        public async Task DiskCleanerAsync()
        {
            try
            {
                if (ct.IsCancellationRequested) return;
                await JunkCleaner();
                await PrivacyCleaner();
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }
        public async Task FixSystemIssueAsync()
        {
            try
            {
                if (ct.IsCancellationRequested) return;
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
                if (ct.IsCancellationRequested) return;
                load.Children.Add(new TextBlock { Text = "Checking:0%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                await Task.Delay(1000);
                if (ct.IsCancellationRequested) return;
                load.Children.Add(new TextBlock { Text = "Checking:10%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                if (ct.IsCancellationRequested) return;
                fragmentscan = new Process();
                fragmentscan.StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = "cmd.exe",
                    Arguments = "/c \"defrag c: /A\" && exit",
                };
                if (ct.IsCancellationRequested) return;
                fragmentscan.Start();
                if (ct.IsCancellationRequested)
                {
                    fragmentscan.Kill();
                    return;
                }
                await Task.Run(() => fragmentscan.WaitForExit());
                if (ct.IsCancellationRequested) return;
                await Task.Delay(1000);
                if (ct.IsCancellationRequested) return;
                load.Children.Add(new TextBlock { Text = "Checking:20%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                await Task.Delay(1000);
                if (ct.IsCancellationRequested) return;
                load.Children.Add(new TextBlock { Text = "Checking:30%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                await Task.Delay(1000);
                if (ct.IsCancellationRequested) return;
                load.Children.Add(new TextBlock { Text = "Checking:40%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                await Task.Delay(1000);
                if (ct.IsCancellationRequested) return;
                load.Children.Add(new TextBlock { Text = "Checking:50%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                await Task.Delay(1000);
                if (ct.IsCancellationRequested) return;
                load.Children.Add(new TextBlock { Text = "Checking:60%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                await Task.Delay(1000);
                if (ct.IsCancellationRequested) return;
                load.Children.Add(new TextBlock { Text = "Checking:70%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                await Task.Delay(1000);
                if (ct.IsCancellationRequested) return;
                load.Children.Add(new TextBlock { Text = "Checking:80%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                await Task.Delay(1000);
                if (ct.IsCancellationRequested) return;
                load.Children.Add(new TextBlock { Text = "Checking:90%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                await Task.Delay(1000);
                if (ct.IsCancellationRequested) return;
                load.Children.Add(new TextBlock { Text = "Checking:100%", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                regdetails.ScrollToEnd();
                await Task.Delay(1000);
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
                if (ct.IsCancellationRequested) return;
                Defrag = new Process();
                Defrag.StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = "cmd.exe",
                    Arguments = "/c \"defrag c: /U /V\" && exit",
                };
                if (ct.IsCancellationRequested) return;
                Defrag.Start();
                if (ct.IsCancellationRequested)
                {
                    Defrag.Kill();
                    return;
                }
                await Task.Run(() => Defrag.WaitForExit());
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
                if (ct.IsCancellationRequested) return;
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                string regvalue = reg.ValueCount.ToString();
                RegistryKey reg1 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                string regvalue1 = reg1.ValueCount.ToString();
                Startupoptimizations.Text = regvalue + "Items";
                string[] startupitems = reg.GetValueNames();
                for (int i = 0; i < reg.GetValueNames().Length; i++)
                {
                    if (ct.IsCancellationRequested) return;
                    load.Children.Add(new TextBlock { Text = startupitems[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    await Task.Delay(1000);
                }
                if (ct.IsCancellationRequested) return;
                string[] startupitems1 = reg1.GetValueNames();
                for (int i = 0; i < reg.GetValueNames().Length; i++)
                {
                    if (ct.IsCancellationRequested) return;
                    load.Children.Add(new TextBlock { Text = startupitems1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    await Task.Delay(1000);
                }
                if (ct.IsCancellationRequested) return;
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
                if (ct.IsCancellationRequested) return;
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                reg.DeleteSubKeyTree("Computer\\HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");

                RegistryKey regupdate = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Assembly curAssembly = Assembly.GetExecutingAssembly();
                /*reg1.SetValue(curAssembly.GetName().Name, curAssembly.Location);*/
                regupdate.SetValue(curAssembly.GetName().Name, System.Windows.Forms.Application.ExecutablePath.ToString());
                if (ct.IsCancellationRequested) return;
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
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
            System.Windows.Application.Current.Shutdown(0);
            /*BaseUI.Visibility = Visibility.Collapsed;
            nIcon.Icon = new Icon("shield.ico");
            nIcon.Visible = true;
            ShowInTaskbar = false;
            nIcon.DoubleClick += NIcon_DoubleClick;*/
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
            //t = new Thread (Tempdelete);
            //t.Start();
            quickfullscan.Visibility = Visibility.Visible;
        }
        //quick & full scan
        private void quickscan_Click(object sender, RoutedEventArgs e)
        {
            ct = CancellationToken.None;
            quickfullscan.Visibility = Visibility.Collapsed;
            statusgrid.Visibility = Visibility.Visible;
            scan.Visibility = Visibility.Collapsed;
            PCstatus.Visibility = Visibility.Collapsed;
            OtherSoftware.Visibility = Visibility.Collapsed;
            onetimefix.Visibility = Visibility.Collapsed;
            regdetails.Visibility = Visibility.Visible;
            Tempdelete();
        }
        private void fullscan_Click(object sender, RoutedEventArgs e)
        {
            quickfullscan.Visibility = Visibility.Collapsed;
            statusgrid.Visibility = Visibility.Visible;
            scan.Visibility = Visibility.Collapsed;
            PCstatus.Visibility = Visibility.Collapsed;
            OtherSoftware.Visibility = Visibility.Collapsed;
            onetimefix.Visibility = Visibility.Collapsed;
            regdetails.Visibility = Visibility.Visible;
            TempdeleteFull();
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
                if (ct.IsCancellationRequested) return;
                await Task.Run(() => ScannerFunctions.FixProblems());
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }
        public async Task RegistryFix()
        {
            try
            {
                if (ct.IsCancellationRequested) return;
                ScannerFunctions.StartScanning();
                for (int i = 0; i < 500; i++)
                {
                    if (ct.IsCancellationRequested) return;
                    string reg = "[Scanning] " + ScannerFunctions.CurrentScannedObject;
                    if (reg.Length > 100)
                    {
                        reg = reg.Substring(0, 100) + "...";
                    }
                    load.Children.Add(new TextBlock { Text = reg, Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    await Task.Delay(7);
                }
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
                if (ct.IsCancellationRequested) return;
                malscan = new Process();
                malscan.StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = @"malware\clamscan.exe",
                    Arguments = "\"C:\\ProgramData\\Microsoft\" -r --detect-pua=yes",
                    RedirectStandardOutput = true
                };
                if (ct.IsCancellationRequested) return;
                malscan.Start();
                await Task.Run(() =>
                {
                    while (!malscan.HasExited)
                    {
                        if (ct.IsCancellationRequested)
                        {
                            malscan.Kill();
                            return;
                        }
                        if (malscan.StandardOutput.EndOfStream) continue;
                        string line = malscan.StandardOutput.ReadLine();
                        if (line.Split(':')[line.Split(':').Length - 1].Trim().Contains("FOUND"))
                        {
                            ScannerFunctions.malwarelist.Add(line.Substring(0, line.LastIndexOf(':')).Trim());
                        }
                        else if (line.Contains(@"SCAN SUMMARY"))
                        {
                            ScannerFunctions.malwarsummary = malscan.StandardOutput.ReadToEnd();
                            foreach (string s in ScannerFunctions.malwarsummary.Split('\n'))
                            {
                                if (s.Trim().Contains("Infected files"))
                                {
                                    ScannerFunctions.infected = s.Trim().Split(':')[1].Trim();
                                }
                            }
                            break;
                        }
                        Dispatcher.Invoke(() =>
                        {
                            if (ct.IsCancellationRequested)
                            {
                                malscan.Kill();
                                return;
                            }
                            string tmp = "[Scanning] " + line;
                            if (tmp.Length > 100)
                            {
                                tmp = tmp.Substring(0, 100) + "...";
                            }
                            load.Children.Add(new TextBlock { Text = tmp, Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                        }
                    );
                    }
                    if (ScannerFunctions.malwarsummary.IsEmpty()) ScannerFunctions.infected = "0";

                });
                if (ScannerFunctions.malwarsummary.IsEmpty() == true)
                {
                    ManualMalwareFix.Content = "Fixed";
                    ManualMalwareFix.Background = System.Windows.Media.Brushes.Green;
                }
                //Task.Run(() => proc.WaitForExit())
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }
        public async Task MalwareCleanerFull()
        {
            try
            {
                if (ct.IsCancellationRequested) return;
                malscan = new Process();
                malscan.StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = @"malware\clamscan.exe",
                    Arguments = "\"C:\\\" -r --detect-pua=yes",
                    RedirectStandardOutput = true
                };
                if (ct.IsCancellationRequested) return;
                malscan.Start();
                await Task.Run(() =>
                {
                    while (!malscan.HasExited)
                    {
                        if (ct.IsCancellationRequested)
                        {
                            malscan.Kill();
                            return;
                        }
                        if (malscan.StandardOutput.EndOfStream) continue;
                        string line = malscan.StandardOutput.ReadLine();
                        if (line.Split(':')[line.Split(':').Length - 1].Trim().Contains("FOUND"))
                        {
                            ScannerFunctions.malwarelist.Add(line.Substring(0, line.LastIndexOf(':')).Trim());
                        }
                        else if (line.Contains(@"SCAN SUMMARY"))
                        {
                            ScannerFunctions.malwarsummary = malscan.StandardOutput.ReadToEnd();
                            foreach (string s in ScannerFunctions.malwarsummary.Split('\n'))
                            {
                                if (s.Trim().Contains("Infected files"))
                                {
                                    ScannerFunctions.infected = s.Trim().Split(':')[1].Trim();
                                }
                            }
                            break;
                        }
                        Dispatcher.Invoke(() =>
                        {
                            if (ct.IsCancellationRequested)
                            {
                                malscan.Kill();
                                return;
                            }
                            string tmp = "[Scanning] " + line;
                            if (tmp.Length > 100)
                            {
                                tmp = tmp.Substring(0, 100) + "...";
                            }
                            load.Children.Add(new TextBlock { Text = tmp, Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                        }
                    );
                    }
                    if (ScannerFunctions.malwarsummary.IsEmpty()) ScannerFunctions.infected = "0";

                });
                if (ScannerFunctions.malwarsummary.IsEmpty() == true)
                {
                    ManualMalwareFix.Content = "Fixed";
                    ManualMalwareFix.Background = System.Windows.Media.Brushes.Green;
                }
                //Task.Run(() => proc.WaitForExit())
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
        }

        public async Task PrivacyCleaner()
        {
            try
            {
                if (ct.IsCancellationRequested) return;
                string ICacheFirefoxpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Mozilla\\Firefox\\Profiles\\";
                string ICacheChromepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Cache\\";
                string ICacheChromepath1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Application Cache\\Cache\\";
                if (Directory.Exists(ICacheChromepath))
                {
                    if (ct.IsCancellationRequested) return;
                    string[] chromedetails = Directory.GetFiles(ICacheChromepath);
                    for (int i = 0; i < 200; i++)
                    {
                        if (ct.IsCancellationRequested) return;
                        string chrometmp = "[Scanning] " + chromedetails[i];
                        if (chrometmp.Length > 100)
                        {
                            chrometmp = chrometmp.Substring(0, 100) + "...";
                        }
                        load.Children.Add(new TextBlock { Text = chrometmp, Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                        regdetails.ScrollToEnd();
                        await Task.Delay(7);
                    }
                }
                if (Directory.Exists(ICacheChromepath1))
                {
                    if (ct.IsCancellationRequested) return;
                    string[] chromedetails1 = Directory.GetFiles(ICacheChromepath1);
                    for (int i = 0; i < 200; i++)
                    {
                        if (ct.IsCancellationRequested) return;
                        string chrometmp1 = "[Scanning] " + chromedetails1[i];
                        if (chrometmp1.Length > 100)
                        {
                            chrometmp1 = chrometmp1.Substring(0, 100) + "...";
                        }
                        load.Children.Add(new TextBlock { Text = chrometmp1, Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                        regdetails.ScrollToEnd();
                        await Task.Delay(7);
                    }
                }
                if (Directory.Exists(ICacheFirefoxpath))
                {
                    if (ct.IsCancellationRequested) return;
                    string[] firefoxdetails = Directory.GetFiles(ICacheFirefoxpath, "*.*", SearchOption.AllDirectories);
                    for (int i = 0; i < 200; i++)
                    {
                        if (ct.IsCancellationRequested) return;
                        string firefoxtmp = "[Scanning] " + firefoxdetails[i];
                        if (firefoxtmp.Length > 100)
                        {
                            firefoxtmp = firefoxtmp.Substring(0, 100) + "...";
                        }
                        load.Children.Add(new TextBlock { Text = firefoxtmp, Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                        regdetails.ScrollToEnd();
                        await Task.Delay(7);
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }
        public async Task JunkCleaner()
        {
            try
            {
                if (ct.IsCancellationRequested) return;
                string[] Junk1 = Directory.GetFiles(Path.GetTempPath());
                string[] Junk2 = Directory.GetFiles("C:\\Windows\\Prefetch\\");
                string[] Junk3 = Directory.GetFiles("C:\\Windows\\Temp\\");
                string[] Junk4 = Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\");
                if (ct.IsCancellationRequested) return;
                for (int i = 0; i < Directory.GetFiles(Path.GetTempPath()).Length; i++)
                {
                    if (ct.IsCancellationRequested) return;
                    load.Children.Add(new TextBlock { Text = Junk1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    await Task.Delay(7);
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Prefetch\\").Length; i++)
                {
                    if (ct.IsCancellationRequested) return;
                    load.Children.Add(new TextBlock { Text = Junk2[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    await Task.Delay(7);
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Temp\\").Length; i++)
                {
                    if (ct.IsCancellationRequested) return;
                    load.Children.Add(new TextBlock { Text = Junk3[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    await Task.Delay(7);
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\").Length; i++)
                {
                    if (ct.IsCancellationRequested) return;
                    load.Children.Add(new TextBlock { Text = Junk4[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
                    regdetails.ScrollToEnd();
                    await Task.Delay(7);
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
                if (ct.IsCancellationRequested) return;
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
                ScannerFunctions.mynum = 0;
                ScannerFunctions.systemissuever = 0;
                ScannerFunctions.filever = 0;
                ScannerFunctions.totalcachenum = 0;
                try
                {
                    if (ct.IsCancellationRequested) return;
                    string Temppath = Path.GetTempPath();
                    string ICacheChromepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Cache\\";
                    string ICacheFirefoxpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Mozilla\\Firefox\\Profiles\\";
                    string ICacheExpolrerpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Local\\Microsoft\\Edge\\User Data\\Default\\";
                    //string ICacheChromepath1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Application Cache\\Cache\\";
                    double Tempsize = 0;
                    double Tempsize1 = 0;
                    Tempsize = DirSize(new DirectoryInfo(Temppath));
                    Tempsize += DirSize(new DirectoryInfo("C:/Windows/Prefetch"));
                    Tempsize += DirSize(new DirectoryInfo("C:/Windows/Temp"));
                    Tempsize1 = DirSize(new DirectoryInfo("C:/Windows/SoftwareDistribution/Download"));
                    //Tempsize += DirSize(new DirectoryInfo(ICacheChromepath));
                    //Tempsize += DirSize(new DirectoryInfo(ICacheFirefoxpath));
                    //Tempsize += DirSize(new DirectoryInfo(ICacheExpolrerpath));
                    sizedir = (float)Convert.ToDouble(string.Format("{0:0.00}", (Tempsize / (1024 * 1024))));
                    sizedir1 = (float)Convert.ToDouble(string.Format("{0:0.00}", (Tempsize1 / (1024 * 1024))));
                    if (Directory.Exists(ICacheExpolrerpath))
                    {
                        ScannerFunctions.totalcachenum += Directory.GetFiles(ICacheExpolrerpath).Length;
                    }
                    if (Directory.Exists(ICacheChromepath))
                    {
                        ScannerFunctions.totalcachenum += Directory.GetFiles(ICacheChromepath).Length;
                    }
                    /*if (Directory.Exists(ICacheChromepath1))
                    {
                        totalcachenum += Directory.GetFiles(ICacheChromepath1).Length;
                    }*/
                    if (Directory.Exists(ICacheFirefoxpath))
                    {
                        ScannerFunctions.totalcachenum += Directory.GetFiles(ICacheFirefoxpath, "*.*", SearchOption.AllDirectories).Length;
                    }
                    /*for (int i = 0; i < 2; i++)
                    {
                        if (Directory.GetFiles("C:\\Windows\\Prefetch\\", "*.*", SearchOption.AllDirectories).Length > 0)
                        {
                            ScannerFunctions.systemissuever = i + 1;
                        }
                        else
                        {
                            ScannerFunctions.systemissuever = 0;
                        }
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        if (Directory.GetFiles("C:\\Windows\\Temp\\", ".", SearchOption.AllDirectories).Length > 0)
                        {
                            ScannerFunctions.filever = i + 1;
                        }
                        else
                        {
                            ScannerFunctions.filever = 0;
                        }
                    }*/
                }
                catch (Exception e)
                {
                    //System.Windows.MessageBox.Show(e.Message);
                }
                //Progress
                //scantext.Text = "Scanning: Malware";
                #region Progress
                Statuspanel.Text = "PC Gold Optimizer and System Repair is currently collecting information from your computer for\nscanning purpose. Please wait until the scanning is complete";
                Loading1.Visibility = Visibility.Visible;
                Status.Text = "Scanning: Junk";
                await JunkCleaner();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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
                await PrivacyCleaner();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await RegistryFix();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await MalwareCleaner();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await Fragments();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await startupoptimization();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await RepairFilesAsync();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await DiskCleanerAsync();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await FixSystemIssueAsync();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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
                #endregion

                //load.Visibility = Visibility.Collapsed;
                Random rnd = new Random();
                int defrag = rnd.Next(0, 9);
                defragmentation.Text = defrag.ToString() + "%";
                MalwareThreats.Text = ScannerFunctions.infected;
                repairfile.Text = "0";
                systemissue.Text = "0";
                diskclean.Text = sizedir1.ToString();
                JunkFiles.Text = sizedir.ToString() + "MB";
                PrivacyFiles.Text = ScannerFunctions.totalcachenum.ToString() + "Items";
                ScannerFunctions.mynum = ScannerFunctions.arrBadRegistryKeys.Problems;
                registrydetails.Text = ScannerFunctions.mynum.ToString() + "Items";
                scanfix.Visibility = Visibility.Visible;
                onetimefix.Visibility = Visibility.Visible;
                resultpanel.Visibility = Visibility.Visible;
                resultpanel2.Visibility = Visibility.Visible;
                resultdetails.Visibility = Visibility.Visible;
                Popups();

            }
        }
        public async void TempdeleteFull()
        {
            {
                ScannerFunctions.mynum = 0;
                ScannerFunctions.systemissuever = 0;
                ScannerFunctions.filever = 0;
                ScannerFunctions.totalcachenum = 0;
                try
                {
                    if (ct.IsCancellationRequested) return;
                    string Temppath = Path.GetTempPath();
                    string ICacheChromepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Cache\\";
                    string ICacheFirefoxpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Mozilla\\Firefox\\Profiles\\";
                    string ICacheExpolrerpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Local\\Microsoft\\Edge\\User Data\\Default\\";
                    //string ICacheChromepath1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Application Cache\\Cache\\";
                    double Tempsize = 0, Tempsize1 = 0;
                    Tempsize = DirSize(new DirectoryInfo(Temppath));
                    Tempsize += DirSize(new DirectoryInfo("C:/Windows/Prefetch"));
                    Tempsize += DirSize(new DirectoryInfo("C:/Windows/Temp"));
                    Tempsize1 = DirSize(new DirectoryInfo("C:/Windows/SoftwareDistribution/Download"));
                    //Tempsize += DirSize(new DirectoryInfo(ICacheChromepath));
                    //Tempsize += DirSize(new DirectoryInfo(ICacheFirefoxpath));
                    //Tempsize += DirSize(new DirectoryInfo(ICacheExpolrerpath));
                    sizedir = (float)Convert.ToDouble(string.Format("{0:0.00}", (Tempsize / (1024 * 1024))));
                    sizedir1 = (float)Convert.ToDouble(string.Format("{0:0.00}", (Tempsize / (1024 * 1024))));

                    if (Directory.Exists(ICacheExpolrerpath))
                    {
                        ScannerFunctions.totalcachenum += Directory.GetFiles(ICacheExpolrerpath).Length;
                    }
                    if (Directory.Exists(ICacheChromepath))
                    {
                        ScannerFunctions.totalcachenum += Directory.GetFiles(ICacheChromepath).Length;
                    }
                    /*if (Directory.Exists(ICacheChromepath1))
                    {
                        totalcachenum += Directory.GetFiles(ICacheChromepath1).Length;
                    }*/
                    if (Directory.Exists(ICacheFirefoxpath))
                    {
                        ScannerFunctions.totalcachenum += Directory.GetFiles(ICacheFirefoxpath, "*.*", SearchOption.AllDirectories).Length;
                    }
                    /*for (int i = 0; i < 2; i++)
                    {
                        if (Directory.GetFiles("C:\\Windows\\Prefetch\\", "*.*", SearchOption.AllDirectories).Length > 0)
                        {
                            ScannerFunctions.systemissuever = i + 1;
                        }
                        else
                        {
                            ScannerFunctions.systemissuever = 0;
                        }
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        if (Directory.GetFiles("C:\\Windows\\Temp\\", ".", SearchOption.AllDirectories).Length > 0)
                        {
                            ScannerFunctions.filever = i + 1;
                        }
                        else
                        {
                            ScannerFunctions.filever = 0;
                        }
                    }*/
                }
                catch (Exception e)
                {
                    //System.Windows.MessageBox.Show(e.Message);
                }
                //Progress
                //scantext.Text = "Scanning: Malware";
                #region Progress
                Statuspanel.Text = "PC Gold Optimizer and System Repair is currently collecting information from your computer for\nscanning purpose. Please wait until the scanning is complete";
                Loading1.Visibility = Visibility.Visible;
                Status.Text = "Scanning: Junk";

                await JunkCleaner();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await PrivacyCleaner();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await RegistryFix();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await MalwareCleanerFull();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await Fragments();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await startupoptimization();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await RepairFilesAsync();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await DiskCleanerAsync();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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

                await FixSystemIssueAsync();
                if (ct.IsCancellationRequested) return;
                await Task.Delay(2000);
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
                #endregion

                //load.Visibility = Visibility.Collapsed;
                Random rnd = new Random();
                int defrag = rnd.Next(0, 9);
                defragmentation.Text = defrag.ToString() + "%";
                MalwareThreats.Text = ScannerFunctions.infected;
                repairfile.Text = "0";
                systemissue.Text = "0";
                diskclean.Text = sizedir1.ToString();
                JunkFiles.Text = sizedir.ToString() + "MB";
                PrivacyFiles.Text = ScannerFunctions.totalcachenum.ToString() + "Items";
                ScannerFunctions.mynum = ScannerFunctions.arrBadRegistryKeys.Problems;
                registrydetails.Text = ScannerFunctions.mynum.ToString() + "Items";
                scanfix.Visibility = Visibility.Visible;
                onetimefix.Visibility = Visibility.Visible;
                resultpanel.Visibility = Visibility.Visible;
                resultpanel2.Visibility = Visibility.Visible;
                resultdetails.Visibility = Visibility.Visible;
                Popups();

            }
        }

        //The Scan stop button
        private void scanstop_Click(object sender, RoutedEventArgs e)
        {
            statusgrid.Visibility = Visibility.Collapsed;
            scan.Visibility = Visibility.Visible;
            PCstatus.Visibility = Visibility.Visible;
            OtherSoftware.Visibility = Visibility.Visible;
            //loadvid.LoadedBehavior = MediaState.Stop;
            //load.Visibility = Visibility.Collapsed;
            resultpanel.Visibility = Visibility.Collapsed;
            resultpanel2.Visibility = Visibility.Collapsed;
            resultpanel3.Visibility = Visibility.Collapsed;
            resultpanel4.Visibility = Visibility.Collapsed;
            resultpanel5.Visibility = Visibility.Collapsed;
            resultdetails.Visibility = Visibility.Collapsed;
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
            try
            {
                CancellationTokenSource ts = new CancellationTokenSource();
                ct = ts.Token;
                ts.Cancel();
            }
            catch (Exception g)
            {

            }

        }
        //The Repair Button
        public async void Scanfix_Click(object sender, RoutedEventArgs e)
        {
            if (premium == 1)
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

                    #region Progress
                    double reset = 0;
                    scanprogress.Value = reset;
                    Loading1.Visibility = Visibility.Visible;
                    Statuspanel.Text = "Fixing: Junk";
                    resultpanel5.Visibility = Visibility.Collapsed;
                    resultpanel61.Visibility = Visibility.Visible;
                    try
                    {
                        string[] Junk1 = Directory.GetFiles(Path.GetTempPath());
                        string[] Junk2 = Directory.GetFiles("C:\\Windows\\Prefetch\\");
                        string[] Junk3 = Directory.GetFiles("C:\\Windows\\Temp\\");
                        string[] Junk4 = Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\");
                        for (int i = 0; i < Directory.GetFiles(Path.GetTempPath()).Length; i++)
                        {
                            resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + Junk1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                            await Task.Delay(50);
                        }
                        for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Prefetch\\").Length; i++)
                        {
                            resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + Junk2[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                            await Task.Delay(50);
                        }
                        for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Temp\\").Length; i++)
                        {
                            resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + Junk3[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                            await Task.Delay(50);
                        }
                        for (int i = 0; i < Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\").Length; i++)
                        {
                            resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + Junk4[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                            await Task.Delay(50);
                        }
                    }
                    catch (Exception)
                    {
                        //System.Windows.MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace);
                    }
                    await Task.Delay(2000);
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
                    await Task.Delay(2000);
                    resultpanel6.Children.Clear();
                    resultpanel61.Visibility = Visibility.Collapsed;
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
                    resultpanel5.Visibility = Visibility.Collapsed;
                    resultpanel61.Visibility = Visibility.Visible;
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
                                resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + chromedetails[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                                await Task.Delay(50);
                            }
                        }
                        /*if (Directory.Exists(ICacheChromepath1))
                        {
                            string[] chromedetails1 = Directory.GetFiles(ICacheChromepath1);
                            for (int i = 0; i < 200; i++)
                            {
                                resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + chromedetails1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                            }
                        }*/
                        if (Directory.Exists(ICacheFirefoxpath))
                        {
                            string[] firefoxdetails = Directory.GetFiles(ICacheFirefoxpath, "*.*", SearchOption.AllDirectories);
                            for (int i = 0; i < 200; i++)
                            {
                                resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + firefoxdetails[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 9 });
                                await Task.Delay(50);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        //System.Windows.MessageBox.Show(e.Message);
                    }
                    await Task.Delay(2000);
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
                    await Task.Delay(2000);
                    resultpanel6.Children.Clear();
                    resultpanel61.Visibility = Visibility.Collapsed;
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
                    resultpanel5.Visibility = Visibility.Collapsed;
                    resultpanel61.Visibility = Visibility.Visible;
                    try
                    {
                        //Task.Run(() => ScannerFunctions.StartScanning());
                        for (int i = 0; i < ScannerFunctions.arrBadRegistryKeys.Count; i++)
                        {
                            resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + ScannerFunctions.arrBadRegistryKeys[i].RegKeyPath, Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                            await Task.Delay(50);
                        }
                    }
                    catch { }
                    Loading06.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    await Registrycleanfix();
                    await Task.Delay(2000);
                    resultpanel6.Children.Clear();
                    resultpanel61.Visibility = Visibility.Collapsed;
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
                    Statuspanel.Text = "Fixing: Malware";
                    resultpanel5.Visibility = Visibility.Collapsed;
                    resultpanel61.Visibility = Visibility.Visible;
                    try
                    {
                        for (int i = 0; i < ScannerFunctions.malwarelist.Count; i++)
                        {
                            resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + ScannerFunctions.malwarelist, Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                            await Task.Delay(50);
                        }
                        foreach (string f in ScannerFunctions.malwarelist)
                        {
                            if (File.Exists(f))
                            {
                                File.Delete(f);
                            }
                        }
                    }
                    catch { }
                    await Task.Delay(2000);
                    resultpanel6.Children.Clear();
                    resultpanel61.Visibility = Visibility.Collapsed;
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
                    Statuspanel.Text = "Defragmenting";
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
                    Statuspanel.Text = "Fixing: Startup";
                    resultpanel5.Visibility = Visibility.Collapsed;
                    resultpanel61.Visibility = Visibility.Visible;
                    try
                    {
                        RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                        string[] startupitems = reg.GetValueNames();
                        for (int i = 0; i < reg.GetValueNames().Length; i++)
                        {
                            resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + startupitems[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                            await Task.Delay(50);
                        }
                    }
                    catch (Exception)
                    {
                        //System.Windows.MessageBox.Show(e.Message);
                    }
                    Loading04.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    startupoptimizationfix();
                    await Task.Delay(2000);
                    resultpanel6.Children.Clear();
                    resultpanel61.Visibility = Visibility.Collapsed;
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
                    Statuspanel.Text = "Optimizing: Files";
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
                    Statuspanel.Text = "Optimizing: Disk";
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
                    Statuspanel.Text = "System Issue";
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
                catch (Exception)
                {
                    //System.Windows.MessageBox.Show(e.Message + "\n" + e.Source);
                }
                popupmain2 = new PopupWindow5();
                popupmain2.Show();
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
            try
            {
                scanfix.Visibility = Visibility.Collapsed;
                onetimefix.Visibility = Visibility.Collapsed;
                resultpanel.Visibility = Visibility.Collapsed;
                resultpanel2.Visibility = Visibility.Collapsed;
                resultdetails.Visibility = Visibility.Collapsed;
                //load.Visibility = Visibility.Collapsed;
                //scanresult.Visibility = Visibility.Collapsed;

                #region Progress
                double reset = 0;
                scanprogress.Value = reset;
                Loading1.Visibility = Visibility.Visible;
                Statuspanel.Text = "Fixing: Junk";
                resultpanel5.Visibility = Visibility.Collapsed;
                resultpanel61.Visibility = Visibility.Visible;
                try
                {
                    string[] Junk1 = Directory.GetFiles(Path.GetTempPath());
                    string[] Junk2 = Directory.GetFiles("C:\\Windows\\Prefetch\\");
                    string[] Junk3 = Directory.GetFiles("C:\\Windows\\Temp\\");
                    string[] Junk4 = Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\");
                    for (int i = 0; i < Directory.GetFiles(Path.GetTempPath()).Length; i++)
                    {
                        resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + Junk1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                        await Task.Delay(50);
                    }
                    for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Prefetch\\").Length; i++)
                    {
                        resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + Junk2[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                        await Task.Delay(50);
                    }
                    for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Temp\\").Length; i++)
                    {
                        resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + Junk3[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                        await Task.Delay(50);
                    }
                    for (int i = 0; i < Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\").Length; i++)
                    {
                        resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + Junk4[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                        await Task.Delay(50);
                    }
                }
                catch (Exception)
                {
                    //System.Windows.MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace);
                }
                await Task.Delay(2000);
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
                await Task.Delay(2000);
                resultpanel6.Children.Clear();
                resultpanel61.Visibility = Visibility.Collapsed;
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
                resultpanel5.Visibility = Visibility.Collapsed;
                resultpanel61.Visibility = Visibility.Visible;
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
                            resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + chromedetails[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                            await Task.Delay(50);
                        }
                    }
                    /*if (Directory.Exists(ICacheChromepath1))
                    {
                        string[] chromedetails1 = Directory.GetFiles(ICacheChromepath1);
                        for (int i = 0; i < 200; i++)
                        {
                            resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + chromedetails1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                        }
                    }*/
                    if (Directory.Exists(ICacheFirefoxpath))
                    {
                        string[] firefoxdetails = Directory.GetFiles(ICacheFirefoxpath, "*.*", SearchOption.AllDirectories);
                        for (int i = 0; i < 200; i++)
                        {
                            resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + firefoxdetails[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 9 });
                            await Task.Delay(50);
                        }
                    }
                }
                catch (Exception)
                {
                    //System.Windows.MessageBox.Show(e.Message);
                }
                await Task.Delay(2000);
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
                await Task.Delay(2000);
                resultpanel6.Children.Clear();
                resultpanel61.Visibility = Visibility.Collapsed;
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
                resultpanel5.Visibility = Visibility.Collapsed;
                resultpanel61.Visibility = Visibility.Visible;
                try
                {
                    //Task.Run(() => ScannerFunctions.StartScanning());
                    for (int i = 0; i < ScannerFunctions.arrBadRegistryKeys.Count; i++)
                    {
                        resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + ScannerFunctions.arrBadRegistryKeys[i].RegKeyPath, Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                        await Task.Delay(50);
                    }
                }
                catch { }
                Loading06.Visibility = Visibility.Visible;
                await Task.Delay(2000);
                await Registrycleanfix();
                await Task.Delay(2000);
                resultpanel6.Children.Clear();
                resultpanel61.Visibility = Visibility.Collapsed;
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
                Statuspanel.Text = "Fixing: Malware";
                resultpanel5.Visibility = Visibility.Collapsed;
                resultpanel61.Visibility = Visibility.Visible;
                try
                {
                    for (int i = 0; i < ScannerFunctions.malwarelist.Count; i++)
                    {
                        resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + ScannerFunctions.malwarelist, Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                        await Task.Delay(50);
                    }
                    foreach (string f in ScannerFunctions.malwarelist)
                    {
                        if (File.Exists(f))
                        {
                            File.Delete(f);
                        }
                    }
                }
                catch { }
                await Task.Delay(2000);
                resultpanel6.Children.Clear();
                resultpanel61.Visibility = Visibility.Collapsed;
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
                Statuspanel.Text = "Defragmenting";
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
                Statuspanel.Text = "Fixing: Startup";
                resultpanel5.Visibility = Visibility.Collapsed;
                resultpanel61.Visibility = Visibility.Visible;
                try
                {
                    RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                    string[] startupitems = reg.GetValueNames();
                    for (int i = 0; i < reg.GetValueNames().Length; i++)
                    {
                        resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + startupitems[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                        await Task.Delay(50);
                    }
                }
                catch (Exception)
                {
                    //System.Windows.MessageBox.Show(e.Message);
                }
                Loading04.Visibility = Visibility.Visible;
                await Task.Delay(2000);
                startupoptimizationfix();
                await Task.Delay(2000);
                resultpanel6.Children.Clear();
                resultpanel61.Visibility = Visibility.Collapsed;
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
                Statuspanel.Text = "Optimizing: Files";
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
                Statuspanel.Text = "Optimizing: Disk";
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
                Statuspanel.Text = "System Issue";
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
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show(e.Message + "\n" + e.Source);
            }
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
            if (premium == 0 && ScannerFunctions.notification == 0)
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
            if (premium == 0 && ScannerFunctions.notification == 0)
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
                    Popups();
                }
            }
            else
            {
                return;
            }

        }
        /*private async void Popups2()
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
        }*/
        private async void Popups3()
        {
            if (premium == 1 && ScannerFunctions.notification == 0)
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

        public void Turnoffnotifications_Checked(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream("sysset.bin", FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(true);
            fs.Close();
            bw.Close();
            ScannerFunctions.NotificationOnOff();
        }
        private void onetimefix_Click(object sender, RoutedEventArgs e)
        {
            OneTimeFix();
        }

        public void OneTimeFix()
        {
            if (File.Exists("sysdone.bin"))
            {
                FileStream fs = new FileStream("sysdone.bin", FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                if (br.ReadBoolean() == true)
                {
                    br.Close();
                    FileInfo fn = new FileInfo("sysdone.bin");
                    if (fn.CreationTime < DateTime.Now.AddHours(-23))
                    {
                        BinaryWriter bw = new BinaryWriter(fs);
                        bw.Write(false);
                        bw.Close();
                    }
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
                bw.Write(true);
                bw.Close();
                fs.Close();
                OneTimeRepair();
            }
        }
        public void OneTimeRepair()
        {
            resultpanel.Visibility = Visibility.Collapsed;
            resultpanel2.Visibility = Visibility.Collapsed;
            resultdetails.Visibility = Visibility.Collapsed;
            resultpanel5.Visibility = Visibility.Visible;
        }

        private void junkdet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] Junk1 = Directory.GetFiles(Path.GetTempPath());
                string[] Junk2 = Directory.GetFiles("C:\\Windows\\Prefetch\\");
                string[] Junk3 = Directory.GetFiles("C:\\Windows\\Temp\\");
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
                for (int i = 0; i < ScannerFunctions.arrBadRegistryKeys.Count; i++)
                {
                    string reg = ScannerFunctions.arrBadRegistryKeys[i].RegKeyPath + ScannerFunctions.arrBadRegistryKeys[i].ValueName;
                    if (reg.Length > 120)
                    {
                        reg = reg.Substring(0, 120) + "...";
                    }
                    resdet6.Children.Add(new TextBlock { Text = reg, Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
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
            catch (Exception)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
            //resdet3.Text = "Privacy: " + TotalICache.ToString() + "MB";
            resultdetails1.Visibility = Visibility.Visible;
            resdet3.Visibility = Visibility.Visible;
        }
        private void malwaredet_Click(object sender, RoutedEventArgs e)
        {
            if (ScannerFunctions.malwarsummary.IsEmpty())
            {
                resultdetails1.Visibility = Visibility.Visible;
                resdet1.Visibility = Visibility.Visible;
                return;
            }
            resdet1.Children.Add(new TextBlock { Text = "----------------Infected Flies-----------------", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
            for (int i = 0; i < ScannerFunctions.malwarelist.Count; i++)
            {
                resdet1.Children.Add(new TextBlock { Text = ScannerFunctions.malwarelist.ToString(), Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
            }
            resdet1.Children.Add(new TextBlock { Text = "", FontSize = 14 });
            resdet1.Children.Add(new TextBlock { Text = "------------------Scan Summary-------------------", Foreground = System.Windows.Media.Brushes.White, FontSize = 14 });
            foreach (string l in ScannerFunctions.malwarsummary.Split('\n'))
            {
                resdet1.Children.Add(new TextBlock { Text = l.Trim(), Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
            }
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
            /*try
            {
                string[] Junk3 = Directory.GetFiles("C:\\Windows\\Temp\\",".",SearchOption.AllDirectories);
                for (int i = 0; i < 2; i++)
                {
                    resdet5.Children.Add(new TextBlock { Text = Junk3[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
            }
            catch { }*/
            resultdetails1.Visibility = Visibility.Visible;
            resdet5.Visibility = Visibility.Visible;
        }
        private void fragments_Click(object sender, RoutedEventArgs e)
        {

        }
        private void diskcln_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] Junk4 = Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\");
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\").Length; i++)
                {
                    resdet8.Children.Add(new TextBlock { Text = Junk4[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace);
            }
            resultdetails1.Visibility = Visibility.Visible;
            resdet8.Visibility = Visibility.Visible;
        }
        private void systemissu_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {
                string[] Junk4 = Directory.GetFiles("C:\\Windows\\Prefetch\\", "*.*", SearchOption.AllDirectories);
                for (int i = 0; i < 2; i++)
                {
                    resdet9.Children.Add(new TextBlock { Text = Junk4[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
            }
            catch(Exception f)
            {
                //System.Windows.MessageBox.Show(f.Message + "\n" + f.StackTrace + "\n" + f.Source);
            }*/
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
            Console.WriteLine("Reset Done!!!");
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



        private async void ManualMalwareFix_Click(object sender, RoutedEventArgs e)
        {
            resultpanel5.Visibility = Visibility.Collapsed;
            resultpanel61.Visibility = Visibility.Visible;
            try
            {
                for (int i = 0; i < ScannerFunctions.malwarelist.Count; i++)
                {
                    resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + ScannerFunctions.malwarelist, Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
                foreach (string f in ScannerFunctions.malwarelist)
                {
                    if (File.Exists(f))
                    {
                        File.Delete(f);
                    }
                }
            }
            catch { }
            await Task.Delay(2000);
            resultpanel6.Children.Clear();
            resultpanel61.Visibility = Visibility.Collapsed;
            ManualMalwareFix.Content = "Fixed";
            ManualMalwareFix.Background = System.Windows.Media.Brushes.Green;
            resultpanel5.Visibility = Visibility.Visible;
        }
        private async void ManualJunkFix_Click(object sender, RoutedEventArgs e)
        {
            resultpanel5.Visibility = Visibility.Collapsed;
            resultpanel61.Visibility = Visibility.Visible;
            try
            {
                string[] Junk1 = Directory.GetFiles(Path.GetTempPath());
                string[] Junk2 = Directory.GetFiles("C:\\Windows\\Prefetch\\");
                string[] Junk3 = Directory.GetFiles("C:\\Windows\\Temp\\");
                for (int i = 0; i < Directory.GetFiles(Path.GetTempPath()).Length; i++)
                {
                    resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + Junk1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Prefetch\\").Length; i++)
                {
                    resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + Junk2[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\Temp\\").Length; i++)
                {
                    resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + Junk3[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace);
            }
            Loading02.Visibility = Visibility.Visible;
            await Task.Delay(2000);
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
            await Task.Delay(2000);
            resultpanel6.Children.Clear();
            resultpanel61.Visibility = Visibility.Collapsed;
            ManualJunkFix.Content = "Fixed";
            ManualJunkFix.Background = System.Windows.Media.Brushes.Green;
            Loading02.Visibility = Visibility.Hidden;
            resultpanel5.Visibility = Visibility.Visible;
        }
        private async void ManualPrivacyFix_Click(object sender, RoutedEventArgs e)
        {
            resultpanel5.Visibility = Visibility.Collapsed;
            resultpanel61.Visibility = Visibility.Visible;
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
                        resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + chromedetails[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                    }
                }
                /*if (Directory.Exists(ICacheChromepath1))
                {
                    string[] chromedetails1 = Directory.GetFiles(ICacheChromepath1);
                    for (int i = 0; i < 200; i++)
                    {
                        resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + chromedetails1[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                    }
                }*/
                if (Directory.Exists(ICacheFirefoxpath))
                {
                    string[] firefoxdetails = Directory.GetFiles(ICacheFirefoxpath, "*.*", SearchOption.AllDirectories);
                    for (int i = 0; i < 200; i++)
                    {
                        resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + firefoxdetails[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 9 });
                    }
                }
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
            Loading03.Visibility = Visibility.Visible;
            await Task.Delay(2000);
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
            await Task.Delay(2000);
            resultpanel6.Children.Clear();
            resultpanel61.Visibility = Visibility.Collapsed;
            ManualPrivacyFix.Content = "Fixed";
            ManualPrivacyFix.Background = System.Windows.Media.Brushes.Green;
            Loading03.Visibility = Visibility.Hidden;
            resultpanel5.Visibility = Visibility.Visible;
        }
        private async void ManualStartupFix_Click(object sender, RoutedEventArgs e)
        {
            resultpanel5.Visibility = Visibility.Collapsed;
            resultpanel61.Visibility = Visibility.Visible;
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                string[] startupitems = reg.GetValueNames();
                for (int i = 0; i < reg.GetValueNames().Length; i++)
                {
                    resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + startupitems[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show(e.Message);
            }
            Loading04.Visibility = Visibility.Visible;
            await Task.Delay(2000);
            startupoptimizationfix();
            await Task.Delay(2000);
            resultpanel6.Children.Clear();
            resultpanel61.Visibility = Visibility.Collapsed;
            ManualStartupFix.Content = "Fixed";
            ManualStartupFix.Background = System.Windows.Media.Brushes.Green;
            Loading04.Visibility = Visibility.Hidden;
            resultpanel5.Visibility = Visibility.Visible;
        }
        private async void ManualRepairFix_Click(object sender, RoutedEventArgs e)
        {
            resultpanel5.Visibility = Visibility.Collapsed;
            resultpanel61.Visibility = Visibility.Visible;
            try
            {
                string[] Junk3 = Directory.GetFiles("C:\\Windows\\Temp\\", ".", SearchOption.AllDirectories);
                for (int i = 0; i < 2; i++)
                {
                    resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + Junk3[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
            }
            catch { }
            Loading05.Visibility = Visibility.Visible;
            await Task.Delay(2000);
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
            await Task.Delay(2000);
            resultpanel6.Children.Clear();
            resultpanel61.Visibility = Visibility.Collapsed;
            ManualRepairFix.Content = "Fixed";
            ManualRepairFix.Background = System.Windows.Media.Brushes.Green;
            Loading05.Visibility = Visibility.Hidden;
            resultpanel5.Visibility = Visibility.Visible;
        }

        private async void ManualRegistryFix_Click(object sender, RoutedEventArgs e)
        {
            resultpanel5.Visibility = Visibility.Collapsed;
            resultpanel61.Visibility = Visibility.Visible;
            try
            {
                //Task.Run(() => ScannerFunctions.StartScanning());
                for (int i = 0; i < ScannerFunctions.arrBadRegistryKeys.Count; i++)
                {
                    resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + ScannerFunctions.arrBadRegistryKeys[i].RegKeyPath, Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
            }
            catch { }
            Loading06.Visibility = Visibility.Visible;
            await Task.Delay(2000);
            await Registrycleanfix();
            await Task.Delay(2000);
            resultpanel6.Children.Clear();
            resultpanel61.Visibility = Visibility.Collapsed;
            ManualRegistryFix.Content = "Fixed";
            ManualRegistryFix.Background = System.Windows.Media.Brushes.Green;
            Loading06.Visibility = Visibility.Hidden;
            resultpanel5.Visibility = Visibility.Visible;
        }
        private async void ManualFragmentsFix_Click(object sender, RoutedEventArgs e)
        {
            resultpanel5.Visibility = Visibility.Collapsed;
            Loading07.Visibility = Visibility.Visible;
            await Task.Delay(2000);
            await DiskDefragmentation();
            await Task.Delay(2000);
            ManualFragmentsFix.Content = "Fixed";
            ManualFragmentsFix.Background = System.Windows.Media.Brushes.Green;
            Loading07.Visibility = Visibility.Hidden;
            resultpanel5.Visibility = Visibility.Visible;
        }
        private async void ManualDiskFix_Click(object sender, RoutedEventArgs e)
        {
            resultpanel5.Visibility = Visibility.Collapsed;
            resultpanel61.Visibility = Visibility.Visible;
            try
            {
                string[] Junk4 = Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\");
                for (int i = 0; i < Directory.GetFiles("C:\\Windows\\SoftwareDistribution\\Download\\").Length; i++)
                {
                    resultpanel6.Children.Add(new TextBlock { Text = "Deleted " + Junk4[i], Foreground = System.Windows.Media.Brushes.White, FontSize = 11 });
                }
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace);
            }
            await Task.Delay(2000);
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
            await Task.Delay(2000);
            resultpanel6.Children.Clear();
            resultpanel61.Visibility = Visibility.Collapsed;
            ManualDiskFix.Content = "Fixed";
            ManualDiskFix.Background = System.Windows.Media.Brushes.Green;
            Loading08.Visibility = Visibility.Hidden;
            resultpanel5.Visibility = Visibility.Visible;
        }
        private async void ManualSystemFix_Click(object sender, RoutedEventArgs e)
        {
            Loading09.Visibility = Visibility.Visible;
            await Task.Delay(2000);
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
            await Task.Delay(2000);
            ManualSystemFix.Content = "Fixed";
            ManualSystemFix.Background = System.Windows.Media.Brushes.Green;
            Loading09.Visibility = Visibility.Hidden;
        }
    }
}
