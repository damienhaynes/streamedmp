﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;

namespace StreamedMPEditor
{
    public partial class streamedMpEditor : Form
    {
        #region Declares

        editorPaths mpPaths = new editorPaths();
        editorValues basicHomeValues = new editorValues();
        defaultImages defImgs = new defaultImages();
        randomFanartSetting randomFanart = new randomFanartSetting();

        List<string> ids = new List<string>();
        List<menuItem> menuItems = new List<menuItem>();
        List<backgroundItem> bgItems = new List<backgroundItem>();
        List<prettyItem> prettyItems = new List<prettyItem>();
        List<string> skinFontsFocused = new List<string>();
        List<string> skinFontsUnFocused = new List<string>();

        Panel selectPanel = new Panel();
        Button nextBatch = new Button();
        Button prevBatch = new Button();
        Button imgCancel = new Button();

        ProgressBar pBar = new ProgressBar();
        Label pLabel = new Label();
        Form downloadForm = new Form();
        Button downloadStop = new Button();


        enum errorCode
        {
            info,
            infoQuestion,
            loadError,
            readError,
            major,
        };

        enum chosenMenuStyle
        {
            verticalStyle,
            horizontalStandardStyle,
            horizontalContextStyle,
        };

        enum chosenWeatherStyle
        {
            bottom,
            middle
        }

        enum menuType
        {
            vertical,
            horizontal,
        };

        enum sync
        {
            OnLoad,
            editing,
        };

        enum rssImageType
        {
            noImage,
            skinImage,
            infoserviceImage,
        };

        enum screenResolutionType
        {
            res1280x720,
            res1920x1080
        };     

        const string quote = "\"";

        bool basicHomeLoadError = false;
        bool useInfoServiceSeparator = false;


        string xml;
        string xmlTemplate;

        string dropShadowColor = "1d1f1b";
        string baseISVer = "0.9.9.3";
        string baseISVerTwitter = "1.2.0.0";
        string isSeparatorVer = "1.1.0.0";
        string mpReleaseVersion = "1.0.2.22555";
        string ticker = "#rssticker";

        string defFocus = "FFFFFF";
        string defUnFocus = "C0C0C0";

        int textXOffset = -25;
        int maxXPosition = 400;
        int menuOffset = 0;

        int deskHeight;
        int deskWidth;



        //Default Style to StreamedMp standard
        chosenMenuStyle menuStyle = chosenMenuStyle.verticalStyle;
        chosenWeatherStyle weatherStyle = chosenWeatherStyle.bottom;
        rssImageType rssImage = rssImageType.skinImage;

        //Defaut to SD res - this is any resoloution other than 1920x1080 (FullHD)
        screenResolutionType screenres = screenResolutionType.res1280x720;
        screenResolutionType detectedres = screenResolutionType.res1280x720;

        #endregion Declares

