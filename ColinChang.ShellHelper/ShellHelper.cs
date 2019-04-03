using System;
using System.Diagnostics;

namespace ColinChang.ShellHelper
{
    public class ShellHelper
    {
        /// <summary>
        /// Execute OS script or script file like bat file on windows or shell file on linux.
        /// </summary>
        /// <param name="shell">script or script file</param>
        /// <param name="args">arguments</param>
        /// <param name="isShellFile">is it a script file</param>
        /// <returns>execute successfully or not</returns>
        public static bool Execute(string shell, string args = null, bool isShellFile = false)
        {
            var psi = new ProcessStartInfo(shell, args) {RedirectStandardOutput = true};
            try
            {
                var proc = Process.Start(psi);
                if (proc == null)
                    throw new NotSupportedException($"Sorry,{shell} can not exec.");

                using (var sr = proc.StandardOutput)
                {
                    while (!sr.EndOfStream)
                    {
                        Console.WriteLine(sr.ReadLine());
                    }

                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Permission denied"))
                    throw new NotSupportedException(isShellFile
                        ? $"Try to fix this by execute 'sudo chmod +x {shell}'"
                        : $"Try to run your command with 'sudo' prefix");

                throw new Exception($"An exception occured,here is the exception message : {ex.Message}");
            }
        }
    }
}