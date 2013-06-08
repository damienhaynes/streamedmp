using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StreamedMPEditor
{
  public partial class TVSereisFormatOptions : Form
  {
    public TVSereisFormatOptions()
    {
      InitializeComponent();
    }

    public bool mrSeriesEpisodeFormat
    {
      get { return cbMRSeriesEpisodeFormat.Checked; }
      set { cbMRSeriesEpisodeFormat.Checked = value; }
    }

    public bool mrTitleLast
    {
      get { return cbTitleSwap.Checked; }
      set { cbTitleSwap.Checked = value; }
    }

    public bool mrDisableFadeLabels
    {
      get { return cbDisableFadeLabels.Checked; }
      set { cbDisableFadeLabels.Checked = value; }
    }
    public string mrSeriesFont
    {
      get 
      {
        if (cboxMRSeriesFont.Text.StartsWith("mediastream10tc"))
          return "mediastream10tc";
        else
          return cboxMRSeriesFont.Text; 
      }

      set
      {
        if (value.StartsWith("mediastream10tc"))
          cboxMRSeriesFont.SelectedIndex = cboxMRSeriesFont.Items.IndexOf("mediastream10tc (Bold)");
        else
          cboxMRSeriesFont.SelectedIndex = cboxMRSeriesFont.Items.IndexOf("mediastream10c");
      }
    }

    public string mrEpisodeFont
    {
      get
      {
        if (cboxMREpisodeFont.Text.StartsWith("mediastream10tc"))
          return "mediastream10tc";
        else
          return cboxMREpisodeFont.Text;
      }

      set
      {
        if (value.StartsWith("mediastream10tc"))
          cboxMREpisodeFont.SelectedIndex = cboxMREpisodeFont.Items.IndexOf("mediastream10tc (Bold)");
        else
          cboxMREpisodeFont.SelectedIndex = cboxMREpisodeFont.Items.IndexOf("mediastream10c");
      }
    }

    private void btSaveAndExit_Click(object sender, EventArgs e)
    {
      this.Hide();
    }
  }
}
