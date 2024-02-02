using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace VeracryptGui
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string procName = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(procName);
            if (processes.Length > 1)
            {
                // Already running, activate the first instance
                Process firstProcess = Array.Find(processes, p => p.Id != Process.GetCurrentProcess().Id);
                IntPtr mainWindowHandle = firstProcess.MainWindowHandle;
                ShowWindowAsync(mainWindowHandle, SW_SHOWNORMAL);
                SetForegroundWindow(mainWindowHandle);
                Shutdown();
                return;
            }

            //// Only instance, start as normal
            //MainWindow = new MainWindow();
            //MainWindow.Show();
        }
    }
}