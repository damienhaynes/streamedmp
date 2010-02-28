﻿using System;
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
            string _selectedFont = null;
            string _labelFont = null;
            string activeRssImageType = null;
            string targetScreenRes = null;


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

            //
            // Get the version of usermenuprofile
            //
            XmlNode versionControlNode = doc.DocumentElement.SelectSingleNode("/profile/version");
            string versionNum = null, optionsTag = null, menuTag = null;

            if (versionControlNode != null)
            {
                versionNum = versionControlNode.InnerText;
            }
            switch (versionNum)
            {
                case "1.0":
                    optionsTag = "StreamedMP Options";
                    menuTag = "StreamedMP Menu Items";
                    break;
                default:
                    optionsTag = "StreamedMP Options";
                    menuTag = "Menu Items";
                    break;
            }

            // Now read the file
            XmlNodeList nodelist = doc.DocumentElement.SelectNodes("/profile/skin");
            // Get the last selected Menu...
            try
            {
                switch (readEntryValue(optionsTag, "menustyle", nodelist))
                {
                    case "verticalStyle":
                        menuStyle = chosenMenuStyle.verticalStyle;
                        verticalStyle.Checked = true;
                        cboLabelFont.Enabled = false;
                        cboSelectedFont.Enabled = false;
                        cboSelectedFont.Text = "mediastream16tc";
                        cboLabelFont.Text = "mediastream28tc";
                        break;
                    case "horizontalStandardStyle":
                        menuStyle = chosenMenuStyle.horizontalStandardStyle;
                        horizontalStyle.Checked = true;
                        cboSelectedFont.Text = "mediastream28tc";
                        cboLabelFont.Text = "mediastream28tc";
                        break;
                    case "horizontalContextStyle":
                        menuStyle = chosenMenuStyle.horizontalContextStyle;
                        horizontalStyle2.Checked = true;
                        cboSelectedFont.Text = "mediastream28tc";
                        cboLabelFont.Text = "mediastream28tc";
                        break;
                    default:
                        menuStyle = chosenMenuStyle.verticalStyle;
                        verticalStyle.Checked = true;
                        cboLabelFont.Enabled = false;
                        cboSelectedFont.Enabled = false;
                        cboSelectedFont.Text = "mediastream16tc";
                        cboLabelFont.Text = "mediastream28tc";
                        break;
                }

                //...and Weather styles
                if (readEntryValue(optionsTag, "weatherstyle", nodelist) == "bottom")
                {
                    weatherStyle = chosenWeatherStyle.bottom;
                }
                else if (readEntryValue(optionsTag, "weatherstyle", nodelist) == "middle")
                {
                    weatherStyle = chosenWeatherStyle.middle;
                }
                else
                {
                    weatherStyle = chosenWeatherStyle.bottom;
                }
            }
            catch { }

            // Get the Focus Colour and set the background on the control
            focusAlpha.Text = readEntryValue(optionsTag, "menuitemFocus", nodelist).Substring(0, 2);
            try
            {
                string RGB = defFocus;
                RGB = readEntryValue(optionsTag, "menuitemFocus", nodelist).Substring(2);
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
            noFocusAlpha.Text = readEntryValue(optionsTag, "menuitemNoFocus", nodelist).Substring(0, 2);
            try
            {
                string RGB = defUnFocus;
                RGB = readEntryValue(optionsTag, "menuitemNoFocus", nodelist).Substring(2);
                Color col = ColorFromRGB(RGB);
                txtNoFocusColour.BackColor = col;
                txtNoFocusColour.ForeColor = ColorInvert(col);
                txtNoFocusColour.Text = RGB;
            }
            catch
            {
                txtNoFocusColour.Text = defUnFocus;
            }


            // Line up all the options this also sets the defaults for the style
            // which can be overidden by user settings below
            syncEditor(sync.OnLoad);

            // Re-read and set menupos and 5 day weather location
            if (readEntryValue(optionsTag, "weatherstyle", nodelist) == "bottom")
            {
                weatherStyle = chosenWeatherStyle.bottom;
            }
            else if (readEntryValue(optionsTag, "weatherstyle", nodelist) == "middle")
            {
                weatherStyle = chosenWeatherStyle.middle;
            }
            else
            {
                weatherStyle = chosenWeatherStyle.bottom;
            }
            if (readEntryValue(optionsTag, "menustyle", nodelist) == "verticalStyle")
                txtMenuPos.Text = readEntryValue(optionsTag, "menuXPos", nodelist);
            else
                txtMenuPos.Text = readEntryValue(optionsTag, "menuYPos", nodelist);



            //
            // Check and set the Global and Plugin options and apply any customization by user
            //
            try
            {
                _selectedFont = readEntryValue(optionsTag, "selectedFont", nodelist);
                _labelFont = readEntryValue(optionsTag, "labelFont", nodelist);
                tbAcceleration.Text = readEntryValue(optionsTag, "acceleration", nodelist);
                tbDuration.Text = readEntryValue(optionsTag, "duration", nodelist);
                cbDropShadow.Checked = bool.Parse(readEntryValue(optionsTag, "dropShadow", nodelist));
                enableRssfeed.Checked = bool.Parse(readEntryValue(optionsTag, "enableRssfeed", nodelist));
                enableTwitter.Checked = bool.Parse(readEntryValue(optionsTag, "enableTwitter", nodelist));
                wrapString.Checked = bool.Parse(readEntryValue(optionsTag, "wrapString", nodelist));
                weatherBGlink.Checked = bool.Parse(readEntryValue(optionsTag, "weatherBGlink", nodelist));
                enableFiveDayWeather.Checked = bool.Parse(readEntryValue(optionsTag, "fiveDayWeatherCheckBox", nodelist));
                summaryWeatherCheckBox.Checked = bool.Parse(readEntryValue(optionsTag, "summaryWeatherCheckBox", nodelist));
                cboClearCache.Checked = bool.Parse(readEntryValue(optionsTag, "cboClearCache", nodelist));
                WeatherIconsAnimated.Checked = bool.Parse(readEntryValue(optionsTag, "animatedWeather", nodelist));
                horizontalContextLabels.Checked = bool.Parse(readEntryValue(optionsTag, "horizontalContextLabels", nodelist));
                fullWeatherSummaryBottom.Checked = bool.Parse(readEntryValue(optionsTag, "fullWeatherSummaryBottom", nodelist));
                fullWeatherSummaryMiddle.Checked = bool.Parse(readEntryValue(optionsTag, "fullWeatherSummaryMiddle", nodelist));
                activeRssImageType = readEntryValue(optionsTag, "activeRssImageType", nodelist);
                cbDisableClock.Checked = bool.Parse(readEntryValue(optionsTag, "disableOnScreenClock", nodelist));
                targetScreenRes = readEntryValue(optionsTag, "targetScreenRes", nodelist);
                splashScreenImage = readEntryValue(optionsTag, "splashScreenImage", nodelist);
            }
            catch
            {
                // Most likley a new option added but not written to file yet - just continue
            }

            if (splashScreenImage == "false")
                splashScreenImage = "splashscreen.png";

            if (targetScreenRes == "HD")
                setHDScreenRes();
            else if (targetScreenRes == "SD")
                setSDScreenRes();

            switch (activeRssImageType)
            {
                case "infoservice":
                    rssImage = rssImageType.infoserviceImage;
                    rbRssInfoServiceImage.Checked = true;
                    break;
                case "noimage":
                    rssImage = rssImageType.noImage;
                    rbRssNoImage.Checked = true;
                    break;
                case "skin":
                    rssImage = rssImageType.skinImage;
                    rbRssSkinImage.Checked = true;
                    break;
                default:
                    rssImage = rssImageType.skinImage;
                    rbRssSkinImage.Checked = true;
                    break;
            }


            if (_selectedFont != "false")
            {
                cboSelectedFont.Text = _selectedFont;
            }
            if (_labelFont != "false")
            {
                cboLabelFont.Text = _labelFont;
            }

            if (menuStyle == chosenMenuStyle.verticalStyle)
                txtMenuPos.Text = readEntryValue(optionsTag, "menuXPos", nodelist);
            else
                txtMenuPos.Text = readEntryValue(optionsTag, "menuYPos", nodelist);

            ticker = "#infoservice";

            // As only saving the animated state set the static state true if animimated state is false
            if (!WeatherIconsAnimated.Checked)
                weatherIconsStatic.Checked = true;

            //
            // Read in the menu items
            //
            for (int i = 0; i < int.Parse(readEntryValue(menuTag, "count", nodelist)); i++)
            {
                menuItem mnuItem = new menuItem();
                mnuItem.disableBGSharing = true;
                mnuItem.name = readEntryValue(menuTag, "menuitem" + i.ToString() + "name", nodelist);
                mnuItem.contextLabel = readEntryValue(menuTag, "menuitem" + i.ToString() + "label", nodelist);
                mnuItem.bgFolder = readEntryValue(menuTag, "menuitem" + i.ToString() + "folder", nodelist);
                mnuItem.fanartProperty = readEntryValue(menuTag, "menuitem" + i.ToString() + "fanartproperty", nodelist);
                mnuItem.hyperlink = readEntryValue(menuTag, "menuitem" + i.ToString() + "hyperlink", nodelist);
                mnuItem.fanartHandlerEnabled = bool.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "fanarthandlerenabled", nodelist));
                mnuItem.EnableMusicNowPlayingFanart = bool.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "enablemusicnowplayingfanart", nodelist));
                mnuItem.isDefault = bool.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "isdefault", nodelist));
                mnuItem.isWeather = bool.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "isweather", nodelist));
                mnuItem.updateStatus = bool.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "updatestatus", nodelist));
                mnuItem.disableBGSharing = bool.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "disableBGSharing", nodelist));
                mnuItem.id = int.Parse(readEntryValue(menuTag, "menuitem" + i.ToString() + "id", nodelist));

                isWeather.Checked = mnuItem.isWeather;
                disableBGSharing.Checked = mnuItem.disableBGSharing;

                if (mnuItem.fanartHandlerEnabled)
                    checkAndSetRandomFanart(mnuItem.fanartProperty);

                // Set the default control
                if (mnuItem.isDefault)
                    defaultcontrol = mnuItem.id.ToString();
                id = mnuItem.id.ToString();
                itemsOnMenubar.Items.Add(mnuItem.name, id.Equals(defaultcontrol));

                // If user decides not to use multiimage backgrounds then we need a default image, lets check and set if one is required
                defaultImage = readEntryValue(menuTag, "menuitem" + i.ToString() + "defaultimage", nodelist);

                // Check if the stored image still exists, if nor set to default.jpg
                if (!System.IO.File.Exists((imageDir(defaultImage))))
                    defaultImage = "animations\\" + mnuItem.bgFolder + "\\default.jpg";

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
            reloadBackgroundItems();
            //UpdateImageControlVisibility();
            generateMenu.Enabled = true;
        }
    }
}


