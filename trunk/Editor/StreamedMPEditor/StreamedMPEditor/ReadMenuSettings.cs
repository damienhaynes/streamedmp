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
    public void loadMenuSettings()
    {
      string defaultcontrol = null;
      string defaultImage = null;
      string usermenuprofile = mpPaths.configBasePath + "usermenuprofile.xml";
      string id = null;
   
      menuItems.Clear();
      itemsOnMenubar.Items.Clear();

      XmlDocument doc = new XmlDocument();
      //
      // Open the usermenu settings file - NOTE: need to check for it in correct location, if not found look in skin dir for default version
      //
      if (!File.Exists(usermenuprofile))
      {
        // Ok, so no usermenuprofile.xml exists, this is most likely because this is a new skin install and this is the first time
        // the editor has been run and the file has not yet been created in the default location.
        // Check for and load the default version supplied with the skin
        usermenuprofile = mpPaths.streamedMPpath + "usermenuprofile.xml";
        if (!File.Exists(usermenuprofile))
        {
          //ok, so now really in trouble, throw an error to the user and bailout!
          showError("Can't find usermenuprofile.xml \r\r" + mpPaths.configBasePath + "usermenuprofile.xml", errorCode.major);
        }
      }
      try
      {
        doc.Load(usermenuprofile);
      }
      catch (Exception e)
      {
        showError("Exception while loading usermenuprofile.xml\n\n" + e.Message, errorCode.loadError);
        basicHomeLoadError = true;
      }
      XmlNodeList nodelist = doc.DocumentElement.SelectNodes("/profile/section");

      // Get the Focus Colour and set the background on the control
      focusAlpha.Text = readEntryValue("StreamedMP Options", "menuitemFocus", nodelist).Substring(0, 2);
      try
      {
        string RGB = defFocus;
        RGB = readEntryValue("StreamedMP Options", "menuitemFocus", nodelist).Substring(2);
        Color col = ColorFromRGB(RGB);
        txtFocusColour.BackColor = col;
        txtFocusColour.ForeColor = ColorInvert(col);
        txtFocusColour.Text = RGB;
      }
      catch
      {
        txtFocusColour.Text = defFocus;
      }

      // Get the NoFocus Colour and set the background on the control
      noFocusAlpha.Text = readEntryValue("StreamedMP Options", "menuitemNoFocus", nodelist).Substring(0, 2);
      try
      {
        string RGB = defUnFocus;
        RGB = readEntryValue("StreamedMP Options", "menuitemNoFocus", nodelist).Substring(2);
        Color col = ColorFromRGB(RGB);
        txtNoFocusColour.BackColor = col;
        txtNoFocusColour.ForeColor = ColorInvert(col);
        txtNoFocusColour.Text = RGB;
      }
      catch
      {
        txtNoFocusColour.Text = defUnFocus;
      }

      // Check menu orientation
      if (readEntryValue("StreamedMP Options", "menuType", nodelist) == "Vertical")
      {
          verticalStyle.Checked = true;
          horizontalStyle.Checked = false;
          menuPosLabel.Text = "Menu X Position:";
          txtMenuPos.Text = readEntryValue("StreamedMP Options", "menuXPos", nodelist);
      }
      else
      {
          verticalStyle.Checked = false;
          horizontalStyle.Checked = true;
          menuPosLabel.Text = "Menu Y Position:";
          txtMenuPos.Text = readEntryValue("StreamedMP Options", "menuYPos", nodelist);
      }
      //
      // Check and set the Global and Plugin options
      //
      checkBoxMultiImage.Checked      = bool.Parse(readEntryValue("StreamedMP Options", "multiimage", nodelist));
      tbAcceleration.Text             = readEntryValue("StreamedMP Options", "acceleration", nodelist);
      tbDuration.Text                 = readEntryValue("StreamedMP Options", "duration", nodelist);
      cbDropShadow.Checked            = bool.Parse(readEntryValue("StreamedMP Options", "dropShadow", nodelist));
      enableRssfeed.Checked           = bool.Parse(readEntryValue("StreamedMP Options", "enableRssfeed", nodelist));
      enableTwitter.Checked           = bool.Parse(readEntryValue("StreamedMP Options", "enableTwitter", nodelist));
      wrapString.Checked              = bool.Parse(readEntryValue("StreamedMP Options", "wrapString", nodelist));
      weatherBGlink.Checked           = bool.Parse(readEntryValue("StreamedMP Options", "weatherBGlink", nodelist));
      fiveDayWeatherCheckBox.Checked  = bool.Parse(readEntryValue("StreamedMP Options", "fiveDayWeatherCheckBox", nodelist));
      summaryWeatherCheckBox.Checked  = bool.Parse(readEntryValue("StreamedMP Options", "summaryWeatherCheckBox", nodelist));

      //
      // Read in the menu items
      //
      for (int i = 0; i < Convert.ToInt64(readEntryValue("Menu Items", "count", nodelist)); i++)
      {
        menuItem mnuItem = new menuItem();
        mnuItem.name          = readEntryValue("Menu Items", "menuitem" + i.ToString() + "name", nodelist);
        mnuItem.contextLabel  = readEntryValue("Menu Items", "menuitem" + i.ToString() + "label", nodelist);
        mnuItem.bgFolder      = readEntryValue("Menu Items", "menuitem" + i.ToString() + "folder", nodelist);
        mnuItem.hyperlink     = readEntryValue("Menu Items", "menuitem" + i.ToString() + "hyperlink", nodelist);
        mnuItem.isDefault     = bool.Parse(readEntryValue("Menu Items", "menuitem" + i.ToString() + "isdefault", nodelist));
        mnuItem.isWeather     = bool.Parse(readEntryValue("Menu Items", "menuitem" + i.ToString() + "isweather", nodelist));
        mnuItem.random        = bool.Parse(readEntryValue("Menu Items", "menuitem" + i.ToString() + "random", nodelist));
        mnuItem.updateStatus  = bool.Parse(readEntryValue("Menu Items", "menuitem" + i.ToString() + "updatestatus", nodelist));
        mnuItem.id            = int.Parse(readEntryValue("Menu Items", "menuitem" + i.ToString() + "id", nodelist));
        mnuItem.timePerImage  = int.Parse(readEntryValue("Menu Items", "menuitem" + i.ToString() + "timeonpage", nodelist));

        isWeather.Checked = mnuItem.isWeather;
        randomChk.Checked = mnuItem.random;
        
        // Set the default control
        if (mnuItem.isDefault)
          defaultcontrol = mnuItem.id.ToString();
        id = mnuItem.id.ToString();
        itemsOnMenubar.Items.Add(mnuItem.name, id.Equals(defaultcontrol)); 

        // If user decides not to use multiimage backgrounds then we need a default image, lets check and set if one is required
        defaultImage = readEntryValue("Menu Items", "menuitem" + i.ToString() + "defaultimage", nodelist);
        if (defaultImage.StartsWith("animations"))
          mnuItem.defaultImage = defaultImage;
        else
        {
          if (!mnuItem.bgFolder.Contains("\\"))
            mnuItem.defaultImage = "animations\\" + mnuItem.bgFolder + "\\default.jpg";
          else
            mnuItem.defaultImage = mnuItem.bgFolder + "\\default.jpg";
        }
     
        menuItems.Add(mnuItem);
      }
      fillBackgroundItems();
      UpdateImageControlVisibility();
      generateMenu.Enabled = true;
    }
  }
}


