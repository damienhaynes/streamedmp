using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Security;
using System.Diagnostics;

namespace StreamedMPEditor
{
  public partial class streamedMpEditor
  {
    private void GetMediaPortalSkinPath()
    {
      infoserviceOptions.Enabled = false;
      fiveDayWeatherCheckBox.Enabled = false;
      summaryWeatherCheckBox.Enabled = false;

      GetMediaPortalPath(ref mpPaths);
      if (mpPaths.sMPbaseDir == null)
        return;

      readMediaPortalDirs();

      string infoServiceVer = getInfoServiceVersion();
      string rssTickerVer = getRSSTickerVersion();

      if (infoServiceVer == "InfoService Not Installed")
      {
        useInfoService.Text += "         (Disabled : Not Installed)";
        useInfoService.Enabled = false;
        useInfoService.Checked = false;
        infoserviceOptions.Enabled = false;
      }
      else if (infoServiceVer.CompareTo(baseISVer) < 0)
      {
        showError("Version " + infoServiceVer + " of InfoService Plugin detected\r\r          Version 0.9.9.3 or greater required\n\nRSS and Weather Tags changed from version 0.9.9.3\n\n          InfoService Options will be disabled", errorCode.info);
        infoserviceOptions.Enabled = false;
      }
      else if (pluginEnabled("InfoService"))
      {
        infoserviceOptions.Enabled = true;
        fiveDayWeatherCheckBox.Enabled = true;
        summaryWeatherCheckBox.Enabled = true;
        useInfoService.Text += "         (Version " + infoServiceVer + " Installed)";
        if (infoServiceVer.CompareTo(isSeperatorVer) >= 0)
          useInfoServiceSeperator = true;
      }
      else
      {
        useInfoService.Text += "         (Disabled : Not Enabled)";
        useInfoService.Enabled = false;
        useInfoService.Checked = false;
        infoserviceOptions.Enabled = false;
      }


      if (rssTickerVer == "MP-RSSTicker Not Installed")
      {
        rssTickerOptions.Enabled = false;
        useRSSTicker.Enabled = false;
        useRSSTicker.Checked = false;
        useRSSTicker.Text += "   (Disabled : Not Installed)";
      }
      else
      {
        if (pluginEnabled("MP-RSSTicker"))
        {
          useRSSTicker.Enabled = true;
          useRSSTicker.Text += "   (Version " + rssTickerVer + " Installed)";
        }
        else
        {
          rssTickerOptions.Enabled = false;
          useRSSTicker.Enabled = false;
          useRSSTicker.Checked = false;
          useRSSTicker.Text += "   (Disabled : Not Enabled)";
        }
      }

      if (!useInfoService.Enabled && useRSSTicker.Enabled)
        useRSSTicker.Checked = true;

      if (useInfoService.Checked && !useRSSTicker.Enabled)
        useInfoService.Checked = true;


      // Display some Info
      infoSkinName.Text = configuredSkin("name") + " (" + getStreamedMPVer() + ")";
      infoSkinpath.Text = mpPaths.streamedMPpath;
      infoInstallPath.Text = mpPaths.sMPbaseDir + "  (Version: " + getMediaPortalVersion() + ")";
      infoConfigpath.Text = mpPaths.configBasePath;


      if (infoServiceVer == "InfoService Not Installed")
        infoserviceOptions.Text = "InfoService Options (Disabled " + infoServiceVer + " )";
      else
        infoserviceOptions.Text = "InfoService Options (InfoService V" + infoServiceVer + " Installed)";
    }

    private string getStreamedMPVer()
    {
      XmlDocument doc = new XmlDocument();
      if (!File.Exists(mpPaths.streamedMPpath + "streamedmp.version.xml"))
      {
        showError("Can't find StreamedMP version information\r\r" + mpPaths.streamedMPpath + "streamedmp.version.xml\r\rThis editor is for StreamedMP skin", errorCode.major);
        return null;
      }

      doc.Load(mpPaths.streamedMPpath + "streamedmp.version.xml");
      XmlNode node = doc.DocumentElement.SelectSingleNode("/window/controls/control/label");
      return node.InnerText;
    }

    private void readMediaPortalDirs()
    {
      string fMPdirs = mpPaths.sMPbaseDir + "\\MediaPortalDirs.xml";
      XmlDocument doc = new XmlDocument();
      if (!File.Exists(fMPdirs))
      {
        showError("Can't find MediaPortalDirs.xml \r\r" + fMPdirs, errorCode.major);
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
            mpPaths.skinBasePath = GetMediaPortalDir(path.InnerText);
          }
        }

