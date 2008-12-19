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

namespace streamedmp_editor
{

    public partial class frmStreamedMPEditor : Form
    {
        List<string> ids = new List<string>();
        List<menuItem> menuItems = new List<menuItem>();
        List<backgroundItem> bgItems = new List<backgroundItem>();
        List<prettyItem> prettyItems = new List<prettyItem>();
        
        string path;
        string xml;
        string defFocus = "FFFFFF";
        string defUnFocus = "FFFFFF";

        int defaultId = 0;
        int textXOffset = -25;
        int maxXPosition = 400;
        int minXPosition = 200;
        
        public frmStreamedMPEditor()
        {
            InitializeComponent();
        }

        private string GetMediaPortalPath()
        {
            string sRegRoot = "SOFTWARE";
            if (IntPtr.Size > 4)
                sRegRoot += "\\Wow6432Node";
            
            RegistryKey MediaPortalKey = Registry.LocalMachine.OpenSubKey(sRegRoot + "\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\MediaPortal\\", true);
            if (MediaPortalKey != null)
            {
                return MediaPortalKey.GetValue("InstallPath").ToString();
            }
            else
            {
                MediaPortalKey = MediaPortalKey.OpenSubKey(sRegRoot + "\\Team MediaPortal\\MediaPortal\\", true);
                if (MediaPortalKey != null)
                {
                    return MediaPortalKey.GetValue("ApplicationDir").ToString();
                }
                else
                    return null;
            }
        }

