using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MediaPortal.GUI.Library;
using MediaPortal.GUI.Weather;
using MediaPortal.Configuration;
using MediaPortal.Util;
using Rss;
using System.Text.RegularExpressions;

namespace StreamedMPConfig
{
    /// <summary>
    /// Process plugin that reeds news items from a live RSS Feed and provides it
    /// to the GUI to be used as a RSS Ticker.
    /// </summary>
    public class StreamedMPConfigPlugin : IPlugin, ISetupForm
    {
        public StreamedMPConfigPlugin()
        {
        }

        #region ISetupForm Members

        /// <summary>
        /// Returns the name of the plugin which is shown in the plugin menu
        /// </summary>
        /// <returns>the name of the plugin which is shown in the plugin menu</returns>
        public string PluginName()
        {
            return "StreamedMP Configuration";
        }

        /// <summary>
        /// Returns the description of the plugin which is shown in the plugin menu
        /// </summary>
        /// <returns>the description of the plugin which is shown in the plugin menu</returns>
        public string Description()
        {
            return "StreamedMP configuration control";
        }

        /// <summary>
        /// Returns the author of the plugin which is shown in the plugin menu
        /// </summary>
        /// <returns>the author of the plugin which is shown in the plugin menu</returns>
        public string Author()
        {
            return "trevor";
        }

        /// <summary>
        /// Indicates whether plugin can be enabled/disabled
        /// </summary>
        public void ShowPlugin()
        {
            ConfigurationForm configurationForm = new ConfigurationForm();
            configurationForm.ShowDialog();
        }

        /// <summary>
        /// Indicates whether plugin can be enabled/disabled
        /// </summary>
        /// <returns>true if the plugin can be enabled/disabled</returns>
        public bool CanEnable()
        {
            return true;
        }

        /// <summary>
        /// Get Windows-ID
        /// </summary>
        /// <returns>unique id for this plugin</returns>
        public int GetWindowId()
        {
            // WindowID of windowplugin belonging to this setup
            // enter your own unique code
            return -1;
        }

        /// <summary>
        /// Indicates if plugin is enabled by default
        /// </summary>
        /// <returns>true if this plugin is enabled by default</returns>
        public bool DefaultEnabled()
        {
            return true;
        }

        /// <summary>
        /// indicates if a plugin has its own setup screen
        /// </summary>
        /// <returns>true if the plugin has its own setup screen</returns>
        public bool HasSetup()
        {
            return true;
        }

        /// <summary>
        /// no Home for this plugin, return false
        /// </summary>
        /// <param name="strButtonText"></param>
        /// <param name="strButtonImage"></param>
        /// <param name="strButtonImageFocus"></param>
        /// <param name="strPictureImage"></param>
        /// <returns></returns>
        public bool GetHome(out string strButtonText, out string strButtonImage,
                            out string strButtonImageFocus, out string strPictureImage)
        {
            strButtonText = String.Empty;
            strButtonImage = String.Empty;
            strButtonImageFocus = String.Empty;
            strPictureImage = String.Empty;
            return false;
        }


        #endregion


        private bool cdCoverOnly = false;

        #region EventHandlers
        private void GUIWindowManager_OnActivateWindow(int windowID)
        {
            if (cdCoverOnly)
            {
                Log.Info("StreamedMPConfig: Setting control 1961001 to visible");
                GUIControl.ShowControl(GUIWindowManager.ActiveWindow, 1962001);
            }
            else
            {
                Log.Info("StreamedMPConfig: Setting control 1961001 to hidden");
                GUIControl.HideControl(GUIWindowManager.ActiveWindow, 1962001);
            }
        }
        #endregion



        public void Start()
        {
            Log.Info("StreamedMPConfig: Process started");

            GUIWindowManager.OnActivateWindow += new GUIWindowManager.WindowActivationHandler(GUIWindowManager_OnActivateWindow);

            using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "StreamedMPConfig.xml")))
            {
                // Read user settings from configuration file

                if (xmlreader.GetValueAsInt("StreamedMPConfig", "cdCoverOnly", 1) != 0)
                    cdCoverOnly = true;               
            }
        }

        public void Stop()
        {
            GUIWindowManager.OnActivateWindow -= new GUIWindowManager.WindowActivationHandler(GUIWindowManager_OnActivateWindow);
        }

    }
}
