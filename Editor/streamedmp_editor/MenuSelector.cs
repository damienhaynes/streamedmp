using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace streamedmp_editor
    {
    public partial class MenuSelector : Form
    {

        public string xml = "";
        public string skindir = "";

        public MenuSelector()
        {
            InitializeComponent();
        }

        private void menuselector_Load(object sender, EventArgs e)
        {
            lstAvailableWindows.SelectedIndex = 0;
            checkBox1.Checked = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter writer;
                switch (lstAvailableWindows.SelectedIndex)
                {
                    case 0:
                        if (checkBox1.Checked == true)
                            if (System.IO.File.Exists(skindir + @"\" + "Basichome.xml"))
                            
                                System.IO.File.Copy(skindir + @"\" + "Basichome.xml", skindir + @"\" + "Basichome.xml.backup." + DateTime.Now.Ticks.ToString());

                        if (System.IO.File.Exists(skindir + @"\" + "Basichome.xml"))
                            System.IO.File.Delete(skindir + @"\" + "Basichome.xml");
                        writer = System.IO.File.CreateText(skindir + @"\" + "Basichome.xml");
                        xml = xml.Replace("<!-- BEGIN GENERATED ID CODE-->", "<id>35</id>");
                        writer.Write(xml);
                        
                        writer.Close();
                        break;
                    case 1:
                        if (checkBox1.Checked == true)
                            if (System.IO.File.Exists(skindir + @"\" + "ExtraMenu.xml"))
                                System.IO.File.Copy(skindir + @"\" + "ExtraMenu.xml", skindir + @"\" + "ExtraMenu.xml.backup." + DateTime.Now.Ticks.ToString());

                        if (System.IO.File.Exists(skindir + @"\" + "ExtraMenu.xml"))
                            System.IO.File.Delete(skindir + @"\" + "ExtraMenu.xml");
                        writer = System.IO.File.CreateText(skindir + @"\" + "ExtraMenu.xml");
                        xml = xml.Replace("<!-- BEGIN GENERATED ID CODE-->", "<id>88884</id>");
                        writer.Write(xml);
                        writer.Close();
                        break;
                    case 2:
                        if (checkBox1.Checked == true)
                            if (System.IO.File.Exists(skindir + @"\" + "GameMenu.xml"))
                                System.IO.File.Copy(skindir + @"\" + "GameMenu.xml", skindir + @"\" + "GameMenu.xml.backup." + DateTime.Now.Ticks.ToString());

                        if (System.IO.File.Exists(skindir + @"\" + "GameMenu.xml"))
                            System.IO.File.Delete(skindir + @"\" + "GameMenu.xml");
                        writer = System.IO.File.CreateText(skindir + @"\" + "GameMenu.xml");
                        xml = xml.Replace("<!-- BEGIN GENERATED ID CODE-->", "<id>888811</id>");
                        writer.Write(xml);
                        writer.Close();
                        break;
                    case 3:
                        if (checkBox1.Checked == true)
                            if (System.IO.File.Exists(skindir + @"\" + "MusicMenu.xml"))
                                System.IO.File.Copy(skindir + @"\" + "MusicMenu.xml", skindir + @"\" + "MusicMenu.xml.backup." + DateTime.Now.Ticks.ToString());
                        if (System.IO.File.Exists(skindir + @"\" + "MusicMenu.xml"))
                            System.IO.File.Delete(skindir + @"\" + "MusicMenu.xml");
                        writer = System.IO.File.CreateText(skindir + @"\" + "MusicMenu.xml");
                        xml = xml.Replace("<!-- BEGIN GENERATED ID CODE-->", "<id>8888</id>");
                        writer.Write(xml);
                        writer.Close();
                        break;
                    case 4:
                        if (checkBox1.Checked == true)
                            if (System.IO.File.Exists(skindir + @"\" + "WatchMenu.xml"))
                                System.IO.File.Copy(skindir + @"\" + "WatchMenu.xml", skindir + @"\" + "WatchMenu.xml.backup." + DateTime.Now.Ticks.ToString());
                        
                        if (System.IO.File.Exists(skindir + @"\" + "WatchMenu.xml"))
                            System.IO.File.Delete(skindir + @"\" + "WatchMenu.xml");
                        writer = System.IO.File.CreateText(skindir + @"\" + "WatchMenu.xml");
                        xml = xml.Replace("<!-- BEGIN GENERATED ID CODE-->", "<id>88883</id>");
                        writer.Write(xml);
                        writer.Close();
                        break;
                }
                MessageBox.Show("File Saved");


        }
    }
}