        private void frmStreamedMPEditor_Load(object sender, EventArgs e)
        {
            // Load Skin folder on load
            string sMediaPortalDir = GetMediaPortalPath();
            if (sMediaPortalDir == null)
                return;
            
            path = sMediaPortalDir + "\\skin\\StreamedMP";
            if (System.IO.Directory.Exists(path))
            {
                if (loadIDs(true) == true)
                {
                    cboBGFolder.Enabled = true;
                    txtItemName.Enabled = true;
                    txtBGTime.Enabled = true;
                    btnAdd.Enabled = true;
                    btnRemove.Enabled = true;
                    chklstWinddowsInMenu.Enabled = true;
                    chkBGRandom.Enabled = true;
                    cboContextLabels.Enabled = true;
                }
            }  
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (lstAvailableWindows.SelectedItem != null && cboBGFolder.Text != "" 
                                                         && txtItemName.Text != "")
                                                         
            {                               
                toolStripStatusLabel1.Text = lstAvailableWindows.SelectedItem.ToString() + " added to menu";
                menuItem item = new menuItem();
                item.name = txtItemName.Text;
                item.contextLabel = cboContextLabels.Text;
                item.hyperlink = ids[lstAvailableWindows.SelectedIndex];
                item.bgFolder = cboBGFolder.Text;                
                item.random = chkBGRandom.Checked;
                item.timePerImage = int.Parse(txtBGTime.Text)*1000; //milliseconds
                menuItems.Add(item);
                chklstWinddowsInMenu.Items.Add(item.name.ToUpper());
         
                if (chklstWinddowsInMenu.Items.Count > 2)
                    btnGenerate.Enabled = true;
         
            }
            else
            {
                MessageBox.Show("All fields must be complete.","Missing Fields",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            
        }

        private bool loadIDs(bool onLoad)
        {
            lstAvailableWindows.Enabled = true;
            lstAvailableWindows.Items.Clear();
            string[] files = System.IO.Directory.GetFiles(path);
            foreach (string file in files)
            {
                try
                {
                    // TODO: Add a Junk Filter XML
                    if (file.ToLower().Contains("basichome") == false
                        && file.ToLower().StartsWith("common") == false 
                        && file.ToLower().Contains("dialog") == false                        
                        && file.ToLower().Contains("myhomeplugins") == false
                        && file.ToLower().Contains("mytvhomeserver") == false
                        && file.ToLower().Contains("wizard") == false)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(file);
                        XmlNode node = doc.DocumentElement.SelectSingleNode("/window/id");
                                                
                        ids.Add(node.InnerText);
                        lstAvailableWindows.Items.Add(file.Remove(0, file.LastIndexOf(@"\") + 1).Replace(".xml", ""));                         
                    }
                }
                catch {}
            }
            //return true;
            if (lstAvailableWindows.Items.Count > 0)
            {    
                loadSkin("BasicHome.xml");
                LoadPrettyItems();
                return true;
            }
            else
            {
                // Dont need to complain when first loading the app as its possible that the skin isnt installed
                if (!onLoad)                
                    MessageBox.Show("Error reading directory.");
                return false;
                 
            }
        }

        private void LoadPrettyItems()
        {
            XmlDocument doc = new XmlDocument();
            Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("streamedmp_editor.QuickSelectList.xml");
            doc.Load(stream);                      

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

                innerNode = node.SelectSingleNode("xmlfile");
                if (innerNode != null) pItem.xmlfile = innerNode.InnerText;

                innerNode = node.SelectSingleNode("id");
                if (innerNode != null) pItem.id = innerNode.InnerText;

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

        private void QuickSelect(int index)
        {            
            lstAvailableWindows.SelectedItem = prettyItems[index].xmlfile;
            cboContextLabels.Text = prettyItems[index].contextlabel;
            txtItemName.Text = prettyItems[index].name;
            cboBGFolder.Text = prettyItems[index].folder;
        }

        private void ClearItems()
        {
            txtItemName.Text = "";
            cboBGFolder.Text = "";
            cboContextLabels.Text = "";            
        }

        private void showLoadError()
        {
            MessageBox.Show("Error loading menu, file seems invalid");
            return;
        }

        public void loadSkin(string menu)
        {

            switch (menu)
            {
                case "BasicHome.xml":
                    //loadedMenuID = 35;
                    break;
                case "ExtraMenu.xml":
                    //loadedMenuID = 88884;
                    break;
                case "GameMenu.xml":
                    //loadedMenuID = 888811;
                    break;
                case "MusicMenu.xml":
                    //loadedMenuID = 8888;
                    break;
                case "WatchMenu.xml":
                    //loadedMenuID = 88883;
                    break;
            }


            XmlDocument doc = new XmlDocument();
            if (!File.Exists(path + @"\" + menu))
            {
                MessageBox.Show("Selected menu not found\r\nSelect the StreamedMP folder first and make sure " + menu + " exists");
                return;
            }
            menuItems.Clear();
            chklstWinddowsInMenu.Items.Clear();

            doc.Load(path + @"\" + menu);

            // Get default control if it is set
            XmlNode defaultControlNode = doc.DocumentElement.SelectSingleNode("/window/defaultcontrol");
            string defaultcontrol = "";
            if (defaultControlNode != null)
            {
                string innerText = defaultControlNode.InnerText;
                if (innerText.Length > 3)
                    defaultcontrol = defaultControlNode.InnerText.Substring(3);
                else
                {
                    showLoadError();
                    return;
                }
            }
          
            // Read menuitemFocus, menuitemNoFocus & menuXPos
            XmlNodeList nodeList = doc.DocumentElement.SelectNodes("/window/define");
            bool foundFocus = false, foundNoFocus = false;
            foreach (XmlNode node in nodeList)
            {
                string nodeValue = node.InnerText;

                if (nodeValue.StartsWith("#menuitemFocus"))
                {
                    try
                    {
                        string RGB = defFocus;
                        if (nodeValue.Contains(":"))
                            RGB = nodeValue.Substring(nodeValue.IndexOf(":") + 3).ToUpper();
                        Color col = ColorFromRGB(RGB);
                        txtfocusColour.BackColor = col;
                        txtfocusColour.ForeColor = ColorInvert(col);
                        txtfocusColour.Text = RGB;
                    }
                    catch
                    {
                        txtfocusColour.Text = defFocus;
                    }
                    foundFocus = true;
                }
                if (nodeValue.StartsWith("#menuitemNoFocus"))
                {
                    try
                    {
                        string RGB = defUnFocus;
                        if (nodeValue.Contains(":"))
                            RGB = nodeValue.Substring(nodeValue.IndexOf(":") + 3).ToUpper();
                        Color col = ColorFromRGB(RGB);
                        txtNoFocusColour.BackColor = col;
                        txtNoFocusColour.ForeColor = ColorInvert(col);
                        txtNoFocusColour.Text = RGB;
                    }                     
                    catch
                    {
                        txtfocusColour.Text = defUnFocus;
                    }
                    foundNoFocus = true;
                }
                if (nodeValue.StartsWith("#menuXPos"))
                {        
                    // Just dummy define for storing position
                    txtMenuXPos.Text = nodeValue.Substring(nodeValue.IndexOf(":") + 1);                    
                }
            }
            if (!foundFocus || !foundNoFocus)
            {
                showLoadError();
                return;
            }
            nodeList = doc.DocumentElement.SelectNodes("/window/controls/control/texture");
            bool rssTicker = false;
            foreach (XmlNode node in nodeList)
            {
                if (node.InnerText.Equals("#weatherimg"))
                    rssTicker = true;
            }
            chkRssTicker.Checked = rssTicker;

            nodeList = doc.DocumentElement.SelectNodes("/window/controls/control");

            string hyperlink = "";
            string id = "";
            foreach (XmlNode node in nodeList)
            {
                XmlNode innerNode = node.SelectSingleNode("type");
                if (innerNode != null)
                {
                    if (innerNode.InnerText == "button")
                    {
                        innerNode = node.SelectSingleNode("id");
                        if (innerNode != null)
                        {
                            string nodeText = innerNode.InnerText;
                            if (nodeText.Length > 3)
                                id = innerNode.InnerText.Substring(3);
                            else
                                id = "";
                        }

                        innerNode = node.SelectSingleNode("hyperlink");
                        if (innerNode != null)
                        {
                            string nodeText = innerNode.InnerText;
                            hyperlink = innerNode.InnerText;
                        }
                    }
                    else if (innerNode.InnerText == "label")
                    {
                        innerNode = node.SelectSingleNode("textcolor");
                        
                        if (innerNode != null && innerNode.InnerText.Equals("#menuitemFocus"))
                        {
                            bool bIsContextLabel = node.SelectSingleNode("description").InnerText.Contains(" Label");
                            if (!bIsContextLabel)
                            {
                                string nodeName = node.SelectSingleNode("label").InnerText;
                                chklstWinddowsInMenu.Items.Add(nodeName, id.Equals(defaultcontrol));

                                menuItem mnuItem = new menuItem();
                                mnuItem.hyperlink = hyperlink;
                                mnuItem.name = nodeName;
                                mnuItem.isDefault = id.Equals(defaultcontrol);
                                mnuItem.id = int.Parse(id);
                                menuItems.Add(mnuItem);
                            }
                        }
                    }
                }
            }// End foreach node

            if (menuItems.Count < 6)
            {
                showLoadError();
                return;
            }

            // Remove double entries
            int cnt = chklstWinddowsInMenu.Items.Count;
            for (int i = 1; i < (cnt / 2) + 1; i++)
            {
                chklstWinddowsInMenu.Items.RemoveAt(i);
                menuItems.RemoveAt(i);
            }
            cnt = chklstWinddowsInMenu.Items.Count - 1;
            chklstWinddowsInMenu.Items.RemoveAt(cnt);
            menuItems.RemoveAt(cnt);

            // Load background image settings for menuitems
            nodeList = doc.DocumentElement.SelectNodes("/window/controls/control");
            foreach (XmlNode node in nodeList)
            {
                XmlNode innerNode = node.SelectSingleNode("type");
                if (innerNode != null && innerNode.InnerText == "multiimage")
                {
                    string imagepath = "";
                    string timeperimage = "";
                    string randomize = "";
                    string visible = "";

                    innerNode = node.SelectSingleNode("imagepath");
                    if (innerNode != null) imagepath = innerNode.InnerText;

                    innerNode = node.SelectSingleNode("timeperimage");
                    if (innerNode != null) timeperimage = innerNode.InnerText;

                    innerNode = node.SelectSingleNode("randomize");
                    if (innerNode != null) randomize = innerNode.InnerText;

                    innerNode = node.SelectSingleNode("visible");
                    if (innerNode != null) visible = innerNode.InnerText;

                    foreach (menuItem mnuItem in menuItems)
                    {
                        if (visible.Contains(mnuItem.id + ")"))
                        {
                            mnuItem.random = randomize.Equals("true");
                            mnuItem.bgFolder = imagepath;
                            mnuItem.timePerImage = int.Parse(timeperimage);
                        }
                    }
                }
            }
            // Load Context Labels
            nodeList = doc.DocumentElement.SelectNodes("/window/controls/control");
            foreach (XmlNode node in nodeList)
            {
                XmlNode innerNode = node.SelectSingleNode("description");
                if (innerNode != null && innerNode.InnerText.Contains(" Label")
                                      && !innerNode.InnerText.Contains(" Label (Default)"))
                {
                    string contextLabel = "";
                    string visible = "";

                    innerNode = node.SelectSingleNode("label");
                    if (innerNode != null) contextLabel = innerNode.InnerText;

                    innerNode = node.SelectSingleNode("visible");
                    if (innerNode != null) visible = innerNode.InnerText;
                   
                    foreach (menuItem mnuItem in menuItems)
                    {
                        if (visible.Contains(mnuItem.id + ")"))
                        {
                            mnuItem.contextLabel = contextLabel;
                        }
                    }
                }
            }

            btnGenerate.Enabled = true;
        }

        private void lstAvailableWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAvailableWindows.SelectedIndex >= 0)
            {
                toolStripStatusLabel1.Text = "Window ID: " + ids[lstAvailableWindows.SelectedIndex];

                // Populate / Clear items bases on selection
                int i = 0;
                bool bFound = false;
                string selectedID = ids[lstAvailableWindows.SelectedIndex];
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

        void lstWinddowsInMenu_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel1.Text = "Place checkmark next to default menu item.";
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int xpos;
            int.TryParse(txtMenuXPos.Text, out xpos); 

            if (chklstWinddowsInMenu.CheckedItems.Count == 0)
                MessageBox.Show("You must set one menu item as default.", "Default",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            else if (chklstWinddowsInMenu.CheckedItems.Count > 1 )
                MessageBox.Show("You must set only one menu item as default.", "Default", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (xpos < minXPosition || xpos > maxXPosition)
                MessageBox.Show(string.Format("You must set an Menu X Position of between {0} - {1}",minXPosition,maxXPosition),"X Position", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                foreach (menuItem item in menuItems)
                {
                    item.id = 1000 + menuItems.IndexOf(item);
                    if (item.name == chklstWinddowsInMenu.CheckedItems[0].ToString())
                    {
                        item.isDefault = true;
                        defaultId = menuItems.IndexOf(item);
                    }
                    else
                    {
                        item.isDefault = false;
                    }
                }

                generateXML();
                generateBg();
                generateTopBar();
                generateMenuGraphics();
                GenerateContextLabels();

                if (chkRssTicker.Checked)
                {
                    generateRSSTicker();
                    generateWeather();
                }
                
                generateCrowdingFix();
                toolStripStatusLabel1.Text = "Done!";

                /*MenuSelector selector = new MenuSelector();
                selector.xml = xml;
                selector.skindir = path;
                selector.ShowDialog();*/
                
                // Backup current BasicHome.xml
                if (File.Exists(path + @"\" + "Basichome.xml"))
                {
                    try
                    {
                        File.Copy(path + @"\" + "Basichome.xml", path + @"\" + "Basichome.xml.backup." + DateTime.Now.Ticks.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Unable to make backup of BasicHome.xml ({0})",ex.Message),"Backup",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }
                }

                // Remove current BasicHome.xml
                if (File.Exists(path + @"\" + "Basichome.xml"))
                    File.Delete(path + @"\" + "Basichome.xml");

                StreamWriter writer = File.CreateText(path + @"\" + "Basichome.xml");
                xml = xml.Replace("<!-- BEGIN GENERATED ID CODE-->", "\t<id>35</id>");              
                writer.Write(xml);
                writer.Close();
                
                /*XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = true;
                doc.Load(path + @"\" + "Basichome.xml");
                doc.PreserveWhitespace = false;
                doc.Save(path + @"\" + "Basichome.xml");*/

                // reset item id's as it is possible to generate again.
                foreach (menuItem item in menuItems)
                {
                    item.id = menuItems.IndexOf(item);
                }                
                MessageBox.Show("A new BasicHome.xml has been successfully generated.","Success",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void generateXML()
        {
            bgItems.Clear();
            Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("streamedmp_editor.BasicHomeSkeleton.xml");
            StreamReader reader = new StreamReader(stream);
            xml = reader.ReadToEnd();
            const string quote = "\"";

            xml = xml.Replace("<!-- BEGIN GENERATED DEFINITIONS -->"
                            , "\t<define>#menuitemFocus:FF" + txtfocusColour.Text + "</define>\n"
                            + "\t<define>#menuitemNoFocus:80" + txtNoFocusColour.Text + "</define>\n"
                            + "\t<define>#menuXPos:" + txtMenuXPos.Text + "</define>");

            StringBuilder rawXML = new StringBuilder();
            int onleft = 0;
            int onright = 0;

            foreach (menuItem menItem in menuItems)
            {
                bool newBG = true;
                foreach (backgroundItem bgitem in bgItems)
                {
                    if (bgitem.folder == menItem.bgFolder)
                    {
                        bgitem.ids.Add(menItem.id.ToString());
                        bgitem.name = bgitem.name + " " + menItem.name;
                        newBG = false;
                    }

                }
                if (newBG == true)
                {
                    backgroundItem newbgItem = new backgroundItem();
                    newbgItem.folder = menItem.bgFolder;
                    newbgItem.ids.Add(menItem.id.ToString());
                    newbgItem.name = menItem.name;
                    newbgItem.random = menItem.random;
                    newbgItem.timeperimage = menItem.timePerImage.ToString();
                    bgItems.Add(newbgItem);

                }
                if (menItem.isDefault == true)
                    xml = xml.Replace("<!-- BEGIN GENERATED DEFAULTCONTROL CODE-->", "\t<defaultcontrol>" + (menItem.id + 900).ToString() + "</defaultcontrol>");

                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>Dummy label indicating " + menItem.name + " visibility</description>");
                rawXML.AppendLine("\t\t\t<id>" + menItem.id + "</id>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>100</posX>");
                rawXML.AppendLine("\t\t\t<posY>-100</posY>");
                rawXML.AppendLine("\t\t\t<width>500</width>");
                rawXML.AppendLine("\t\t\t<height>0</height>");
                rawXML.AppendLine("\t\t\t<label>Movies</label>");
                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")|Control.HasFocus(" + (menItem.id + 900).ToString() + ")|Control.IsVisible(" + (menItem.id + 100).ToString() + ")</visible>");
                rawXML.AppendLine("\t\t</control>");

                for (int i = 0; i < 14; i++)
                {
                    switch (i)
                    {
                        case 0:

                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>button</type>");
                            rawXML.AppendLine("\t\t\t<id>" + (menItem.id + 700).ToString() + "</id>");
                            rawXML.AppendLine("\t\t\t<posX>0</posX>");
                            rawXML.AppendLine("\t\t\t<posY>0</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textureFocus>-</textureFocus>");
                            rawXML.AppendLine("\t\t\t<textureNoFocus>-</textureNoFocus>");
                            rawXML.AppendLine("\t\t\t<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");
                            rawXML.AppendLine("\t\t\t<hover>-</hover>");

                            if (menuItems.IndexOf(menItem) == 0)
                                onleft = menuItems[menuItems.Count - 1].id;
                            else
                                onleft = (menItem.id - 1);
                            if (menuItems.IndexOf(menItem) == menuItems.Count - 1)
                                onright = menuItems[0].id;
                            else
                                onright = (menItem.id + 1);
                            rawXML.AppendLine("\t\t\t<onleft>-</onleft>");
                            rawXML.AppendLine("\t\t\t<onright>17</onright>");
                            rawXML.AppendLine("\t\t\t<onup>" + (onleft + 800).ToString() + "</onup>");
                            rawXML.AppendLine("\t\t\t<ondown>" + (onright + 700).ToString() + "</ondown>");
                            rawXML.AppendLine("\t\t\t<visible>Control.IsVisible(" + (menItem.id + 700).ToString() + ")</visible>");
                            rawXML.AppendLine("\t\t</control>");
                            break;

                        case 1:
                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>button</type>");
                            rawXML.AppendLine("\t\t\t<id>" + (menItem.id + 800).ToString() + "</id>");
                            rawXML.AppendLine("\t\t\t<posX>0</posX>");
                            rawXML.AppendLine("\t\t\t<posY>0</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textureFocus>-</textureFocus>");
                            rawXML.AppendLine("\t\t\t<textureNoFocus>-</textureNoFocus>");
                            rawXML.AppendLine("\t\t\t<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");
                            rawXML.AppendLine("\t\t\t<hover>-</hover>");

                            if (menuItems.IndexOf(menItem) == 0)
                                onleft = menuItems[menuItems.Count - 1].id;
                            else
                                onleft = (menItem.id - 1);
                            if (menuItems.IndexOf(menItem) == menuItems.Count - 1)
                                onright = menuItems[0].id;
                            else
                                onright = (menItem.id + 1);
                            rawXML.AppendLine("\t\t\t<onleft>-</onleft>");
                            rawXML.AppendLine("\t\t\t<onright>17</onright>");
                            rawXML.AppendLine("\t\t\t<onup>" + (onleft + 800).ToString() + "</onup>");
                            rawXML.AppendLine("\t\t\t<ondown>" + (onright + 700).ToString() + "</ondown>");
                            rawXML.AppendLine("\t\t\t<visible>Control.IsVisible(" + (menItem.id + 800).ToString() + ")</visible>");
                            rawXML.AppendLine("\t\t</control>");
                            break;

                        case 2:
                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>label</type>");
                            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                            rawXML.AppendLine("\t\t\t<posY>-24</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                            rawXML.AppendLine("\t\t\t<align>right</align>");
                            rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 1)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 2)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[1].id + 100).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 3)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[2].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[2].id + 100).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")|Control.IsVisible(" + (menItem.id + 101).ToString() + ")</visible>");

                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,118" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                            rawXML.AppendLine("\t\t</control>");
                            break;
                        case 3:
                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>label</type>");
                            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                            rawXML.AppendLine("\t\t\t<posY>542</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                            rawXML.AppendLine("\t\t\t<align>right</align>");
                            rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) - 2 == -2)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[menuItems.Count - 2].id + 100).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) - 2 == -1)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[menuItems.Count - 1].id + 100).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 698).ToString() + ")|Control.IsVisible(" + (menItem.id + 98).ToString() + ")</visible>");
                            
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                            rawXML.AppendLine("\t\t</control>");
                            break;
                        case 4:
                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>label</type>");
                            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                            rawXML.AppendLine("\t\t\t<posY>442</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                            rawXML.AppendLine("\t\t\t<align>right</align>");
                            rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) - 1 == -1)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[menuItems.Count - 1].id + 100).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 699).ToString() + ")|Control.IsVisible(" + (menItem.id + 99).ToString() + ")</visible>");

                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                            rawXML.AppendLine("\t\t</control>");
                            break;
                        case 5:
                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>label</type>");
                            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                            rawXML.AppendLine("\t\t\t<posY>342</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textcolor>#menuitemFocus</textcolor>");
                            rawXML.AppendLine("\t\t\t<font>#selectedfont</font>");
                            rawXML.AppendLine("\t\t\t<align>right</align>");
                            rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");
                            rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.IsVisible(" + (menItem.id + 100).ToString() + ")</visible>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                            rawXML.AppendLine("\t\t</control>");
                            break;
                        case 6:
                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>label</type>");
                            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                            rawXML.AppendLine("\t\t\t<posY>242</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                            rawXML.AppendLine("\t\t\t<align>right</align>");
                            rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")|Control.IsVisible(" + (menItem.id + 101).ToString() + ")</visible>");

                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                            rawXML.AppendLine("\t\t</control>");
                            break;
                        case 7:
                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>label</type>");
                            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                            rawXML.AppendLine("\t\t\t<posY>142</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                            rawXML.AppendLine("\t\t\t<align>right</align>");
                            rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[1].id + 100).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 702).ToString() + ")|Control.IsVisible(" + (menItem.id + 102).ToString() + ")</visible>");

                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                            rawXML.AppendLine("\t\t</control>");
                            break;
                        case 8:
                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>label</type>");
                            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                            rawXML.AppendLine("\t\t\t<posY>-24</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                            rawXML.AppendLine("\t\t\t<align>right</align>");
                            rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) - 3 == -3)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 3].id + 800).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) - 3 == -2)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) - 3 == -1)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + ((menItem.id + 800) - 3).ToString() + ")</visible>");

                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-118" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                            rawXML.AppendLine("\t\t</control>");
                            break;
                        case 9:
                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>label</type>");
                            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                            rawXML.AppendLine("\t\t\t<posY>542</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                            rawXML.AppendLine("\t\t\t<align>right</align>");
                            rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) - 2 == -2)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) - 2 == -1)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + ((menItem.id + 800) - 2).ToString() + ")</visible>");

                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                            rawXML.AppendLine("\t\t</control>");
                            break;
                        case 10:
                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>label</type>");
                            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                            rawXML.AppendLine("\t\t\t<posY>442</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                            rawXML.AppendLine("\t\t\t<align>right</align>");
                            rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) - 1 == -1)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + ((menItem.id + 800) - 1).ToString() + ")</visible>");
                            
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                            rawXML.AppendLine("\t\t</control>");
                            break;
                        case 11:
                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>label</type>");
                            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                            rawXML.AppendLine("\t\t\t<posY>342</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textcolor>#menuitemFocus</textcolor>");
                            rawXML.AppendLine("\t\t\t<font>#selectedfont</font>");
                            rawXML.AppendLine("\t\t\t<align>right</align>");
                            rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");
                            rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                            rawXML.AppendLine("\t\t</control>");
                            break;
                        case 12:
                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>label</type>");
                            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                            rawXML.AppendLine("\t\t\t<posY>242</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                            rawXML.AppendLine("\t\t\t<align>right</align>");
                            rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + ((menItem.id + 800) + 1).ToString() + ")</visible>");

                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                            rawXML.AppendLine("\t\t</control>");
                            break;
                        case 13:
                            rawXML.AppendLine("\t\t<control>");
                            rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("\t\t\t<type>label</type>");
                            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                            rawXML.AppendLine("\t\t\t<posY>142</posY>");
                            rawXML.AppendLine("\t\t\t<width>320</width>");
                            rawXML.AppendLine("\t\t\t<height>72</height>");
                            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                            rawXML.AppendLine("\t\t\t<align>right</align>");
                            rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[1].id + 800).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + ((menItem.id + 800) + 2).ToString() + ")</visible>");

                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                            rawXML.AppendLine("\t\t</control>");
                            break;
                    }
                }
            }

            xml = xml.Replace("<!-- BEGIN GENERATED BUTTON CODE-->", rawXML.ToString());

        }

        private void GenerateContextLabels()
        {
            StringBuilder rawXML = new StringBuilder();
            const string quote = "\"";         

            rawXML.AppendLine("\t\t<!-- Menu Context Labels -->");
            foreach (menuItem menItem in menuItems)
            {
                if (menItem.isDefault)
                {
                    // Add default label
                    rawXML.AppendLine("\t\t<control>");
                    rawXML.AppendLine("\t\t\t<description>" + menItem.name + " Label (Default)</description>");
                    rawXML.AppendLine("\t\t\t<type>label</type>");
                    rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                    rawXML.AppendLine("\t\t\t<posY>322</posY>");
                    rawXML.AppendLine("\t\t\t<width>320</width>");
                    rawXML.AppendLine("\t\t\t<height>72</height>");
                    rawXML.AppendLine("\t\t\t<label>" + menItem.contextLabel + "</label>");
                    rawXML.AppendLine("\t\t\t<textcolor>#menuitemFocus</textcolor>");
                    rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                    rawXML.AppendLine("\t\t\t<align>right</align>");
                    rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                    rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                    rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 900).ToString() + ")</visible>");
                    rawXML.AppendLine("\t\t</control>");
                }

                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + " Label</description>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("\t\t\t<posY>322</posY>");
                rawXML.AppendLine("\t\t\t<width>320</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.contextLabel + "</label>");
                rawXML.AppendLine("\t\t\t<textcolor>#menuitemFocus</textcolor>");
                rawXML.AppendLine("\t\t\t<font>#labelFont</font>");                
                rawXML.AppendLine("\t\t\t<align>right</align>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "500" + quote + " time=" + quote + "300" + quote + ">Visible</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "0" + quote + " delay=" + quote + "0" + quote + " time=" + quote + "50" + quote + ">Hidden</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");                                
                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
                rawXML.AppendLine("\t\t</control>");
            } 
            
            xml = xml.Replace("<!-- BEGIN CONTEXT LABELS CODE-->", rawXML.ToString());
        }

        private void generateMenuGraphics()
        {
            StringBuilder rawXML = new StringBuilder();
            const string quote = "\"";

            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>Menu Background</description>");
            rawXML.AppendLine("\t\t\t<type>image</type>");
            rawXML.AppendLine("\t\t\t<id>1</id>");
            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) - maxXPosition) + "</posX>");
            rawXML.AppendLine("\t\t\t<posY>0</posY>");
            rawXML.AppendLine("\t\t\t<width>1280</width>");
            rawXML.AppendLine("\t\t\t<height>720</height>");
            rawXML.AppendLine("\t\t\t<texture>streamed_album_preview_thumb_background.png</texture>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t\t<visible>!Control.HasFocus(7888)+!Control.HasFocus(7999)+!Control.HasFocus(7777)</visible>");
            rawXML.AppendLine("\t\t</control>");
            
            if (chkRssTicker.Checked)
            {
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>Rss Background</description>");
                rawXML.AppendLine("\t\t\t<type>image</type>");
                rawXML.AppendLine("\t\t\t<id>1</id>");
                rawXML.AppendLine("\t\t\t<posX>80</posX>");
                rawXML.AppendLine("\t\t\t<posY>685</posY>");
                rawXML.AppendLine("\t\t\t<width>1300</width>");
                rawXML.AppendLine("\t\t\t<height>50</height>");
                rawXML.AppendLine("\t\t\t<texture>homerss.png</texture>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,100" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t</control>");
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>Weather Background</description>");
                rawXML.AppendLine("\t\t\t<type>image</type>");
                rawXML.AppendLine("\t\t\t<id>1</id>");
                rawXML.AppendLine("\t\t\t<posX>976</posX>");
                rawXML.AppendLine("\t\t\t<posY>-3</posY>");
                rawXML.AppendLine("\t\t\t<width>306</width>");
                rawXML.AppendLine("\t\t\t<height>75</height>");
                rawXML.AppendLine("\t\t\t<texture>homeweatheroverlaybg.png</texture>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<visible>plugin.isenabled(MP-RSSTicker)</visible>");
                rawXML.AppendLine("\t\t</control>");
            }

            xml = xml.Replace("<!-- BEGIN GENERATED MENUGRAPHICS CODE-->", rawXML.ToString());
        }

        private void generateRSSTicker()
        {
            StringBuilder rawXML = new StringBuilder();
            const string quote = "\"";

            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>RSS Items</description>");
            rawXML.AppendLine("\t\t\t<type>fadelabel</type>");
            rawXML.AppendLine("\t\t\t<id>1</id>");
            rawXML.AppendLine("\t\t\t<width>1250</width>");
            rawXML.AppendLine("\t\t\t<height>50</height>");
            rawXML.AppendLine("\t\t\t<posY>695</posY>");
            rawXML.AppendLine("\t\t\t<posX>120</posX>");
            rawXML.AppendLine("\t\t\t<font>mediastream12tc</font>");
            rawXML.AppendLine("\t\t\t<label>#rssfeed</label>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,100" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t\t<visible>plugin.isenabled(MP-RSSTicker)</visible>");
            rawXML.AppendLine("\t\t</control>");
	       
            xml = xml.Replace("<!-- BEGIN GENERATED RSS TICKER CODE-->", rawXML.ToString());

        }

        private void generateWeather()
        {
            StringBuilder rawXML = new StringBuilder();
            const string quote = "\"";

            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>Weather image</description>");
            rawXML.AppendLine("\t\t\t<type>image</type>");
            rawXML.AppendLine("\t\t\t<id>1</id>");
            rawXML.AppendLine("\t\t\t<posX>987</posX>");
            rawXML.AppendLine("\t\t\t<posY>9</posY>");            
            rawXML.AppendLine("\t\t\t<height>53</height>");
            rawXML.AppendLine("\t\t\t<width>55</width>");
            rawXML.AppendLine("\t\t\t<centered>yes</centered>");
            rawXML.AppendLine("\t\t\t<texture>#weatherimg</texture>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t\t<visible>plugin.isenabled(MP-RSSTicker)</visible>");
            rawXML.AppendLine("\t\t</control>");

            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>Temperature</description>");
            rawXML.AppendLine("\t\t\t<type>label</type>");
            rawXML.AppendLine("\t\t\t<id>1</id>");
            rawXML.AppendLine("\t\t\t<width>400</width>");
            rawXML.AppendLine("\t\t\t<height>50</height>");
            rawXML.AppendLine("\t\t\t<align>right</align>");
            rawXML.AppendLine("\t\t\t<posX>1250</posX>");
            rawXML.AppendLine("\t\t\t<posY>22</posY>");            
            rawXML.AppendLine("\t\t\t<font>mediastream14c</font>");
            rawXML.AppendLine("\t\t\t<label>#temp</label>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t\t<visible>plugin.isenabled(MP-RSSTicker)</visible>");
            rawXML.AppendLine("\t\t</control>");

            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>condition</description>");
            rawXML.AppendLine("\t\t\t<type>textbox</type>");
            rawXML.AppendLine("\t\t\t<id>1</id>");
            rawXML.AppendLine("\t\t\t<width>400</width>");
            rawXML.AppendLine("\t\t\t<height>50</height>");
            rawXML.AppendLine("\t\t\t<align>right</align>");
            rawXML.AppendLine("\t\t\t<posX>1048</posX>");
            rawXML.AppendLine("\t\t\t<posY>15</posY>");            
            rawXML.AppendLine("\t\t\t<font>mediastream10tc</font>");
            rawXML.AppendLine("\t\t\t<label>#condition</label>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t\t<visible>plugin.isenabled(MP-RSSTicker)</visible>");
            rawXML.AppendLine("\t\t</control>");
        
            xml = xml.Replace("<!-- BEGIN GENERATED WEATHER CODE-->", rawXML.ToString());
        }

        private void generateTopBar()
        {
            StringBuilder rawXML = new StringBuilder();
            const string quote = "\"";

            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>Exit Button</description>");
            rawXML.AppendLine("\t\t\t<type>button</type>");
            rawXML.AppendLine("\t\t\t<id>7777</id>");
            rawXML.AppendLine("\t\t\t<posX>493</posX>");
            rawXML.AppendLine("\t\t\t<posY>350</posY>");
            rawXML.AppendLine("\t\t\t<onleft>" + (menuItems[defaultId].id + 900).ToString() + "</onleft>");
            rawXML.AppendLine("\t\t\t<onright>7888</onright>");
            rawXML.AppendLine("\t\t\t<width>80</width>");
            rawXML.AppendLine("\t\t\t<height>80</height>");
            rawXML.AppendLine("\t\t\t<textureNoFocus>exit_button.png</textureNoFocus>");
            rawXML.AppendLine("\t\t\t<textureFocus>exit_button.png</textureFocus>");
            rawXML.AppendLine("\t\t\t<action>97</action>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
            rawXML.AppendLine("\t\t</control>");

            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>Restart Button</description>");
            rawXML.AppendLine("\t\t\t<type>button</type>");
            rawXML.AppendLine("\t\t\t<id>7888</id>");
            rawXML.AppendLine("\t\t\t<posX>633</posX>");
            rawXML.AppendLine("\t\t\t<posY>350</posY>");
            rawXML.AppendLine("\t\t\t<onleft>7777</onleft>");
            rawXML.AppendLine("\t\t\t<onright>7999</onright>");
            rawXML.AppendLine("\t\t\t<width>80</width>");
            rawXML.AppendLine("\t\t\t<height>80</height>");
            rawXML.AppendLine("\t\t\t<textureNoFocus>restart_button.png</textureNoFocus>");
            rawXML.AppendLine("\t\t\t<textureFocus>restart_button.png</textureFocus>");
            rawXML.AppendLine("\t\t\t<action>98</action>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
            rawXML.AppendLine("\t\t</control>");

            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>Shutdown Button</description>");
            rawXML.AppendLine("\t\t\t<type>button</type>");
            rawXML.AppendLine("\t\t\t<id>7999</id>");
            rawXML.AppendLine("\t\t\t<posX>773</posX>");
            rawXML.AppendLine("\t\t\t<posY>350</posY>");
            rawXML.AppendLine("\t\t\t<onleft>7888</onleft>");
            rawXML.AppendLine("\t\t\t<onright>" + (menuItems[defaultId].id + 900).ToString() + "</onright>");
            rawXML.AppendLine("\t\t\t<width>80</width>");
            rawXML.AppendLine("\t\t\t<height>80</height>");
            rawXML.AppendLine("\t\t\t<textureNoFocus>shutdown_button.png</textureNoFocus>");
            rawXML.AppendLine("\t\t\t<textureFocus>shutdown_button.png</textureFocus>");
            rawXML.AppendLine("\t\t\t<action>99</action>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
            rawXML.AppendLine("\t\t</control>");

            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>Icon Fix</description>");
            rawXML.AppendLine("\t\t\t<id>0</id>");
            rawXML.AppendLine("\t\t\t<type>image</type>");
            rawXML.AppendLine("\t\t\t<posx>0</posx>");
            rawXML.AppendLine("\t\t\t<posy>0</posy>");
            rawXML.AppendLine("\t\t\t<width>1280</width>");
            rawXML.AppendLine("\t\t\t<height>720</height>");
            rawXML.AppendLine("\t\t\t<texture>tv_black.png</texture>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " start=" + quote + "200" + quote + " end=" + quote + "100" + quote + " time=" + quote + "1000" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t\t<visible>!Control.HasFocus(7777)+!Control.HasFocus(7888)+!Control.HasFocus(7999)</visible>");
            rawXML.AppendLine("\t\t</control>");
                     
            xml = xml.Replace("<!-- BEGIN GENERATED TOPBAR CODE -->", rawXML.ToString());
        }

        private void generateCrowdingFix()
        {
            //defaultId
            StringBuilder rawXML = new StringBuilder();
            int first  = defaultId-2;
            if(first < 0)first += menuItems.Count;

            int second = defaultId-1;
            if(second < 0) second += menuItems.Count;

            int third  = defaultId+1;
            if (third >= menuItems.Count)third -= menuItems.Count;

            int fourth = defaultId+2;
            if(fourth >= menuItems.Count)fourth -= menuItems.Count;

            const string quote = "\"";

            rawXML.AppendLine("\t\t<control>");
              rawXML.AppendLine("\t\t\t<description>home "+menuItems[defaultId].name+"</description>");
              rawXML.AppendLine("\t\t\t<type>button</type>");
              rawXML.AppendLine("\t\t\t<id>"+(menuItems[defaultId].id + 900).ToString()+"</id>");
              rawXML.AppendLine("\t\t\t<posX>0</posX>");
              rawXML.AppendLine("\t\t\t<posY>0</posY>");
              rawXML.AppendLine("\t\t\t<width>320</width>");
              rawXML.AppendLine("\t\t\t<height>72</height>");
	          rawXML.AppendLine("\t\t\t<hyperlink>"+menuItems[defaultId].hyperlink+"</hyperlink>");
              rawXML.AppendLine("\t\t\t<textureFocus>-</textureFocus>");
              rawXML.AppendLine("\t\t\t<textureNoFocus>-</textureNoFocus>");
              rawXML.AppendLine("\t\t\t<hover>-</hover>");
              rawXML.AppendLine("\t\t\t<onleft>-</onleft>");
              rawXML.AppendLine("\t\t\t<onright>17</onright>");
              rawXML.AppendLine("\t\t\t<onup>" + (menuItems[second].id + 800).ToString() + "</onup>");
              rawXML.AppendLine("\t\t\t<ondown>" + (menuItems[third].id + 700).ToString() + "</ondown>");
              rawXML.AppendLine("\t\t\t<visible>Control.IsVisible(" + (menuItems[defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
            rawXML.AppendLine("\t\t</control>");
                    // ************ FIRST


            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>home " + menuItems[first].name + "</description>");
            rawXML.AppendLine("\t\t\t<type>label</type>");
            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
            rawXML.AppendLine("\t\t\t<posY>142</posY>");
            rawXML.AppendLine("\t\t\t<width>320</width>");
            rawXML.AppendLine("\t\t\t<height>72</height>");
            rawXML.AppendLine("\t\t\t<label>"+menuItems[first].name+"</label>");
            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
            rawXML.AppendLine("\t\t\t<align>right</align>");
            rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t</control>");

                    // ************** SECOND



            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>home " + menuItems[second].name + "</description>");
            rawXML.AppendLine("\t\t\t<type>label</type>");
            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
            rawXML.AppendLine("\t\t\t<posY>242</posY>");
            rawXML.AppendLine("\t\t\t<width>320</width>");
            rawXML.AppendLine("\t\t\t<height>72</height>");
            rawXML.AppendLine("\t\t\t<label>"+menuItems[second].name+"</label>");
            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
            rawXML.AppendLine("\t\t\t<align>right</align>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
            rawXML.AppendLine("\t\t</control>");

                    // ******** CENTER

            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>home " + menuItems[defaultId].name + "</description>");
            rawXML.AppendLine("\t\t\t<type>label</type>");
            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
            rawXML.AppendLine("\t\t\t<posY>342</posY>");
            rawXML.AppendLine("\t\t\t<width>320</width>");
            rawXML.AppendLine("\t\t\t<height>72</height>");
            rawXML.AppendLine("\t\t\t<label>"+menuItems[defaultId].name+"</label>");
            rawXML.AppendLine("\t\t\t<textcolor>#menuitemFocus</textcolor>");
            rawXML.AppendLine("\t\t\t<font>#selectedfont</font>");
            rawXML.AppendLine("\t\t\t<align>right</align>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
            rawXML.AppendLine("\t\t</control>");

                    // ******** THIRD


            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>home " + menuItems[third].name + "</description>");
            rawXML.AppendLine("\t\t\t<type>label</type>");
            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
            rawXML.AppendLine("\t\t\t<posY>442</posY>");
            rawXML.AppendLine("\t\t\t<width>320</width>");
            rawXML.AppendLine("\t\t\t<height>72</height>");
            rawXML.AppendLine("\t\t\t<label>"+menuItems[third].name+"</label>");
            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
            rawXML.AppendLine("\t\t\t<align>right</align>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
            rawXML.AppendLine("\t\t</control>");

                    // *************** FOURTH


            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>home " + menuItems[fourth].name + "</description>");
            rawXML.AppendLine("\t\t\t<type>label</type>");
            rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuXPos.Text) + textXOffset) + "</posX>");
            rawXML.AppendLine("\t\t\t<posY>542</posY>");
            rawXML.AppendLine("\t\t\t<width>320</width>");
            rawXML.AppendLine("\t\t\t<height>72</height>");
            rawXML.AppendLine("\t\t\t<label>"+menuItems[fourth].name+"</label>");
            rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
            rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
            rawXML.AppendLine("\t\t\t<align>right</align>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
            rawXML.AppendLine("\t\t</control>");

            xml = xml.Replace("<!-- BEGIN CROWDING FIX CODE-->", rawXML.ToString());
        }

        private void generateBg()
        {
            StringBuilder rawXML = new StringBuilder();
            
            const string quote = "\"";
            foreach (backgroundItem item in bgItems)
            {          
                
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + item.name + " BACKGROUND</description>");
                rawXML.AppendLine("\t\t\t<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "</id>");
                rawXML.AppendLine("\t\t\t<type>multiimage</type>");
                rawXML.AppendLine("\t\t\t<posx>0</posx>");
                rawXML.AppendLine("\t\t\t<posy>0</posy>");
                rawXML.AppendLine("\t\t\t<width>1280</width>");
                rawXML.AppendLine("\t\t\t<height>720</height>");
                rawXML.AppendLine("\t\t\t<imagepath>" + item.folder + "</imagepath>");
                rawXML.AppendLine("\t\t\t<timeperimage>" + int.Parse(item.timeperimage).ToString() + "</timeperimage>");
                rawXML.AppendLine("\t\t\t<fadetime>800</fadetime>");
                rawXML.AppendLine("\t\t\t<loop>yes</loop>");
                rawXML.AppendLine("\t\t\t<randomize>" + item.random.ToString() + "</randomize>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " start=" + quote + "200" + quote + " end=" + quote + "0" + quote + " time=" + quote + "500" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "350" + quote + ">VisibleChange</animation>");
                rawXML.Append("\t\t\t<visible>");
                //Control.HasFocus(98703)|Control.HasFocus(98803)|Control.HasFocus(98705)|Control.HasFocus(98805)</visible>");

                for (int i = 0; i < item.ids.Count; i++)
                {
                    if (i == 0)
                        rawXML.Append("Control.IsVisible(" + item.ids[i] + ")");
                    else
                    {
                        rawXML.Append("|Control.IsVisible(" + item.ids[i] + ")");
                    }
                }
                rawXML.AppendLine("</visible>");
                rawXML.AppendLine("\t\t</control>");
            }

            xml = xml.Replace("<!-- BEGIN GENERATED BACKGROUND CODE-->", rawXML.ToString());
            
        }      

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chklstWinddowsInMenu.SelectedItem != null)
            {
                menuItems.RemoveAt(chklstWinddowsInMenu.SelectedIndex);
                chklstWinddowsInMenu.Items.Remove(chklstWinddowsInMenu.SelectedItem);
            }
            else
                MessageBox.Show("No item is selected for removal", "Remove", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void openStreamedMPFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sMediaPortalDir = GetMediaPortalPath();

            
            if (sMediaPortalDir != null)
                folderBrowserDialog1.SelectedPath = sMediaPortalDir + "\\skin\\StreamedMP";

            folderBrowserDialog1.Description = "Select the skin Directory to load:";
            DialogResult dlgResult = folderBrowserDialog1.ShowDialog();
            if (dlgResult == DialogResult.Cancel)
                return;
            
            path = folderBrowserDialog1.SelectedPath;
            if (path != "")
            {
                if (loadIDs(false))
                {
                    cboBGFolder.Enabled = true;
                    txtItemName.Enabled = true;
                    txtBGTime.Enabled = true;
                    btnAdd.Enabled = true;
                    btnRemove.Enabled = true;
                    chklstWinddowsInMenu.Enabled = true;
                    chkBGRandom.Enabled = true;
                }
            }
        }

        void cboBGFolder_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel1.Text = "Default folders are emulator, games, music, pictures, tv, movies, weatherbg, radio, settings, and pluginsbg";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            removeToolStripMenuItem_Click(sender, e);
        }

        // Move selected menu item down one position in list. 
        private void btMoveDown_Click(object sender, EventArgs e)
        {
            // Do nothing if no item is selected or if already at bottom
            if (chklstWinddowsInMenu.SelectedItem != null && chklstWinddowsInMenu.SelectedIndex < chklstWinddowsInMenu.Items.Count-1)
            {
                int index = chklstWinddowsInMenu.SelectedIndex;


                Object listItem = chklstWinddowsInMenu.SelectedItem;
                menuItem mnuItem = menuItems[index];

                chklstWinddowsInMenu.Items.RemoveAt(index);
                menuItems.RemoveAt(index);
                if (index < chklstWinddowsInMenu.Items.Count)
                {
                    chklstWinddowsInMenu.Items.Insert(index + 1, listItem);
                    menuItems.Insert(index + 1, mnuItem);
                }
                else
                {
                    chklstWinddowsInMenu.Items.Add(listItem);
                    menuItems.Add(mnuItem);
                }
                chklstWinddowsInMenu.SelectedIndex = index+1;
                
            }

            foreach (menuItem item in menuItems)
            {
                Console.WriteLine(item.name);
            }
            Console.WriteLine("");
        }

        // Move selected menu item up one position in list. 
        private void btMoveUp_Click(object sender, EventArgs e)
        {
            // Do nothing if no item is selected or if already at top
            if (chklstWinddowsInMenu.SelectedItem != null && chklstWinddowsInMenu.SelectedIndex > 0)
            {
                int index = chklstWinddowsInMenu.SelectedIndex;


                Object listItem = chklstWinddowsInMenu.SelectedItem;
                menuItem mnuItem = menuItems[index];

                chklstWinddowsInMenu.Items.RemoveAt(index);
                menuItems.RemoveAt(index);

                chklstWinddowsInMenu.Items.Insert(index - 1, listItem);
                menuItems.Insert(index - 1, mnuItem);

                chklstWinddowsInMenu.SelectedIndex = index - 1;

            }
            foreach (menuItem item in menuItems)
            {
                Console.WriteLine(item.name);
            }
            Console.WriteLine("");

        }

        private void llRssTicker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore", "http://forum.team-mediaportal.com/plugins-47/rss-ticker-43603/");
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

        private void txtfocusColour_MouseClick(object sender, MouseEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();            
            if (colorDialog.ShowDialog() != DialogResult.Cancel)
            {
                txtfocusColour.BackColor = colorDialog.Color;
                txtfocusColour.ForeColor = ColorInvert(colorDialog.Color);
                txtfocusColour.Text = (colorDialog.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
            }
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

        private void txtMenuXPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow numbers
            if ((!Char.IsDigit(e.KeyChar)) && (e.KeyChar != Convert.ToChar(Keys.Back)))
                e.Handled = true;
        }

        private void txtBGTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow numbers
            if ((!Char.IsDigit(e.KeyChar)) && (e.KeyChar != Convert.ToChar(Keys.Back)))
                e.Handled = true;
        }

        private void cboQuickSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Auto fill items on new selection for quicker add
            QuickSelect(cboQuickSelect.SelectedIndex);            
        }

    }

    public class menuItem
    {
        public string hyperlink;
        public bool isDefault;
        public string bgFolder;
        public string name;
        public int id;
        public int timePerImage;
        public bool random;
        public string contextLabel;    
    }

    public class backgroundItem
    {
        public string name;
        public string folder;
        public List<string> ids = new List<string>();
        public bool random;
        public string timeperimage;        
    }

    public class prettyItem
    {
        public string name;
        public string name2;
        public string folder;
        public string contextlabel;
        public string xmlfile;
        public string id;
    }
}
