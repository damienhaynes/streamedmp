using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Diagnostics;
using MediaPortal.Configuration;

namespace StreamedMPEditor
{
  public partial class formStreamedMpEditor
  {
    private void GetMediaPortalSkinPath()
    {
      infoserviceOptions.Enabled = false;
      enableFiveDayWeather.Enabled = false;
      summaryWeatherCheckBox.Enabled = false;

      readMediaPortalDirs();

      // Check in Infoservice is enabled
      if (!helper.pluginEnabled(Helper.Plugins.InfoService))
      {
        infoserviceOptions.Enabled = false;
        infoserviceOptions.Text = "InfoService Options(Disabled)";
      }
      else
      {
        infoserviceOptions.Enabled = true;
        enableFiveDayWeather.Enabled = true;
        summaryWeatherCheckBox.Enabled = true;
        useInfoServiceSeparator = true;
        infoserviceOptions.Text = "InfoService Options";
      }

      // Checkl if Fanart Handler is Enabled
      if (helper.pluginEnabled(Helper.Plugins.FanartHandler))
        cbItemFanartHandlerEnable.Visible = true;
      else
        cbItemFanartHandlerEnable.Visible = false;

      // Display some Info
      infoSkinName.Text = configuredSkin + " (" + getStreamedMPVer() + ")";
      infoSkinpath.Text = SkinInfo.mpPaths.streamedMPpath;
      infoInstallPath.Text = SkinInfo.mpPaths.sMPbaseDir + "  (Version: " + MediaPortalVersion + ")";
      infoConfigpath.Text = SkinInfo.mpPaths.configBasePath;


    }

    private string getStreamedMPVer()
    {
      XmlDocument doc = new XmlDocument();
      if (!File.Exists(SkinInfo.mpPaths.streamedMPpath + "streamedmp.version.xml"))
      {
        helper.showError("Unable to find StreamedMP version information in the following path:\r\r" + SkinInfo.mpPaths.streamedMPpath + "streamedmp.version.xml\r\rIt seems you have another skin selected in Mediaportal configuration.\rThis editor is for StreamedMP skin.\rGo to General->Skin and select StreamedMP to be able to open this editor.", errorCode.major);
        return null;
      }

      doc.Load(SkinInfo.mpPaths.streamedMPpath + "streamedmp.version.xml");
      XmlNode node = doc.DocumentElement.SelectSingleNode("/window/controls/control/label");
      return node.InnerText;
    }

    private void readMediaPortalDirs()
    {
      // Check if user MediaPortalDirs.xml exists in Personal Directory
      string PersonalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      string fMPdirs = Path.Combine(PersonalFolder, @"Team MediaPortal\MediaPortalDirs.xml");

      if (!File.Exists(fMPdirs))
        fMPdirs = SkinInfo.mpPaths.sMPbaseDir + "\\MediaPortalDirs.xml";

      XmlDocument doc = new XmlDocument();
      if (!File.Exists(fMPdirs))
      {
        helper.showError("Can't find MediaPortalDirs.xml \r\r" + fMPdirs, errorCode.major);
        return;
      }

      doc.Load(fMPdirs);
      XmlNodeList nodeList = doc.DocumentElement.SelectNodes("/Config/Dir");

      foreach (XmlNode node in nodeList)
      {

        XmlNode innerNode = node.Attributes.GetNamedItem("id");

        // get the Skin base path
        if (innerNode.InnerText == "Skin")
        {
          XmlNode path = node.SelectSingleNode("Path");
          if (path != null)
          {
            SkinInfo.mpPaths.skinBasePath = GetMediaPortalDir(path.InnerText);
          }
        }

        // get the Cache base path
        if (innerNode.InnerText == "Cache")
        {
          XmlNode path = node.SelectSingleNode("Path");
          if (path != null)
          {
            SkinInfo.mpPaths.cacheBasePath = GetMediaPortalDir(path.InnerText);
          }
        }

        // get the Config base path
        if (innerNode.InnerText == "Config")
        {
          XmlNode path = node.SelectSingleNode("Path");
          if (path != null)
          {
            SkinInfo.mpPaths.configBasePath = GetMediaPortalDir(path.InnerText);            
          }
        }

        // get the Plugin base path
        if (innerNode.InnerText == "Plugins")
        {
          XmlNode path = node.SelectSingleNode("Path");
          if (path != null)
          {
            SkinInfo.mpPaths.pluginPath = GetMediaPortalDir(path.InnerText);
          }
        }
        // get the Thumbs base path
        if (innerNode.InnerText == "Thumbs")
        {
          XmlNode path = node.SelectSingleNode("Path");
          if (path != null)
          {
            SkinInfo.mpPaths.thumbsPath = GetMediaPortalDir(path.InnerText);
            SkinInfo.mpPaths.fanartBasePath = SkinInfo.mpPaths.thumbsPath + "Skin Fanart\\";
          }
        }
      }
      SkinInfo.mpPaths.streamedMPpath = SkinInfo.mpPaths.skinBasePath + configuredSkin + "\\";
    }

    private string GetMediaPortalDir(string path)
    {
      string CommonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
      string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

      // Replace special folder variables if they exist
      // MediaPortal currently only uses two types
      path = path.ToLower();
      path = path.Replace("%appdata%", AppData);
      path = path.Replace("%programdata%", CommonAppData);

      // Check if the path is not rooted ie. a custom directory (including UNC)
      // If directory is relative e.g. 'skin\', prefix with Base Dir
      if (!Path.IsPathRooted(path))
      {
        path = Path.Combine(SkinInfo.mpPaths.sMPbaseDir, path);
      }

      // Check if there is a trailing backslash
      if (!path.EndsWith(@"\"))
      {
        path += @"\";
      }

      return path;
    }


    private string configuredSkin
    {
      get
      {
        return helper.readMPConfiguration("skin", "name");
      }
    }

    private void GetMediaPortalPath(ref editorPaths mpPaths)
    {
      string sRegRoot = "SOFTWARE";


      if (IntPtr.Size > 4)
        sRegRoot += "\\Wow6432Node";

      try
      {

        RegistryKey MediaPortalKey = Registry.LocalMachine.OpenSubKey(sRegRoot + "\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\MediaPortal\\", false);
        if (MediaPortalKey != null)
        {
          mpPaths.sMPbaseDir = MediaPortalKey.GetValue("InstallPath").ToString();

        }
        else
        {
          MediaPortalKey = MediaPortalKey.OpenSubKey(sRegRoot + "\\Team MediaPortal\\MediaPortal\\", false);
          if (MediaPortalKey != null)
          {
            mpPaths.sMPbaseDir = MediaPortalKey.GetValue("ApplicationDir").ToString();


          }
          else
            mpPaths.sMPbaseDir = null;
        }
      }
      catch (Exception e)
      {
        helper.showError("Exception while attempting to read MediaPortal location from registry\n\nMediaPortal must be installed, is MediaPortal Installed?\n\n" + e.Message, errorCode.major);
        mpPaths.sMPbaseDir = null;
      }



    }

    private void xmlFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (xmlFiles.SelectedIndex >= 0)
      {
        toolStripStatusLabel1.Text = "Window ID: " + ids[xmlFiles.SelectedIndex];

        // Populate / Clear items bases on selection
        int i = 0;
        bool bFound = false;
        string selectedID = ids[xmlFiles.SelectedIndex];
        string xmlFileName = xmlFiles.SelectedItem.ToString();
        selectedWindowID.Text = selectedID;
        selectedWindow.Text = xmlFileName;
        foreach (prettyItem p in prettyItems)
        {
          if (p.id == selectedID && xmlFileName.ToLower() == p.xmlfile.ToLower())
          {
            // Populate
            QuickSelect(i);
            cboQuickSelect.SelectedIndex = i;

            bFound = true;
            int k = 0;
            foreach (menuItem mnuItem in menuItems)
            {
              if (p.id == mnuItem.hyperlink)
              {
                if (itemsOnMenubar.Items.Count > 0 && itemsOnMenubar.Items.Count <= k)
                  itemsOnMenubar.SelectedIndex = k;
                break;
              }
              k++;
            }
            break;
          }
          i++;
        }


        if (!bFound)
        {
          // Clear Items
          ClearItems();
        }
      }
    }

 
    private void QuickSelect(int index)
    {
      xmlFiles.SelectedItem = prettyItems[index].xmlfile;
      cboContextLabel.Text = prettyItems[index].contextlabel;
      tbItemName.Text = prettyItems[index].name;
      bgBox.Text = prettyItems[index].folder;
      cboFanartProperty.Text = prettyItems[index].fanartProperty;
      cbItemFanartHandlerEnable.Checked = prettyItems[index].fanartHandlerEnabled;
      isWeather.Checked = prettyItems[index].isweather;
      selectedWindow.Text = prettyItems[index].xmlfile;
      selectedWindowID.Text = prettyItems[index].id;
      buttonTexture.MenuItem = prettyItems[index].name;
      if (pluginTakesParameter(selectedWindowID.Text))
      {
        cboParameterViews.Visible = true;
        lbParameterView.Visible = true;
        if (selectedWindowID.Text == onlineVideosSkinID)
          cbOnlineVideosReturn.Visible = true;
      }
      else
      {
        cboParameterViews.Visible = false;
        lbParameterView.Visible = false;
        cbOnlineVideosReturn.Visible = false;
      }
      switch (selectedWindowID.Text)
      {
        case tvseriesSkinID:
          cboParameterViews.DataSource = theTVSeriesViews;
          cboParameterViews.Text = string.Empty;
          break;
        case musicSkinID:
          cboParameterViews.DataSource = theMusicViews;
          cboParameterViews.Text = string.Empty;
          break;
        case onlineVideosSkinID:
          cboParameterViews.DataSource = theOnlineVideosViews;
          cboParameterViews.Text = string.Empty;
          break;
        default:
          break;
      }
      fhChoice.Visible = false;
      fhRBScraper.Visible = false;
      fhRBUserDef.Visible = false;
    }

    private void ClearItems()
    {
      tbItemName.Text = "";
      bgBox.Text = "";
      cboFanartProperty.Text = "";
      cbItemFanartHandlerEnable.Checked = false;
      cbEnableMusicNowPlayingFanart.Checked = false;
      cboContextLabel.Text = "";
      isWeather.Checked = false;

    }

    private void cboQuickSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
      // Auto fill items on new selection for quicker add
      QuickSelect(cboQuickSelect.SelectedIndex);
      enableItemControls();
      cancelCreateButton.Visible = true;
      editButton.Enabled = false;
      btGenerateMenu.Enabled = false;
      disableBGSharing.Checked = false;
    }



