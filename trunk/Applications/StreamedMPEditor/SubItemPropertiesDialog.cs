using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Cornerstone.Database.Tables;
using Cornerstone.GUI.Filtering;
using MediaPortal.Plugins.MovingPictures;
using MediaPortal.Plugins.MovingPictures.Database;

namespace StreamedMPEditor
{
  public partial class SubItemProperties : Form
  {

    public string baseName = null;
    public int initialIndex = -1;
    public int initialMovPicIndex = -1;
    public string currentSkinID = string.Empty;

    public SubItemProperties(bool showHyperlinkParameterDialog, string skinFileID)
    {
      InitializeComponent();
      gbHyperlinkParameter.Enabled = false;
      currentSkinID = skinFileID;

      if (showHyperlinkParameterDialog)
        gbHyperlinkParameter.Enabled = true;

      cbOnlineVideosReturn.Visible = false;
      cboOnlineVideosCategories.Visible = false;
      movPicsCategoryCombo.Visible = false;
      lblSearch.Visible = false;
      lblCategory.Visible = false;
      txtSearch.Visible = false;

      switch (skinFileID)
      {
        case formStreamedMpEditor.tvseriesSkinID:
          foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.tvseriesViews)
          {
            cboViews.Items.Add(tvv.Value);
          }
          break;
        case formStreamedMpEditor.musicSkinID:
          foreach (KeyValuePair<string, string> mvv in formStreamedMpEditor.musicViews)
          {
            cboViews.Items.Add(mvv.Value);
          }
          break;
        case formStreamedMpEditor.onlineVideosSkinID:
          cbOnlineVideosReturn.Visible = true;
          cboOnlineVideosCategories.Visible = true;
          lblSearch.Visible = true;
          lblCategory.Visible = true;
          txtSearch.Visible = true;          
          foreach (KeyValuePair<string, string> mvv in formStreamedMpEditor.onlineVideosViews)
          {
            cboViews.Items.Add(mvv.Value);
          }
          break;
        case formStreamedMpEditor.movingPicturesSkinID:
          movPicsCategoryCombo.Visible = true;
          LoadMovingPicturesCategories();
          break;

      }      
    }

    /// <summary>
    /// Loads the Custom Category list into a Cornerstone Filter Combo Box
    /// </summary>
    private void LoadMovingPicturesCategories()
    {
      // initialize filter combo to manage the default filter
      movPicsCategoryCombo.TreePanel.TranslationParser = new TranslationParserDelegate(MediaPortal.Plugins.MovingPictures.MainUI.Translation.ParseString);
      movPicsCategoryCombo.Menu = MovingPicturesCore.Settings.CategoriesMenu;
    }

    public string BaseName
    {
      set { baseName = value; }
    }

    public int InitialIndex
    {
      set { initialIndex = value; }
    }

    public string DisplayName
    {
      get
      {
        return tbItemDisplayName.Text;
      }
      set
      {
        tbItemDisplayName.Text = value;
      }
    }

    public string HypelinkParameterName
    {
      get
      {
        return cboViews.Text;
      }
      set
      {
        cboViews.Text = value;
      }
    }

    public string onlineVideosSearchString
    {
      get
      {
        return txtSearch.Text;
      }
      set
      {
        txtSearch.Text = value;
      }
    }

    public string onlineVideosReturnOption
    {
      get
      {
        if (cbOnlineVideosReturn.Checked)
          return "Root";
        else
          return "Locked";
      }
      set
      {
        if (string.IsNullOrEmpty(value))
          cbOnlineVideosReturn.Checked = false;
        else
        {
          if (value.ToString().ToLower() == "root")
            cbOnlineVideosReturn.Checked = true;
          else
            cbOnlineVideosReturn.Checked = false;
        }
      }
    }


    public string tvseriesHypelinkParameter
    {
      get
      {
        if (formStreamedMpEditor.tvseriesViews.Count == 0)
          return cboViews.Text;

        foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.tvseriesViews)
        {
          if (tvv.Value == cboViews.Text)
            return tvv.Key;
        }
        return "false";
      }

