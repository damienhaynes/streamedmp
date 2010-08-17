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

    public string mrSeriesFont
    {
      get 
      {
        if (cboxMRSeriesFont.Text.StartsWith("mediastream10tc"))
          return "mediastream10tc";
        else
          return cboxMRSeriesFont.Text; 
      }

      set { cboxMRSeriesFont.Text = value; }
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

      set { cboxMREpisodeFont.Text = value; }
    }


    private void btSaveAndExit_Click(object sender, EventArgs e)
    {
      this.Hide();
    }



  }
}
