using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;


namespace SMPCheckSum
{
  public partial class SMPCheckSum : Form
  {
    SkinInfo skInfo = new SkinInfo();
    string[] skinFiles;
    string xml;
    CheckSum checkSum = new CheckSum();


    public SMPCheckSum()
    {
      InitializeComponent();
    }


    private void SMPCheckSum_Load(object sender, EventArgs e)
    {
      btVerifyChksum.Enabled = false;
    }

    
    private void btAddChksumToAll_Click(object sender, EventArgs e)
    {
      btVerifyChksum.Enabled = false;

      DirectoryInfo dInfo = new DirectoryInfo(SkinInfo.mpPaths.streamedMPpath);
      pbCheckSum.Maximum = dInfo.GetFiles("*.xml").Length;

      skinFiles = Directory.GetFiles(SkinInfo.mpPaths.streamedMPpath, "*.xml");
      skinFile fileToCheck = new skinFile();

      chkSumFiles.Items.Clear();
      
      foreach (string chkFile in skinFiles)
      {

        fileToCheck.pathAndFilename = chkFile;
        fileToCheck.fileName = Path.GetFileName(chkFile);
        if (checkSum.Read(chkFile) == null)
          fileToCheck.checkSum = checkSum.Add(chkFile);
        else
          fileToCheck.checkSum = checkSum.Read(chkFile);

        ListViewItem item = new ListViewItem(new[] { fileToCheck.fileName, fileToCheck.checkSum });
        if (checkSum.Compare(chkFile))
          item.ImageIndex = 0;
        else
          item.ImageIndex = 1;

        chkSumFiles.Items.Add(item);
        pbCheckSum.Value++;
      }
    }



    private void btVerifyChksum_Click(object sender, EventArgs e)
    {
      if (!checkSum.Compare(Path.Combine(SkinInfo.mpPaths.streamedMPpath, chkSumFiles.SelectedItems[0].Text)))
        chkSumFiles.SelectedItems[0].ImageIndex = 1;
      else
        chkSumFiles.SelectedItems[0].ImageIndex = 0;
      chkSumFiles.Refresh();
    }

    public class skinFile
    {
      public string pathAndFilename { get; set; }
      public string fileName { get; set; }
      public string checkSum { get; set; }
    }

    private void chkSumFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
      btVerifyChksum.Enabled = true;
    }

  }
}
