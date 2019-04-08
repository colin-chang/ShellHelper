using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace ColinChang.ShellHelper
{
    public class ShellHelper
    {
        /// <summary>
        /// Execute OS script or script file like bat file on windows or shell file on linux.
        /// </summary>
        /// <param name="shell">script or script file</param>
        /// <param name="args">arguments</param>
        /// <returns>execute successfully or not</returns>
        public static void ExecuteCommand(string shell, string args = null)
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
                        Console.WriteLine(sr.ReadLine());

                    if (!proc.HasExited)
                        proc.Kill();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Permission denied"))
                    throw new NotSupportedException($"Try to run your command with 'sudo' prefix");

                throw new Exception($"An exception occured,here is the exception message : {ex.Message}");
            }
        }

        public static void ExecuteFile(string shell, string args = null)
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
                        Console.WriteLine(sr.ReadLine());

                    if (!proc.HasExited)
                        proc.Kill();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Permission denied"))
                    throw new NotSupportedException($"Try to fix this by execute 'sudo chmod +x {shell}'");

                throw new Exception($"An exception occured,here is the exception message : {ex.Message}");
            }
        }

        public static bool ExecuteFile(string shell, string args = null, bool useStatusCode = true)
        {
            if (!useStatusCode)
            {
                ExecuteFile(shell, args);
                return true;
            }

            var origin = File.ReadLines(shell);
            var newCmds = new List<string>();
            string comment, cmd, output;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                comment = "::";
                cmd = "set /a code=code+%errorlevel%";
                output = "echo %code%";
            }
            else
            {
                comment = "#";
                cmd = "code=$[code+$?]";
                output = "echo ${code}";
                newCmds.Add("#!/bin/bash");
            }

            for (var i = 0; i < origin.Count(); i++)
            {
                var line = origin.ElementAt(i).Trim();
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith(comment))
                    continue;

                newCmds.Add(line);
                newCmds.Add(cmd);
            }

            newCmds.Add(output);

            var newFile = $"{Guid.NewGuid()}{Path.GetExtension(shell)}";
            //copy file to make sure that you have the same permissions to execute it.
            File.Copy(shell,newFile);
            File.WriteAllLines(newFile, newCmds);

            var psi = new ProcessStartInfo(newFile, args) {RedirectStandardOutput = true};
            try
            {
                var proc = Process.Start(psi);
                if (proc == null)
                    throw new NotSupportedException($"Sorry,{shell} can not exec.");

                string reuslt = null;
                using (var sr = proc.StandardOutput)
                {
                    while (!sr.EndOfStream)
                    {
                        reuslt = sr.ReadLine();
                        Console.WriteLine(reuslt);
                    }

                    if (!proc.HasExited)
                        proc.Kill();
                }

                File.Delete(newFile);
                return Convert.ToInt32(reuslt) <= 0;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Permission denied"))
                    throw new NotSupportedException($"Try to fix this by execute 'sudo chmod +x {shell}'");

                throw new Exception($"An exception occured,here is the exception message : {ex.Message}");
            }
        }
    }
}