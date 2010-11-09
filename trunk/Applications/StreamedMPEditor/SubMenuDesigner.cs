﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace StreamedMPEditor
{
  public partial class formSubMenuDesigner : Form
  {
    #region enums
    #endregion

    #region Variables

    List<string> rawXMLFileNames = new List<string>();
    List<string> prettyFileNames = new List<string>();
    List<string> ids = new List<string>();
    List<string> subMenu1DisplayNames = new List<string>();
    List<string> subMenu2DisplayNames = new List<string>();

    List<formStreamedMpEditor.subMenuItem> subMenuLevel1 = new List<formStreamedMpEditor.subMenuItem>();
    List<formStreamedMpEditor.subMenuItem> subMenuLevel2 = new List<formStreamedMpEditor.subMenuItem>();

    bool xmlFilesDisplayed = false;

    Helper helper = new Helper();

    public int menuIndex;

    #endregion

    #region Public Methods

    public formSubMenuDesigner()
    {
      InitializeComponent();
    }

    #endregion

    #region Base Overrides

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
    }

    #endregion

    #region Private Methods

    public void createSubmenu(int index)
    {
      lbSubMen1Display.Text = string.Empty;
      lbSubMen1SelID.Text = string.Empty;
      lbSubMen1SelXML.Text = string.Empty;

      lbSubMen2Display.Text = string.Empty;
      lbSubMen2SelID.Text = string.Empty;
      lbSubMen2SelXML.Text = string.Empty;

      btSubMenu1MostRecentSettings.Enabled = false;
      btSubMenu2MostRecentSettings.Enabled = false;

      lbBaseMenuItem.Text += formStreamedMpEditor.menuItems[index].name;
      menuIndex = index;

      // Load the skin file list
      helper.getSkinFileList(ref lboxXMLSkinFiles, ref ids);

      //Load the file names into our own list
      rawXMLFileNames = lboxXMLSkinFiles.Items.OfType<string>().ToList();
      lboxXMLSkinFiles.Items.Clear();

      // Load Pretty filenames list
      foreach (formStreamedMpEditor.prettyItem p in formStreamedMpEditor.prettyItems)
      {
        prettyFileNames.Add(p.name);
      }

      // Default to displaying pretty names
      lboxXMLSkinFiles.DataSource = prettyFileNames;

      // Add the items to the submenu level1 and level2 listboxes
      if (formStreamedMpEditor.menuItems[menuIndex].subMenuLevel1.Count > 0)
      {
        for (int i = 0; i < formStreamedMpEditor.menuItems[menuIndex].subMenuLevel1.Count; i++)
        {
          lboxSubMenuLevel1.Items.Add(formStreamedMpEditor.menuItems[menuIndex].subMenuLevel1[i].displayName);
        }
      }

      if (formStreamedMpEditor.menuItems[menuIndex].subMenuLevel2.Count > 0)
      {
        for (int i = 0; i < formStreamedMpEditor.menuItems[menuIndex].subMenuLevel2.Count; i++)
        {
          lboxSubMenuLevel2.Items.Add(formStreamedMpEditor.menuItems[menuIndex].subMenuLevel2[i].displayName);
        }
      }
      // Load the local submen data structures
      subMenuLevel1 = formStreamedMpEditor.menuItems[menuIndex].subMenuLevel1;
      subMenuLevel2 = formStreamedMpEditor.menuItems[menuIndex].subMenuLevel2;

      this.Refresh();
      this.ShowDialog();
    }

    private void lboxXMLSkinFiles_MouseDown(object sender, MouseEventArgs e)
    {

      if (lboxXMLSkinFiles.Items.Count == 0)
        return;

      displayItemInfo();

      int index = lboxXMLSkinFiles.IndexFromPoint(e.X, e.Y);
      string s = index.ToString();
      DragDropEffects dde1 = DoDragDrop(s, DragDropEffects.Copy);
    }

    private void lboxSubMenuLevel2_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
        e.Effect = DragDropEffects.Copy;
    }

    private void lboxSubMenuLevel1_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
        e.Effect = DragDropEffects.Copy;
    }

    private void lboxSubMenuLevel1_DragDrop(object sender, DragEventArgs e)
    {
      formStreamedMpEditor.subMenuItem subItem = new formStreamedMpEditor.subMenuItem();

      if (e.Data.GetDataPresent(DataFormats.StringFormat))
      {
        int index = int.Parse((string)e.Data.GetData(DataFormats.StringFormat));

        formStreamedMpEditor.menuItems[menuIndex].subMenuLevel1ID = (formStreamedMpEditor.menuItems[menuIndex].id - 999) * 10000;

        SubItemProperties itemProperties = new SubItemProperties(formStreamedMpEditor.pluginTakesParameter(ids[index]));

        if (xmlFilesDisplayed)
        {
          itemProperties.ShowDialog();
          subItem.displayName = itemProperties.DisplayName;
          subItem.xmlFileName = rawXMLFileNames[index];
          subItem.hyperlink = ids[index];
        }
        else
        {
          subItem.displayName = formStreamedMpEditor.prettyItems[index].name;
          subItem.xmlFileName = formStreamedMpEditor.prettyItems[index].xmlfile;
          subItem.hyperlink = formStreamedMpEditor.prettyItems[index].id;
        }
      }
      formStreamedMpEditor.changeOutstanding = true;
      subItem.baseDisplayName = subItem.displayName;
      subItem.hyperlinkParameter = "false";
      subMenuLevel1.Add(subItem);
      lboxSubMenuLevel1.Items.Add(subItem.displayName);
    }

    private void lboxSubMenuLevel2_DragDrop(object sender, DragEventArgs e)
    {
      formStreamedMpEditor.subMenuItem subItem = new formStreamedMpEditor.subMenuItem();

      if (e.Data.GetDataPresent(DataFormats.StringFormat))
      {
        int index = int.Parse((string)e.Data.GetData(DataFormats.StringFormat));

        formStreamedMpEditor.menuItems[menuIndex].subMenuLevel1ID = (formStreamedMpEditor.menuItems[menuIndex].id - 999) * 10000;

        SubItemProperties itemProperties = new SubItemProperties(formStreamedMpEditor.pluginTakesParameter(ids[index]));

        if (xmlFilesDisplayed)
        {
          itemProperties.ShowDialog();
          subItem.displayName = itemProperties.DisplayName;
          subItem.xmlFileName = rawXMLFileNames[index];
          subItem.hyperlink = ids[index];
        }
        else
        {
          subItem.displayName = formStreamedMpEditor.prettyItems[index].name;
          subItem.xmlFileName = formStreamedMpEditor.prettyItems[index].xmlfile;
          subItem.hyperlink = formStreamedMpEditor.prettyItems[index].id;
        }
      }
      formStreamedMpEditor.changeOutstanding = true;
      subItem.baseDisplayName = subItem.displayName;
      subItem.hyperlinkParameter = "false";
      subMenuLevel2.Add(subItem);
      lboxSubMenuLevel2.Items.Add(subItem.displayName);
    }

    private void lboxSubMenuLevel1_DragOver(object sender, DragEventArgs e)
    {
      string str = null;
      int index = int.Parse((string)e.Data.GetData(DataFormats.StringFormat));

      e.Effect = DragDropEffects.All;

      if (xmlFilesDisplayed)
        str = rawXMLFileNames[index];
      else
        str = formStreamedMpEditor.prettyItems[index].xmlfile;

      foreach (formStreamedMpEditor.subMenuItem sItem in subMenuLevel1)
      {
        if (str == sItem.xmlFileName && !formStreamedMpEditor.pluginTakesParameter(sItem.hyperlink))
          e.Effect = DragDropEffects.None;
      }
    }

    private void lboxSubMenuLevel2_DragOver(object sender, DragEventArgs e)
    {
      string str = null;
      int index = int.Parse((string)e.Data.GetData(DataFormats.StringFormat));

      e.Effect = DragDropEffects.All;

      if (xmlFilesDisplayed)
        str = rawXMLFileNames[index];
      else
        str = formStreamedMpEditor.prettyItems[index].xmlfile;

      foreach (formStreamedMpEditor.subMenuItem sItem in subMenuLevel2)
      {
        if (str == sItem.xmlFileName && sItem.hyperlink != formStreamedMpEditor.tvseriesSkinID)
          e.Effect = DragDropEffects.None;
      }
    }

    private void lboxXMLSkinFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
      displayItemInfo();
    }

    private void lboxXMLSkinFiles_Click(object sender, EventArgs e)
    {
    }

    void displayItemInfo()
    {
      lbSelectedID.Text = ids[lboxXMLSkinFiles.SelectedIndex];
      if (xmlFilesDisplayed)
      {
        lbDisplayName.Text = "<undefined>";
        lbSelectedXML.Text = lboxXMLSkinFiles.SelectedItem.ToString();
      }
      else
      {
        lbSelectedXML.Text = formStreamedMpEditor.prettyItems[lboxXMLSkinFiles.SelectedIndex].xmlfile;
        lbDisplayName.Text = formStreamedMpEditor.prettyItems[lboxXMLSkinFiles.SelectedIndex].name;
      }
    }

    private void lboxSubMenuLevel1_Click(object sender, EventArgs e)
    {
      if (lboxSubMenuLevel1.SelectedIndex >= 0)
      {
        lbSubMen1Display.Text = subMenuLevel1[lboxSubMenuLevel1.SelectedIndex].displayName;
        lbSubMen1SelID.Text = subMenuLevel1[lboxSubMenuLevel1.SelectedIndex].hyperlink;
        lbSubMen1SelXML.Text = subMenuLevel1[lboxSubMenuLevel1.SelectedIndex].xmlFileName;
        btSubMenu1MostRecentSettings.Enabled = true;
      }
    }

    private void lboxSubMenuLevel2_Click(object sender, EventArgs e)
    {
      if (lboxSubMenuLevel2.SelectedIndex >= 0)
      {
        lbSubMen2Display.Text = subMenuLevel2[lboxSubMenuLevel2.SelectedIndex].displayName;
        lbSubMen2SelID.Text = subMenuLevel2[lboxSubMenuLevel2.SelectedIndex].hyperlink;
        lbSubMen2SelXML.Text = subMenuLevel2[lboxSubMenuLevel2.SelectedIndex].xmlFileName;
        btSubMenu2MostRecentSettings.Enabled = true;
      }
    }

    private void btRemoveSubItem1_Click(object sender, EventArgs e)
    {
      if (lboxSubMenuLevel1.SelectedIndex != -1)
      {
        int index = lboxSubMenuLevel1.SelectedIndex;
        subMenuLevel1.Remove(subMenuLevel1[index]);
        lboxSubMenuLevel1.Items.RemoveAt(index);
        formStreamedMpEditor.changeOutstanding = true;
      }
    }

    private void btRemoveSubItem2_Click(object sender, EventArgs e)
    {
      if (lboxSubMenuLevel2.SelectedIndex != -1)
      {
        int index = lboxSubMenuLevel2.SelectedIndex;
        subMenuLevel2.Remove(subMenuLevel2[index]);
        lboxSubMenuLevel2.Items.RemoveAt(index);
        formStreamedMpEditor.changeOutstanding = true;
      }
    }

    private void btEditItemSubMenu1_Click(object sender, EventArgs e)
    {
      if (lboxSubMenuLevel1.SelectedIndex != -1)
      {
        int index = lboxSubMenuLevel1.SelectedIndex;

        SubItemProperties itemProperties = new SubItemProperties(formStreamedMpEditor.pluginTakesParameter(subMenuLevel1[index].hyperlink));

        itemProperties.DisplayName = subMenuLevel1[index].displayName;
        itemProperties.HypelinkParameter = subMenuLevel1[index].hyperlinkParameter;
        itemProperties.BaseName = subMenuLevel1[index].baseDisplayName;
        itemProperties.initialIndex = index;
        itemProperties.ShowDialog();


        if (itemProperties.DisplayName != subMenuLevel1[index].displayName)
        {
          subMenuLevel1[index].displayName = itemProperties.DisplayName;
          formStreamedMpEditor.changeOutstanding = true;
        }
        //
        // Only do this part if we care about hyperlink parameters
        //
        if (formStreamedMpEditor.pluginTakesParameter(subMenuLevel1[index].hyperlink))
        {
          if (string.IsNullOrEmpty(itemProperties.HypelinkParameter) || itemProperties.HypelinkParameter == "false")
          {
            subMenuLevel1[index].hyperlinkParameter = "false";
            subMenuLevel1[index].displayName = subMenuLevel1[index].baseDisplayName;
            formStreamedMpEditor.changeOutstanding = true;
          }
          else if (formStreamedMpEditor.pluginTakesParameter(subMenuLevel1[index].hyperlink))
          {
            subMenuLevel1[index].hyperlinkParameter = itemProperties.HypelinkParameter;
            formStreamedMpEditor.changeOutstanding = true;
          }
        }
        // Refresh the listbox, only way to do this is clear re-populate.
        if (formStreamedMpEditor.changeOutstanding)
        {
          lboxSubMenuLevel1.Items.Clear();
          for (int i = 0; i < subMenuLevel1.Count; i++)
          {
            lboxSubMenuLevel1.Items.Add(subMenuLevel1[i].displayName);
          }
        }
      }
    }

    private void btEditSubMenu2_Click(object sender, EventArgs e)
    {
      if (lboxSubMenuLevel2.SelectedIndex != -1)
      {
        int index = lboxSubMenuLevel2.SelectedIndex;

        SubItemProperties itemProperties = new SubItemProperties(formStreamedMpEditor.pluginTakesParameter(subMenuLevel2[index].hyperlink));

        itemProperties.DisplayName = subMenuLevel2[index].displayName;
        itemProperties.HypelinkParameter = subMenuLevel2[index].hyperlinkParameter;
        itemProperties.BaseName = subMenuLevel2[index].baseDisplayName;
        itemProperties.initialIndex = index;
        itemProperties.ShowDialog();

        if (itemProperties.DisplayName != subMenuLevel2[index].displayName)
        {
          subMenuLevel2[index].displayName = itemProperties.DisplayName;
          formStreamedMpEditor.changeOutstanding = true;
        }
        if (string.IsNullOrEmpty(itemProperties.HypelinkParameter) || itemProperties.HypelinkParameter == "false")
        {
          subMenuLevel2[index].hyperlinkParameter = "false";
          subMenuLevel2[index].displayName = subMenuLevel2[index].baseDisplayName;
          formStreamedMpEditor.changeOutstanding = true;
        }
        else if (formStreamedMpEditor.pluginTakesParameter(subMenuLevel2[index].hyperlink))
        {
          subMenuLevel2[index].hyperlinkParameter = itemProperties.HypelinkParameter;
          formStreamedMpEditor.changeOutstanding = true;
        }

        if (formStreamedMpEditor.changeOutstanding)
        {
          lboxSubMenuLevel2.Items.Clear();
          for (int i = 0; i < subMenuLevel2.Count; i++)
          {
            lboxSubMenuLevel2.Items.Add(subMenuLevel1[i].displayName);
          }
        }
      }
    }

    private void btSub1MoveUp_Click(object sender, EventArgs e)
    {
      if (lboxSubMenuLevel1.SelectedItem != null && lboxSubMenuLevel1.SelectedIndex > 0)
      {
        int index = lboxSubMenuLevel1.SelectedIndex;

        Object listItem = lboxSubMenuLevel1.SelectedItem;
        formStreamedMpEditor.subMenuItem subItem = subMenuLevel1[index];

        lboxSubMenuLevel1.Items.RemoveAt(index);
        subMenuLevel1.RemoveAt(index);
        lboxSubMenuLevel1.Items.Insert(index - 1, listItem);
        subMenuLevel1.Insert(index - 1, subItem);
        lboxSubMenuLevel1.SelectedIndex = index - 1;
        formStreamedMpEditor.changeOutstanding = true;
      }
    }

    private void btSub1MoveDown_Click(object sender, EventArgs e)
    {
      if (lboxSubMenuLevel1.SelectedItem != null && lboxSubMenuLevel1.SelectedIndex < lboxSubMenuLevel1.Items.Count - 1)
      {
        int index = lboxSubMenuLevel1.SelectedIndex;

        Object listItem = lboxSubMenuLevel1.SelectedItem;
        formStreamedMpEditor.subMenuItem subItem = subMenuLevel1[index];

        lboxSubMenuLevel1.Items.RemoveAt(index);
        subMenuLevel1.RemoveAt(index);
        lboxSubMenuLevel1.Items.Insert(index + 1, listItem);
        subMenuLevel1.Insert(index + 1, subItem);
        lboxSubMenuLevel1.SelectedIndex = index + 1;
        formStreamedMpEditor.changeOutstanding = true;
      }
    }

    private void btSub2MoveUp_Click(object sender, EventArgs e)
    {
      if (lboxSubMenuLevel2.SelectedItem != null && lboxSubMenuLevel2.SelectedIndex > 0)
      {
        int index = lboxSubMenuLevel2.SelectedIndex;

        Object listItem = lboxSubMenuLevel2.SelectedItem;
        formStreamedMpEditor.subMenuItem subItem = subMenuLevel2[index];

        lboxSubMenuLevel2.Items.RemoveAt(index);
        subMenuLevel2.RemoveAt(index);
        lboxSubMenuLevel2.Items.Insert(index - 1, listItem);
        subMenuLevel2.Insert(index - 1, subItem);
        lboxSubMenuLevel2.SelectedIndex = index - 1;
        formStreamedMpEditor.changeOutstanding = true;
      }
    }

    private void btSub2MoveDown_Click(object sender, EventArgs e)
    {
      if (lboxSubMenuLevel2.SelectedItem != null && lboxSubMenuLevel2.SelectedIndex < lboxSubMenuLevel2.Items.Count - 1)
      {
        int index = lboxSubMenuLevel2.SelectedIndex;

        Object listItem = lboxSubMenuLevel2.SelectedItem;
        formStreamedMpEditor.subMenuItem subItem = subMenuLevel2[index];

        lboxSubMenuLevel2.Items.RemoveAt(index);
        subMenuLevel2.RemoveAt(index);
        lboxSubMenuLevel2.Items.Insert(index + 1, listItem);
        subMenuLevel2.Insert(index + 1, subItem);
        lboxSubMenuLevel2.SelectedIndex = index + 1;
        formStreamedMpEditor.changeOutstanding = true;
      }
    }

    private void btSwapList_Click(object sender, EventArgs e)
    {
      if (xmlFilesDisplayed)
      {
        lboxXMLSkinFiles.DataSource = prettyFileNames;
        xmlFilesDisplayed = false;
        btSwapList.Text = "Display XML Filenames";
      }
      else
      {
        lboxXMLSkinFiles.DataSource = rawXMLFileNames;
        xmlFilesDisplayed = true;
        btSwapList.Text = "Display Plugin Names";
      }
    }

    private void subMenu1MostRecentSettings_Click(object sender, EventArgs e)
    {
      if (lboxSubMenuLevel1.SelectedIndex >= 0)
      {
        mostRecentDisplaySelection mrDisplaySelection = new mostRecentDisplaySelection();
        mrDisplaySelection.mrToDisplay = subMenuLevel1[lboxSubMenuLevel1.SelectedIndex].showMostRecent;
        mrDisplaySelection.ShowDialog();
        subMenuLevel1[lboxSubMenuLevel1.SelectedIndex].showMostRecent = mrDisplaySelection.mrToDisplay;
      }
    }

    private void subMenu2MostRecentSettings_Click(object sender, EventArgs e)
    {
      if (lboxSubMenuLevel2.SelectedIndex >= 0)
      {
        mostRecentDisplaySelection mrDisplaySelection = new mostRecentDisplaySelection();
        mrDisplaySelection.mrToDisplay = subMenuLevel2[lboxSubMenuLevel2.SelectedIndex].showMostRecent;
        mrDisplaySelection.ShowDialog();
        subMenuLevel2[lboxSubMenuLevel2.SelectedIndex].showMostRecent = mrDisplaySelection.mrToDisplay;
      };
    }

    private void btSaveAndClose_Click(object sender, EventArgs e)
    {
      this.Hide();
    }

    #endregion
  }
}
