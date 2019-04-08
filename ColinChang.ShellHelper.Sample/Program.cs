using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ColinChang.ShellHelper.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
//            ExecuteCommand();
            Console.WriteLine(ExecuteScriptFile());

            Console.ReadKey();
        }

        static void ExecuteCommand()
        {
            ShellHelper.ExecuteCommand("dotnet", "--info");
        }

        static bool ExecuteScriptFile()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? ShellHelper.ExecuteFile("win.bat", "C:\\", true)
                : ShellHelper.ExecuteFile("linux-mac.sh", "-lh", true);
        }
    }
}