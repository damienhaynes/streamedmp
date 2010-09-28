using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace StreamedMPEditor
{
  public partial class ItemNameDialog : Form
  {
    public ItemNameDialog()
    {
      InitializeComponent();
    }

    public string itemDisplayName
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

  }
}
