using Microsoft.Win32;
using System;
using System.Diagnostics;

internal static class WebBrowserIE11Fix
{
    public static void SetIE11KeyforWebBrowserControl(string processPath = null)
    {
        if (processPath == null)
        {
            processPath = Process.GetCurrentProcess().ProcessName + ".exe";
        }

        RegistryKey Regkey = null;
        try
        {
            //Environment.Is64BitOperatingSystem
            Regkey = Registry.CurrentUser.OpenSubKey(false
                ? @"SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION"
                : @"SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);

            if (Regkey == null)
            {
                Debug.WriteLine("Could not open registry key to set IE11 for browser control");
                return;
            }

            // see https://msdn.microsoft.com/en-us/library/ee330730(VS.85).aspx#browser_emulation
            int? emulationCode = (int?)Regkey.GetValue(processPath);

            const int ie11Code = 11000;

            if (emulationCode == null || emulationCode != ie11Code)
                Debug.WriteLine("Key already set to a different value ({0}), overwriting.", emulationCode);

            Regkey.SetValue(processPath, ie11Code, RegistryValueKind.DWord);
        }
        finally
        {
            Regkey?.Close();
        }
    }
}