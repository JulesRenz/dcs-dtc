using DTC.Models;
using DTC.Models.Base;
using DTC.Models.A10CII;
using DTC.UI.Base.GlobalHotKey;
using DTC.UI.CommonPages;
using System;

namespace DTC.UI.Aircrafts.A10CII
{
    public partial class UploadToJetPage : AircraftSettingPage
    {
        private A10CIIUpload _jetInterface;
        private readonly A10CIIConfiguration _cfg;

        private KeyboardHookManager _keyboardHookManager;

        public UploadToJetPage(AircraftPage parent, A10CIIConfiguration cfg) : base(parent)
        {
            InitializeComponent();
            _jetInterface = new A10CIIUpload(cfg);

            txtWaypointStart.LostFocus += TxtWaypointStart_LostFocus;
            txtWaypointEnd.LostFocus += TxtWaypointEnd_LostFocus;
            txtWaypointStart.Text = cfg.Waypoints.SteerpointStart.ToString();
            txtWaypointEnd.Text = cfg.Waypoints.SteerpointEnd.ToString();
            _cfg = cfg;

            chkWaypoints.Checked = _cfg.Waypoints.EnableUpload;


            CheckUploadButtonEnabled();

            _keyboardHookManager = new KeyboardHookManager();
            _keyboardHookManager.Start();

            HotKey hotkey;
            if (ParseHotKey.TryParse(Settings.UploadHotKey, out hotkey))
            {
                _keyboardHookManager.RegisterHotkey(hotkey.Modifiers, (int)hotkey.Key, () =>
                {
                    _jetInterface.Load();
                });
            }
        }

        private void CheckUploadButtonEnabled()
        {
            btnUpload.Enabled = (_cfg.Waypoints.EnableUpload);
        }

        public override string GetPageTitle()
        {
            return "Upload to Jet";
        }

        private void TxtWaypointEnd_LostFocus(object sender, EventArgs e)
        {
            if (int.TryParse(txtWaypointEnd.Text, out int n))
            {
                _cfg.Waypoints.SetSteerpointEnd(n);
                _parent.DataChangedCallback();
            }

            txtWaypointEnd.Text = _cfg.Waypoints.SteerpointEnd.ToString();
        }

        private void TxtWaypointStart_LostFocus(object sender, EventArgs e)
        {
            if (int.TryParse(txtWaypointStart.Text, out int n))
            {
                _cfg.Waypoints.SetSteerpointStart(n);
                _parent.DataChangedCallback();
            }

            txtWaypointStart.Text = _cfg.Waypoints.SteerpointStart.ToString();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            _jetInterface.Load();
        }

        private void chkWaypoints_CheckedChanged(object sender, EventArgs e)
        {
            txtWaypointStart.Enabled = chkWaypoints.Checked;
            txtWaypointEnd.Enabled = chkWaypoints.Checked;
            _cfg.Waypoints.EnableUpload = chkWaypoints.Checked;
            _parent.DataChangedCallback();
            CheckUploadButtonEnabled();
        }
    }
}
