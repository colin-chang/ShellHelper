using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ColinChang.ShellHelper.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteScript();
            ExecuteScriptFile();
            
            Console.ReadKey();
        }

        static void ExecuteScript()
        {
            ShellHelper.Execute("dotnet", "--info");
        }

        static void ExecuteScriptFile()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ShellHelper.Execute("win.bat", "C:\\", true);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                     RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                ShellHelper.Execute("linux-mac.sh", "-lh", true);
            }
        }
    }
}