using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;

// Installer stuff

// SCHTASKS.exe /Create /SC ONLOGON /TN Neo\GUI-MEU /TR "C:\Program Files\Neo Yuki Aylor\GUI-MEU\GUI-MEU.exe" /RL HIGHEST /IT /f

namespace GUI_MEU {
    public partial class gooeyMew : Window {

        // Check if Microsoft Edge is installed, if not, dont open Gooey Mew and silently exit.
        public gooeyMew() {
            if (File.Exists(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\msedge.exe", "Path", null).ToString() + @"\msedge.exe") == true) {
                InitializeComponent();  
            } else {
                Process.GetCurrentProcess().Kill();
            }
        }

        // Once no button is pressed, exit the program.
        private void edgeUninstallNoButton_Click(object sender, RoutedEventArgs e) {
            Process.GetCurrentProcess().Kill();
        }

        // Once yes button is pressed, begin the edge uninstallation process.
        private void edgeUninstallYesButton_Click(object sender, RoutedEventArgs e) {

            // Set the Microsoft Edge executable path and the flags needed for uninstallation.
            var MicrosoftEdgeExecutablePath = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\msedge.exe", "Path", null).ToString();
            var edgeUninstallFlags = "--uninstall --msedge --system-level --verbose-logging --force-uninstall";

            // Prevent Microsoft Edge from reinstalling via Eindows Updates
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\EdgeUpdate", "DoNotUpdateToEdgeWithChromium", 1, RegistryValueKind.DWord);

            // Unlock the Edge uninstallation process and determine setup.exe path for 64 bit machines
            if (System.Environment.Is64BitOperatingSystem == true) {

                var MicrosoftEdgeVersionNumber = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\EdgeUpdate\ClientState\{56EB18F8-B008-4CBD-B6D2-8C97FE7E9062}", "pv", null).ToString();
                var MicrosoftEdgeSetupPath = MicrosoftEdgeExecutablePath + "\\" + MicrosoftEdgeVersionNumber + @"\Installer\setup.exe";

                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\EdgeUpdateDev", "AllowUninstall", 1, RegistryValueKind.DWord);

                RegistryKey edgeUpdateClientStateRegKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\ClientState");
                using (RegistryKey edgeUpdateClientStateUUIDRegKey = edgeUpdateClientStateRegKey.OpenSubKey("{56EB18F8-B008-4CBD-B6D2-8C97FE7E9062}", true)) {
                    edgeUpdateClientStateUUIDRegKey.DeleteValue("experiment_control_labels", false);
                    edgeUpdateClientStateRegKey.Close();
                }

                RegistryKey edgeUninstallParentRegKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                using (RegistryKey edgeUninstallKey = edgeUninstallParentRegKey.OpenSubKey("Microsoft Edge", true)) {
                    edgeUninstallKey.DeleteValue("NoRemove", false);
                    edgeUninstallParentRegKey.Close();
                }

                // Finally, we get to uninstall Microsoft Edge

                Process uninstallEdge = new Process();

                uninstallEdge.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                uninstallEdge.StartInfo.FileName = MicrosoftEdgeSetupPath;
                uninstallEdge.StartInfo.Arguments = edgeUninstallFlags;
                uninstallEdge.StartInfo.UseShellExecute = true;
                uninstallEdge.StartInfo.Verb = "runas";

                uninstallEdge.Start();
                uninstallEdge.WaitForExit();

                MessageBox.Show("Microsoft Edge successfully uninstalled.\n\nYou may need to manually delete Microsoft Edge shortcut files.", "Succesfully Uninstalled!", MessageBoxButton.OK, MessageBoxImage.Information);
                Process.GetCurrentProcess().Kill();
            }

            // Unlock the Edge uninstallation process and determine setup.exe path for 32 bit machines.
            else {

                var MicrosoftEdgeVersionNumber = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\EdgeUpdate\ClientState\{56EB18F8-B008-4CBD-B6D2-8C97FE7E9062}", "pv", null).ToString();
                var MicrosoftEdgeSetupPath = MicrosoftEdgeExecutablePath + "\\" + MicrosoftEdgeVersionNumber + @"\Installer\setup.exe";

                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\EdgeUpdateDev", "AllowUninstall", 1, RegistryValueKind.DWord);

                RegistryKey edgeUpdateClientStateRegKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\EdgeUpdate\ClientState");
                using (RegistryKey edgeUpdateClientStateUUIDRegKey = edgeUpdateClientStateRegKey.OpenSubKey("{56EB18F8-B008-4CBD-B6D2-8C97FE7E9062}", true)) {
                    edgeUpdateClientStateUUIDRegKey.DeleteValue("experiment_control_labels", false);
                    edgeUpdateClientStateRegKey.Close();
                }

                RegistryKey edgeUninstallParentRegKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                using (RegistryKey edgeUninstallKey = edgeUninstallParentRegKey.OpenSubKey("Microsoft Edge", true)) {
                    edgeUninstallKey.DeleteValue("NoRemove", false);
                    edgeUninstallParentRegKey.Close();
                }

                // Finally, we get to uninstall Microsoft Edge

                Process uninstallEdge = new Process();

                uninstallEdge.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                uninstallEdge.StartInfo.FileName = MicrosoftEdgeSetupPath;
                uninstallEdge.StartInfo.Arguments = edgeUninstallFlags;
                uninstallEdge.StartInfo.UseShellExecute = true;
                uninstallEdge.StartInfo.Verb = "runas";

                uninstallEdge.Start();
                uninstallEdge.WaitForExit();

                MessageBox.Show("Microsoft Edge successfully uninstalled.\n\nYou may need to manually delete Microsoft Edge shortcut files.", "Succesfully Uninstalled!", MessageBoxButton.OK, MessageBoxImage.Information);
                Process.GetCurrentProcess().Kill();
            }
        }
    }
}
