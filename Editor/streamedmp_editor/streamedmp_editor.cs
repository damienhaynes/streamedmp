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
        string path;
        string xml;
        int defaultId = 0;

        public frmStreamedMPEditor()
        {
            InitializeComponent();
        }

        private void frmStreamedMPEditor_Load(object sender, EventArgs e)
        {
            RegistryKey MediaPortalKey = Registry.LocalMachine;
            MediaPortalKey = MediaPortalKey.OpenSubKey(@"SOFTWARE\Team MediaPortal\MediaPortal\", true);
            string sMediaPortalDir = "";
            if (MediaPortalKey != null)
            {
                sMediaPortalDir = MediaPortalKey.GetValue("ApplicationDir").ToString();
            }
            else
                return;

            path = sMediaPortalDir + "\\skin\\StreamedMP";
            if (System.IO.Directory.Exists(path))
            {
                if (loadIDs() == true)
                {
                    txtBGFolder.Enabled = true;
                    txtItemName.Enabled = true;
                    txtBGTime.Enabled = true;
                    btnAdd.Enabled = true;
                    btnRemove.Enabled = true;
                    lstWinddowsInMenu.Enabled = true;
                    chkBGRandom.Enabled = true;
                }
            }  
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (lstAvailableWindows.SelectedItem != null &&  txtBGFolder.Text != "" && txtItemName.Text != "")
            {                               
                toolStripStatusLabel1.Text = lstAvailableWindows.SelectedItem.ToString() + " added to menu";
                menuItem item = new menuItem();
                item.name = txtItemName.Text;
                item.hyperlink = ids[lstAvailableWindows.SelectedIndex];
                item.bgFolder = txtBGFolder.Text;
                //item.name = lstAvailableWindows.SelectedItem.ToString();
                item.random = chkBGRandom.Checked;
                item.timePerImage = int.Parse(txtBGTime.Text);
                menuItems.Add(item);
                lstWinddowsInMenu.Items.Add(item.name);

                txtItemName.Text = "";
                txtBGFolder.Text = "";
                if (lstWinddowsInMenu.Items.Count > 2)
                    btnGenerate.Enabled = true;
                lstAvailableWindows.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("All fields must be complete.");
            }
            
        }

        private bool loadIDs()
        {
            lstAvailableWindows.Enabled = true;
            string[] files = System.IO.Directory.GetFiles(path);
            foreach (string file in files)
            {
                try
                {
                    if (file.StartsWith("common") == false && file.Contains("Dialog") == false && file.Contains("dialog") == false && file.Contains("wizard") == false)
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
                return true;
            else
            {
                MessageBox.Show("Error reading directory.");
                return false;
            }
        }

        private void lstAvailableWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( lstAvailableWindows.SelectedIndex >= 0)
                toolStripStatusLabel1.Text = "Window ID: "+ids[lstAvailableWindows.SelectedIndex];           
        }
               
        private void generateXML()
        {
            bgItems.Clear();
            Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("streamedmp_editor.BasicHomeSkeleton.xml");
            StreamReader reader = new StreamReader(stream);
            xml = reader.ReadToEnd();
            const string quote = "\"";

            xml = xml.Replace("<!-- BEGIN GENERATED DEFINITIONS -->"
                            , "<define>#menuitemFocus:ff" + txtfocusColour.Text + "</define>\n\t" 
                            + "<define>#menuitemNoFocus:80" + txtNoFocusColour.Text + "</define>");

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
                    xml = xml.Replace("<!-- BEGIN GENERATED DEFAULTCONTROL CODE-->", "<defaultcontrol>" + (menItem.id + 900).ToString() + "</defaultcontrol>");

                rawXML.AppendLine("<control>");
		        rawXML.AppendLine("\t<description>Dummy label indicating "+menItem.name+" visibility</description>");
		        rawXML.AppendLine("\t<id>"+menItem.id+"</id>");
		        rawXML.AppendLine("\t<type>label</type>");
		        rawXML.AppendLine("\t<posX>100</posX>");
		        rawXML.AppendLine("\t<posY>-100</posY>");
		        rawXML.AppendLine("\t<width>500</width>");
		        rawXML.AppendLine("\t<height>0</height>");
		        rawXML.AppendLine("\t<label>Movies</label>");
                rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")|Control.HasFocus(" + (menItem.id + 900).ToString() + ")|Control.IsVisible(" + (menItem.id + 100).ToString() + ")</visible>");
                rawXML.AppendLine("\t</control>");

                for (int i = 0; i < 14; i++)
                {
                    switch (i)
                    {
                        case 0:

                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>button</type>");
                            rawXML.AppendLine("<id>" + (menItem.id+700).ToString() + "</id>");
                            rawXML.AppendLine("<posX>0</posX>");
                            rawXML.AppendLine("<posY>0</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textureFocus>-</textureFocus>");
                            rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");
                            rawXML.AppendLine("<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");
                            rawXML.AppendLine("<hover>-</hover>");

                            if (menuItems.IndexOf(menItem) == 0)
                                onleft = menuItems[menuItems.Count - 1].id;
                            else
                                onleft = (menItem.id - 1);
                            if (menuItems.IndexOf(menItem) == menuItems.Count - 1)
                                onright = menuItems[0].id;
                            else
                                onright = (menItem.id + 1);
                            rawXML.AppendLine("<onleft>-</onleft>");
                            rawXML.AppendLine("<onright>7777</onright>");
                            rawXML.AppendLine("<onup>" + (onleft + 800).ToString() + "</onup>");
                            rawXML.AppendLine("<ondown>" + (onright + 700).ToString() + "</ondown>");
                            rawXML.AppendLine("<visible>Control.IsVisible(" + (menItem.id + 700).ToString() + ")</visible>");
                            rawXML.AppendLine("</control>");
                            break;

                        case 1:
                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>button</type>");
                            rawXML.AppendLine("<id>" + (menItem.id + 800).ToString() + "</id>");
                            rawXML.AppendLine("<posX>0</posX>");
                            rawXML.AppendLine("<posY>0</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textureFocus>-</textureFocus>");
                            rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");
                            rawXML.AppendLine("<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");
                            rawXML.AppendLine("<hover>-</hover>");

                            if (menuItems.IndexOf(menItem) == 0)
                                onleft = menuItems[menuItems.Count - 1].id;
                            else
                                onleft = (menItem.id - 1);
                            if (menuItems.IndexOf(menItem) == menuItems.Count - 1)
                                onright = menuItems[0].id;
                            else
                                onright = (menItem.id + 1);
                            rawXML.AppendLine("<onleft>-</onleft>");
                            rawXML.AppendLine("<onright>7777</onright>");
                            rawXML.AppendLine("<onup>" + (onleft + 800).ToString() + "</onup>");
                            rawXML.AppendLine("<ondown>" + (onright + 700).ToString() + "</ondown>");
                            rawXML.AppendLine("<visible>Control.IsVisible(" + (menItem.id + 800).ToString() + ")</visible>");
                            rawXML.AppendLine("</control>");
                            break;

                        case 2:
                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>label</type>");
                            rawXML.AppendLine("<posX>350</posX>");
                            rawXML.AppendLine("<posY>-124</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
	                        rawXML.AppendLine("<font>#labelFont</font>");
                            rawXML.AppendLine("<align>right</align>");
                            rawXML.AppendLine("<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 1)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 2)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[1].id + 100).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 3)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[2].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[2].id + 100).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")|Control.IsVisible(" + (menItem.id + 101).ToString() + ")</visible>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("</control>");
                            break;
                        case 3:
                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>label</type>");
                            rawXML.AppendLine("<posX>350</posX>");
                            rawXML.AppendLine("<posY>524</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("<font>#labelFont</font>");
                            rawXML.AppendLine("<align>right</align>");
                            rawXML.AppendLine("<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) - 2 == -2)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[menuItems.Count-2].id + 100).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) - 2 == -1)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[menuItems.Count - 1].id + 100).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 698).ToString() + ")|Control.IsVisible(" + (menItem.id + 98).ToString() + ")</visible>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("</control>");
                            break;
                        case 4:
                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>label</type>");
                            rawXML.AppendLine("<posX>350</posX>");
                            rawXML.AppendLine("<posY>424</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("<font>#labelFont</font>");
                            rawXML.AppendLine("<align>right</align>");
                            rawXML.AppendLine("<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) - 1 == -1)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[menuItems.Count-1].id + 100).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 699).ToString() + ")|Control.IsVisible(" + (menItem.id + 99).ToString() + ")</visible>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("</control>");
                            break;
                        case 5:
                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>label</type>");
                            rawXML.AppendLine("<posX>350</posX>");
                            rawXML.AppendLine("<posY>324</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
                            rawXML.AppendLine("<font>#selectedfont</font>");
                            rawXML.AppendLine("<align>right</align>");
                            rawXML.AppendLine("<label>" + menItem.name + "</label>");

                            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.IsVisible(" + (menItem.id + 100).ToString() + ")</visible>");

                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("</control>");
                            break;
                        case 6:
                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>label</type>");
                            rawXML.AppendLine("<posX>350</posX>");
                            rawXML.AppendLine("<posY>224</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("<font>#labelFont</font>");
                            rawXML.AppendLine("<align>right</align>");
                            rawXML.AppendLine("<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")|Control.IsVisible(" + (menItem.id + 101).ToString() + ")</visible>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("</control>");
                            break;
                        case 7:
                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>label</type>");
                            rawXML.AppendLine("<posX>350</posX>");
                            rawXML.AppendLine("<posY>124</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("<font>#labelFont</font>");
                            rawXML.AppendLine("<align>right</align>");
                            rawXML.AppendLine("<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[1].id + 100).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|Control.IsVisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 702).ToString() + ")|Control.IsVisible(" + (menItem.id + 102).ToString() + ")</visible>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");

                            rawXML.AppendLine("</control>");
                            break;
                        case 8:
                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>label</type>");
                            rawXML.AppendLine("<posX>350</posX>");
                            rawXML.AppendLine("<posY>-24</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("<font>#labelFont</font>");
                            rawXML.AppendLine("<align>right</align>");
                            rawXML.AppendLine("<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) - 3 == -3)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 3].id + 800).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) - 3 == -2)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) - 3 == -1)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 3).ToString() + ")</visible>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("</control>");
                            break;
                        case 9:
                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>label</type>");
                            rawXML.AppendLine("<posX>350</posX>");
                            rawXML.AppendLine("<posY>524</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("<font>#labelFont</font>");
                            rawXML.AppendLine("<align>right</align>");
                            rawXML.AppendLine("<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) - 2 == -2)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem) - 2 == -1)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                             else
                                rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 2).ToString() + ")</visible>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("</control>");
                            break;
                        case 10:
                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>label</type>");
                            rawXML.AppendLine("<posX>350</posX>");
                            rawXML.AppendLine("<posY>424</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("<font>#labelFont</font>");
                            rawXML.AppendLine("<align>right</align>");
                            rawXML.AppendLine("<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) - 1 == -1)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 1).ToString() + ")</visible>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("</control>");
                            break;
                        case 11:
                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>label</type>");
                            rawXML.AppendLine("<posX>350</posX>");
                            rawXML.AppendLine("<posY>324</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
                            rawXML.AppendLine("<font>#selectedfont</font>");
                            rawXML.AppendLine("<align>right</align>");
                            rawXML.AppendLine("<label>" + menItem.name + "</label>");
                            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("</control>");
                            break;
                        case 12:
                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>label</type>");
                            rawXML.AppendLine("<posX>350</posX>");
                            rawXML.AppendLine("<posY>224</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("<font>#labelFont</font>");
                            rawXML.AppendLine("<align>right</align>");
                            rawXML.AppendLine("<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) + 1).ToString() + ")</visible>");

                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("</control>");
                            break;
                        case 13:
                            rawXML.AppendLine("<control>");
                            rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                            rawXML.AppendLine("<type>label</type>");
                            rawXML.AppendLine("<posX>350</posX>");
                            rawXML.AppendLine("<posY>124</posY>");
                            rawXML.AppendLine("<width>320</width>");
                            rawXML.AppendLine("<height>72</height>");
                            rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                            rawXML.AppendLine("<font>#labelFont</font>");
                            rawXML.AppendLine("<align>right</align>");
                            rawXML.AppendLine("<label>" + menItem.name + "</label>");

                            if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 800).ToString() + ")</visible>");
                            else if (menuItems.IndexOf(menItem)+2==menuItems.Count)
                                rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
                            else
                                rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) + 2).ToString() + ")</visible>");

                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                            rawXML.AppendLine("</control>");
                            break;
                    }                              
                }
            }

            xml = xml.Replace("<!-- BEGIN GENERATED BUTTON CODE-->", rawXML.ToString());

        }

        void lstWinddowsInMenu_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel1.Text = "Place checkmark next to default menu item.";
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (lstWinddowsInMenu.CheckedItems.Count > 1 || lstWinddowsInMenu.CheckedItems.Count == 0)
                MessageBox.Show("You must set 1 item as the default menu item.");
            else
            {

                foreach (menuItem item in menuItems)
                {
                    item.id = 1000 + menuItems.IndexOf(item);
                    if (item.name == lstWinddowsInMenu.CheckedItems[0].ToString())
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
                if (chkRssTicker.Checked)
                {
                    generateRSSTicker();
                }
                
                generateCrowdingFix();
                toolStripStatusLabel1.Text = "Done!";
                MenuSelector selector = new MenuSelector();
                selector.xml = xml;
                selector.skindir = path;
                selector.ShowDialog();

                // reset item id's as it is possible to generate again.
                foreach (menuItem item in menuItems)
                {
                    item.id = menuItems.IndexOf(item);
                }

            }
        }

        private void generateMenuGraphics()
        {
            StringBuilder rawXML = new StringBuilder();
            const string quote = "\"";

            rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>Menu Background</description>");
            rawXML.AppendLine("\t<type>image</type>");
            rawXML.AppendLine("\t<id>1</id>");
            rawXML.AppendLine("\t<posX>0</posX>");
            rawXML.AppendLine("\t<posY>0</posY>");
            rawXML.AppendLine("\t<width>1220</width>");
            rawXML.AppendLine("\t<height>720</height>");
            rawXML.AppendLine("\t<texture>streamed_album_preview_thumb_background.png</texture>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("<visible>!Control.HasFocus(7888)+!Control.HasFocus(7999)+!Control.HasFocus(7777)</visible>");
            rawXML.AppendLine("</control>");
            rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>Rss Background</description>");
            rawXML.AppendLine("\t<type>image</type>");
            rawXML.AppendLine("\t<id>1</id>");
            rawXML.AppendLine("\t<posX>80</posX>");
            rawXML.AppendLine("\t<posY>685</posY>");
            rawXML.AppendLine("\t<width>1300</width>");
            rawXML.AppendLine("\t<height>50</height>");
            rawXML.AppendLine("\t<texture>homerss.png</texture>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,100" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("</control>");
            rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>Weather Background</description>");
            rawXML.AppendLine("\t<type>image</type>");
            rawXML.AppendLine("\t<id>1</id>");
            rawXML.AppendLine("\t<posX>1080</posX>");
            rawXML.AppendLine("\t<posY>-5</posY>");
            rawXML.AppendLine("\t<width>200</width>");
            rawXML.AppendLine("\t<height>75</height>");
            rawXML.AppendLine("\t<texture>homeweatheroverlaybg.png</texture>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("</control>");

            xml = xml.Replace("<!-- BEGIN GENERATED MENUGRAPHICS CODE-->", rawXML.ToString());
        }

        private void generateRSSTicker()
        {
            StringBuilder rawXML = new StringBuilder();
            const string quote = "\"";

            /**rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>RSS image</description>");
            rawXML.AppendLine("\t<type>image</type>");
            rawXML.AppendLine("\t<id>1</id>");
            rawXML.AppendLine("\t<width>100</width>");
            rawXML.AppendLine("\t<height>36</height>");
            rawXML.AppendLine("\t<posY>600</posY>");
            rawXML.AppendLine("\t<posX>0</posX>");
            rawXML.AppendLine("\t<keepaspectratio>no</keepaspectratio>");
            rawXML.AppendLine("\t<texture>#rssimg</texture>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("</control>");**/

            rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>RSS Items</description>");
            rawXML.AppendLine("\t<type>fadelabel</type>");
            rawXML.AppendLine("\t<id>1</id>");
            rawXML.AppendLine("\t<width>1250</width>");
            rawXML.AppendLine("\t<height>50</height>");
            rawXML.AppendLine("\t<posY>695</posY>");
                rawXML.AppendLine("\t<posX>120</posX>");
                rawXML.AppendLine("\t<font>mediastream12tc</font>");
                rawXML.AppendLine("\t<label>#rssfeed</label>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,100" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("</control>");

	        rawXML.AppendLine("<control>");
	            rawXML.AppendLine("\t<description>Weather image</description>");
	            rawXML.AppendLine("\t<type>image</type>");
	            rawXML.AppendLine("\t<id>1</id>");
	            rawXML.AppendLine("\t<posY>5</posY>");
	            rawXML.AppendLine("\t<posX>1215</posX>");
                rawXML.AppendLine("\t<height>55</height>");
                rawXML.AppendLine("\t<width>55</width>");
	            rawXML.AppendLine("\t<texture>#weatherimg</texture>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("</control>");
            
            rawXML.AppendLine("<control>");
	            rawXML.AppendLine("\t<description>Temperature</description>");
	            rawXML.AppendLine("\t<type>label</type>");
	            rawXML.AppendLine("\t<id>1</id>");
	            rawXML.AppendLine("\t<width>400</width>");
	            rawXML.AppendLine("\t<height>50</height>");
                rawXML.AppendLine("\t<align>right</align>");
	            rawXML.AppendLine("\t<posY>35</posY>");
	            rawXML.AppendLine("\t<posX>1200</posX>");
                rawXML.AppendLine("\t<font>mediastream10tc</font>");
	            rawXML.AppendLine("\t<label>#temp</label>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("</control>");
            
            rawXML.AppendLine("<control>");
	            rawXML.AppendLine("\t<description>condition</description>");
	            rawXML.AppendLine("\t<type>label</type>");
	            rawXML.AppendLine("\t<id>1</id>");
	            rawXML.AppendLine("\t<width>400</width>");
	            rawXML.AppendLine("\t<height>50</height>");
                rawXML.AppendLine("\t<align>right</align>");
	            rawXML.AppendLine("\t<posY>15</posY>");
	            rawXML.AppendLine("\t<posX>1200</posX>");
                rawXML.AppendLine("\t<font>mediastream10tc</font>");
	            rawXML.AppendLine("\t<label>#condition</label>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("</control>");
            xml = xml.Replace("<!-- BEGIN GENERATED RSS TICKER CODE-->", rawXML.ToString());

        }

        private void generateWeather()
        {
            StringBuilder rawXML = new StringBuilder();
            const string quote = "\"";

            rawXML.AppendLine("<control>");
	        rawXML.AppendLine("\t<description>Weather Icon Image</description>");
	        rawXML.AppendLine("\t<type>image</type>");
	        rawXML.AppendLine("\t<id>21</id>");
	        rawXML.AppendLine("\t<posX>10</posX>");
	        rawXML.AppendLine("\t<posY>10</posY>");
	        rawXML.AppendLine("\t<texture></texture>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("</control>");

	        rawXML.AppendLine("<control>");
	        rawXML.AppendLine("\t<description>Location Label</description>");
	        rawXML.AppendLine("\t<type>label</type>");
	        rawXML.AppendLine("\t<id>23</id>");
	        rawXML.AppendLine("\t<posX>105</posX>");
	        rawXML.AppendLine("\t<posY>15</posY>");
	        rawXML.AppendLine("\t<label>-</label>");
	        rawXML.AppendLine("\t<align>left</align>");
			rawXML.AppendLine("\t<textcolor>79CDCD</textcolor>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("</control>");

            rawXML.AppendLine("<control>");
	        rawXML.AppendLine("\t<description>Temp Label</description>");
	        rawXML.AppendLine("\t<type>label</type>");
	        rawXML.AppendLine("\t<id>20</id>");
	        rawXML.AppendLine("\t<posX>105</posX>");
	        rawXML.AppendLine("\t<posY>35</posY>");
	        rawXML.AppendLine("\t<label>-</label>");
	        rawXML.AppendLine("\t<align>right</align>");
			rawXML.AppendLine("\t<font>font13</font>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
			rawXML.AppendLine("\t<visible>no</visible>");
            rawXML.AppendLine("</control>");

            rawXML.AppendLine("<control>");
	        rawXML.AppendLine("\t<description>Current Weather Label</description>");
	        rawXML.AppendLine("\t<type>label</type>");
	        rawXML.AppendLine("\t<id>22</id>");
	        rawXML.AppendLine("\t<posX>105</posX>");
	        rawXML.AppendLine("\t<posY>35</posY>");
	        rawXML.AppendLine("\t<label>-</label>");
            rawXML.AppendLine("\t<align>right</align>"); rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
			rawXML.AppendLine("\t<visible>no</visible>");
            rawXML.AppendLine("</control>");

	        rawXML.AppendLine("<control>");
	        rawXML.AppendLine("\t<description>Description and Temperature Label</description>");
	        rawXML.AppendLine("\t<type>label</type>");
	        rawXML.AppendLine("\t<id>24</id>");
	        rawXML.AppendLine("\t<posX>105</posX>");
	        rawXML.AppendLine("\t<posY>45</posY>");
	        rawXML.AppendLine("\t<label>-</label>");
	        rawXML.AppendLine("\t<align>left</align>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("</control>");
           
            xml = xml.Replace("<!-- BEGIN GENERATED WEATHER CODE-->", rawXML.ToString());
        }

        private void generateTopBar()
        {
            StringBuilder rawXML = new StringBuilder();
            const string quote = "\"";

            rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>Exit Button</description>");
            rawXML.AppendLine("\t<type>button</type>");
            rawXML.AppendLine("\t<id>7777</id>");
            rawXML.AppendLine("\t<posX>493</posX>");
            rawXML.AppendLine("\t<posY>350</posY>");
            rawXML.AppendLine("<onleft>" + (menuItems[defaultId].id + 900).ToString() + "</onleft>");
            rawXML.AppendLine("<onright>7888</onright>");
            rawXML.AppendLine("\t<width>80</width>");
            rawXML.AppendLine("\t<height>80</height>");
            rawXML.AppendLine("\t<textureNoFocus>exit_button.png</textureNoFocus>");
            rawXML.AppendLine("\t<textureFocus>exit_button.png</textureFocus>");
            rawXML.AppendLine("\t<action>97</action>");
            rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
            rawXML.AppendLine("</control>");

            rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>Restart Button</description>");
            rawXML.AppendLine("\t<type>button</type>");
            rawXML.AppendLine("\t<id>7888</id>");
            rawXML.AppendLine("\t<posX>633</posX>");
            rawXML.AppendLine("\t<posY>350</posY>");
            rawXML.AppendLine("<onleft>7777</onleft>");
            rawXML.AppendLine("<onright>7999</onright>");
            rawXML.AppendLine("\t<width>80</width>");
            rawXML.AppendLine("\t<height>80</height>");
            rawXML.AppendLine("\t<textureNoFocus>restart_button.png</textureNoFocus>");
            rawXML.AppendLine("\t<textureFocus>restart_button.png</textureFocus>");
            rawXML.AppendLine("\t<action>98</action>");
            rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
            rawXML.AppendLine("</control>");

            rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>Shutdown Button</description>");
            rawXML.AppendLine("\t<type>button</type>");
            rawXML.AppendLine("\t<id>7999</id>");
            rawXML.AppendLine("\t<posX>773</posX>");
            rawXML.AppendLine("\t<posY>350</posY>");
            rawXML.AppendLine("<onleft>7888</onleft>");
            rawXML.AppendLine("<onright>" + (menuItems[defaultId].id + 900).ToString() + "</onright>");
            rawXML.AppendLine("\t<width>80</width>");
            rawXML.AppendLine("\t<height>80</height>");
            rawXML.AppendLine("\t<textureNoFocus>shutdown_button.png</textureNoFocus>");
            rawXML.AppendLine("\t<textureFocus>shutdown_button.png</textureFocus>");
            rawXML.AppendLine("\t<action>99</action>");
            rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
            rawXML.AppendLine("</control>");

            rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>Icon Fix</description>");
            rawXML.AppendLine("\t<id>0</id>");
            rawXML.AppendLine("\t<type>image</type>");
            rawXML.AppendLine("\t<posx>0</posx>");
            rawXML.AppendLine("\t<posy>0</posy>");
            rawXML.AppendLine("\t<width>1280</width>");
            rawXML.AppendLine("\t<height>720</height>");
            rawXML.AppendLine("\t<texture>tv_black.png</texture>");
            rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " start=" + quote + "200" + quote + " end=" + quote + "0" + quote + " time=" + quote + "500" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("\t<visible>!Control.HasFocus(7777)+!Control.HasFocus(7888)+!Control.HasFocus(7999)</visible>");
            rawXML.AppendLine("</control>");
                     
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

            rawXML.AppendLine("<control>");
              rawXML.AppendLine("\t<description>home "+menuItems[defaultId].name+"</description>");
              rawXML.AppendLine("\t<type>button</type>");
              rawXML.AppendLine("\t<id>"+(menuItems[defaultId].id + 900).ToString()+"</id>");
              rawXML.AppendLine("\t<posX>0</posX>");
              rawXML.AppendLine("\t<posY>0</posY>");
              rawXML.AppendLine("\t<width>320</width>");
              rawXML.AppendLine("\t<height>72</height>");
	          rawXML.AppendLine("\t<hyperlink>"+menuItems[defaultId].hyperlink+"</hyperlink>");
              rawXML.AppendLine("\t<textureFocus>-</textureFocus>");
              rawXML.AppendLine("\t<textureNoFocus>-</textureNoFocus>");
              rawXML.AppendLine("\t<hover>-</hover>");
              rawXML.AppendLine("\t<onleft>-</onleft>");
              rawXML.AppendLine("\t<onright>7777</onright>");
              rawXML.AppendLine("\t<onup>" + (menuItems[second].id + 800).ToString() + "</onup>");
              rawXML.AppendLine("\t<ondown>" + (menuItems[third].id + 700).ToString() + "</ondown>");
              rawXML.AppendLine("\t<visible>Control.IsVisible("+(menuItems[defaultId].id+900).ToString()+")</visible>");
            rawXML.AppendLine("</control>	");
                    // ************ FIRST


            rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>home " + menuItems[first].name + "</description>");
              rawXML.AppendLine("\t<type>label</type>");
              rawXML.AppendLine("\t<posX>350</posX>");
              rawXML.AppendLine("\t<posY>124</posY>");
              rawXML.AppendLine("\t<width>320</width>");
              rawXML.AppendLine("\t<height>72</height>");
	          rawXML.AppendLine("\t<label>"+menuItems[first].name+"</label>");
	          rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
	          rawXML.AppendLine("\t<font>#labelFont</font>");
	          rawXML.AppendLine("\t<align>right</align>");
              rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[defaultId].id + 900).ToString() + ")</visible>");
              rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
              rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
            rawXML.AppendLine("</control>");

                    // ************** SECOND



            rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>home " + menuItems[second].name + "</description>");
              rawXML.AppendLine("\t<type>label</type>");
              rawXML.AppendLine("\t<posX>350</posX>");
              rawXML.AppendLine("\t<posY>224</posY>");
              rawXML.AppendLine("\t<width>320</width>");
              rawXML.AppendLine("\t<height>72</height>");
	          rawXML.AppendLine("\t<label>"+menuItems[second].name+"</label>");
	          rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
	          rawXML.AppendLine("\t<font>#labelFont</font>");
	          rawXML.AppendLine("\t<align>right</align>");
              rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
              rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
              rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[defaultId].id + 900).ToString() + ")</visible>");
            rawXML.AppendLine("</control>	");

                    // ******** CENTER

            rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>home " + menuItems[defaultId].name + "</description>");
              rawXML.AppendLine("\t<type>label</type>");
              rawXML.AppendLine("\t<posX>350</posX>");
              rawXML.AppendLine("\t<posY>324</posY>");
              rawXML.AppendLine("\t<width>320</width>");
              rawXML.AppendLine("\t<height>72</height>");
	          rawXML.AppendLine("\t<label>"+menuItems[defaultId].name+"</label>");
	          rawXML.AppendLine("\t<textcolor>#menuitemFocus</textcolor>");
              rawXML.AppendLine("\t<font>#selectedfont</font>");
	          rawXML.AppendLine("\t<align>right</align>");
              rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
              rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
              rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[defaultId].id + 900).ToString() + ")</visible>");
            rawXML.AppendLine("</control>	");

                    // ******** THIRD


            rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>home " + menuItems[third].name + "</description>");
              rawXML.AppendLine("\t<type>label</type>");
              rawXML.AppendLine("\t<posX>350</posX>");
              rawXML.AppendLine("\t<posY>424</posY>");
              rawXML.AppendLine("\t<width>320</width>");
              rawXML.AppendLine("\t<height>72</height>");
	          rawXML.AppendLine("\t<label>"+menuItems[third].name+"</label>");
              rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
              rawXML.AppendLine("\t<font>#labelFont</font>");
	          rawXML.AppendLine("\t<align>right</align>");
              rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
              rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
              rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[defaultId].id + 900).ToString() + ")</visible>");
            rawXML.AppendLine("</control>	");

                    // *************** FOURTH


            rawXML.AppendLine("<control>");
            rawXML.AppendLine("\t<description>home " + menuItems[fourth].name + "</description>");
              rawXML.AppendLine("\t<type>label</type>");
              rawXML.AppendLine("\t<posX>350</posX>");
              rawXML.AppendLine("\t<posY>524</posY>");
              rawXML.AppendLine("\t<width>320</width>");
              rawXML.AppendLine("\t<height>72</height>");
	          rawXML.AppendLine("\t<label>"+menuItems[fourth].name+"</label>");
	          rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
	          rawXML.AppendLine("\t<font>#labelFont</font>");
	          rawXML.AppendLine("\t<align>right</align>");
              rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
              rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
              rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[defaultId].id + 900).ToString() + ")</visible>");
            rawXML.AppendLine("</control>	");

            xml = xml.Replace("<!-- BEGIN CROWDING FIX CODE-->", rawXML.ToString());
        }

        private void generateBg()
        {
            StringBuilder rawXML = new StringBuilder();
            
            const string quote = "\"";
            foreach (backgroundItem item in bgItems)
            {          
                
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + item.name + " BACKGROUND</description>");
                rawXML.AppendLine("\t<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "</id>");
                rawXML.AppendLine("\t<type>multiimage</type>");
                rawXML.AppendLine("\t<posx>0</posx>");
                rawXML.AppendLine("\t<posy>0</posy>");
                rawXML.AppendLine("\t<width>1280</width>");
                rawXML.AppendLine("\t<height>720</height>");
                rawXML.AppendLine("\t<imagepath>" + item.folder + "</imagepath>");
                rawXML.AppendLine("\t<timeperimage>" + (int.Parse(item.timeperimage) * 1000).ToString() + "</timeperimage>");
                rawXML.AppendLine("\t<fadetime>800</fadetime>");
                rawXML.AppendLine("\t<loop>yes</loop>");
                rawXML.AppendLine("\t<randomize>" + item.random.ToString() + "</randomize>");
                rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " start=" + quote + "200" + quote + " end=" + quote + "0" + quote + " time=" + quote + "500" + quote + ">WindowClose</animation>");
                rawXML.Append("\t<visible>");
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
                rawXML.AppendLine("</control>");
            }

            xml = xml.Replace("<!-- BEGIN GENERATED BACKGROUND CODE-->", rawXML.ToString());
            
        }      

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstWinddowsInMenu.SelectedItem != null)
            {
                menuItems.RemoveAt(lstWinddowsInMenu.SelectedIndex);
                lstWinddowsInMenu.Items.Remove(lstWinddowsInMenu.SelectedItem);
            }
            else
                MessageBox.Show("No item selected.");
        }

        private void openAeonFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistryKey MediaPortalKey = Registry.LocalMachine;
            MediaPortalKey = MediaPortalKey.OpenSubKey(@"SOFTWARE\Team MediaPortal\MediaPortal\", true);
            string sMediaPortalDir = "";
            if (MediaPortalKey != null)
                sMediaPortalDir = MediaPortalKey.GetValue("ApplicationDir").ToString();

            folderBrowserDialog1.Description = "Select the skin Directory to load:";
            folderBrowserDialog1.SelectedPath = sMediaPortalDir + "\\skin\\StreamedMP";            
            DialogResult dlgResult = folderBrowserDialog1.ShowDialog();
            if (dlgResult == DialogResult.Cancel)
                return;
            
            path = folderBrowserDialog1.SelectedPath;
            if (path != "")
            {
                if (loadIDs() == true)
                {
                    txtBGFolder.Enabled = true;
                    txtItemName.Enabled = true;
                    txtBGTime.Enabled = true;
                    btnAdd.Enabled = true;
                    btnRemove.Enabled = true;
                    lstWinddowsInMenu.Enabled = true;
                    chkBGRandom.Enabled = true;
                }
            }
        }

        void txtBGFolder_MouseEnter(object sender, System.EventArgs e)
        {
            toolStripStatusLabel1.Text = "Default folders are music, pictures, tv, movies, weatherbg, radio, settings, and pluginsbg";
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
            if (lstWinddowsInMenu.SelectedItem != null && lstWinddowsInMenu.SelectedIndex < lstWinddowsInMenu.Items.Count-1)
            {
                int index = lstWinddowsInMenu.SelectedIndex;


                Object listItem = lstWinddowsInMenu.SelectedItem;
                menuItem mnuItem = menuItems[index];

                lstWinddowsInMenu.Items.RemoveAt(index);
                menuItems.RemoveAt(index);
                if (index < lstWinddowsInMenu.Items.Count)
                {
                    lstWinddowsInMenu.Items.Insert(index + 1, listItem);
                    menuItems.Insert(index + 1, mnuItem);
                }
                else
                {
                    lstWinddowsInMenu.Items.Add(listItem);
                    menuItems.Add(mnuItem);
                }
                lstWinddowsInMenu.SelectedIndex = index+1;
                
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
            if (lstWinddowsInMenu.SelectedItem != null && lstWinddowsInMenu.SelectedIndex > 0)
            {
                int index = lstWinddowsInMenu.SelectedIndex;


                Object listItem = lstWinddowsInMenu.SelectedItem;
                menuItem mnuItem = menuItems[index];

                lstWinddowsInMenu.Items.RemoveAt(index);
                menuItems.RemoveAt(index);

                lstWinddowsInMenu.Items.Insert(index - 1, listItem);
                menuItems.Insert(index - 1, mnuItem);

                lstWinddowsInMenu.SelectedIndex = index - 1;

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

        private void llrssweather_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore", "http://forum.team-mediaportal.com/general-development-no-feature-request-here-48/rss-weather-basic-home-release-43190/");            
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
    }

    public class backgroundItem
    {
        public string name;
        public string folder;
        public List<string> ids = new List<string>();
        public bool random;
        public string timeperimage;
    }
}