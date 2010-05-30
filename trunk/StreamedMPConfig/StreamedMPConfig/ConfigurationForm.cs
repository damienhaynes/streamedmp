using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;


namespace StreamedMPConfig
{
  partial class ConfigurationForm : Form
  {
    #region Variables
    // Private Variables
    private DateTimePicker timePicker;

    // Protected Variables
    // Public Variables
    #endregion

    #region Public methods

    public ConfigurationForm()
    {
      InitializeComponent();
      SkinInfo.GetMediaPortalSkinPath();

      releaseVersion.Text = String.Format("Version: {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
      DateTime buildDate = getLinkerTimeStamp(System.Reflection.Assembly.GetEntryAssembly().Location.ToString());
      compileTime.Text += " " + buildDate.ToString() + " GMT";

      timePicker = new DateTimePicker();
      timePicker.Format = DateTimePickerFormat.Time;
      timePicker.ShowUpDown = true;
      timePicker.Location = new Point(309, 123);
      timePicker.Width = 100;
      CheckUpdate.Controls.Add(timePicker);
    }

    #endregion

    #region Private methods

    // Save settings to file
    void btSave_Click(object sender, EventArgs e)
    {
      StreamedMPConfig.cdCoverOnly = cbCdCoverOnly.Checked;
      StreamedMPConfig.showEqGraphic = cbShowEqGraphic.Checked;
      StreamedMPConfig.fullVideoOSD = fullVideoOSD.Checked;
      StreamedMPConfig.checkOnStart = cbCheckOnStart.Checked;
      StreamedMPConfig.checkForUpdate = cbCheckForUpdate.Checked;
      StreamedMPConfig.checkInterval = comboCheckInterval.Text;
      StreamedMPConfig.checkTime = timePicker.Value;
      settings.Save();
      this.Close();
    }

    void btCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    // Load settings from xml
    void ConfigurationForm_Load(object sender, EventArgs e)
    {
      settings.Load();
      cbCdCoverOnly.Text = Translation.Strings["CDCover"];
      cbShowEqGraphic.Text = Translation.Strings["ShowEQ"];

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
      cbCheckOnStart.Checked = StreamedMPConfig.checkOnStart;
      cbCheckForUpdate.Checked = StreamedMPConfig.checkForUpdate;
      if (StreamedMPConfig.checkForUpdate)
      {
        cbCheckForUpdate.Checked = StreamedMPConfig.checkForUpdate;
        comboCheckInterval.Text = StreamedMPConfig.checkInterval;
        timePicker.Value = StreamedMPConfig.checkTime;
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

    void btCheckForUpdate_Click(object sender, EventArgs e)
    {
      if (updateCheck.updateAvailable())
        updateFound.displayDetail();
    }

    #endregion
  }
}