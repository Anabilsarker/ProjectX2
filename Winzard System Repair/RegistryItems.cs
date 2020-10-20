using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Winzard_System_Repair.RegistryScanner;

namespace Winzard_System_Repair
{
    public class RegistryItems
    {
        public static int RegistryProblemNumbers()
        {
            int regproblems = ScannerFunctions.arrBadRegistryKeys.Problems;
            return regproblems;
        }
    }
}
