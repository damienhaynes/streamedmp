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
using System.Xml;
using SMPCheckSum;

namespace SMPCheckSum_Test
{
  public partial class SMPCheckSum_Test : Form
  {
    SkinInfo skInfo = new SkinInfo();
    string[] skinFiles;
    CheckSum checkSum = new CheckSum();
    string baseDirectory = null;
      
    public SMPCheckSum_Test()
    {
      InitializeComponent();
      baseDirectory = SkinInfo.mpPaths.streamedMPpath;
      cboxXmlFolder.Items.Add(baseDirectory);
      cboxXmlFolder.SelectedIndex = 0;

      if (Properties.Settings.Default.baseDirs.Count < 2)
      {
        baseDirectory = SkinInfo.mpPaths.streamedMPpath;
        Properties.Settings.Default.baseDirs.Add(baseDirectory);
        Properties.Settings.Default.Save();
      }

      for (int i = 1; i < Properties.Settings.Default.baseDirs.Count; i++)
      {
        cboxXmlFolder.Items.Insert(i,Properties.Settings.Default.baseDirs[i].ToString());
        baseDirectory = cboxXmlFolder.Items[i - 1].ToString();
        //MessageBox.Show(baseDirectory);
      }
      //MessageBox.Show("xx" + baseDirectory);
      cboxXmlFolder.Text = baseDirectory;
      readAndVerifyFiles();
    }


    private void SMPCheckSum_Load(object sender, EventArgs e)
    {
      btVerifyChksum.Enabled = false;
      btReGenerate.Enabled = false;
    }

    
    private void btAddChksumToAll_Click(object sender, EventArgs e)
    {
      btVerifyChksum.Enabled = false;
      btReGenerate.Enabled = false;
      DirectoryInfo dInfo = new DirectoryInfo(baseDirectory);
      pbCheckSum.Maximum = dInfo.GetFiles("*.xml").Length;
      pbCheckSum.Value = 0;

      skinFiles = Directory.GetFiles(baseDirectory, "*.xml");
      skinFile fileToCheck = new skinFile();

      chkSumFiles.Items.Clear();
      
      foreach (string chkFile in skinFiles)
      {
        fileToCheck.pathAndFilename = chkFile;
        fileToCheck.fileName = Path.GetFileName(chkFile);
        if (checkSum.Get(chkFile) == null)
          fileToCheck.checkSum = checkSum.Add(chkFile);
        else
          fileToCheck.checkSum = checkSum.Get(chkFile);

        ListViewItem item = new ListViewItem(new[] { fileToCheck.fileName, fileToCheck.checkSum });
        if (checkSum.Compare(chkFile))
          item.ImageIndex = 0;
        else
          item.ImageIndex = 1;

        chkSumFiles.Items.Add(item);
        pbCheckSum.Value++;
      }
    }
    private void btCompare_Click(object sender, EventArgs e)
    {
      readAndVerifyFiles();
    }


    void readAndVerifyFiles()
    {
      btVerifyChksum.Enabled = false;
      btReGenerate.Enabled = false;
      DirectoryInfo dInfo = new DirectoryInfo(baseDirectory);
      pbCheckSum.Maximum = dInfo.GetFiles("*.xml").Length;
      pbCheckSum.Value = 0;

      skinFiles = Directory.GetFiles(baseDirectory, "*.xml");
      skinFile fileToCheck = new skinFile();

      chkSumFiles.Items.Clear();

      foreach (string chkFile in skinFiles)
      {
        fileToCheck.pathAndFilename = chkFile;
        fileToCheck.fileName = Path.GetFileName(chkFile);
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
      for (int i = 0; i < chkSumFiles.SelectedItems.Count; i++)
      {
        if (!checkSum.Compare(Path.Combine(baseDirectory, chkSumFiles.SelectedItems[0].Text)))
          chkSumFiles.SelectedItems[0].ImageIndex = 1;
        else
          chkSumFiles.SelectedItems[0].ImageIndex = 0;
      }
      chkSumFiles.Refresh();
      btVerifyChksum.Enabled = false;
      btReGenerate.Enabled = false;
    }

    private void btReGenerate_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < chkSumFiles.SelectedItems.Count; i++)
      {
        checkSum.Replace(Path.Combine(baseDirectory, chkSumFiles.SelectedItems[i].Text));
        if (!checkSum.Compare(Path.Combine(baseDirectory, chkSumFiles.SelectedItems[i].Text)))
          chkSumFiles.SelectedItems[i].ImageIndex = 1;
        else
          chkSumFiles.SelectedItems[i].ImageIndex = 0;
      }
      chkSumFiles.Refresh();
      btVerifyChksum.Enabled = false;
      btReGenerate.Enabled = false;
    }
    
    private void btRemoveCheckSum_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < chkSumFiles.SelectedItems.Count; i++)
      {
      checkSum.Remove(Path.Combine(baseDirectory, chkSumFiles.SelectedItems[i].Text));
      if (!checkSum.Compare(Path.Combine(baseDirectory, chkSumFiles.SelectedItems[i].Text)))
        chkSumFiles.SelectedItems[i].ImageIndex = 1;
      else
        chkSumFiles.SelectedItems[i].ImageIndex = 0;
      }
      chkSumFiles.Refresh();
      btVerifyChksum.Enabled = false;
      btReGenerate.Enabled = false;
    }

    private void chkSumFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
      btVerifyChksum.Enabled = true;
      btReGenerate.Enabled = true;
    }


    private void button1_Click(object sender, EventArgs e)
    {
      checkSum.Add("c:\\xyz.xml");
    }

    private void btInvaildFile_Click(object sender, EventArgs e)
    {
      checkSum.Get("G:\\Development\\StreamedMP 1.2.1 Release Notes.rtf");
    }

    private void btBrowse_Click(object sender, EventArgs e)
    {
      folderBrowserDialog.ShowNewFolderButton = false;
      folderBrowserDialog.Description = "Select the folder containing the images for this menu item";
      folderBrowserDialog.SelectedPath = SkinInfo.mpPaths.streamedMPpath;
      if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
      {
        baseDirectory = folderBrowserDialog.SelectedPath;
        chkSumFiles.Refresh();
        btVerifyChksum.Enabled = false;
        btReGenerate.Enabled = false;
        if (!Properties.Settings.Default.baseDirs.Contains(baseDirectory))
        {
          cboxXmlFolder.Items.Add(folderBrowserDialog.SelectedPath);
          Properties.Settings.Default.baseDirs.Add(baseDirectory);
          Properties.Settings.Default.Save();
          cboxXmlFolder.SelectedIndex++;
        }
        else
          cboxXmlFolder.SelectedIndex = cboxXmlFolder.Items.IndexOf(baseDirectory);

      }
    }

    public class skinFile
    {
      public string pathAndFilename { get; set; }
      public string fileName { get; set; }
      public string checkSum { get; set; }
    }

    private void cboxXmlFolder_SelectedIndexChanged(object sender, EventArgs e)
    {
      baseDirectory = cboxXmlFolder.SelectedItem.ToString();
      readAndVerifyFiles();
    }

    private void SMPCheckSum_Test_FormClosing(object sender, FormClosingEventArgs e)
    {
      Properties.Settings.Default.Save();
    }


  }
}
