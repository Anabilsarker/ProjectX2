using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace Winzard_System_Repair.RegistryScanner
{
    public abstract class ScannerBase
    {
        /// <summary>
        /// Returns the scanner name
        /// </summary>
        abstract public string ScannerName
        {
            get;
        }

        /// <summary>
        /// The root node (used for scan dialog)
        /// </summary>
        public BadRegistryKey RootNode = new BadRegistryKey();

        /// <summary>
        /// Returns the scanner name
        /// </summary>
        public override string ToString()
        {
            return (string)ScannerName.Clone();
        }

        //public virtual void Scan()
        //{
        //    return;
        //}
 
    }
}
