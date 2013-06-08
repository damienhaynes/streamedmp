using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace StreamedMPEditor
{
  public partial class getButtonTexture : Form
  {
    List<string> iconFiles = new List<string>();
    Image workingImage = null;
    Image boarderImage = null;
    string streamedMPMediaPath = string.Empty;

    public static int workingIndex = -1;
    public static string buttonTexture = string.Empty;
    public static string menuName = string.Empty;

    public getButtonTexture()
    {
      InitializeComponent();
    }

    public void initButtonTexture()
    {
      streamedMPMediaPath = Path.Combine(SkinInfo.mpPaths.streamedMPpath, "media");
      iconList();
      groupBox2.Text = "Available Menu Icons (" + iconFiles.Count.ToString() + ")";
      groupBox2.Refresh();
    }

    public int menIndex
    {
      set
      {
        workingIndex = value;
      }
    }

    public string SelectedIcon
    {
      get
      {
        if (string.IsNullOrEmpty(buttonTexture))
          return string.Empty;
        else
          return "homeButtons\\" + Path.GetFileName(buttonTexture);
      }
      set
      {
        buttonTexture = value;
      }
    }

    public string MenuItem
    {
      set
      {
        menuName = value;
      }
    }

    public void setButtonTexture()
    {
      displayIcons();
      this.ShowDialog();
    }

    void displayIcons()
    {
      string currentIcon = string.Empty;
      lbMenuItem.Text = menuName;
      // Create currently selected icon picture box
      if (string.IsNullOrEmpty(buttonTexture) || !File.Exists(Path.Combine(streamedMPMediaPath, buttonTexture)))
        currentIcon = Path.Combine(streamedMPMediaPath, "homeButtons\\_noimage.png");
      else
        currentIcon = buttonTexture;

      workingImage = Image.FromFile(Path.Combine(streamedMPMediaPath, currentIcon));
      pbCurrentIcon.Image = workingImage.GetThumbnailImage(128, 128, null, new IntPtr());
      workingImage.Dispose();
      flIconPanel.Controls.Clear();
      foreach (string icon in iconList())
      {
        PictureBox newPBox = new PictureBox();
        newPBox.Size = new Size(64, 64);
        workingImage = Image.FromFile(icon);
        newPBox.Image = workingImage.GetThumbnailImage(64, 64, null, new IntPtr());
        workingImage.Dispose();
        newPBox.Tag = icon;
        newPBox.Name = Path.GetFileNameWithoutExtension(icon);
        newPBox.Click += new System.EventHandler(newPBox_Click);
        newPBox.MouseEnter += new System.EventHandler(newPBox_MouseEnter);
        newPBox.MouseLeave += new System.EventHandler(newPBox_MouseLeave);
        flIconPanel.Controls.Add(newPBox);
      }
    }

    public void newPBox_Click(object sender, EventArgs e)
    {
      string newIcon = ((PictureBox)sender).Tag.ToString();
      workingImage = Image.FromFile(newIcon);
      pbCurrentIcon.Image = workingImage.GetThumbnailImage(128, 128, null, new IntPtr());
      workingImage.Dispose();
      buttonTexture = newIcon;
    }

    public void newPBox_MouseEnter(object sender, EventArgs e)
    {
      string homeButtonPath = Path.Combine(streamedMPMediaPath, "homebuttons");
      string newIcon = ((PictureBox)sender).Tag.ToString();

      boarderImage = Image.FromFile(Path.Combine(homeButtonPath, "_glow.png")).GetThumbnailImage(64, 64, null, new IntPtr());
      workingImage = Image.FromFile(newIcon).GetThumbnailImage(64, 64, null, new IntPtr());

      using (Graphics grfx = Graphics.FromImage(boarderImage))
      {
        grfx.DrawImage(workingImage, 0, 0);
      }
      ((PictureBox)sender).Image = boarderImage;
      // Clean-up
      workingImage.Dispose();
      //boarderImage.Dispose();
    }

    public void newPBox_MouseLeave(object sender, EventArgs e)
    {
      string newIcon = ((PictureBox)sender).Tag.ToString();
      ((PictureBox)sender).Image = Image.FromFile(newIcon).GetThumbnailImage(64, 64, null, new IntPtr());
    }


    List<string> iconList()
    {
      Helper helper = new Helper();
      DirectoryInfo dInfo = new DirectoryInfo(Path.Combine(streamedMPMediaPath, "homebuttons"));
      iconFiles.Clear();
      //get list of files from directory
      foreach (FileInfo fInfo in dInfo.GetFiles("*.png"))
      {
        if (!fInfo.Name.StartsWith("_"))
          iconFiles.Add(fInfo.FullName);
      }
      return iconFiles;
    }

    private void btSaveAndExit_Click(object sender, EventArgs e)
    {
      this.Hide();
    }

    private void buttonThemes_Click(object sender, EventArgs e)
    {
      showButtonTheme showTheme = new showButtonTheme();
      showTheme.ShowDialog();
      SelectedIcon = formStreamedMpEditor.menuItems[workingIndex].buttonTexture;
      workingImage = Image.FromFile(Path.Combine(streamedMPMediaPath, SelectedIcon));
      pbCurrentIcon.Image = workingImage.GetThumbnailImage(128, 128, null, new IntPtr());
      workingImage.Dispose();
    }
  }
}
