using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;
using System;

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

            // Prevent Microsoft Edge from reinstalling via Eindows Updates
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\EdgeUpdate", "DoNotUpdateToEdgeWithChromium", 1, RegistryValueKind.DWord);

            // Unlock the Edge uninstallation process for 64 bit machines
            if (System.Environment.Is64BitOperatingSystem == true) {

                var MicrosoftEdgeWebViewUninstallString = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Microsoft EdgeWebView", "UninstallString", null).ToString() + " --force-uninstall";
                var MicrosoftEdgeUninstallString = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Microsoft Edge", "UninstallString", null).ToString() + " --force-uninstall";

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

                RegistryKey edgeWebViewUninstallParentRegKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                using (RegistryKey edgeWebViewUninstallKey = edgeWebViewUninstallParentRegKey.OpenSubKey("Microsoft EdgeWebView", true)) {
                    edgeWebViewUninstallKey.DeleteValue("NoRemove", false);
                    edgeWebViewUninstallParentRegKey.Close();
                }

                // Finally, we get to uninstall Microsoft Edge

                Process uninstallEdge = new Process();

                uninstallEdge.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                uninstallEdge.StartInfo.FileName = "cmd.exe";
                uninstallEdge.StartInfo.Arguments = "/c" + MicrosoftEdgeUninstallString;
                uninstallEdge.StartInfo.UseShellExecute = true;
                uninstallEdge.StartInfo.CreateNoWindow = true;
                uninstallEdge.StartInfo.Verb = "runas";

                uninstallEdge.Start();
                uninstallEdge.WaitForExit();

                try {
                    Process uninstallEdgeWebView = new Process();

                    uninstallEdgeWebView.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    uninstallEdgeWebView.StartInfo.FileName = "cmd.exe";
                    uninstallEdgeWebView.StartInfo.Arguments = "/c " + MicrosoftEdgeWebViewUninstallString;
                    uninstallEdgeWebView.StartInfo.UseShellExecute = true;
                    uninstallEdgeWebView.StartInfo.CreateNoWindow = true;
                    uninstallEdgeWebView.StartInfo.Verb = "runas";

                    uninstallEdgeWebView.Start();
                    uninstallEdgeWebView.WaitForExit();
                }
                catch {

                }
            }

            // Unlock the Edge uninstallation process for 32 bit machines.
            else {

                var MicrosoftEdgeWebViewUninstallString = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Microsoft EdgeWebView", "UninstallString", null).ToString() + " --force-uninstall";
                var MicrosoftEdgeUninstallString = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Microsoft Edge", "UninstallString", null).ToString() + " --force-uninstall";

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

                RegistryKey edgeWebViewUninstallParentRegKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                using (RegistryKey edgeWebViewUninstallKey = edgeWebViewUninstallParentRegKey.OpenSubKey("Microsoft EdgeWebView", true)) {
                    edgeWebViewUninstallKey.DeleteValue("NoRemove", false);
                    edgeWebViewUninstallParentRegKey.Close();
                }

                // Finally, we get to uninstall Microsoft Edge

                Process uninstallEdge = new Process();

                uninstallEdge.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                uninstallEdge.StartInfo.FileName = "cmd.exe";
                uninstallEdge.StartInfo.Arguments = "/c" + MicrosoftEdgeUninstallString;
                uninstallEdge.StartInfo.UseShellExecute = true;
                uninstallEdge.StartInfo.Verb = "runas";

                uninstallEdge.Start();
                uninstallEdge.WaitForExit();

                try {
                    Process uninstallEdgeWebView = new Process();

                    uninstallEdgeWebView.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    uninstallEdgeWebView.StartInfo.FileName = "cmd.exe";
                    uninstallEdgeWebView.StartInfo.Arguments = "/c " + MicrosoftEdgeWebViewUninstallString;
                    uninstallEdgeWebView.StartInfo.UseShellExecute = true;
                    uninstallEdgeWebView.StartInfo.CreateNoWindow = true;
                    uninstallEdgeWebView.StartInfo.Verb = "runas";

                    uninstallEdgeWebView.Start();
                    uninstallEdgeWebView.WaitForExit();
                } catch {

                }
            }

            // Remove some residual files that Microsoft Edge creates
            Directory.Delete(Environment.ExpandEnvironmentVariables("%localappdata%") + @"\Microsoft\Edge", true);
            File.Delete(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Microsoft Edge.lnk");

            MessageBox.Show("Microsoft Edge successfully uninstalled.\n\nYou may need to manually delete Microsoft Edge shortcut files.", "Succesfully Uninstalled!", MessageBoxButton.OK, MessageBoxImage.Information);
            Process.GetCurrentProcess().Kill();
        }
    }
}
