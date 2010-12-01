using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StreamedMPConfig
{
  class PostPatchUpdate
  {
    public static void UpdateSettings()
    {
      // update references.xml with user settings
      MiscConfigGUI.SetColors();
      MiscConfigGUI.SetAlphaTransparency();

      // Set Skin Imports
      TVSeriesConfig.ApplyConfigurationChanges();
      MovingPicturesConfig.ApplyConfigurationChanges();
      TVConfig.SetTVGuideSize();
      TVConfig.SetRandomFanartProperties();
      MusicOptionsGUI.SetMusicNowPlayingStyle();
    }

  }
}
