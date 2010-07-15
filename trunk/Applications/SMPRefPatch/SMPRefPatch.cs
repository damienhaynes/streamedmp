using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace SMPRefPatch
{
  public partial class fmRefPatch : Form
  {
    public fmRefPatch()
    {
      InitializeComponent();
    }

    private void fmRefPatch_Load(object sender, EventArgs e)
    {
      string xml;
      Version minVersion = new Version("1.0.0.1008");
      SkinInfo.GetMediaPortalSkinPath();
      if (minVersion.CompareTo(SkinInfo.streamedMPVersion()) >= 0)
      {
        MessageBox.Show("     Sorry - Update to StreamedMP V" + SkinInfo.streamedMPVersion() + "\n\n is not supported - Please download latest Version");
        Application.Exit();
      }


      Version smpVersion = SkinInfo.streamedMPVersion();
      FileStream referancesFile = new FileStream(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "references.xml"),FileMode.Open,FileAccess.Read);
      StreamReader reader = new StreamReader(referancesFile);
      xml = reader.ReadToEnd();
      reader.Close();

      xml = xml.Replace("<version>1.0.1.0</version>", "<version>1.1.0.0</version>");
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(xml);
      Encoding encoding = Encoding.GetEncoding("utf-8");
      XmlTextWriter writer = new XmlTextWriter(Path.Combine(SkinInfo.mpPaths.streamedMPpath, "references.xml"), encoding);
      writer.Formatting = Formatting.Indented;
      doc.Save(writer);
    }

    private void appExit_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }
  }
}
