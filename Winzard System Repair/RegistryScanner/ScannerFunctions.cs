using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winzard_System_Repair.RegistryScanner
{
    public class ScannerFunctions
    {

        private static string _currentObject = string.Empty;
        private static int _objectsScanned = 0;
        public delegate void ScanDelegate();

        Thread threadMain, threadScan;

        public static string CurrentSection
        {
            get;
            set;
        }

        public static string CurrentScannedObject
        {
            get
            {
                return _currentObject;
            }
            set
            {
                _currentObject = Utils.PrefixRegPath(value);
                _objectsScanned++;
            }
        }

        private static ScannerBase currentScanner;

        public static BadRegKeyArray arrBadRegistryKeys = new BadRegKeyArray();

        /// <summary>
        /// Begins scanning for errors in the registry
        /// </summary>
        public void StartScanning()
        {
            // Get start time of scan
            DateTime dateTimeStart = DateTime.Now;

            // Begin Critical Region
            Thread.BeginCriticalRegion();

            // Begin scanning
            try
            {

                    this.StartScanner(new StartupFiles());

                    this.StartScanner(new SharedDLLs());

                    this.StartScanner(new WindowsFonts());

                    this.StartScanner(new ApplicationInfo());

                    this.StartScanner(new ApplicationPaths());

                    this.StartScanner(new ActivexComObjects());

                    this.StartScanner(new SystemDrivers());

                    this.StartScanner(new WindowsHelpFiles());

                    this.StartScanner(new WindowsSounds());

                    this.StartScanner(new ApplicationSettings());

                    this.StartScanner(new RecentDocs());
            }
            catch (ThreadAbortException)
            {
                // Scanning was aborted
                if (this.threadScan.IsAlive)
                    this.threadScan.Abort();
            }

            // End Critical Region
            Thread.EndCriticalRegion();
            // Dialog will be closed automatically
            return;
        }

        /// <summary>
        /// Starts a scanner
        /// </summary>
        public void StartScanner(ScannerBase scannerName)
        {
            currentScanner = scannerName;

            System.Reflection.MethodInfo mi = scannerName.GetType().GetMethod("Scan", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            ScanDelegate objScan = (ScanDelegate)Delegate.CreateDelegate(typeof(ScanDelegate), mi);


            // Update section name
            scannerName.RootNode.SectionName = scannerName.ScannerName;
            ScannerFunctions.CurrentSection = scannerName.ScannerName;

            // Start scanning
            this.threadScan = new Thread(new ThreadStart(objScan));
            this.threadScan.Start();
            this.threadScan.Join();

            // Wait 250ms
            Thread.Sleep(250);

            ScannerFunctions.arrBadRegistryKeys.Add(scannerName.RootNode);
        }

        /// <summary>
        /// <para>Stores an invalid registry key to array list</para>
        /// <para>Use IsOnIgnoreList to check for ignored registry keys and paths</para>
        /// </summary>
        /// <param name="Problem">Reason its invalid</param>
        /// <param name="Path">The path to registry key (including registry hive)</param>
        /// <returns>True if it was added</returns>
        public static bool StoreInvalidKey(string Problem, string Path)
        {
            return StoreInvalidKey(Problem, Path, "");
        }

        /// <summary>
        /// <para>Stores an invalid registry key to array list</para>
        /// <para>Use IsOnIgnoreList to check for ignored registry keys and paths</para>
        /// </summary>
        /// <param name="problem">Reason its invalid</param>
        /// <param name="regPath">The path to registry key (including registry hive)</param>
        /// <param name="valueName">Value name (leave blank if theres none)</param>
        /// <returns>True if it was added. Otherwise, false.</returns>
        public static bool StoreInvalidKey(string problem, string regPath, string valueName)
        {
            string baseKey, subKey;

            // Check for null parameters
            if (string.IsNullOrEmpty(problem) || string.IsNullOrEmpty(regPath))
                return false;

            // Make sure registry key exists
            if (string.IsNullOrEmpty(valueName))
            {
                if (!Utils.RegKeyExists(regPath))
                    return false;
            }
            else
            {
                if (!Utils.ValueNameExists(regPath, valueName))
                    return false;
            }

            // Parse registry key to base and subkey
            if (!Utils.ParseRegKeyPath(regPath, out baseKey, out subKey))
                return false;

            // If value name is specified, see if it exists
            if (!string.IsNullOrEmpty(valueName))
                if (!Utils.ValueNameExists(baseKey, subKey, valueName))
                    return false;

            // Make sure we have the correct permissions for the registry key
            if (!Utils.CanDeleteKey(Utils.RegOpenKey(baseKey, subKey)))
                return false;

            arrBadRegistryKeys.Add(new BadRegistryKey(problem, baseKey, subKey, valueName));

            arrBadRegistryKeys.Problems++;

            return true;
        }

        /*private void FixProblems()
        {
            xmlRegistry xmlReg = new xmlRegistry();
            long lSeqNum = 0;

            if (this.treeModel.Nodes.Count > 0)
            {
                if (!Properties.Settings.Default.bOptionsAutoRepair)
                {
                    if (MessageBox.Show(this, Properties.Resources.mainProblemsFix, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;
                }

                // Send usage data to Little Software Stats
                Watcher.Event("Functions", "Fix Problems");

                // Create Restore Point
                SysRestore.StartRestore("Before Little Registry Cleaner Registry Fix", out lSeqNum);

                // Generate filename to backup registry
                string strBackupFile = string.Format("{0}\\{1:yyyy}_{1:MM}_{1:dd}_{1:HH}{1:mm}{1:ss}.xml", Properties.Settings.Default.strOptionsBackupDir, DateTime.Now);

                BadRegKeyArray arrBadRegKeys = new BadRegKeyArray();
                foreach (BadRegistryKey badRegKeyRoot in this.treeModel.Nodes)
                {
                    foreach (BadRegistryKey badRegKey in badRegKeyRoot.Nodes)
                        if (badRegKey.Checked == CheckState.Checked)
                            arrBadRegKeys.Add(badRegKey);
                }

                // Generate a restore file and delete keys & values
                xmlReg.deleteAsXml(arrBadRegKeys, strBackupFile);

                SysRestore.EndRestore(lSeqNum);

                // Disable menu items
                this.fixToolStripMenuItem.Enabled = false;
                this.toolStripButtonFix.Enabled = false;

                // Clear status text
                ResourceManager rm = new ResourceManager(this.GetType());
                this.toolStripStatusLabel1.Text = rm.GetString("toolStripStatusLabel1.Text");
                this.toolStripStatusLabel1.Tag = "toolStripStatusLabel1.Text";

                // Display message box
                if (!Properties.Settings.Default.bOptionsAutoExit)
                    MessageBox.Show(this, Properties.Resources.mainProblemsRemoved, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear old results
                this.treeModel.Nodes.Clear();

                // If power user option selected, Automatically exit program
                if (Properties.Settings.Default.bOptionsAutoExit)
                {
                    this.bDisplayExitMsgBox = false;
                    this.Close();
                    return;
                }

                // Scan again
                if (Properties.Settings.Default.bOptionsRescan)
                    ScanRegistry();
            }
        }*/
    }
}