        public streamedMpEditor()
        {
            InitializeComponent();

            randomFanart.fanartGames = false;
            randomFanart.fanartMovies = false;
            randomFanart.fanartMovingPictures = false;
            randomFanart.fanartMusic = false;
            randomFanart.fanartPictures = false;
            randomFanart.fanartPlugins = false;
            randomFanart.fanartTv = false;
            randomFanart.fanartTVSeries = false;

            //Check the display res
            deskHeight = Screen.PrimaryScreen.Bounds.Height;
            deskWidth = Screen.PrimaryScreen.Bounds.Width;
            //MessageBox.Show("Your screen resolution is " + deskWidth + "x" + deskHeight);

            if (deskWidth == 1920 && deskHeight == 1080)
            {
                detectedres = screenResolutionType.res1920x1080;
                setHDScreenRes();
            }
            else
            {
                detectedres = screenResolutionType.res1280x720;
                setSDScreenRes();
            }
;
            buildDownloadForm();

            releaseVersion.Text = String.Format("Version: {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            DateTime buildDate = getLinkerTimeStamp(System.Reflection.Assembly.GetEntryAssembly().Location.ToString());
            compileTime.Text += " " + buildDate.ToString() + " GMT";

            lastUsedTab.Checked = Properties.Settings.Default.rememberLastUsedTab;
            if (Properties.Settings.Default.rememberLastUsedTab)
            {
                StreamedMPMenu.SelectedIndex = Properties.Settings.Default.lastUsedTab;
            }
            backupVersionsToKeep.Text = Properties.Settings.Default.keepVersions.ToString();
            if (Properties.Settings.Default.autoPurge)
            {
                autoPurgeBackups.Checked = true;
            }



        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.rtfFiles.introduction.rtf");
            menuDescription.LoadFile(stream, RichTextBoxStreamType.RichText);
            GetMediaPortalSkinPath();
            readFonts();
            getBackupFileTotals();

            if (File.Exists(mpPaths.streamedMPpath + "BasicHome.xml"))
            {
                if (loadIDs() == true)
                {
                    bgBox.Enabled = true;
                    itemName.Enabled = true;
                    addButton.Enabled = true;
                    editButton.Enabled = false;
                    cancelButton.Enabled = false;
                    saveButton.Enabled = false;
                    removeButton.Enabled = true;
                    itemsOnMenubar.Enabled = true;
                    disableBGSharing.Enabled = true;

                    rbRssInfoServiceImage.Checked = false;
                    rbRssNoImage.Checked = false;
                    rbRssSkinImage.Checked = true;

                    menuitemName.Text = null;
                    menuItemLabel.Text = null;
                    menuitemBGFolder.Text = null;
                    menuitemTimeonPage.Text = null;
                    menuitemWindow.Text = null;

                    xmlFiles.SelectedItem = null;
                    cboContextLabel.Text = null;
                    itemName.Text = null;
                    bgBox.Text = null;
                    isWeather.Checked = false;
                    selectedWindow.Text = null;
                    selectedWindowID.Text = null;


                    selectedWindow.Text = null;
                    selectedWindowID.Text = null;
                }

                loadMenuSettings();
                itemsOnMenubar.SelectedIndex = 0;
                screenReset();
                setScreenProperties(itemsOnMenubar.SelectedIndex);
                disableItemControls();
                cancelCreateButton.Visible = false;
                editButton.Enabled = true;


                string mpVersion = getMediaPortalVersion();
                if (mpVersion.CompareTo(mpReleaseVersion) > 0)
                {
                    wrapString.Enabled = true;
                }
                else
                {
                    wrapString.Enabled = false;
                    WeatherIconsAnimated.Enabled = false;
                    WeatherIconsAnimated.Checked = false;
                    weatherIconsStatic.Checked = true;
                }

                if (basicHomeLoadError)
                {
                    DialogResult result = showError("There was an issue reading your current BasicHome.xml file\r\rthe format is to differnet to be parsed correctly\r\rWould you like save your existing BasicHome\r\rand load a template BasicHome for Editing?", errorCode.infoQuestion);
                    if (result == DialogResult.Yes)
                    {
                        BasicHomeFromTemplate();
                        basicHomeLoadError = false;
                        loadMenuSettings();
                        GetDefaultBackgroundImages();
                    }
                    else
                        showError("Editing is not possible due to parsing issues with current BasicHome.xml file", errorCode.major);
                }
            }

            else
                showError(mpPaths.skinBasePath + "BasicHome.xml Not Found", errorCode.major);
        }



        private bool loadIDs()
        {
            xmlFiles.Enabled = true;
            string[] files = System.IO.Directory.GetFiles(mpPaths.streamedMPpath);
            foreach (string file in files)
            {
                try
                {
                    if (file.StartsWith("common") == false && file.Contains("Dialog") == false && file.Contains("dialog") == false && file.Contains("wizard") == false && file.Contains("xml.backup") == false)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(file);
                        XmlNode node = doc.DocumentElement.SelectSingleNode("/window/id");

                        ids.Add(node.InnerText);
                        xmlFiles.Items.Add(file.Remove(0, file.LastIndexOf(@"\") + 1).Replace(".xml", ""));
                    }
                }

                catch { }

            }
            //return true;
            if (xmlFiles.Items.Count > 0)
            {
                LoadPrettyItems();
                disableItemControls();
                cancelCreateButton.Visible = false;
                return true;
            }
            else
            {
                showError("Error reading Skin diectory - no files found", errorCode.major);
                return false;
            }
        }





        private void LoadPrettyItems()
        {
            XmlDocument doc = new XmlDocument();

            Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.xmlFiles.QuickSelectList.xml");

            try
            {
                doc.Load(stream);
            }
            catch (Exception e)
            {
                showError("Exception while loading QuickSelectList.xml\n\n" + e.Message, errorCode.loadError);
                basicHomeLoadError = true;
                return;
            }

            XmlNodeList nodeList = doc.DocumentElement.SelectNodes("/items/item");

            foreach (XmlNode node in nodeList)
            {
                prettyItem pItem = new prettyItem();

                XmlNode innerNode = node.SelectSingleNode("name");
                if (innerNode != null) pItem.name = innerNode.InnerText;
                innerNode = node.SelectSingleNode("name2");
                if (innerNode != null) pItem.name2 = innerNode.InnerText;

                innerNode = node.SelectSingleNode("context");
                if (innerNode != null) pItem.contextlabel = innerNode.InnerText;

                innerNode = node.SelectSingleNode("folder");
                if (innerNode != null) pItem.folder = innerNode.InnerText;

                innerNode = node.SelectSingleNode("fanartproperty");
                if (innerNode != null) pItem.fanartProperty = innerNode.InnerText;

                innerNode = node.SelectSingleNode("fanarthandlerenabled");
                if (innerNode != null) pItem.fanartHandlerEnabled = bool.Parse(innerNode.InnerText);

                

                innerNode = node.SelectSingleNode("xmlfile");
                if (innerNode != null) pItem.xmlfile = innerNode.InnerText;

                innerNode = node.SelectSingleNode("id");
                if (innerNode != null) pItem.id = innerNode.InnerText;

                innerNode = node.SelectSingleNode("isweather");
                if (innerNode != null) pItem.isweather = bool.Parse(innerNode.InnerText);

                // Dont Add item if its not available
                if (ids.Contains(pItem.id))
                    prettyItems.Add(pItem);

            }

            // Load list
            foreach (prettyItem p in prettyItems)
            {
                if (p.name2 != null)
                    cboQuickSelect.Items.Add(p.name + " " + p.name2);
                else
                    cboQuickSelect.Items.Add(p.name);
            }
            cboQuickSelect.SelectedIndex = 0;

        }




        private void addButton_Click(object sender, EventArgs e)
        {

            if (xmlFiles.SelectedItem != null && (bgBox.Text != "" || cboFanartProperty.Text != "") && itemName.Text != "")
            {
                toolStripStatusLabel1.Text = xmlFiles.SelectedItem.ToString() + " added to menu";
                menuItem item = new menuItem();
                item.name = itemName.Text;
                item.contextLabel = cboContextLabel.Text;
                item.hyperlink = ids[xmlFiles.SelectedIndex];
                item.bgFolder = bgBox.Text;
                item.fanartProperty = cboFanartProperty.Text;
                item.fanartHandlerEnabled = cbItemFanartHandlerEnable.Checked;
                item.EnableMusicNowPlayingFanart = cbEnableMusicNowPlayingFanart.Checked;
                item.isWeather = isWeather.Checked;
                item.disableBGSharing = disableBGSharing.Checked;

                // Set default image....
                if (!item.bgFolder.Contains("\\"))
                    item.defaultImage = "animations\\" + item.bgFolder + "\\default.jpg";
                else
                    item.defaultImage = item.bgFolder + "\\default.jpg";
                // And check if it exists and create if not.

                string filetotest = imageDir(item.defaultImage);
                if (!System.IO.File.Exists(filetotest))
                {

                    string[] fileList = getFileListing(Path.GetDirectoryName(item.defaultImage), "*.*");
                    createDefaultJpg(Path.GetDirectoryName(item.defaultImage));

                }

                if (item.fanartHandlerEnabled)
                    checkAndSetRandomFanart(item.fanartProperty);

                menuItems.Add(item);
                itemsOnMenubar.Items.Add(item.name);
                reloadBackgroundItems();
                GetDefaultBackgroundImages();
                itemName.Text = "";
                bgBox.Text = "";


                if (itemsOnMenubar.Items.Count > 2)
                    generateMenu.Enabled = true;
                xmlFiles.SelectedIndex = -1;
            }
            else
            {
                showError("All fields must be complete before a Menu Item can be added", errorCode.info);
            }

        }

        private void editButton_Click(object sender, EventArgs e)
        {

            if (itemsOnMenubar.SelectedIndex == -1)
            {
                showError("No menu item selected\n\nPlease select item above to edit", errorCode.info);
                return;
            }

            int index = itemsOnMenubar.SelectedIndex;
            menuItem mnuItem = menuItems[index];

            setXMLFilesIndex(mnuItem.hyperlink);

            itemName.Text = mnuItem.name;
            cboContextLabel.Text = mnuItem.contextLabel;
            bgBox.Text = mnuItem.bgFolder;
            cboFanartProperty.Text = mnuItem.fanartProperty;
            if (cboFanartProperty.Text.ToLower() == "false")
                cboFanartProperty.Text = "";
            cbItemFanartHandlerEnable.Checked = mnuItem.fanartHandlerEnabled;
            cbEnableMusicNowPlayingFanart.Checked = mnuItem.EnableMusicNowPlayingFanart;
            disableBGSharing.Checked = mnuItem.disableBGSharing;
            isWeather.Checked = mnuItem.isWeather;
            selectedWindow.Text = xmlFiles.Text;
            selectedWindowID.Text = ids[index];

            if (mnuItem.fanartHandlerEnabled)
                checkAndSetRandomFanart(mnuItem.fanartProperty);



            saveButton.Enabled = true;
            cancelButton.Enabled = true;
            editButton.Enabled = false;
            enableItemControls();
            addButton.Enabled = false;
            cancelCreateButton.Visible = false;
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            if (itemsOnMenubar.SelectedItem != null)
            {
                int index = itemsOnMenubar.SelectedIndex;
                menuItem item = new menuItem();

                item = menuItems[index];
                itemsOnMenubar.Items.RemoveAt(index);

                item.name = itemName.Text;
                item.contextLabel = cboContextLabel.Text;
                item.bgFolder = bgBox.Text;
                item.fanartProperty = cboFanartProperty.Text;
                item.fanartHandlerEnabled = cbItemFanartHandlerEnable.Checked;
                item.EnableMusicNowPlayingFanart = cbEnableMusicNowPlayingFanart.Checked;
                item.hyperlink = ids[xmlFiles.SelectedIndex];
                item.disableBGSharing = disableBGSharing.Checked;
                item.isWeather = isWeather.Checked;




                if (item.fanartHandlerEnabled)
                    checkAndSetRandomFanart(item.fanartProperty);
                else
                {
                    // Set default image....
                    if (!item.bgFolder.Contains("\\"))
                        item.defaultImage = "animations\\" + item.bgFolder + "\\default.jpg";
                    else
                        item.defaultImage = item.bgFolder + "\\default.jpg";
                    // And check if it exists and create if not.

                    string filetotest = imageDir(item.defaultImage);
                    if (!System.IO.File.Exists(filetotest))
                    {

                        string[] fileList = getFileListing(Path.GetDirectoryName(item.defaultImage),"*.*");
                        createDefaultJpg(Path.GetDirectoryName(item.defaultImage));

                    }
                }

                
                menuItems[index] = item;
                itemsOnMenubar.Items.Insert(index, item.name);
                reloadBackgroundItems();
                screenReset();
                disableItemControls();
                cancelCreateButton.Visible = false;
            }
        }

        private void setXMLFilesIndex(string hyperlink)
        {
            for (int i = 1; i < ids.Count; i++)
            {
                if (ids[i] == hyperlink)
                    xmlFiles.SelectedIndex = i;
            }
        }

        private void screenReset()
        {
            if (saveButton.Enabled)
            {
                itemName.Text = null;
                cboContextLabel.Text = null;
                isWeather.Checked = false;
                bgBox.SelectedIndex = -1;
                cboFanartProperty.SelectedIndex = -1;
                saveButton.Enabled = false;
                cancelButton.Enabled = false;
                editButton.Enabled = true;
            }
            selectedWindow.Text = null;
            selectedWindowID.Text = null;

        }

        private void itemsOnMenubar_Click(object sender, EventArgs e)
        {
            screenReset();
            setScreenProperties(itemsOnMenubar.SelectedIndex);
            editButton.Enabled = true;
            disableItemControls();

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            screenReset();
            setScreenProperties(itemsOnMenubar.SelectedIndex);
            disableItemControls();
        }

        private void setScreenProperties(int index)
        {
            menuItem mnuItem = menuItems[index];

            setXMLFilesIndex(mnuItem.hyperlink);
            cboContextLabel.Text = mnuItem.contextLabel;
            itemName.Text = mnuItem.name;
            cboFanartProperty.Text = mnuItem.fanartProperty;
            cbItemFanartHandlerEnable.Checked = mnuItem.fanartHandlerEnabled;
            cbEnableMusicNowPlayingFanart.Checked = mnuItem.EnableMusicNowPlayingFanart;
            disableBGSharing.Checked = mnuItem.disableBGSharing;

            menuitemName.Text = mnuItem.name;
            menuItemLabel.Text = mnuItem.contextLabel;
            menuitemBGFolder.Text = mnuItem.bgFolder;
            bgBox.Text = mnuItem.bgFolder;
            menuitemWindow.Text = xmlFiles.Text;

            UpdateImageControlVisibility(mnuItem.fanartHandlerEnabled);
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.Description = "Select the folder containing the images for this menu item";
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                bgBox.Text = folderBrowserDialog.SelectedPath;
            }
        }



        private void xmlFiles_Click(object sender, EventArgs e)
        {
            selectedWindow.Text = xmlFiles.Text;
            selectedWindowID.Text = ids[xmlFiles.SelectedIndex];
            enableItemControls();
            editButton.Enabled = false;
            cancelCreateButton.Visible = true;
        }


        // Move selected menu item up one position in list. 
        private void btMoveUp_Click(object sender, EventArgs e)
        {
            // Do nothing if no item is selected or if already at top
            if (itemsOnMenubar.SelectedItem != null && itemsOnMenubar.SelectedIndex > 0)
            {
                int index = itemsOnMenubar.SelectedIndex;


                Object listItem = itemsOnMenubar.SelectedItem;
                menuItem mnuItem = menuItems[index];

                itemsOnMenubar.Items.RemoveAt(index);
                menuItems.RemoveAt(index);

                itemsOnMenubar.Items.Insert(index - 1, listItem);
                menuItems.Insert(index - 1, mnuItem);

                itemsOnMenubar.SelectedIndex = index - 1;

            }
            foreach (menuItem item in menuItems)
            {
                Console.WriteLine(item.name);
            }
            Console.WriteLine("");

        }

        // Move selected menu item down one position in list. 
        private void btMoveDown_Click(object sender, EventArgs e)
        {
            // Do nothing if no item is selected or if already at bottom
            if (itemsOnMenubar.SelectedItem != null && itemsOnMenubar.SelectedIndex < itemsOnMenubar.Items.Count - 1)
            {
                int index = itemsOnMenubar.SelectedIndex;


                Object listItem = itemsOnMenubar.SelectedItem;
                menuItem mnuItem = menuItems[index];

                itemsOnMenubar.Items.RemoveAt(index);
                menuItems.RemoveAt(index);
                if (index < itemsOnMenubar.Items.Count)
                {
                    itemsOnMenubar.Items.Insert(index + 1, listItem);
                    menuItems.Insert(index + 1, mnuItem);
                }
                else
                {
                    itemsOnMenubar.Items.Add(listItem);
                    menuItems.Add(mnuItem);
                }
                itemsOnMenubar.SelectedIndex = index + 1;

            }

            foreach (menuItem item in menuItems)
            {
                Console.WriteLine(item.name);
            }
            Console.WriteLine("");
        }

        private void itemName_TextChanged(object sender, EventArgs e)
        {
            int start = itemName.SelectionStart;
            itemName.Text = itemName.Text.ToUpper();
            itemName.SelectionStart = start;
        }

        private void cboContextLabels_TextChanged(object sender, EventArgs e)
        {
            int start = cboContextLabel.SelectionStart;
            cboContextLabel.Text = cboContextLabel.Text.ToUpper();
            cboContextLabel.SelectionStart = start;
        }


        private void generateMenu_Click(object sender, EventArgs e)
        {

            validateMenuOffset();

            if (itemsOnMenubar.CheckedItems.Count > 1 || itemsOnMenubar.CheckedItems.Count == 0)
            {
                if (itemsOnMenubar.CheckedItems.Count == 0) showError("\t      No Default Item\n\nYou must set one item as the default menu item.", errorCode.info);
                if (itemsOnMenubar.CheckedItems.Count > 1) showError("          More than one default item set\n\nOnly one item can be set as the default menu.", errorCode.info);
            }
            else
            {
                foreach (menuItem item in menuItems)
                {
                    item.id = 1000 + menuItems.IndexOf(item);
                    if (item.name == itemsOnMenubar.CheckedItems[0].ToString())
                    {
                        item.isDefault = true;
                        basicHomeValues.defaultId = menuItems.IndexOf(item);
                    }
                    else
                    {
                        item.isDefault = false;
                    }
                    if (!infoserviceOptions.Enabled || !weatherBGlink.Checked)
                        if (item.bgFolder == null && item.fanartProperty == null)
                        {
                            showError("Menu Item " + item.name + " Has no Background Image folder\n\n\tPlease set a Folder", errorCode.info);
                            return;
                        }
                }
                setBasicHomeValues();
                if (menuStyle == chosenMenuStyle.verticalStyle)
                {
                    writeMenu(menuType.vertical);
                }
                else
                {
                    writeMenu(menuType.horizontal);
                }
            }
            if (cboClearCache.Checked)
                clearCacheDir();
        }

        private void writeMenu(menuType direction)
        {
            System.IO.StreamWriter writer;
            generateXML(direction);
            generateBg(direction);
            if (!cbDisableClock.Checked)
                generateClock();
            if (enableFiveDayWeather.Checked)
                GenerateFiveDayWeather();
            if (summaryWeatherCheckBox.Checked && infoserviceOptions.Enabled)
            {
                foreach (backgroundItem item in bgItems)
                {
                    if (item.isWeather)
                    {
                        basicHomeValues.weatherControl = (int.Parse(item.ids[0]) + 200);
                        generateWeathersummary(basicHomeValues.weatherControl);
                    }
                }

            }

            if (direction == menuType.horizontal)
            {
                generateTopBarH();
                generateMenuGraphicsH();
                generateCrowdingFixH();
                if (horizontalContextLabels.Checked)
                    GenerateContextLabelsH();
                GenerateOverlayImport();

            }
            else if (direction == menuType.vertical)
            {
                generateTopBarV();
                generateMenuGraphicsV();
                generateCrowdingFixV();
                GenerateContextLabelsV();
            }

            if (enableRssfeed.Checked && infoserviceOptions.Enabled)
            {
                if (direction == menuType.horizontal)
                {
                    generateRSSTicker();
                    if (enableTwitter.Checked && infoserviceOptions.Enabled) generateTwitter();
                }
                else if (direction == menuType.vertical)
                {
                    generateRSSTickerV();
                    if (enableTwitter.Checked && infoserviceOptions.Enabled) generateTwitterV();
                }
            }

            toolStripStatusLabel1.Text = "Done!";

            if (System.IO.File.Exists(mpPaths.streamedMPpath + "BasicHome.xml"))
                System.IO.File.Copy(mpPaths.streamedMPpath + "BasicHome.xml", mpPaths.streamedMPpath + "BasicHome.xml.backup." + DateTime.Now.Ticks.ToString());

            if (System.IO.File.Exists(mpPaths.streamedMPpath + "BasicHome.xml"))
                System.IO.File.Delete(mpPaths.streamedMPpath + "BasicHome.xml");
            writer = System.IO.File.CreateText(mpPaths.streamedMPpath + "BasicHome.xml");
            xml = xml.Replace("<!-- BEGIN GENERATED ID CODE-->", "<id>35</id>");
            writer.Write(xml);
            writer.Close();
            getBackupFileTotals();
            DialogResult result = showError("BasicHome.xml Saved Sucessfully \n\n  Backup file has been created \n\nDo you want to Contine Editing", errorCode.infoQuestion);
            if (result == DialogResult.No)
                this.Close();

            // reset item id's as it is possible to generate again.
            foreach (menuItem item in menuItems)
            {
                item.id = menuItems.IndexOf(item);
            }
        }
    }
}



