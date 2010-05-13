using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MediaPortal.Configuration;
using System.Diagnostics;
using System.Reflection;

namespace StreamedMPConfig
{
  public partial class ConfigurationForm : Form
  {
    public ConfigurationForm()
    {
      InitializeComponent();

      releaseVersion.Text = String.Format("Version: {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
      DateTime buildDate = getLinkerTimeStamp(System.Reflection.Assembly.GetEntryAssembly().Location.ToString());
      compileTime.Text += " " + buildDate.ToString() + " GMT";

    }

    // Save settings to file
    private void btSave_Click(object sender, EventArgs e)
    {
      StreamedMPConfig.cdCoverOnly = cbCdCoverOnly.Checked;
      StreamedMPConfig.showEqGraphic = cbShowEqGraphic.Checked;
      StreamedMPConfig.fullVideoOSD = fullVideoOSD.Checked;
      settings.Save();
      this.Close();
    }

    private void btCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    // Load settings from xml
    private void ConfigurationForm_Load(object sender, EventArgs e)
    {
      settings.Load();
      cbCdCoverOnly.Checked = StreamedMPConfig.cdCoverOnly;
      cbShowEqGraphic.Checked = StreamedMPConfig.showEqGraphic;

      if (StreamedMPConfig.fullVideoOSD)
      {
        fullVideoOSD.Checked = true;
        minVideoOSD.Checked = false;
      }
      else
      {
        fullVideoOSD.Checked = false;
        minVideoOSD.Checked = true;
      }
    }

    private static DateTime getLinkerTimeStamp(string filePath)
    {
      const int PeHeaderOffset = 60;
      const int LinkerTimestampOffset = 8;

      byte[] b = new byte[2047];
      using (System.IO.Stream s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
      {
        s.Read(b, 0, 2047);
      }
      int secondsSince1970 = BitConverter.ToInt32(b, BitConverter.ToInt32(b, PeHeaderOffset) + LinkerTimestampOffset);
      return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(secondsSince1970);
    }
  }
}