        // get the Cache base path
        if (innerNode.InnerText == "Cache")
        {
          XmlNode path = node.SelectSingleNode("Path");
          if (path != null)
          {
            mpPaths.cacheBasePath = GetMediaPortalDir(path.InnerText);
          }

        }

        // get the Config base path
        if (innerNode.InnerText == "Config")
        {
          XmlNode path = node.SelectSingleNode("Path");
          if (path != null)
          {
            mpPaths.configBasePath = GetMediaPortalDir(path.InnerText);
          }
        }

        // get the Plugin base path
        if (innerNode.InnerText == "Plugins")
        {
          XmlNode path = node.SelectSingleNode("Path");
          if (path != null)
          {
            mpPaths.pluginPath = GetMediaPortalDir(path.InnerText);
            
          }
        }
      }
      mpPaths.streamedMPpath = mpPaths.skinBasePath + configuredSkin("name") + "\\";
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
        path = Path.Combine(mpPaths.sMPbaseDir, path);
      }

      // Check if there is a trailing backslash
      if (!path.EndsWith(@"\"))
      {
        path += @"\";
      }

      return path;
    }
 


    private bool pluginEnabled(string pluginName)
    {
      string fMPdirs = mpPaths.configBasePath + "MediaPortal.xml";
      XmlDocument doc = new XmlDocument();
      if (!File.Exists(fMPdirs))
      {
        showError("Can't find MediaPortal.xml \r\r" + fMPdirs, errorCode.major);
        return false; ;
      }
      doc.Load(fMPdirs);
      XmlNodeList nodeList = doc.DocumentElement.SelectNodes("/profile/section");
      foreach (XmlNode node in nodeList)
      {
        XmlNode innerNode = node.Attributes.GetNamedItem("name");

        // get the currently configured plugins
        if (innerNode.InnerText == "plugins")
        {
          XmlNode path = node.SelectSingleNode("entry[@name=\"" + pluginName + "\"]");
          if (path != null)
          {
            if (path.InnerText.ToLower() == "no")
              return false;
            else
              return true;
          }
        }
      }
      return false;
    }

    private string configuredSkin(string elementName)
    {
      string fMPdirs = mpPaths.configBasePath + "MediaPortal.xml";
      string entryValue;
      XmlDocument doc = new XmlDocument();
      if (!File.Exists(fMPdirs))
      {
        showError("Can't find MediaPortal.xml \r\r" + fMPdirs, errorCode.major);
        return "Error";
      }

      doc.Load(fMPdirs);
      XmlNodeList nodeList = doc.DocumentElement.SelectNodes("/profile/section");

      foreach (XmlNode node in nodeList)
      {

        XmlNode innerNode = node.Attributes.GetNamedItem("name");

        // get the currently configured Skin name
        if (innerNode.InnerText == "skin")
        {

          XmlNode path = node.SelectSingleNode("entry[@name=\"" + elementName + "\"]");
          if (path != null)
          {
            entryValue = path.InnerText;
            return entryValue;

          }
        }
      }
      return null;
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
        showError("Exception while attempting to read MediaPortal location from registry\n\nMediaPortal must be installed, is MediaPortal Installed?\n\n" + e.Message, errorCode.major);
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
        foreach (prettyItem p in prettyItems)
        {
          if (p.id == selectedID)
          {
            // Populate
            QuickSelect(i);
            cboQuickSelect.SelectedIndex = i;
            bFound = true;
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
      itemName.Text = prettyItems[index].name;
      bgBox.Text = prettyItems[index].folder;
      isWeather.Checked = prettyItems[index].isweather;
      selectedWindow.Text = prettyItems[index].xmlfile;
      selectedWindowID.Text = prettyItems[index].id;
    }

    private void ClearItems()
    {
      itemName.Text = "";
      bgBox.Text = "";
      cboContextLabel.Text = "";
      isWeather.Checked = false;

    }

    private void cboQuickSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
      // Auto fill items on new selection for quicker add
      QuickSelect(cboQuickSelect.SelectedIndex);
    }



    private void BasicHomeFromTemplate()
    {
      System.IO.StreamWriter writer;
      string backupFilename = mpPaths.streamedMPpath + "BasicHome.xml.backup." + DateTime.Now.Ticks.ToString();

      if (System.IO.File.Exists(mpPaths.streamedMPpath + "BasicHome.xml"))
        System.IO.File.Copy(mpPaths.streamedMPpath + "BasicHome.xml", backupFilename);

      if (System.IO.File.Exists(mpPaths.streamedMPpath + "BasicHome.xml"))
        System.IO.File.Delete(mpPaths.streamedMPpath + "BasicHome.xml");

      Stream streamTemplate = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.xmlFiles.AeonBasicHomeDefault.xml");
      StreamReader reader = new StreamReader(streamTemplate);
      xmlTemplate = reader.ReadToEnd();

      writer = System.IO.File.CreateText(mpPaths.streamedMPpath + "BasicHome.xml");
      writer.Write(xmlTemplate);
      writer.Close();
      showError("Existing BasicHome.xml Saved Sucessfully as\n\n" + backupFilename + "\n\nNew BasicHome.xml created from Internal template", errorCode.info);

      foreach (menuItem item in menuItems)
      {
        item.id = menuItems.IndexOf(item);
      }
    }

    private void verticalStyle_Click(object sender, EventArgs e)
    {
      menuStyle = chosenMenuStyle.verticalStyle;
      syncEditor(sync.editing);
    }

    private void horizontalStyle_Click(object sender, EventArgs e)
    {
      menuStyle = chosenMenuStyle.horizontalStandardStyle;
      syncEditor(sync.editing);
    }

    private void horizontalStyle2_Click(object sender, EventArgs e)
    {
      menuStyle = chosenMenuStyle.horizontalContextStyle;
      syncEditor(sync.editing);
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

    private void UpdateImageControlVisibility()
    {
      if (checkBoxMultiImage.Checked)
      {
        //textBoxDefaultImage.Enabled = false;
        timeBox.Enabled = true;
        randomChk.Enabled = true;
        timeBox.Visible = true;
        randomChk.Visible = true;
        timePerImageL.Visible = true;
        secondsL.Visible = true;

      }
      else
      {
        //textBoxDefaultImage.Enabled = true;
        timeBox.Enabled = false;
        randomChk.Enabled = false;
        timeBox.Visible = false;
        randomChk.Visible = false;
        timePerImageL.Visible = false;
        secondsL.Visible = false;


      }
    }
    private void reloadBackgroundItems()
    {
      bgItems.Clear();
      foreach (menuItem menItem in menuItems)
      {
        fillBackgroundItem(menItem);
      }
    }

    private void fillBackgroundItem(menuItem menItem)
    {
        bool newBG = true;
        foreach (backgroundItem bgitem in bgItems)
        {
          if (bgitem.folder == menItem.bgFolder)
          {
            bgitem.ids.Add(menItem.id.ToString());
            bgitem.mname.Add(menItem.name.ToString());
            bgitem.name = bgitem.name + ", " + menItem.name;
            newBG = false;
          }

        }
        if (newBG == true)
        {
          backgroundItem newbgItem = new backgroundItem();
          newbgItem.folder = menItem.bgFolder;
          newbgItem.ids.Add(menItem.id.ToString());
          newbgItem.mname.Add(menItem.name.ToString());
          newbgItem.name = menItem.name;
          newbgItem.image = menItem.defaultImage;
          newbgItem.random = menItem.random;
          newbgItem.timeperimage = menItem.timePerImage.ToString();
          newbgItem.isWeather = menItem.isWeather;
          bgItems.Add(newbgItem);
        }
    }

    private void setBasicHomeValues()
    {

      basicHomeValues.offsetMymenu = -39;
      basicHomeValues.textYOffset = 6;
      basicHomeValues.offsetSubmenu = 76;
      basicHomeValues.offsetRssImage = 76;
      basicHomeValues.offsetRssText = 75;
      basicHomeValues.offsetTwitter = 51;
      basicHomeValues.offsetTwitterImage = 32;
      basicHomeValues.offsetButtons = 42;
      basicHomeValues.menuHeight = 165;
      basicHomeValues.subMenuHeight = 60;
      basicHomeValues.subMenuXpos = 0;
      basicHomeValues.subMenuWidth = 1280;
      basicHomeValues.subMenuTopHeight = 60;
      basicHomeValues.Button3Slide = 55;

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
          break;
        case chosenMenuStyle.horizontalStandardStyle:
          if (horizontalContextLabels.Checked)
          {
            basicHomeValues.menuHeight += 34;
            basicHomeValues.offsetMymenu -= 25;
            basicHomeValues.offsetButtons += 16;
          }
          break;
        case chosenMenuStyle.horizontalContextStyle:
          basicHomeValues.menuHeight += 33;
          basicHomeValues.offsetMymenu -= 24;
          basicHomeValues.offsetButtons += 16;
          break;
      }




    }

    private string getInfoServiceVersion()
    {

      if (!File.Exists(mpPaths.pluginPath + "\\windows\\infoservice.dll"))
      {
        //showError("Can't find InfoService Plugin\r\r" + mpPaths.pluginPath + "windows\\infoservice.dll\n\nInfoService Options will be Disabled", errorCode.info);
        return "InfoService Not Installed";
      }


      getAsmVersion ver = new getAsmVersion();
      if (ver.GetVersion(mpPaths.pluginPath + "\\windows\\infoservice.dll"))
      {
        AssemblyInformation info = ver.CurrentAssemblyInfo;
        return info.Version;
      }
      else
        showError(ver.ErrorMessage, errorCode.major);
      return "";

    }

    private string getRSSTickerVersion()
    {

      if (!File.Exists(mpPaths.pluginPath + "\\process\\MP-RSSTicker.dll"))
      {
        //showError("Can't find MP-RSSTicker Plugin\r\r" + mpPaths.pluginPath + "process\\MP-RSSTicker.dll\n\nMP-RSSTicker Options will be Disabled", errorCode.info);
        return "MP-RSSTicker Not Installed";
      }


      getAsmVersion ver = new getAsmVersion();
      if (ver.GetVersion(mpPaths.pluginPath + "\\process\\MP-RSSTicker.dll"))
      {
        AssemblyInformation info = ver.CurrentAssemblyInfo;
        return info.Version;
      }
      else
        showError(ver.ErrorMessage, errorCode.major);
      return "";

    }

    private string getMediaPortalVersion()
    {
      if (!File.Exists(mpPaths.sMPbaseDir + "\\MediaPortal.exe"))
      {
        showError("Can't find MediaPortal! \r\r" + mpPaths.sMPbaseDir + "MediaPortal.exe", errorCode.major);
      }
      FileVersionInfo mpFileVersion = FileVersionInfo.GetVersionInfo(mpPaths.sMPbaseDir + "\\MediaPortal.exe");
      return mpFileVersion.FileVersion;
    }


    private DialogResult showError(string errorText, errorCode errorType)
    {


      switch (errorType)
      {
        case errorCode.info:
          MessageBox.Show(errorText,
              "OK to Continue",
              MessageBoxButtons.OK,
              MessageBoxIcon.Information,
              MessageBoxDefaultButton.Button1);
          break;
        case errorCode.infoQuestion:
          return MessageBox.Show(errorText,
              "Continue Editing",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button1);
        case errorCode.loadError:
          MessageBox.Show("Error loading menu, file seems invalid\r\rReason: " + errorText + "  ",
              "OK to Continue",
              MessageBoxButtons.OK,
              MessageBoxIcon.Error,
              MessageBoxDefaultButton.Button1);
          break;
        case errorCode.readError:
          MessageBox.Show("Error loading menu, file seems invalid\r\rReason: " + errorText + "  ",
              "OK to Continue",
              MessageBoxButtons.OK,
              MessageBoxIcon.Error,
              MessageBoxDefaultButton.Button1);
          break;
        case errorCode.major:
          MessageBox.Show(errorText + "\r\rEditor will now Terminate",
              "OK to Exit",
              MessageBoxButtons.OK,
              MessageBoxIcon.Error,
              MessageBoxDefaultButton.Button1);
          this.Close();
          break;
      }

      return DialogResult;

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
    }

    private void removeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (itemsOnMenubar.SelectedItem != null)
      {
        menuItems.RemoveAt(itemsOnMenubar.SelectedIndex);
        itemsOnMenubar.Items.Remove(itemsOnMenubar.SelectedItem);
        screenReset();


      }
      else
        showError("No menu item selected to Remove\n\nPlease select menu item to Remove", errorCode.info);
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

    private void fillBackgroundItems()
    {
      foreach (menuItem menItem in menuItems)
      {
        bool newBG = true;
        foreach (backgroundItem bgitem in bgItems)
        {
          if (bgitem.folder == menItem.bgFolder)
          {
            bgitem.ids.Add(menItem.id.ToString());
            bgitem.mname.Add(menItem.name.ToString());
            bgitem.name = bgitem.name + " " + menItem.name;
            newBG = false;
          }

        }
        if (newBG == true)
        {
          backgroundItem newbgItem = new backgroundItem();
          newbgItem.folder = menItem.bgFolder;
          newbgItem.ids.Add(menItem.id.ToString());
          newbgItem.mname.Add(menItem.name.ToString());
          newbgItem.name = menItem.name;
          newbgItem.image = menItem.defaultImage;
          newbgItem.random = menItem.random;
          newbgItem.timeperimage = menItem.timePerImage.ToString();
          newbgItem.isWeather = menItem.isWeather;
          bgItems.Add(newbgItem);

        }
      }
    }
    private void btnClearCache_Click(object sender, EventArgs e)
    {
      DialogResult result = showError("Clearing cache\n\n" + mpPaths.cacheBasePath + configuredSkin("name") + "\n\nPlease confirm clearing of the cache", errorCode.infoQuestion);
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
        System.IO.Directory.Delete(mpPaths.cacheBasePath + configuredSkin("name"), true);
        //showError("Skin cache has been cleared\n\nOk To Continue", errorCode.info);
      }
      catch (Exception ex)
      {
        //showError("Exception while deleteing Cache\n\n" + ex.Message, errorCode.info);
      }


    }

    private string weatherIcon(string theDay)
    {
      if (WeatherIconsAnimated.Checked)
      {
        // relative from Animations folder
        return "weathericons\\animated\\128x128\\#infoservice.weather." + theDay + ".img.big.filenamewithoutext"; 
      }
      else
      {
        // relative from Media folder
        return "animations\\weathericons\\static\\128x128\\#infoservice.weather." + theDay + ".img.big.filenamewithoutext.png";
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
        // find the longest Context and Menu items
        foreach (menuItem menItem in menuItems)
        {
          if (maxContextSize < menItem.contextLabel.Length)
            maxContextSize = menItem.contextLabel.Length;
          if (maxMenuItemSize < menItem.name.Length)
            maxMenuItemSize = menItem.name.Length;
        }
        // now calc the minimum xpos based on longest sring in context and menu labels
        minXPos = (maxContextSize * 17);
        if ((maxMenuItemSize * 41) > minXPos)
          minXPos = (maxMenuItemSize * 41);


        if (menuOffset < minXPos)
        {
          txtMenuPos.Text = minXPos.ToString();
          menuOffset = int.Parse(txtMenuPos.Text);
          showError("The Menu X Position value will result in blank Context or Menu labels. \n\nMenu X Position reset to calculated minium value of " + txtMenuPos.Text, errorCode.info);
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
      if (!animatedIconsInstalled())
        WeatherIconsAnimated.Enabled = false;

      switch (menuStyle)
      {
        case chosenMenuStyle.verticalStyle:
          fiveDayWeatherCheckBox.Checked = true;
          horizontalContextLabels.Enabled = false;
          weatherSummaryGroup.Visible = false;
          verticalStyle.Checked = true;
          weatherIconsStatic.Checked = true;          
          useAeonGraphics.Visible = false;
          txtMenuPos.Text = "350";
          menuPosLabel.Text = "Menu X Position:";
          break;

        case chosenMenuStyle.horizontalStandardStyle:
          weatherStyle = chosenWeatherStyle.bottom;
          horizontalContextLabels.Checked = false;
          fiveDayWeatherCheckBox.Checked = true;
          fullWeatherSummaryBottom.Enabled = true;
          fullWeatherSummaryBottom.Checked = true;
          horizontalContextLabels.Enabled = true;
          horizontalStyle.Checked = true;
          weatherIconsStatic.Checked = true;
          weatherSummaryGroup.Visible = true;          
          useAeonGraphics.Visible = false;
          txtMenuPos.Text = "430";
          break;

        case chosenMenuStyle.horizontalContextStyle:
          weatherStyle = chosenWeatherStyle.middle;
          horizontalContextLabels.Checked = true;
          fiveDayWeatherCheckBox.Checked = true;
          fullWeatherSummaryMiddle.Checked = true;
          fullWeatherSummaryBottom.Enabled = false;
          horizontalContextLabels.Enabled = true;
          horizontalStyle2.Checked = true;
          weatherIconsStatic.Checked = true;
          weatherSummaryGroup.Visible = true;          
          useAeonGraphics.Visible = false;
          txtMenuPos.Text = "620";
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
      getFileListing(mpPaths.configBasePath, "usermenuprofile.xml.backup*");
      numUPBackups.Text = totalImages.ToString();
      getFileListing(mpPaths.streamedMPpath, "BasicHome.xml.backup.*");
      numBHBackups.Text = totalImages.ToString();
    }

    private void StreamedMPMenu_Selected(object sender, TabControlEventArgs e)
    {
      if (lastUsedTab.Checked)
      {
        Properties.Settings.Default.rememberLastUsedTab = true;
        Properties.Settings.Default.lastUsedTab = StreamedMPMenu.SelectedIndex;
      }
    }

    private void streamedMpEditor_FormClosing(object sender, FormClosingEventArgs e)
    {
      int versionCount = 0;
      Properties.Settings.Default.autoPurge = autoPurgeBackups.Checked;
      if (autoPurgeBackups.Checked)
      {
        Properties.Settings.Default.keepVersions = int.Parse(backupVersionsToKeep.Text);
        Properties.Settings.Default.autoPurge = true;

        string[] filesToDelete = getFileListing(mpPaths.configBasePath, "usermenuprofile.xml.backup.*");
        foreach (string file in filesToDelete)
        {
          if (versionCount >= int.Parse(backupVersionsToKeep.Text))
            System.IO.File.Delete(file);
          versionCount++;
        }
        versionCount = 0;

        string[] filesToDelete2 = getFileListing(mpPaths.streamedMPpath, "BasicHome.xml.backup.*");
        foreach (string file in filesToDelete2)
        {
          if (versionCount >= int.Parse(backupVersionsToKeep.Text))
            System.IO.File.Delete(file);
          versionCount++;
        }
      }
      Properties.Settings.Default.Save();
    }

    private void purgeUPBackups_Click(object sender, EventArgs e)
    {
      string[] filesToDelete = getFileListing(mpPaths.configBasePath, "usermenuprofile.xml.backup.*");
      foreach (string file in filesToDelete)
      {
        System.IO.File.Delete(file);
      }
      getBackupFileTotals();
    }

    private void purgeBHBackups_Click(object sender, EventArgs e)
    {
      string[] filesToDelete = getFileListing(mpPaths.streamedMPpath, "BasicHome.xml.backup.*");
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
      if (System.IO.Directory.Exists(mpPaths.configBasePath))
        System.Diagnostics.Process.Start(mpPaths.configBasePath);
      else MessageBox.Show("The directory/file does not exist.");
    }

    private void showMPDir_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (System.IO.Directory.Exists(mpPaths.sMPbaseDir))
        System.Diagnostics.Process.Start(mpPaths.sMPbaseDir);
      else MessageBox.Show("The directory/file does not exist.");

    }

    private void showSkinDir_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (System.IO.Directory.Exists(mpPaths.streamedMPpath))
        System.Diagnostics.Process.Start(mpPaths.streamedMPpath);
      else MessageBox.Show("The directory/file does not exist.");
    }

    private bool animatedIconsInstalled()
    {
      if (Directory.Exists(mpPaths.streamedMPpath + "media\\animations\\weathericons\\animated\\128x128"))
        return true;
      else
        return false;
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
      public bool isDefault;
      public bool isWeather;
      public string bgFolder;
      public string name;
      public int id;
      public int timePerImage;
      public bool random;
      public bool updateStatus;
      public string contextLabel;
      public string defaultImage;
    }

    public class backgroundItem
    {
      public string name;
      public string folder;
      public string image;
      public List<string> ids = new List<string>();
      public List<string> mname = new List<string>();
      public bool random;
      public string timeperimage;
      public bool isWeather;
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
      public PictureBox[] picBoxes = new PictureBox[24];
    }

    public class prettyItem
    {
      public string name;
      public string name2;
      public string folder;
      public string contextlabel;
      public string xmlfile;
      public bool isweather;
      public string id;
    }

    public struct editorPaths
    {
      public string sMPbaseDir;
      public string skinBasePath;
      public string cacheBasePath;
      public string configBasePath;
      public string streamedMPpath;
      public string pluginPath;
      public string backgroundPath;
    }

    public struct editorValues
    {
      public bool basicHomeLoadError;
      public bool useInfoServiceSeperator;
      public int defaultId;
      public int textYOffset;
      public int weatherControl;
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
