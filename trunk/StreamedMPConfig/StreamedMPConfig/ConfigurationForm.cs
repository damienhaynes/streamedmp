using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MediaPortal.Configuration;

namespace StreamedMPConfig
{
    public partial class ConfigurationForm : Form
    {
        public ConfigurationForm()
        {
            InitializeComponent();
        }

        // Save settings to file
        private void btSave_Click(object sender, EventArgs e)
        {
            using (MediaPortal.Profile.Settings xmlwriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "StreamedMPConfig.xml")))
            {
                xmlwriter.SetValue("StreamedMPConfig", "cdCoverOnly", cbCdCoverOnly.Checked ? 1 : 0);
                xmlwriter.SetValue("StreamedMPConfig", "showEqGraphic", cbShowEqGraphic.Checked ? 1 : 0);

            }
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "StreamedMPConfig.xml")))
            {
                cbCdCoverOnly.Checked = true;
                if (xmlreader.GetValueAsInt("StreamedMPConfig", "cdCoverOnly", 1) != 1)
                    cbCdCoverOnly.Checked = false;
                cbShowEqGraphic.Checked = true;
                if (xmlreader.GetValueAsInt("StreamedMPConfig", "showEqGraphic", 1) != 1)
                  cbShowEqGraphic.Checked = false;
            }
        }
    }
}