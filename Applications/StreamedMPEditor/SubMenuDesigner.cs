using System;
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

    List<string> ids = new List<string>();

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

    public void createSubmenu(ref formStreamedMpEditor.menuItem baseItem)
    {
      lbBaseMenuItem.Text += baseItem.name;

      string[] files = System.IO.Directory.GetFiles(formStreamedMpEditor.mpPaths.streamedMPpath);
      foreach (string file in files)
      {
        try
        {
          if (file.StartsWith("common") == false && file.Contains("Dialog") == false && file.Contains("dialog") == false && file.Contains("wizard") == false && file.Contains("xml.backup") == false)
          {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNode node = doc.DocumentElement.SelectSingleNode("/window/id");
            ids.Add(node.InnerText);
            lboxSubmenuXMLFiles.Items.Add(file.Remove(0, file.LastIndexOf(@"\") + 1).Replace(".xml", ""));
          }
        }
        catch { }
      }
      lboxSubmenuXMLFiles.Refresh();
      this.Show();
    }

    #endregion
    
    #region Private Methods



    #endregion




  }
}
