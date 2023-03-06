using DTC.Models.DCS;
using DTC.Models.A10CII.Waypoints;
using System.Text;
using System.Drawing;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System;

namespace DTC.Models.A10CII.Upload
{
    public class WaypointBuilder : BaseBuilder
    {
        private A10CIIConfiguration _cfg;

        public WaypointBuilder(A10CIIConfiguration cfg, IAircraftDeviceManager aircraft, StringBuilder sb) : base(aircraft, sb)
        {
            _cfg = cfg;
        }

        public override void Build()
        {
            var wpts = _cfg.Waypoints.Waypoints;
            var wptStart = _cfg.Waypoints.SteerpointStart;
            var wptEnd = _cfg.Waypoints.SteerpointEnd;

            if (wpts.Count == 0)
            {
                return;
            }

            var wptDiff = wptEnd - wptStart + 1;

            var cdu = _aircraft.GetDevice("CDU");

            AppendCommand(cdu.GetCommand("WP"));
            AppendCommand(cdu.GetCommand("R1"));
            AppendCommand(cdu.GetCommand("CLR"));

            for (var i = 0; i < wpts.Count; i++)
            {
                // we need to make sure, that there are enough waypoints created
                // if there are waypoints already set up, this will cause excess waypoints, that are unused
                AppendCommand(cdu.GetCommand("R3"));
            }

            for (var i = 0; i < wptDiff; i++)
            {
                Waypoint wpt; ;
                if (i < wpts.Count)
                {
                    wpt = wpts[i];
                }
                else
                {
                    //Repeats the last waypoint till it fills
                    var lastWpt = wpts[wpts.Count - 1];

                    //wpts cannot have the same name, so we add a incrementing suffix to all "fill" waypoints
                    var wptSuffix = (i - wpts.Count + 1).ToString();
                    wpt = new Waypoint(lastWpt.Sequence, lastWpt.Name + wptSuffix, lastWpt.Latitude, lastWpt.Longitude, lastWpt.Elevation);
                }

                if (wpt.Blank)
                {
                    continue;
                }

                AppendCommand(BuildDigits(cdu, (i + wptStart).ToString()));
                AppendCommand(cdu.GetCommand("L1"));

                AppendCommand(BuildCoordinate(cdu, wpt.Latitude));
                AppendCommand(cdu.GetCommand("L3"));

                AppendCommand(BuildCoordinate(cdu, wpt.Longitude));
                AppendCommand(cdu.GetCommand("L4"));

                AppendCommand(BuildDigits(cdu, wpt.Elevation.ToString()));
                AppendCommand(cdu.GetCommand("L2"));

                if (!string.IsNullOrEmpty(wpt.Name))
                {
                    AppendCommand(BuildWaypointName(cdu, wpt.Name));
                    AppendCommand(cdu.GetCommand("R1"));
                }
            }
        }

        private string BuildCoordinate(Device cdu, string coord)
        {
            var sb = new StringBuilder();

            var latStr = RemoveSeparators(coord.Replace(" ", ""));

            foreach (var c in latStr.ToCharArray())
            {
                sb.Append(cdu.GetCommand(c.ToString()));
            }

            return sb.ToString();
        }

        private string BuildWaypointName(Device cdu, string str)
        {

            var sb = new StringBuilder();
            var upperStr = str.ToUpper();

            // From Manual: Maximum of 12 alphanumeric characters;
            var cleanedStr = upperStr.Length > 12 ? upperStr.Substring(0, 12) : upperStr;

            // From Manual: first character must be a letter.
            if (!Char.IsLetter(cleanedStr[0])) cleanedStr = "." + cleanedStr.Substring(1);

            // From Manual: No special characters other than numbers or letters allowed in name except a period(“.”).
            // However: space seems to work as well
            cleanedStr = Regex.Replace(cleanedStr, @"[^A-Z0-9\s\.]+", ".");


            foreach (var c in cleanedStr.ToCharArray())
            {
                string commandStr = c.ToString();

                if (commandStr == " ") commandStr = "SPC";
                else if (commandStr == ".") commandStr = "PNT";

                sb.Append(cdu.GetCommand(commandStr));
            }

            return sb.ToString();
        }
    }
}
