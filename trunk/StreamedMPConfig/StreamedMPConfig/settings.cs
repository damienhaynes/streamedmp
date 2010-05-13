using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;
using MediaPortal.Configuration;
using MediaPortal.Dialogs;
using MediaPortal.GUI.Library;
using MediaPortal.Player;
using MediaPortal.Util;

namespace StreamedMPConfig
{
  internal static class settings
  {

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

      }
    }
  }
}
