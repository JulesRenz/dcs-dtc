using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using DTC.Models.A10CII;
using DTC.Models.Base;
using System.Windows.Forms;
using DTC.Models.A10CII.Upload;
using System.Linq;

namespace DTC.Models
{
	public class A10CIIUpload
    {
		private int tcpPort = 42070;

		private A10CIIConfiguration _cfg;
		private A10CIICommands a10cii = new A10CIICommands();

		public A10CIIUpload(A10CIIConfiguration cfg)
		{
			tcpPort = Settings.TCPSendPort;

			_cfg = cfg;
		}
        	internal A10CIIConfiguration Cfg => _cfg;

        	public void Load()
		{
			var sb = new StringBuilder();

			if (_cfg.Waypoints.EnableUpload)
			{
				var waypointBuilder = new WaypointBuilder(_cfg, a10cii, sb);
				waypointBuilder.Build();
			}

			if (sb.Length > 0)
			{
				sb.Remove(sb.Length - 1, 1);
			}

			var str = sb.ToString();

            if (str != "")
            {
                try
                {
                    using (var tcpClient = new TcpClient("127.0.0.1", tcpPort))
                    using (var ns = tcpClient.GetStream())
                    using (var sw = new StreamWriter(ns))
                    {
                        var data = "[" + str + "]";
                        Console.WriteLine(data);

                        sw.WriteLine(data);
                        sw.Flush();
                    }
                }
                catch (SocketException e)
                {
                    MessageBox.Show("Error:" + e.ToString(), "Connection error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
	}
}