    private void BasicHomeFromTemplate()
    {
      System.IO.StreamWriter writer;
      string backupFilename = SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml.backup." + DateTime.Now.Ticks.ToString();

      if (System.IO.File.Exists(SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml"))
        System.IO.File.Copy(SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml", backupFilename);

      if (System.IO.File.Exists(SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml"))
        System.IO.File.Delete(SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml");

      Stream streamTemplate = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.xmlFiles.AeonBasicHomeDefault.xml");
      StreamReader reader = new StreamReader(streamTemplate);
      xmlTemplate = reader.ReadToEnd();

      writer = System.IO.File.CreateText(SkinInfo.mpPaths.streamedMPpath + "BasicHome.xml");
      writer.Write(xmlTemplate);
      writer.Close();
      helper.showError("Existing BasicHome.xml Saved Sucessfully as\n\n" + backupFilename + "\n\nNew BasicHome.xml created from Internal template", errorCode.info);

      foreach (menuItem item in menuItems)
      {
        item.id = menuItems.IndexOf(item);
      }
    }

    private void verticalStyle_Click(object sender, EventArgs e)
    {
      if (menuStyle != chosenMenuStyle.verticalStyle)
      {
        menuStyle = chosenMenuStyle.verticalStyle;
        syncEditor(sync.editing);
      }
    }

    private void horizontalStyle_Click(object sender, EventArgs e)
    {
      if (menuStyle != chosenMenuStyle.horizontalStandardStyle)
      {
        menuStyle = chosenMenuStyle.horizontalStandardStyle;
        syncEditor(sync.editing);
      }
    }

    private void horizontalStyle2_Click(object sender, EventArgs e)
    {
      if (menuStyle != chosenMenuStyle.horizontalContextStyle)
      {
        menuStyle = chosenMenuStyle.horizontalContextStyle;
        syncEditor(sync.editing);
      }
    }

    private void graphicalStyle_CheckedChanged(object sender, EventArgs e)
    {
      if (menuStyle != chosenMenuStyle.graphicMenuStyle)
      {
        menuStyle = chosenMenuStyle.graphicMenuStyle;
        syncEditor(sync.editing);
      }
    }

    private void stdWeatherStyle_Click(object sender, EventArgs e)
    {
      fullWeatherSummaryBottom.Checked = true;
      fullWeatherSummaryMiddle.Checked = false;
      weatherIconsGroup.Enabled = true;
      weatherStyle = chosenWeatherStyle.bottom;
    }

    private void centeredWeatherStyle_Click(object sender, EventArgs e)
    {
      fullWeatherSummaryBottom.Checked = false;
      fullWeatherSummaryMiddle.Checked = true;
      weatherStyle = chosenWeatherStyle.middle;
    }

    private void buildFHchoiceControls()
    {
        fhChoice.Text = "Fanart Source:";
        fhChoice.Location = new Point(37, 100);

        fhRBUserDef.Location = new Point(140, 100);
        fhRBUserDef.Size = new Size(78, 17);
        fhRBUserDef.Text = "User";

        fhRBScraper.Location = new Point(220, 100);
        fhRBScraper.Size = new Size(78, 17);
        fhRBScraper.Text = "Scaper";

        fhChoice.Visible = false;
        fhRBScraper.Visible = false;
        fhRBUserDef.Visible = false;

        fhRBScraper.Checked = true;
        fhRBUserDef.Checked = false;


        this.backgroundImages.Controls.Add(fhChoice);
        this.backgroundImages.Controls.Add(fhRBUserDef);
        this.backgroundImages.Controls.Add(fhRBScraper);
    }


    private void UpdateImageControlVisibility(bool fanartHandlerEnabled)
    {

      if (fanartHandlerEnabled)
      {
        cboFanartProperty.Visible = true;
        labelFanartProperty.Visible = true;
        cbEnableMusicNowPlayingFanart.Visible = true;
        labelImageFolder.Visible = false;
        bgBox.Visible = false;
        folderBrowse.Visible = false;

        if (fanartHandlerRelease2 && (cboFanartProperty.Text.ToLower() == "music" || cboFanartProperty.Text.ToLower() == "movie"))
        {
          fhChoice.Visible = true;
          fhRBScraper.Visible = true;
          fhRBUserDef.Visible = true;
        }
        else
        {
          fhRBScraper.Visible = false;
          fhRBUserDef.Visible = false;
        }
      }
      else
      {
        // Hide the fanart selection
        cboFanartProperty.Visible = false;
        labelFanartProperty.Visible = false;
        cbEnableMusicNowPlayingFanart.Visible = false;
        fhChoice.Visible = false;
        fhRBScraper.Visible = false;
        fhRBUserDef.Visible = false;
        //set the x,y of the skin image settings and display
        labelImageFolder.Location = new Point(5, 52);
        bgBox.Location = new Point(113, 49);
        folderBrowse.Location = new Point(255, 49);
        labelImageFolder.Visible = true;
        bgBox.Visible = true;
        folderBrowse.Visible = true;
      }
    }

    private void disableItemControls()
    {
      itemProperties.Enabled = false;
      backgroundImages.Enabled = false;
      addButton.Enabled = false;
      removeButton.Enabled = true;
      cancelCreateButton.Visible = false;
      btGenerateMenu.Enabled = true;
    }

    private void enableItemControls()
    {
      itemProperties.Enabled = true;
      backgroundImages.Enabled = true;
      addButton.Enabled = true;
      removeButton.Enabled = false;
    }

    private void reloadBackgroundItems()
    {
      bgItems.Clear();
      foreach (menuItem menItem in menuItems)
      {
        fillBackgroundItem(menItem);
      }
      GetDefaultBackgroundImages();
    }


    private void fillBackgroundItem(menuItem menItem)
    {
      bool newBG = true;

      foreach (backgroundItem bgitem in bgItems)
      {
        // if we are sharing the same image folder and background sharing is enabled
        // update the existing background item
        if (!menItem.disableBGSharing && !bgitem.disableBGSharing)
        {
          // check if current item is unique
          if (!menItem.fanartHandlerEnabled.Equals(bgitem.fanartHandlerEnabled))
            continue;

          if (menItem.fanartHandlerEnabled)
          {
            if (bgitem.fanartPropery != menItem.fanartProperty)
              continue;
          }
          else
          {
            if (bgitem.folder != menItem.bgFolder)
              continue;
          }

          // share background item
          bgitem.ids.Add(menItem.id.ToString());
          bgitem.mname.Add(menItem.name.ToString());
          bgitem.name = bgitem.name + ", " + menItem.name;
          if (bgitem.subMenuID == 0)
            bgitem.subMenuID = menItem.subMenuLevel1ID;
          newBG = false;
        }
      }

      // create a new background item
      if (newBG == true)
      {
        backgroundItem newbgItem = new backgroundItem();
        newbgItem.folder = menItem.bgFolder;
        newbgItem.fanartPropery = menItem.fanartProperty;
        newbgItem.fhBGSource = menItem.fhBGSource;
        newbgItem.fanartHandlerEnabled = menItem.fanartHandlerEnabled;
        newbgItem.EnableMusicNowPlayingFanart = menItem.EnableMusicNowPlayingFanart;
        newbgItem.ids.Add(menItem.id.ToString());
        newbgItem.mname.Add(menItem.name.ToString());
        newbgItem.name = menItem.name;
        newbgItem.image = menItem.defaultImage;
        newbgItem.isWeather = menItem.isWeather;
        newbgItem.disableBGSharing = menItem.disableBGSharing;
        newbgItem.subMenuID = menItem.subMenuLevel1ID;
        bgItems.Add(newbgItem);
      }
    }


    private void setBasicHomeValues()
    {

      basicHomeValues.offsetMymenu = -39;
      basicHomeValues.textYOffset = 6;
      basicHomeValues.offsetSubmenu = 76;
      basicHomeValues.offsetRssImage = 73;
      basicHomeValues.offsetRssText = 75;
      basicHomeValues.offsetTwitter = 55;
      basicHomeValues.offsetTwitterImage = 28;
      basicHomeValues.offsetButtons = 42;
      basicHomeValues.menuHeight = 165;
      basicHomeValues.subMenuHeight = 60;
      basicHomeValues.subMenuXpos = 0;
      basicHomeValues.subMenuWidth = 1280;
      basicHomeValues.subMenuTopHeight = 60;
      basicHomeValues.Button3Slide = 70;

      if (useAeonGraphics.Checked)
      {
        basicHomeValues.mymenu = "vmenu_main-a.png";
        basicHomeValues.mymenu_submenu = "vmenu_submenu-a.png";
        basicHomeValues.mymenu_submenutop = "vmenu_submenutop-a.png";
      }
      else
      {
        basicHomeValues.mymenu = "vmenu_main.png";
        basicHomeValues.mymenu_submenu = "vmenu_submenu.png";
        basicHomeValues.mymenu_submenutop = "vmenu_submenutop.png";
      }

      // Now adjust depending on Menu Style chosen
      switch (menuStyle)
      {
        case chosenMenuStyle.verticalStyle:
          basicHomeValues.mymenu_submenutop = "hometwitter.png";
          basicHomeValues.offsetTwitter = 0;
          break;
        case chosenMenuStyle.horizontalStandardStyle:
          if (horizontalContextLabels.Checked)
          {
            basicHomeValues.menuHeight += 34;
            basicHomeValues.offsetMymenu -= 25;
            basicHomeValues.offsetButtons += 16;
            basicHomeValues.offsetTwitter += 15;
            basicHomeValues.offsetTwitterImage += 15;
          }
          break;
        case chosenMenuStyle.horizontalContextStyle:
          if (horizontalContextLabels.Checked)
          {
            basicHomeValues.menuHeight += 33;
            basicHomeValues.offsetMymenu -= 24;
            basicHomeValues.offsetButtons += 16;
            basicHomeValues.offsetTwitter += 15;
            basicHomeValues.offsetTwitterImage += 15;
          }
          break;
        case chosenMenuStyle.graphicMenuStyle:
          basicHomeValues.menuHeight = 198;
          basicHomeValues.offsetMymenu = 27;
          basicHomeValues.subMenuXpos = -70;
          basicHomeValues.subMenuWidth = 1420;
          basicHomeValues.subMenuHeight = 70;
          basicHomeValues.offsetSubmenu = 257;
          basicHomeValues.offsetRssText = 259;


          basicHomeValues.textYOffset = 30;

          basicHomeValues.offsetRssImage = 73;

          basicHomeValues.offsetTwitter = 55;
          basicHomeValues.offsetTwitterImage = 28;
          basicHomeValues.offsetButtons = 42;



          basicHomeValues.subMenuTopHeight = 60;
          basicHomeValues.Button3Slide = 70;
          weatherStyle = chosenWeatherStyle.middle;
          break;
      }
    }

    private Version getInfoServiceVersion()
    {
      Version fv = new Version(helper.fileVersion(SkinInfo.mpPaths.pluginPath + "\\windows\\infoservice.dll"));
      return fv;
    }


    private string MovingPicturesVersion
    {
      get
      {
        return helper.fileVersion(SkinInfo.mpPaths.pluginPath + "\\windows\\MovingPictures.dll");
      }
    }

    private string TVSeriesVersion
    {
      get
      {
        return helper.fileVersion(SkinInfo.mpPaths.pluginPath + "\\windows\\MP-TVSeries.dll");
      }
    }

    private string MediaPortalVersion
    {
      get
      {
        return helper.fileVersion(Path.Combine(SkinInfo.mpPaths.sMPbaseDir, "MediaPortal.exe"));
      }
    }


    public bool validForMPVersion(string validForMPVersion)
    {
      Version versionToTest = new Version(validForMPVersion);
      Version mpVersion = new Version(MediaPortalVersion);

      if (mpVersion.CompareTo(versionToTest) > 0)
        return true;
      else
        return false;
    }

    private Color ColorFromRGB(string RGB)
    {
      if (RGB.Length != 6)
        return System.Drawing.Color.FromArgb(255, 255, 255);

      byte R = ColorTranslator.FromHtml("#" + RGB).R;
      byte G = ColorTranslator.FromHtml("#" + RGB).G;
      byte B = ColorTranslator.FromHtml("#" + RGB).B;

      return System.Drawing.Color.FromArgb(int.Parse(R.ToString()),
                                           int.Parse(G.ToString()),
                                           int.Parse(B.ToString()));
    }

    public Color ColorInvert(Color colorIn)
    {
      return Color.FromArgb(colorIn.A, Color.White.R - colorIn.R,
             Color.White.G - colorIn.G, Color.White.B - colorIn.B);
    }


    private void txtNoFocusColour_MouseClick(object sender, MouseEventArgs e)
    {
      ColorDialog colorDialog = new ColorDialog();
      if (colorDialog.ShowDialog() != DialogResult.Cancel)
      {
        txtNoFocusColour.BackColor = colorDialog.Color;
        txtNoFocusColour.ForeColor = ColorInvert(colorDialog.Color);
        txtNoFocusColour.Text = (colorDialog.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
      }

    }

    private void txtFocusColour_MouseClick(object sender, MouseEventArgs e)
    {
      ColorDialog colorDialog = new ColorDialog();
      if (colorDialog.ShowDialog() != DialogResult.Cancel)
      {
        txtFocusColour.BackColor = colorDialog.Color;
        txtFocusColour.ForeColor = ColorInvert(colorDialog.Color);
        txtFocusColour.Text = (colorDialog.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
      }
    }

    private void removeButton_Click(object sender, EventArgs e)
    {
      removeToolStripMenuItem_Click(sender, e);
      reloadBackgroundItems();
      changeOutstanding = true;
    }

    private void removeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (itemsOnMenubar.SelectedItem != null)
      {
        menuItems.RemoveAt(itemsOnMenubar.SelectedIndex);
        itemsOnMenubar.Items.RemoveAt(itemsOnMenubar.SelectedIndex);
        screenReset();
        if (itemsOnMenubar.Items.Count > 0)
          itemsOnMenubar.SelectedIndex = 0;
      }
      else
        helper.showError("No menu item selected to Remove\n\nPlease select menu item to Remove", errorCode.info);
    }

    private string generateEntry(string entry, string value, int tabs, bool addcr)
    {
      string entryLine = null;

      for (int i = 1; i < tabs + 1; i++)
        entryLine += "\t";

      entryLine += "<entry name=" + quote + entry + quote + ">" + value + "</entry>";
      if (addcr) entryLine += "\n";
      return entryLine;
    }

    private string readEntryValue(string section, string elementName, XmlNodeList unodeList)
    {
      string entryValue;

      foreach (XmlNode node in unodeList)
      {
        XmlNode innerNode = node.Attributes.GetNamedItem("name");
        if (innerNode.InnerText == "StreamedMP")
        {
          XmlNodeList skinNodeList = node.SelectNodes("section");
          foreach (XmlNode skinNode in skinNodeList)
          {
            XmlNode skinNodeSection = skinNode.Attributes.GetNamedItem("name");
            if (skinNodeSection.InnerText == section)
            {
              XmlNode path = skinNode.SelectSingleNode("entry[@name=\"" + elementName + "\"]");
              if (path != null)
              {
                entryValue = path.InnerText;
                return entryValue;
              }
            }
          }
        }
      }
      return "false";
    }

    private void txtFocusColour_TextChanged(object sender, EventArgs e)
    {
      int start = txtFocusColour.SelectionStart;
      txtFocusColour.Text = txtFocusColour.Text.ToUpper();
      txtFocusColour.SelectionStart = start;
    }

    private void txtNoFocusColour_TextChanged(object sender, EventArgs e)
    {
      int start = txtNoFocusColour.SelectionStart;
      txtNoFocusColour.Text = txtNoFocusColour.Text.ToUpper();
      txtNoFocusColour.SelectionStart = start;
    }


    private void btnClearCache_Click(object sender, EventArgs e)
    {
      DialogResult result = helper.showError("Clearing cache\n\n" + SkinInfo.mpPaths.cacheBasePath + configuredSkin + "\n\nPlease confirm clearing of the cache", errorCode.infoQuestion);
      if (result == DialogResult.No)
      {
        return;
      }
      clearCacheDir();
    }

    private void clearCacheDir()
    {
      try
      {
        System.IO.Directory.Delete(SkinInfo.mpPaths.cacheBasePath + configuredSkin, true);
        //helper.showError("Skin cache has been cleared\n\nOk To Continue", errorCode.info);
      }
      catch
      {
        //helper.showError("Exception while deleteing Cache\n\n" + ex.Message, errorCode.info);
      }
    }

    private string weatherIcon(int theDay)
    {
      string day;
      if (theDay == 0)
        day = "today";
      else
        if (getInfoServiceVersion().CompareTo(isWeatherVersion) >= 0)
          day = "forecast" + (theDay + 1).ToString() + ".day";
      else
        day = "day" + (theDay + 1).ToString() + ".day";
      if (WeatherIconsAnimated.Checked)
      {
        // relative from Animations folder
        return "weathericons\\animated\\128x128\\#infoservice.weather." + day + ".img.big.filenamewithoutext";
      }
      else
      {
        // relative from Media folder
        return "animations\\weathericons\\static\\128x128\\#infoservice.weather." + day + ".img.big.filenamewithoutext.png";
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

    private void txtMenuPos_Leave(object sender, EventArgs e)
    {
      validateMenuOffset();

    }

    private void validateMenuOffset()
    {
      // Don't allow users to select 5 day weather postions that conflicts with menu bar position
      if (menuStyle != chosenMenuStyle.verticalStyle)
      {
        if (int.Parse(txtMenuPos.Text) > 433)
        {
          weatherStyle = chosenWeatherStyle.middle;
          fullWeatherSummaryBottom.Enabled = false;
          return;
        }
        else
        {
          fullWeatherSummaryBottom.Enabled = true;
          return;
        }
      }
      else
      {
        // Check if the new value will result in context lable not being displayed
        int minXPos = 0;
        menuOffset = int.Parse(txtMenuPos.Text);

        int maxContextSize = 0;
        int maxMenuItemSize = 0;
        Font nameFont = new Font("Fluid Title Caps", 28.0F);
        Font contextFont = new Font("Fluid Title Caps", 16.0F);
        // find the longest Context and Menu items
        foreach (menuItem menItem in menuItems)
        {
          Size textNameSize = TextRenderer.MeasureText(menItem.name, nameFont);
          Size textContextSize = TextRenderer.MeasureText(menItem.contextLabel, contextFont);

          if (maxMenuItemSize < textNameSize.Width)
            maxMenuItemSize = textNameSize.Width;

          if (maxContextSize < textContextSize.Width)
            maxContextSize = textContextSize.Width;
        }
        // now calc the minimum xpos based on longest string in context and menu labels

        minXPos = maxContextSize;
        if (minXPos < maxMenuItemSize)
          minXPos = maxMenuItemSize;

        minXPos = (int)((double)minXPos * 1.35);
        if (menuOffset < minXPos)
        {
          txtMenuPos.Text = minXPos.ToString();
          menuOffset = int.Parse(txtMenuPos.Text);
          helper.showError("The Menu X Position value will result in blank Context or Menu labels. \n\nMenu X Position reset to calculated minium value of " + txtMenuPos.Text, errorCode.info);
        }
      }
    }

    private void syncEditor(sync syncType)
    {
      // Here we setup the editor for the chosen style, this is an inital setup and will configure the editor 
      // for the default options for that style. This is call after the inital load of the menu setting and the defaults set
      // will be overridden by the options that are read next.
      // If called because a user has clicked a style button the editor will reset to that style overiding any custom settings set by the user
      // for the previous selected style
      summaryWeatherCheckBox.Checked = true;
      menuPosLabel.Text = "Menu Y Position:";
      enableRssfeed.Checked = true;

      if (animatedIconsInstalled())
      {
        WeatherIconsAnimated.Enabled = true;
        WeatherIconsAnimated.Text = "Animated";
        installAnimatedIcons.Visible = false;
      }
      else
      {
        WeatherIconsAnimated.Enabled = false;
        weatherIconsStatic.Checked = true;
        WeatherIconsAnimated.Text = "Animated (Not Installed)";
        installAnimatedIcons.Visible = true;
      }

      if (weatherBackgoundsInstalled())
      {
        weatherBGlink.Enabled = true;
        weatherBGlink.Text = "Link Background to Current Weather";
        installWeatherBackgrounds.Visible = false;
      }
      else
      {
        weatherBGlink.Checked = false;
        weatherBGlink.Enabled = false;
        weatherBGlink.Text = "Link Background to Current Weather (Not Installed)";
        installWeatherBackgrounds.Visible = true;
      }

      switch (menuStyle)
      {
        case chosenMenuStyle.verticalStyle:
          enableFiveDayWeather.Checked = true;
          horizontalContextLabels.Enabled = false;
          weatherSummaryGroup.Visible = false;
          verticalStyle.Checked = true;
          
          if (!WeatherIconsAnimated.Checked)
            weatherIconsStatic.Checked = true;

          useAeonGraphics.Visible = false;
          txtMenuPos.Text = "350";
          menuPosLabel.Text = "Menu X Position:";
          cboLabelFont.Enabled = false;
          cboSelectedFont.Enabled = false;
          cboSelectedFont.Text = "mediastream28tc";
          cboLabelFont.Text = "mediastream16tc";
          cbExitStyleNew.Visible = true;
          btMenuIcon.Visible = false;
          pbMenuIconInfo.Visible = false;
          break;
        case chosenMenuStyle.horizontalStandardStyle:
          weatherStyle = chosenWeatherStyle.bottom;
          horizontalContextLabels.Checked = false;
          enableFiveDayWeather.Checked = true;
          fullWeatherSummaryBottom.Enabled = true;
          fullWeatherSummaryBottom.Checked = true;
          horizontalContextLabels.Enabled = true;
          horizontalStyle.Checked = true;

          if (!WeatherIconsAnimated.Checked)
            weatherIconsStatic.Checked = true;

          weatherSummaryGroup.Visible = true;
          useAeonGraphics.Visible = false;
          txtMenuPos.Text = "430";
          cboLabelFont.Enabled = true;
          cboSelectedFont.Enabled = true;
          cboSelectedFont.Text = "mediastream28tc";
          cboLabelFont.Text = "mediastream28tc";
          cbExitStyleNew.Visible = false;
          btMenuIcon.Visible = false;
          pbMenuIconInfo.Visible = false;
          break;
        case chosenMenuStyle.horizontalContextStyle:
          weatherStyle = chosenWeatherStyle.middle;
          horizontalContextLabels.Checked = true;
          enableFiveDayWeather.Checked = true;
          fullWeatherSummaryMiddle.Checked = true;
          fullWeatherSummaryBottom.Enabled = false;
          horizontalContextLabels.Enabled = true;
          horizontalStyle2.Checked = true;
          if (!WeatherIconsAnimated.Checked)
            weatherIconsStatic.Checked = true;
          weatherSummaryGroup.Visible = true;
          useAeonGraphics.Visible = false;
          txtMenuPos.Text = "620";
          cboSelectedFont.Text = "mediastream28tc";
          cboLabelFont.Text = "mediastream28tc";
          cboLabelFont.Enabled = false;
          cboSelectedFont.Enabled = false;
          cbExitStyleNew.Visible = false;
          btMenuIcon.Visible = false;
          pbMenuIconInfo.Visible = false;
          break;
        case chosenMenuStyle.graphicMenuStyle:
          //First check if there is an icon, if not set a default
          foreach (menuItem mnuItem in menuItems)
          {
            if (string.IsNullOrEmpty(mnuItem.buttonTexture) || mnuItem.buttonTexture.ToLower() == "false")
            {
              mnuItem.buttonTexture = setDefaultIcons(int.Parse(mnuItem.hyperlink),"Black");
            }
          }
          weatherStyle = chosenWeatherStyle.bottom;
          horizontalContextLabels.Checked = false;
          enableFiveDayWeather.Checked = true;
          fullWeatherSummaryBottom.Enabled = false;
          fullWeatherSummaryBottom.Checked = true;
          horizontalContextLabels.Enabled = false;
          graphicalStyle.Checked = true;

          if (!WeatherIconsAnimated.Checked)
            weatherIconsStatic.Checked = true;

          weatherSummaryGroup.Visible = true;
          useAeonGraphics.Visible = false;
          txtMenuPos.Text = "430";
          cboLabelFont.Enabled = true;
          cboSelectedFont.Enabled = true;
          cboSelectedFont.Text = "mediastream28tc";
          cboLabelFont.Text = "mediastream28tc";
          cbExitStyleNew.Visible = false;
          btMenuIcon.Visible = true;
          pbMenuIconInfo.Visible = true;
          break;
      }
    }

    private void style1Description_MouseEnter(object sender, EventArgs e)
    {
      Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.rtfFiles.style1.rtf");
      menuDescription.LoadFile(stream, RichTextBoxStreamType.RichText);
    }

    private void style2Description_MouseEnter(object sender, EventArgs e)
    {
      Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.rtfFiles.style2.rtf");
      menuDescription.LoadFile(stream, RichTextBoxStreamType.RichText);
    }

    private void style3Description_MouseEnter(object sender, EventArgs e)
    {
      Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.rtfFiles.style3.rtf");
      menuDescription.LoadFile(stream, RichTextBoxStreamType.RichText);
    }

    private void style4Description_MouseEnter(object sender, EventArgs e)
    {
      Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.rtfFiles.style4.rtf");
      menuDescription.LoadFile(stream, RichTextBoxStreamType.RichText);
    }

    private void style1Description_MouseLeave(object sender, EventArgs e)
    {
      Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.rtfFiles.introduction.rtf");
      menuDescription.LoadFile(stream, RichTextBoxStreamType.RichText);
    }

    private void style2Description_MouseLeave(object sender, EventArgs e)
    {
      Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.rtfFiles.introduction.rtf");
      menuDescription.LoadFile(stream, RichTextBoxStreamType.RichText);
    }

    private void style3Description_MouseLeave(object sender, EventArgs e)
    {
      Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.rtfFiles.introduction.rtf");
      menuDescription.LoadFile(stream, RichTextBoxStreamType.RichText);
    }

    private void style4Description_MouseLeave(object sender, EventArgs e)
    {
      Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.rtfFiles.introduction.rtf");
      menuDescription.LoadFile(stream, RichTextBoxStreamType.RichText);
    }

    private void fullWeatherSummaryMiddle_CheckedChanged(object sender, EventArgs e)
    {
      if (menuStyle == chosenMenuStyle.verticalStyle)
        return;

      if (fullWeatherSummaryMiddle.Checked)
      {
        weatherStyle = chosenWeatherStyle.middle;
      }
      else
      {
        weatherStyle = chosenWeatherStyle.bottom;
      }

    }

    private void getBackupFileTotals()
    {
      getFileListing(SkinInfo.mpPaths.configBasePath, "usermenuprofile.xml.backup*", false);
      numUPBackups.Text = totalImages.ToString();
    }

    private void StreamedMPMenu_Selected(object sender, TabControlEventArgs e)
    {
      if (lastUsedTab.Checked)
      {
        Properties.Settings.Default.rememberLastUsedTab = true;
        Properties.Settings.Default.lastUsedTab = StreamedMPMenu.SelectedIndex;
      }
      screenReset();
      setScreenProperties(itemsOnMenubar.SelectedIndex);
      disableItemControls();
      btGenerateMenu.Enabled = true;
    }

    private void streamedMpEditor_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (changeOutstanding)
      {
        DialogResult result = helper.showError("There are outstanding menu changes\n\nDo you want Re-Generate your menu", errorCode.infoQuestion);
        if (result == DialogResult.Yes)
        {
          genMenu(true);
        }
      }

      if (!exitCondition)
      {
        int versionCount = 0;
        Properties.Settings.Default.autoPurge = autoPurgeBackups.Checked;
        if (autoPurgeBackups.Checked)
        {
          Properties.Settings.Default.keepVersions = int.Parse(backupVersionsToKeep.Text);
          Properties.Settings.Default.autoPurge = true;

          string[] filesToDelete = getFileListing(SkinInfo.mpPaths.configBasePath, "usermenuprofile.xml.backup.*", false);
          foreach (string file in filesToDelete)
          {
            if (versionCount >= int.Parse(backupVersionsToKeep.Text))
              System.IO.File.Delete(file);
            versionCount++;
          }
        }
        Properties.Settings.Default.Save();
      }
      changeOutstanding = false;

      //reset everything
      xmlFiles.Items.Clear();
      cboQuickSelect.Items.Clear();
      itemsOnMenubar.Items.Clear();
      prettyItems.Clear();
      ids.Clear();
      bgItems.Clear();
      menuItems.Clear();
    }

    private void purgeUPBackups_Click(object sender, EventArgs e)
    {
      string[] filesToDelete = getFileListing(SkinInfo.mpPaths.configBasePath, "usermenuprofile.xml.backup.*", false);
      foreach (string file in filesToDelete)
      {
        System.IO.File.Delete(file);
      }
      getBackupFileTotals();
    }

    private void lastUsedTab_CheckedChanged(object sender, EventArgs e)
    {
      Properties.Settings.Default.rememberLastUsedTab = lastUsedTab.Checked;
    }

    private void showConfigPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (System.IO.Directory.Exists(SkinInfo.mpPaths.configBasePath))
        System.Diagnostics.Process.Start(SkinInfo.mpPaths.configBasePath);
      else MessageBox.Show("The directory/file does not exist.");
    }

    private void showMPDir_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (System.IO.Directory.Exists(SkinInfo.mpPaths.sMPbaseDir))
        System.Diagnostics.Process.Start(SkinInfo.mpPaths.sMPbaseDir);
      else MessageBox.Show("The directory/file does not exist.");

    }

    private void showSkinDir_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (System.IO.Directory.Exists(SkinInfo.mpPaths.streamedMPpath))
        System.Diagnostics.Process.Start(SkinInfo.mpPaths.streamedMPpath);
      else MessageBox.Show("The directory/file does not exist.");
    }

    private bool animatedIconsInstalled()
    {
      if (Directory.Exists(SkinInfo.mpPaths.streamedMPpath + "media\\animations\\weathericons\\animated\\128x128"))
        return true;
      else
        return false;
    }

    private bool weatherBackgoundsInstalled()
    {
      if (Directory.Exists(SkinInfo.mpPaths.streamedMPpath + "media\\ + bgFolderName + \\linkedweather"))
        return true;
      else
        return false;
    }


    private void readFonts()
    {
      XmlDocument doc = new XmlDocument();
      if (!File.Exists(SkinInfo.mpPaths.streamedMPpath + "Fonts.xml"))
      {
        helper.showError("Fonts.xml Not Found in \r\r" + SkinInfo.mpPaths.streamedMPpath + " \r\rPlease make sure fonts.xml exists", errorCode.major);
        return;
      }
      try
      {
        doc.Load(SkinInfo.mpPaths.streamedMPpath + "Fonts.xml");
      }
      catch (Exception e)
      {
        helper.showError("Exception while loading Fonts.xml\n\n" + e.Message, errorCode.loadError);
        return;
      }

      XmlNodeList fonts = doc.DocumentElement.SelectNodes("/fonts/font");

      foreach (XmlNode font in fonts)
      {
        XmlNode innerNode = font.SelectSingleNode("name");
        skinFontsFocused.Add(innerNode.InnerText);
        skinFontsUnFocused.Add(innerNode.InnerText);
      }

      cboSelectedFont.DataSource = skinFontsFocused;
      cboLabelFont.DataSource = skinFontsUnFocused;

    }


    public void checkAndSetRandomFanart(string fanartProperty)
    {
      // Check and set random fanart
      if (fanartProperty.ToLower().Contains("games"))
        randomFanart.fanartGames = true;

      if (fanartProperty.ToLower().Contains("plugins"))
        randomFanart.fanartPlugins = true;

      if (fanartProperty.ToLower().Contains("picture"))
        randomFanart.fanartPictures = true;

      if (fanartProperty.ToLower().Contains("tv"))
        randomFanart.fanartTv = true;
      
      if (fanartProperty.ToLower().Contains("music"))
      {
        if (fanartHandlerRelease2)
        {
          if (fhRBScraper.Checked)
          {
            randomFanart.fanartMusic = false;
            randomFanart.fanartMusicScraperFanart = true;
          }
          else
          {
            randomFanart.fanartMusic = true;
            randomFanart.fanartMusicScraperFanart = false;
          }
        }
        else
          randomFanart.fanartMusic = true;
      }

      if (fanartProperty.ToLower().Contains("tvseries"))
        randomFanart.fanartTVSeries = true;

      if (fanartProperty.ToLower().Contains("movingpicture"))
        randomFanart.fanartMovingPictures = true;

      if (fanartProperty.ToLower().Contains("movie"))
      {
        if (fanartHandlerRelease2)
        {
          if (fhRBScraper.Checked)
          {
            randomFanart.fanartMovies = false;
            randomFanart.fanartMoviesScraperFanart = true;
          }
          else
          {
            randomFanart.fanartMovies = true;
            randomFanart.fanartMoviesScraperFanart = false;
          }
        }
        else
          randomFanart.fanartMovies = true;
      }

      if (fanartProperty.ToLower().Contains("scorecenter"))
        randomFanart.fanartScoreCenter = true;
    }


    private void rbRssNoImage_CheckedChanged(object sender, EventArgs e)
    {
      if (rbRssNoImage.Checked)
        rssImage = rssImageType.noImage;
    }

    private void rbRssSkinImage_CheckedChanged(object sender, EventArgs e)
    {
      if (rbRssSkinImage.Checked)
        rssImage = rssImageType.skinImage;
    }

    private void rbRssInfoServiceImage_CheckedChanged(object sender, EventArgs e)
    {
      if (rbRssInfoServiceImage.Checked)
        rssImage = rssImageType.infoserviceImage;
    }

    private void cbItemFanartHandlerEnable_CheckedChanged(object sender, EventArgs e)
    {
      UpdateImageControlVisibility(cbItemFanartHandlerEnable.Checked);
    }

    private void buttonCancelCreate_Click(object sender, EventArgs e)
    {
      screenReset();
      setScreenProperties(itemsOnMenubar.SelectedIndex);
      disableItemControls();
    }

    private void sdRes_CheckedChanged(object sender, EventArgs e)
    {
      if (sdRes.Checked)
      {
        setSDScreenRes();
      }
      else
      {
        setHDScreenRes();
      }

    }

    private void hdRes_CheckedChanged(object sender, EventArgs e)
    {
      if (hdRes.Checked)
      {
        setHDScreenRes();
      }
      else
      {
        setSDScreenRes();
      }

    }

    private void setHDScreenRes()
    {
      screenres = screenResolutionType.res1920x1080;
      hdRes.Checked = true;
      sdRes.Checked = false;
      if (screenres == detectedres)
        detectedHD.Text = "Auto Detected";
      else
        detectedHD.Text = "Set Manually";
      detectedHD.Visible = true;
      detectedSD.Visible = false;
    }

    private void setSDScreenRes()
    {
      screenres = screenResolutionType.res1280x720;
      hdRes.Checked = false;
      sdRes.Checked = true;
      if (screenres == detectedres)
        detectedSD.Text = "Auto Detected";
      else
        detectedSD.Text = "Set Manually";
      detectedSD.Visible = true;
      detectedHD.Visible = false;
    }

    private void StreamedMPMenu_Click(object sender, EventArgs e)
    {
      // Index 5 is the SplashScreen Tab
      if (StreamedMPMenu.SelectedIndex == 5)
      {
        pbActiveSplashScreen.Visible = true;
        lbActiveSplashScreen.Visible = true;
        toolStripStatusLabel2.Visible = true;
      }
      else
      {
        pbActiveSplashScreen.Visible = false;
        lbActiveSplashScreen.Visible = false;
        toolStripStatusLabel2.Visible = false;
      }
    }

    private void checkAndSetDefultImage(menuItem item)
    {
      // Set default image....
      if (!item.bgFolder.Contains("\\"))
        item.defaultImage = bgFolderName + "\\" + item.bgFolder + "\\default.jpg";
      else
        item.defaultImage = item.bgFolder + "\\default.jpg";
      // And check if it exists and create if not.

      string filetotest = imageDir(item.defaultImage);
      if (!System.IO.File.Exists(filetotest))
      {
        string[] fileList = getFileListing(Path.GetDirectoryName(item.defaultImage), "*.*", true);
        createDefaultJpg(Path.GetDirectoryName(item.defaultImage));
      }
    }

    private void pTVSeriesRecent_Click(object sender, EventArgs e)
    {
      if (cbMostRecentTvSeries.Checked && rbTVSeriesSummary.Checked)
      {
        cboxSummaryFor.Text = "TVSeries";
        cboxSummaryFor.Refresh();
        doSummaryFor();
      }
    }

    private void pMovPicsRecent_Click(object sender, EventArgs e)
    {
      if (cbMostRecentMovPics.Checked && rbMovPicsSummary.Checked)
      {
        cboxSummaryFor.Text = "MovingPictures";
        cboxSummaryFor.Refresh();
        doSummaryFor();
      }
    }

    private void cbMostRecentTvSeries_CheckedChanged(object sender, EventArgs e)
    {
      if (cbMostRecentTvSeries.Checked == true)
      {
        gbTvSeriesOptions.Enabled = true;

        if (cbMostRecentMovPics.Checked)
          if (rbMovPicsSummary.Checked && rbTVSeriesSummary.Checked)
          {
            cboxSummaryFor.Enabled = true;
            gbSummaryStyle.Visible = true;
            pSumHeader.Visible = true;
          }
          else
          {
            if (rbTVSeriesSummary.Checked)
            {
              cboxSummaryFor.Text = "TVSeries";
              cboxSummaryFor.Refresh();
              doSummaryFor();
              cboxSummaryFor.Enabled = false;
              gbSummaryStyle.Enabled = true;
              gbSummaryStyle.Visible = true;
              pSumHeader.Visible = true;
            }
            if (rbMovPicsSummary.Checked)
            {
              cboxSummaryFor.Text = "MovingPictures";
              cboxSummaryFor.Refresh();
              doSummaryFor();
              cboxSummaryFor.Enabled = false;
              gbSummaryStyle.Enabled = true;
              gbSummaryStyle.Visible = true;
              pSumHeader.Visible = true;
            }
          }
      }
      else
      {
        if (cbMostRecentMovPics.Checked)
        {
          if (rbMovPicsSummary.Checked)
          {
            cboxSummaryFor.Text = "MovingPictures";
            cboxSummaryFor.Refresh();
            doSummaryFor();
            cboxSummaryFor.Enabled = false;
            gbTvSeriesOptions.Enabled = false;
            gbSummaryStyle.Enabled = true;
          }
          else
          {
            gbSummaryStyle.Enabled = false;
            gbSummaryStyle.Visible = false;
            pSumHeader.Visible = false;
          }
        }
        else
        {
          cboxSummaryFor.Enabled = false;
          gbTvSeriesOptions.Enabled = false;
          gbSummaryStyle.Enabled = false;
          gbSummaryStyle.Visible = false;
          pSumHeader.Visible = false;
        }
      }
    }

    private void cbMostRecentMovPics_CheckedChanged(object sender, EventArgs e)
    {
      if (cbMostRecentMovPics.Checked)
      {
        gbMovPicsOptions.Enabled = true;

        if (rbMovPicsSummary.Checked && rbTVSeriesSummary.Checked)
        {
          cboxSummaryFor.Enabled = true;
          gbSummaryStyle.Visible = true;
          pSumHeader.Visible = true;
        }
        else
        {
          if (rbTVSeriesSummary.Checked)
          {
            cboxSummaryFor.Text = "TVSeries";
            cboxSummaryFor.Refresh();
            doSummaryFor();
            cboxSummaryFor.Enabled = false;
            gbSummaryStyle.Enabled = true;
            gbSummaryStyle.Visible = true;
            pSumHeader.Visible = true;
          }
          if (rbMovPicsSummary.Checked)
          {
            cboxSummaryFor.Text = "MovingPictures";
            cboxSummaryFor.Refresh();
            doSummaryFor();
            cboxSummaryFor.Enabled = false;
            gbSummaryStyle.Enabled = true;
            gbSummaryStyle.Visible = true;
            pSumHeader.Visible = true;
          }
        }
      }
      else
      {
        if (cbMostRecentTvSeries.Checked)
        {
          if (rbTVSeriesSummary.Checked)
          {
            cboxSummaryFor.Text = "TVSeries";
            cboxSummaryFor.Refresh();
            doSummaryFor();
            cboxSummaryFor.Enabled = false;
            gbMovPicsOptions.Enabled = false;
            gbSummaryStyle.Enabled = true;
          }
          else
          {
            gbSummaryStyle.Visible = false;
            pSumHeader.Visible = false;
          }
        }
        else
        {
          cboxSummaryFor.Enabled = false;
          gbTvSeriesOptions.Enabled = false;
          gbSummaryStyle.Enabled = false;
          gbSummaryStyle.Visible = false;
          pSumHeader.Visible = false;
        }

      }
    }

    private void rbTVSeriesSummary_CheckedChanged(object sender, EventArgs e)
    {
      if (rbTBSeriesFull.Checked)
      {
        tvSeriesRecentStyle = tvSeriesRecentType.full;
        if (rbMovPicsFull.Checked)
        {
          gbSummaryStyle.Enabled = false;
          gbSummaryStyle.Visible = false;
          pSumHeader.Visible = false;
        }
        else
        {
          cboxSummaryFor.Text = "MovingPictures";
          cboxSummaryFor.Refresh();
          doSummaryFor();
          cboxSummaryFor.Enabled = false;
        }
      }
      else
      {
        tvSeriesRecentStyle = tvSeriesRecentType.summary;
        gbSummaryStyle.Enabled = true;
        gbSummaryStyle.Visible = true;
        pSumHeader.Visible = true;
        if (rbMovPicsSummary.Checked)
        {
          cboxSummaryFor.Enabled = true;
          cboxSummaryFor.Text = "TVSeries";
          cboxSummaryFor.Refresh();
          doSummaryFor();
        }
        else
        {
          cboxSummaryFor.Text = "TVSeries";
          cboxSummaryFor.Refresh();
          doSummaryFor();
          cboxSummaryFor.Enabled = false;
        }
      }
    }



    private void rbMovPicsSummary_CheckedChanged(object sender, EventArgs e)
    {
      if (rbMovPicsFull.Checked)
      {
        movPicsRecentStyle = movPicsRecentType.full;
        if (rbTBSeriesFull.Checked)
        {
          gbSummaryStyle.Enabled = false;
          gbSummaryStyle.Visible = false;
          pSumHeader.Visible = false;
        }
        else
        {
          cboxSummaryFor.Text = "TVSeries";
          cboxSummaryFor.Refresh();
          doSummaryFor();
          cboxSummaryFor.Enabled = false;
        }
      }
      else
      {
        movPicsRecentStyle = movPicsRecentType.summary;
        gbSummaryStyle.Enabled = true;
        gbSummaryStyle.Visible = true;
        pSumHeader.Visible = true;
        if (rbTVSeriesSummary.Checked)
        {
          cboxSummaryFor.Enabled = true;
          cboxSummaryFor.Text = "MovingPictures";
          cboxSummaryFor.Refresh();
          doSummaryFor();

        }
        else
        {
          cboxSummaryFor.Text = "MovingPictures";
          cboxSummaryFor.Refresh();
          doSummaryFor();
          cboxSummaryFor.Enabled = false;
        }
      }
    }

    private void rbFanartStyle_CheckedChanged(object sender, EventArgs e)
    {
      if (rbFanartStyle.Checked)
      {
        if (cboxSummaryFor.Text == "TVSeries")
          mrTVSeriesSummStyle = mostRecentTVSeriesSummaryStyle.fanart;
        else
          mrMovPicsSummStyle = mostRecentMovPicsSummaryStyle.fanart;        
      }
      else
      {
        if (cboxSummaryFor.Text == "TVSeries")
          mrTVSeriesSummStyle = mostRecentTVSeriesSummaryStyle.poster;
        else
          mrMovPicsSummStyle = mostRecentMovPicsSummaryStyle.poster;
      }
    }

    private void cbCycleFanart_CheckStateChanged(object sender, EventArgs e)
    {
      if (cboxSummaryFor.Text == "TVSeries")
      {
        if (cbCycleFanart.Checked)
          mostRecentTVSeriesCycleFanart = true;
        else
          mostRecentTVSeriesCycleFanart = false;
      }
      else
      {
        if (cbCycleFanart.Checked)
          mostRecentMovPicsCycleFanart = true;
        else
          mostRecentMovPicsCycleFanart = false;
      }
    }

    private void cboxSummaryFor_SelectedIndexChanged(object sender, EventArgs e)
    {
      doSummaryFor();
    }

    private void btFormatOptions_Click(object sender, EventArgs e)
    {
      if (cboxSummaryFor.Text == "TVSeries")
        tvSeriesOptions.Show();
      else
        movPicsOptions.Show();
    }

    void doSummaryFor()
    {

      if (cboxSummaryFor.Text == "TVSeries")
      {
        cbCycleFanart.Checked = mostRecentTVSeriesCycleFanart;

        pbPosterPicTVSeries.Visible = true;
        pbFanartPicTVSeries.Visible = true;
        pbPosterPicMovPics.Visible = false;
        pbFanartPicMovPics.Visible = false;

        if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.fanart)
        {
          rbFanartStyle.Checked = true;
          rbPosterStyle.Checked = false;
        }
        else
        {
          rbFanartStyle.Checked = false;
          rbPosterStyle.Checked = true;
        }
      }
      else
      {
        cbCycleFanart.Checked = mostRecentMovPicsCycleFanart;

        pbPosterPicTVSeries.Visible = false;
        pbFanartPicTVSeries.Visible = false;
        pbPosterPicMovPics.Visible = true;
        pbFanartPicMovPics.Visible = true;

        if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.fanart)
        {
          rbFanartStyle.Checked = true;
          rbPosterStyle.Checked = false;
        }
        else
        {
          rbFanartStyle.Checked = false;
          rbPosterStyle.Checked = true;
        }
      }
    }

    void checkAndEnableOverlays()
    {
        if (helper.pluginEnabled(Helper.Plugins.SleepControl))
            cbSleepControlOverlay.Enabled = true;
        else
            cbSleepControlOverlay.Text += " (Not Installed)";

      if (helper.pluginEnabled(Helper.Plugins.Stocks))
        cbSocksOverlay.Enabled = true;
      else
          cbSocksOverlay.Text += " (Not Installed)";

      if (helper.pluginEnabled(Helper.Plugins.PowerControl))
        cbPowerControlOverlay.Enabled = true;
      else
          cbPowerControlOverlay.Text += " (Not Installed)";

      if (helper.pluginEnabled(Helper.Plugins.HTPCInfo))
        cbHtpcInfoOverlay.Enabled = true;
      else
        cbHtpcInfoOverlay.Text += " (Not Installed)";

      if (helper.pluginEnabled(Helper.Plugins.DriveFreeSpace))
        cbFreeDriveSpaceOverlay.Enabled = true;
      else
        cbFreeDriveSpaceOverlay.Text += " (Not Installed)";

      if (helper.pluginEnabled(Helper.Plugins.UpdateControl))
          cbUpdateControlOverlay.Enabled = true;
      else
          cbUpdateControlOverlay.Text += " (Not Installed)";

    }

    //
    // Write out a formatted xml file
    //
    public void writeXMLFile(string xmlFileName)
    {
      // Delete any existing file
      if (System.IO.File.Exists(SkinInfo.mpPaths.streamedMPpath + xmlFileName))
        System.IO.File.Delete(SkinInfo.mpPaths.streamedMPpath + xmlFileName);

      //Write tempory file
      StreamWriter tmpwriter;
      tmpwriter = System.IO.File.CreateText(Path.Combine(SkinInfo.mpPaths.streamedMPpath,xmlFileName));
      tmpwriter.Write(xml);
      tmpwriter.Close();

      checkSum.Add(Path.Combine(SkinInfo.mpPaths.streamedMPpath, xmlFileName));
    }


    public static string SkinName()
    {
      try
      {
        using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.MPSettings())
        {
          return xmlreader.GetValueAsString("skin", "name", "");
        }
      }
      catch (Exception)
      {
        return string.Empty;
      }
    }

    public class getAsmVersion
    {
      #region Private Variables
      private string _errMsg;
      private AssemblyInformation _info;
      private List<AssemblyInformation> _lstReferences;
      #endregion

      #region Public Properties
      public string ErrorMessage
      {
        get
        {
          return this._errMsg;
        }
      }
      public AssemblyInformation CurrentAssemblyInfo
      {
        get
        {
          return this._info;
        }
      }
      public List<AssemblyInformation> ReferenceAssembly
      {
        get
        {
          return this._lstReferences;
        }
      }
      #endregion

      #region Constructor
      public getAsmVersion() { }
      #endregion

      #region Private Methods
      private bool GetReferenceAssembly(Assembly asm)
      {
        try
        {
          AssemblyName[] list = asm.GetReferencedAssemblies();
          if (list.Length > 0)
          {
            AssemblyInformation info = null;
            _lstReferences = new List<AssemblyInformation>();
            for (int i = 0; i < list.Length; i++)
            {
              info = new AssemblyInformation();
              info.Name = list[i].Name;
              info.Version = list[i].Version.ToString();
              info.FullName = list[i].ToString();

              this._lstReferences.Add(info);
            }
          }
        }
        catch (Exception err)
        {
          this._errMsg = err.Message;
          return false;
        }

        return true;
      }
      #endregion

      #region Public Methods
      public bool GetVersion(string fileName)
      {
        Assembly asm = null;
        try
        {
          asm = Assembly.LoadFrom(fileName);
        }
        catch (Exception err)
        {
          this._errMsg = err.Message;
          return false;
        }
        if (asm != null)
        {
          this._info = new AssemblyInformation();
          this._info.Name = asm.GetName().Name;
          this._info.Version = asm.GetName().Version.ToString();
          this._info.FullName = asm.GetName().ToString();
        }
        else
        {
          this._errMsg = "Invalid assembly";
          return false;
        }

        return GetReferenceAssembly(asm);
      }
      public bool GetVersion(Assembly asm)
      {
        if (asm != null)
        {
          this._info = new AssemblyInformation();
          this._info.Name = asm.GetName().Name;
          this._info.Version = asm.GetName().Version.ToString();
          this._info.FullName = asm.GetName().ToString();
        }
        else
        {
          this._errMsg = "Invalid assembly";
          return false;
        }

        return GetReferenceAssembly(asm);
      }
      #endregion
    }

    public class AssemblyInformation
    {
      private string _name;
      private string _ver;
      private string _fullName;

      public string Name
      {
        set
        {
          this._name = value;
        }
        get
        {
          return this._name;
        }
      }
      public string Version
      {
        set
        {
          this._ver = value;
        }
        get
        {
          return this._ver;
        }
      }
      public string FullName
      {
        set
        {
          this._fullName = value;
        }
        get
        {
          return this._fullName;
        }
      }
    }

    public class menuItem
    {
      public string hyperlink;
      public string hyperlinkParameter;
      public string hyperlinkParameterOption;
      public bool isDefault;
      public bool isWeather;
      public string bgFolder;
      public bool fanartHandlerEnabled;
      public bool EnableMusicNowPlayingFanart;
      public string fanartProperty;
      public fanartSource fhBGSource; 
      public string name;
      public int id;
      public bool updateStatus;
      public string contextLabel;
      public string defaultImage;
      public bool disableBGSharing;
      public displayMostRecent showMostRecent;
      public int subMenuLevel1ID;
      public List<subMenuItem> subMenuLevel1 = new List<subMenuItem>();
      public List<subMenuItem> subMenuLevel2 = new List<subMenuItem>();
      public string xmlFileName;
      public string buttonTexture;
    }

    public class subMenuItem
    {
      public string displayName;
      public string baseDisplayName;
      public string xmlFileName;
      public string hyperlink;
      public string hyperlinkParameter;
      public string hyperlinkParameterOption;
      public displayMostRecent showMostRecent;
    }

    public class backgroundItem
    {
      public string name;
      public string folder;
      public string fanartPropery;
      public fanartSource fhBGSource;
      public bool fanartHandlerEnabled;
      public bool EnableMusicNowPlayingFanart;
      public string image;
      public List<string> ids = new List<string>();
      public List<string> mname = new List<string>();
      public bool isWeather;
      public bool disableBGSharing;
      public int subMenuID;
    }

    public class defaultImages
    {
      public int count;
      public int activeBGItem;
      public int activePicBox;
      public int activeSelectPbox;
      public string activeDir;
      public string[] newDefault = new string[3];
      public PictureBox[] NewPicBoxes = new PictureBox[3];
      public PictureBox[] picBoxes = new PictureBox[48];
    }

    public class prettyItem
    {
      public string name;
      public string folder;
      public string fanartProperty;
      public bool fanartHandlerEnabled;
      public string contextlabel;
      public string xmlfile;
      public bool isweather;
      public string id;
      public bool takesParmeter;
      public string pluginParmeter;
    }

    public struct editorPaths
    {
      public string sMPbaseDir;
      public string skinBasePath;
      public string cacheBasePath;
      public string configBasePath;
      public string streamedMPpath;
      public string pluginPath;
      //public string backgroundPath;
      public string fanartBasePath;
      public string thumbsPath;
    }

    public struct randomFanartSetting
    {
      public bool fanartGames;
      public bool fanartTVSeries;
      public bool fanartPlugins;
      public bool fanartMovingPictures;
      public bool fanartMusic;
      public bool fanartPictures;
      public bool fanartTv;
      public bool fanartMovies;
      public bool fanartScoreCenter;
      public bool fanartMoviesScraperFanart;
      public bool fanartMusicScraperFanart;
    }

    public struct editorValues
    {
      public bool basicHomeLoadError;
      public bool useInfoServiceSeparator;
      public int defaultId;
      public int textYOffset;
      public int weatherControl;
      public int tvseriesControl;
      public int movingPicturesControl;
      public int musicControl;
      public int tvControl;
      public int offsetMymenu;
      public int offsetSubmenu;
      public int offsetRssImage;
      public int offsetRssText;
      public int offsetTwitter;
      public int offsetTwitterImage;
      public int offsetButtons;
      public int menuHeight;
      public int subMenuHeight;
      public int subMenuXpos;
      public int subMenuWidth;
      public int subMenuTopHeight;
      public int Button3Slide;
      public string mymenu;
      public string mymenu_submenu;
      public string mymenu_submenutop;
    }
  }
}
