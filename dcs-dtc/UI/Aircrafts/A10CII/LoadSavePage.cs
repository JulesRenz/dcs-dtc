﻿using DTC.Models.A10CII;
using DTC.Models.Base;
using DTC.UI.CommonPages;
using System;
using System.Windows.Forms;

namespace DTC.UI.Aircrafts.A10CII
{
    public partial class LoadSavePage : AircraftSettingPage
    {
        private A10CIIConfiguration _mainConfig;
        private A10CIIConfiguration _configToLoad;

        public delegate void RefreshCallback();

        public LoadSavePage(AircraftPage parent, A10CIIConfiguration cfg) : base(parent)
        {
            _mainConfig = cfg;
            InitializeComponent();
        }

        public override string GetPageTitle()
        {
            return "Load / Save";
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (optClipboard.Checked)
            {
                var txt = Clipboard.GetText();
                _configToLoad = A10CIIConfiguration.FromCompressedString(txt);
            }
            else
            {
                if (openFileDlg.ShowDialog() == DialogResult.OK)
                {
                    var file = FileStorage.LoadFile(openFileDlg.FileName);
                    _configToLoad = A10CIIConfiguration.FromJson(file);
                }
            }

            DisableLoadControls();

            var enableLoad = false;

            if (_configToLoad != null)
            {
                if (_configToLoad.Waypoints != null)
                {
                    chkLoadWaypoints.Enabled = true;
                    enableLoad = true;
                }

                if (enableLoad == true)
                {
                    btnLoadApply.Enabled = true;
                }
            }
        }

        private void btnLoadApply_Click(object sender, EventArgs e)
        {
            var load = false;

            var cfg = _configToLoad.Clone();

            if (!chkLoadWaypoints.Checked)
            {
                cfg.Waypoints = null;
            }
            else
            {
                load = true;
            }

            if (load)
            {
                _mainConfig.CopyConfiguration(cfg);
                _parent.RefreshPages();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var cfg = _mainConfig.Clone();

            if (!chkSaveWaypoints.Checked)
            {
                cfg.Waypoints = null;
            }

            if (optClipboard.Checked)
            {
                Clipboard.SetText(cfg.ToCompressedString());
            }
            else
            {
                if (saveFileDlg.ShowDialog() == DialogResult.OK)
                {
                    FileStorage.Save(cfg, saveFileDlg.FileName);
                }
            }
        }

        private void DisableLoadControls()
        {
            chkLoadWaypoints.Enabled = false;
            btnLoadApply.Enabled = false;
        }

        private void optFile_CheckedChanged(object sender, EventArgs e)
        {
            _configToLoad = null;
            grpLoad.Text = "Load from File";
            grpSave.Text = "Save to File";
            grpLoad.Visible = true;
            grpSave.Visible = true;
            DisableLoadControls();
        }

        private void optClipboard_CheckedChanged(object sender, EventArgs e)
        {
            _configToLoad = null;
            grpLoad.Text = "Load from Clipboard";
            grpSave.Text = "Save to Clipboard";
            grpLoad.Visible = true;
            grpSave.Visible = true;
            DisableLoadControls();
        }
    }
}
