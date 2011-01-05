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
  public partial class showButtonTheme : Form
  {

    List<string> iconFiles = new List<string>();
    Image workingImage = null;
    //Image boarderImage = null;
    string streamedMPMediaPath = Path.Combine(SkinInfo.mpPaths.streamedMPpath, "media");
    string theTheme = "Black";

    public showButtonTheme()
    {
      InitializeComponent();
      populateScreen(theTheme);
    }

    List<string> iconList(string theme)
    {
      Helper helper = new Helper();
      DirectoryInfo dInfo = new DirectoryInfo(Path.Combine(streamedMPMediaPath, "homebuttons"));
      iconFiles.Clear();
      //get list of files from directory
      foreach (FileInfo fInfo in dInfo.GetFiles(theme + "*.png"))
      {
        if (!fInfo.Name.StartsWith("_"))
          iconFiles.Add(fInfo.FullName);
      }
      return iconFiles;
    }

    private void cboSelectTheme_SelectedIndexChanged(object sender, EventArgs e)
    {
      theTheme = cboSelectTheme.Text;
      iconList(cboSelectTheme.Text);
      populateScreen(cboSelectTheme.Text);
    }

    void populateScreen(string theme)
    {
      iconFiles.Clear();
      flThemeButtons.Controls.Clear();
      themePreview.Image = Image.FromFile(Path.Combine(Path.Combine(streamedMPMediaPath, "homebuttons"), theme + "themepreview.jpg"));
      iconList(theme);
      foreach (string icon in iconFiles)
      {
        PictureBox newPBox = new PictureBox();
        newPBox.Size = new Size(64, 64);
        workingImage = Image.FromFile(icon);
        newPBox.Image = workingImage.GetThumbnailImage(64, 64, null, new IntPtr());
        workingImage.Dispose();
        flThemeButtons.Controls.Add(newPBox);
      }
    }

    private void btSetActiveTheme_Click(object sender, EventArgs e)
    {
      for (int i = 0; i<formStreamedMpEditor.menuItems.Count;i++)
      {
        formStreamedMpEditor.menuItems[i].buttonTexture = formStreamedMpEditor.setDefaultIcons(int.Parse(formStreamedMpEditor.menuItems[i].hyperlink), theTheme);
      }
      this.Close();
    }

    private void btClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

  }
}
