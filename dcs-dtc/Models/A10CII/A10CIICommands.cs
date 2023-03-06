using System.Collections.Generic;
using DTC.Models.Base;
using DTC.Models.DCS;

namespace DTC.Models.A10CII
{
	public class A10CIICommands : IAircraftDeviceManager
	{
		private Dictionary<string, Device> Devices = new Dictionary<string, Device>();

		public A10CIICommands()
		{
			var delay = Settings.CommandDelayMs;

			var delayAlphaNum = delay / 4;
            var delayRadio = delay / 4;

			var cdu = new Device(9, "CDU");
            cdu.AddCommand(new Command(3000 + 1, "L1", delay, 1));
            cdu.AddCommand(new Command(3000 + 2, "L2", delay, 1));
            cdu.AddCommand(new Command(3000 + 3, "L3", delay, 1));
            cdu.AddCommand(new Command(3000 + 4, "L4", delay, 1));
            cdu.AddCommand(new Command(3000 + 5, "R1", delay, 1));
            cdu.AddCommand(new Command(3000 + 6, "R2", delay, 1));
            cdu.AddCommand(new Command(3000 + 7, "R3", delay, 1));
            cdu.AddCommand(new Command(3000 + 8, "R4", delay, 1));
            cdu.AddCommand(new Command(3000 + 9, "SYS", delay, 1));
            cdu.AddCommand(new Command(3000 + 10, "NAV", delay, 1));
            cdu.AddCommand(new Command(3000 + 11, "WP", delay, 1));
            cdu.AddCommand(new Command(3000 + 12, "OSET", delay, 1));
            cdu.AddCommand(new Command(3000 + 13, "FPM", delay, 1));
            cdu.AddCommand(new Command(3000 + 14, "PREV", delay, 1));
            cdu.AddCommand(new Command(3000 + 15, "1", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 16, "2", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 17, "3", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 18, "4", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 19, "5", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 20, "6", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 21, "7", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 22, "8", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 23, "9", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 24, "0", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 25, "PNT", delay, 1));
            cdu.AddCommand(new Command(3000 + 26, "SLASH", delay, 1));
            cdu.AddCommand(new Command(3000 + 27, "A", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 28, "B", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 29, "C", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 30, "D", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 31, "E", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 32, "F", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 33, "G", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 34, "H", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 35, "I", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 36, "J", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 37, "K", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 38, "L", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 39, "M", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 40, "N", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 41, "O", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 42, "P", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 43, "Q", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 44, "R", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 45, "S", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 46, "T", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 47, "U", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 48, "V", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 49, "W", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 50, "X", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 51, "Y", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 52, "Z", delayAlphaNum, 1));
            cdu.AddCommand(new Command(3000 + 53, "V1", delay, 1));
            cdu.AddCommand(new Command(3000 + 54, "V2", delay, 1));
            cdu.AddCommand(new Command(3000 + 55, "MK", delay, 1));
            cdu.AddCommand(new Command(3000 + 56, "BCK", delay, 1));
            cdu.AddCommand(new Command(3000 + 57, "SPC", delay, 1));
            cdu.AddCommand(new Command(3000 + 58, "CLR", delay, 1));
            cdu.AddCommand(new Command(3000 + 59, "FA", delay, 1));
            cdu.AddCommand(new Command(3000 + 60, "DIMBRT-L", delay, 1));
            cdu.AddCommand(new Command(3000 + 61, "DIMBRT-R", delay, 1));
            cdu.AddCommand(new Command(3000 + 62, "PG-UP", delay, 1));
            cdu.AddCommand(new Command(3000 + 63, "PG-DN", delay, 1));
            cdu.AddCommand(new Command(3000 + 64, "BLANC-L", delay, 1));
            cdu.AddCommand(new Command(3000 + 65, "BLANC-R", delay, 1));
            cdu.AddCommand(new Command(3000 + 66, "PLUS", delay, 1));
            cdu.AddCommand(new Command(3000 + 67, "MINUS", delay, 1));
            AddDevice(cdu);

            // todo: add radios
            //var uhf = new Device(54, "UHF");
            //cdu.AddCommand(new Command(3000 + 1, "CHANNEL", delayRadio, 1));
            //cdu.AddCommand(new Command(3000 + 2, "100MHZ", delayRadio, 1));
            //cdu.AddCommand(new Command(3000 + 3, "10MHZ", delayRadio, 1));
            //cdu.AddCommand(new Command(3000 + 4, "1MHZ", delayRadio, 1));
            //cdu.AddCommand(new Command(3000 + 5, "01MHZ", delayRadio, 1));
            //cdu.AddCommand(new Command(3000 + 6, "0025MHZ", delayRadio, 1));
            //cdu.AddCommand(new Command(3000 + 15, "LOAD", delayRadio, 1));
            //AddDevice(uhf);

            //var vhfAm = new Device(55, "VHFAM");
            //var vhfFm = new Device(56, "VHFFM");

        }

		private void AddDevice(Device d)
		{
			Devices.Add(d.Name, d);
		}

		public Device GetDevice(string id)
		{
			return Devices[id];
		}
	}
}