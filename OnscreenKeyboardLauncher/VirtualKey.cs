using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnscreenKeyboardLauncher
{
    public class VirtualKey
    {
        [System.Runtime.InteropServices.DllImport("Kernel32.Dll", EntryPoint = "Wow64EnableWow64FsRedirection")]
        public static extern bool EnableWow64FSRedirection(bool enable);

        public void OpenKeyboard()
        {
            EnableWow64FSRedirection(false);
            var path = Environment.GetFolderPath(Environment.SpecialFolder.System);
            var startupInfo = new ProcessStartInfo()
            {
                FileName = Path.Combine(path, "osk.exe"),
                Arguments = "",
                UseShellExecute = true
            };

            Process.Start(startupInfo);
            EnableWow64FSRedirection(true);
        }

        public void CloseKeyboard()
        {
            var processes = Process.GetProcessesByName("osk");
            foreach (var process in processes)
            {
                process.Kill();
            }
        }
    }
}
