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
  public partial class SubItemProperties : Form
  {
    public SubItemProperties(bool showHyperlinkParameterDialog)
    {
      InitializeComponent();
      gbHyperlinkParameter.Enabled = false;

      if (showHyperlinkParameterDialog)
          gbHyperlinkParameter.Enabled = true;

      foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.tvseriesViews)
      {
          cboTVSViews.Items.Add(tvv.Value);
      }
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
            return cboTVSViews.Text;
        }
        set
        {
            cboTVSViews.Text = value;
        }
    }

    public string HypelinkParameter
    {
        get
        {
            if (formStreamedMpEditor.tvseriesViews.Count == 0)
                return cboTVSViews.Text;

            foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.tvseriesViews)
            {
                if (tvv.Value == cboTVSViews.Text)
                    return tvv.Key;
            }
            return "false";
        }

        set
        {
            if (formStreamedMpEditor.tvseriesViews.Count == 0)
                cboTVSViews.Text = value;

            foreach (KeyValuePair<string, string> tvv in formStreamedMpEditor.tvseriesViews)
            {
                if (value == tvv.Key)
                    cboTVSViews.Text = tvv.Value;
            }
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

    private void btSaveAndClose_Click(object sender, EventArgs e)
    {
        this.Hide();
    }

    private void btClearParameter_Click(object sender, EventArgs e)
    {
        cboTVSViews.Text = string.Empty;
    }
  }
}