      set
      {
        if (formStreamedMpEditor.tvseriesViews.Count == 0)
          cboViews.Text = value;
        int i = 0;
        foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.tvseriesViews)
        {
          if (value == tvv.Key)
          {
            cboViews.Text = tvv.Value;
            initialIndex = i;
            break;
          }
          i++;
        }
      }
    }

    public string musicHypelinkParameter
    {
      get
      {
        if (formStreamedMpEditor.musicViews.Count == 0)
          return cboViews.Text;

        foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.musicViews)
        {
          if (tvv.Value == cboViews.Text)
            return tvv.Key;
        }
        return "false";
      }

      set
      {
        if (formStreamedMpEditor.musicViews.Count == 0)
          cboViews.Text = value;
        int i = 0;
        foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.musicViews)
        {
          if (value == tvv.Key)
          {
            cboViews.Text = tvv.Value;
            initialIndex = i;
            break;
          }
          i++;
        }
      }
    }

    public string onlineVideosHypelinkParameter
    {
      get
      {
        if (formStreamedMpEditor.onlineVideosViews.Count == 0)
          return cboViews.Text;

        foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.onlineVideosViews)
        {
          if (tvv.Value == cboViews.Text)
            return tvv.Key;
        }
        return "false";
      }

      set
      {
        if (formStreamedMpEditor.onlineVideosViews.Count == 0)
          cboViews.Text = value;
        int i = 0;
        foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.onlineVideosViews)
        {
          if (value == tvv.Key)
          {
            cboViews.Text = tvv.Value;
            initialIndex = i;
            break;
          }
          i++;
        }
      }
    }

    public string onlinevideosHyperlinkCategory
    {
      get
      {
        return cboOnlineVideosCategories.Text;
      }
      set
      {
        cboOnlineVideosCategories.Text = value;
      }
    }

    public string movingPicturesHyperlinkParmeter
    {
      get
      {
        if (movPicsCategoryCombo.SelectedIndex == -1)
          return "false";

        int? id = GetMovPicsCategoryNodeID(movPicsCategoryCombo.SelectedNode);
        if (id != null)
          return id.ToString();
        else
          return "false";
      }
      set
      {
        if (value != "false" && !string.IsNullOrEmpty(value))
        {
          int id = 0;
          Int32.TryParse(value, out id);
          movPicsCategoryCombo.SelectedNode = GetMovPicsDBNodeFromID(id);
          initialMovPicIndex = movPicsCategoryCombo.SelectedIndex;
        }
        else
          movPicsCategoryCombo.SelectedIndex = -1;
      }
    }

    int? GetMovPicsCategoryNodeID(IDBNode node)
    {
      return ((DBNode<DBMovieInfo>)node).ID;
    }

    IDBNode GetMovPicsDBNodeFromID(int id)
    {
      return MovingPicturesCore.DatabaseManager.Get<DBNode<DBMovieInfo>>(id);
    }

    private void tbItemDisplayName_TextChanged(object sender, EventArgs e)
    {
      int start = tbItemDisplayName.SelectionStart;
      if (isIlegalXML(tbItemDisplayName.Text))
      {
        tbItemDisplayName.Text = tbItemDisplayName.Text.Substring(0, tbItemDisplayName.Text.Length - 1);
        tbItemDisplayName.SelectionStart = start;
        return;
      }
      tbItemDisplayName.Text = tbItemDisplayName.Text.ToUpper();
      tbItemDisplayName.SelectionStart = start;
    }

    bool isIlegalXML(string theValue)
    {
      Match m = formStreamedMpEditor.isIleagalXML.Match(theValue);
      return m.Success;
    }

    private void tbItemDisplayName_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        this.Hide();

      if (e.KeyCode == Keys.Escape)
      {
        tbItemDisplayName.Text = string.Empty;
        this.Hide();
      }
    }

    private void btSaveAndClose_Click(object sender, EventArgs e)
    {
      this.Hide();
    }

    private void btClearParameter_Click(object sender, EventArgs e)
    {
      if (movPicsCategoryCombo.Visible)
      {
        tbItemDisplayName.Text = baseName;
        movPicsCategoryCombo.SelectedIndex = -1;
      }
      else
      {
        cboViews.Text = string.Empty;
        txtSearch.Text = string.Empty;
        cboOnlineVideosCategories.Text = string.Empty;
      }
    }


    private void cboViews_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (currentSkinID == formStreamedMpEditor.onlineVideosSkinID)
        LoadOnlineVideosCategories(((ComboBox)sender).Text);

      txtSearch.Text = string.Empty;
      if (initialIndex != -1 && (tbItemDisplayName.Text == baseName || initialIndex != cboViews.SelectedIndex))
      {
        //TVSeries
        if (currentSkinID == formStreamedMpEditor.tvseriesSkinID)
          tbItemDisplayName.Text = formStreamedMpEditor.tvseriesViews[cboViews.SelectedIndex].Value;
        // Music
        if (currentSkinID == formStreamedMpEditor.musicSkinID)
          tbItemDisplayName.Text = formStreamedMpEditor.musicViews[cboViews.SelectedIndex].Value;
        // OnlineVideos
        if (currentSkinID == formStreamedMpEditor.onlineVideosSkinID)
          tbItemDisplayName.Text = formStreamedMpEditor.onlineVideosViews[cboViews.SelectedIndex].Value;
         
        initialIndex = cboViews.SelectedIndex;
      }
    }

    private void movPicsCategoryCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (movPicsCategoryCombo.SelectedIndex != -1)
        tbItemDisplayName.Text = movPicsCategoryCombo.Text;
    }

    private void movPicsCategoryCombo_DropDown(object sender, EventArgs e)
    {
      if (movPicsCategoryCombo.SelectedIndex == -1 && MovingPicturesCore.Settings.CategoriesEnabled)
      {
        movPicsCategoryCombo.SelectedNode = MovingPicturesCore.Settings.CategoriesMenu.RootNodes[0];
      }
    }

    void LoadOnlineVideosCategories(string site)
    {
      try
      {
        if (formStreamedMpEditor.theOnlineVideosViews.Contains(site))
        {
          // load onlinevideo categories
          cboOnlineVideosCategories.DataSource = formStreamedMpEditor.onlineVideosCategories[site];
        }
        cboOnlineVideosCategories.SelectedIndex = -1;
      }
      catch { }
    }

  }
}
