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
    SkinInfo skInfo = new SkinInfo();

    // Protected Variables
    // Public Variables
    #endregion

    #region Public methods

    public ConfigurationForm()
    {
      InitializeComponent();

      settings.Load(settings.cXMLSectionUpdate);
      settings.Load(settings.cXMLSectionMusic);
      settings.Load(settings.cXMLSectionVideo);
      settings.Load(settings.cXMLSectionMisc);

      releaseVersion.Text = String.Format("Version: {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
      DateTime buildDate = getLinkerTimeStamp(Assembly.GetExecutingAssembly().Location);
      compileTime.Text += " " + buildDate.ToString() + " GMT";

      timePicker = new DateTimePicker();
      timePicker.Format = DateTimePickerFormat.Time;
      timePicker.ShowUpDown = true;
      timePicker.Location = new Point(309, 85);
      timePicker.Width = 100;
      CheckUpdate.Controls.Add(timePicker);

      if (MiscConfigGUI.MostRecentFanartTimerInt != 0)
        numFanartTimer.Value = (decimal)MiscConfigGUI.MostRecentFanartTimerInt;
      else
        numFanartTimer.Value = 7;
    }

    #endregion

    #region Private methods

    // Save settings to file
    void btSave_Click(object sender, EventArgs e)
    {
      // get current settings and save to file
      MusicOptionsGUI.cdCoverOnly = cbCdCoverOnly.Checked;
      MusicOptionsGUI.showEqGraphic = cbShowEqGraphic.Checked;
      VideoOptionsGUI.FullVideoOSD = fullVideoOSD.Checked;
      StreamedMPConfig.checkOnStart = cbCheckOnStart.Checked;
      StreamedMPConfig.checkForUpdateAt = cbCheckForUpdateAt.Checked;
      StreamedMPConfig.checkInterval = comboCheckInterval.SelectedIndex;
      StreamedMPConfig.checkTime = timePicker.Value;
      StreamedMPConfig.nextUpdateCheck = StreamedMPConfig.nextCheckAt.ToString();
      StreamedMPConfig.patchUtilityRunUnattended = cbRunUnattended.Checked;
      StreamedMPConfig.patchUtilityRestartMP = cbRestartMP.Checked;
      MiscConfigGUI.MostRecentFanartTimerInt = (int)numFanartTimer.Value;
      
      settings.Save(settings.cXMLSectionUpdate);
      settings.Save(settings.cXMLSectionMusic);
      settings.Save(settings.cXMLSectionVideo);
      settings.Save(settings.cXMLSectionMisc);
      
      this.Close();
    }

    void btCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    // Load settings from xml
    void ConfigurationForm_Load(object sender, EventArgs e)
    { 
      cbCdCoverOnly.Text = Translation.Strings["CDCover"];
      cbShowEqGraphic.Text = Translation.Strings["ShowEQ"];

      cbCdCoverOnly.Checked = MusicOptionsGUI.cdCoverOnly;
      cbShowEqGraphic.Checked = MusicOptionsGUI.showEqGraphic;
      cbRunUnattended.Checked = StreamedMPConfig.patchUtilityRunUnattended;
      cbRestartMP.Checked = StreamedMPConfig.patchUtilityRestartMP;

      if (VideoOptionsGUI.FullVideoOSD)
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
      cbCheckForUpdateAt.Checked = StreamedMPConfig.checkForUpdateAt;
      if (StreamedMPConfig.checkForUpdateAt)
      {
        cbCheckForUpdateAt.Checked = StreamedMPConfig.checkForUpdateAt;
        comboCheckInterval.SelectedIndex = StreamedMPConfig.checkInterval;
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
      updateCheck.patchList.Clear();
      if (updateCheck.updateAvailable(true))
        updateFound.displayDetail(true);
      else
        MessageBox.Show(Translation.NoUpdatesAvailable);
    }

    #endregion

  }
}