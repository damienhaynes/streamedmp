using System;
using System.Collections.Generic;
using System.Globalization;
using MediaPortal.Configuration;
using MediaPortal.GUI.Library;
using MediaPortal.Util;

namespace StreamedMPConfig
{
  class settings
  {
    #region Public methods
 
    public static void Load()
    {
      Log.Info("StreamedMPConfig: Settings.Load()");
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "StreamedMPConfig.xml")))
      {
        // Read user settings from configuration file

        if (xmlreader.GetValueAsInt("StreamedMPConfig", "cdCoverOnly", 1) != 0)
          StreamedMPConfig.cdCoverOnly = true;
        if (xmlreader.GetValueAsInt("StreamedMPConfig", "showEqGraphic", 1) != 0)
          StreamedMPConfig.showEqGraphic = true;
        if (xmlreader.GetValueAsInt("StreamedMPConfig", "fullVideoOSD", 1) != 0)
          StreamedMPConfig.fullVideoOSD = true;
        if (xmlreader.GetValueAsInt("StreamedMPConfig", "checkOnStart", 1) != 0)
          StreamedMPConfig.checkOnStart = true;
        if (xmlreader.GetValueAsInt("StreamedMPConfig", "checkForUpdateAt", 1) != 0)
          StreamedMPConfig.checkForUpdateAt = true;

        if (StreamedMPConfig.checkForUpdateAt)
        {
          string checkTime = xmlreader.GetValueAsString("StreamedMPConfig", "checkTime", "");

          if (string.IsNullOrEmpty(checkTime))
          {
            StreamedMPConfig.checkTime = DateTime.Parse("03:00");
            StreamedMPConfig.checkInterval = 1;
          }
          else
          {
            DateTime dtCheckTime = new DateTime();
            if (!DateTime.TryParse(checkTime, CultureInfo.CurrentCulture, DateTimeStyles.None, out dtCheckTime))
              dtCheckTime = DateTime.Now;
            StreamedMPConfig.checkTime = dtCheckTime;
            
            StreamedMPConfig.nextUpdateCheck = xmlreader.GetValueAsString("StreamedMPConfig", "nextUpdateCheck", "");
            StreamedMPConfig.checkInterval = xmlreader.GetValueAsInt("StreamedMPConfig", "checkInterval", 1);
          }
        }
      }
    }

    public static void Save()
    {
      Log.Info("StreamedMP: Settings.Save()");
      using (MediaPortal.Profile.Settings xmlwriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "StreamedMPConfig.xml")))
      {
        xmlwriter.SetValue("StreamedMPConfig", "cdCoverOnly", StreamedMPConfig.cdCoverOnly ? 1 : 0);
        xmlwriter.SetValue("StreamedMPConfig", "showEqGraphic", StreamedMPConfig.showEqGraphic ? 1 : 0);
        xmlwriter.SetValue("StreamedMPConfig", "fullVideoOSD", StreamedMPConfig.fullVideoOSD ? 1 : 0);
        xmlwriter.SetValue("StreamedMPConfig", "checkOnStart", StreamedMPConfig.checkOnStart ? 1 : 0);
        xmlwriter.SetValue("StreamedMPConfig", "checkForUpdateAt", StreamedMPConfig.checkForUpdateAt ? 1 : 0);
        xmlwriter.SetValue("StreamedMPConfig", "checkInterval", StreamedMPConfig.checkInterval);
        xmlwriter.SetValue("StreamedMPConfig", "checkTime", StreamedMPConfig.checkTime.ToShortTimeString());
        xmlwriter.SetValue("StreamedMPConfig", "nextUpdateCheck", StreamedMPConfig.nextUpdateCheck);
        MediaPortal.Profile.Settings.SaveCache();
      }
    }

    #endregion
  }
}
