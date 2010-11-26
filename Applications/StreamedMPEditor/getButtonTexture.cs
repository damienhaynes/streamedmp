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

    public getButtonTexture()
    {
      InitializeComponent();

      displayIcons();
    }


    public List<string> IconList
    {
      get
      {
        return iconList();
      }
    }

    void displayIcons()
    {
      int xPos = 16, yPos = 51;
      foreach (string icon in IconList)
      {
        PictureBox newPBox = new PictureBox();
        newPBox.Size = new Size(64, 64);
        newPBox.Location = new Point(xPos, yPos);
        workingImage = Image.FromFile(icon);
        newPBox.Image = workingImage.GetThumbnailImage(64, 64, null, new IntPtr());
        workingImage.Dispose();
        this.Controls.Add(newPBox);
        xPos += 74;
      }

    }

    List<string> iconList()
    {
      Helper helper = new Helper();

      DirectoryInfo dInfo = new DirectoryInfo(Path.Combine(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "Media"),"homebuttons"));
      //get list of files from directory
      foreach (FileInfo fInfo in dInfo.GetFiles("*.png"))
      {
        iconFiles.Add(fInfo.FullName);
      }

      return iconFiles;
    }



  }
}
