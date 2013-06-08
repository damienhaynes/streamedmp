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
  public partial class MovPicsSummaryOptions : Form
  {
    public MovPicsSummaryOptions()
    {
      InitializeComponent();
    }

    public bool HideRuntime
    {
      get { return cbHideRuntime.Checked; }
      set { cbHideRuntime.Checked = value; }
    }

    public bool HideCertification
    {
      get { return cbHideCertification.Checked; }
      set { cbHideCertification.Checked = value; }
    }

    public bool HideRating
    {
      get { return cbHideRating.Checked; }
      set { cbHideRating.Checked = value; }
    }

    public bool UseTextRating
    {
      get { return cbUseTextRating.Checked; }
      set { cbUseTextRating.Checked = value; }
    }

    public bool DisableFadeLabels
    {
      get { return cbDisableFadeLabels.Checked; }
      set { cbDisableFadeLabels.Checked = value; }
    }

    public string MovieTitleFont
    {
      get
      {
        if (cboxMRMovieTitle.Text.StartsWith("mediastream10tc"))
          return "mediastream10tc";
        else
          return cboxMRMovieTitle.Text;
      }

      set
      {
        if (value.StartsWith("mediastream10tc"))
          cboxMRMovieTitle.SelectedIndex = cboxMRMovieTitle.Items.IndexOf("mediastream10tc (Bold)");
        else
          cboxMRMovieTitle.SelectedIndex = cboxMRMovieTitle.Items.IndexOf("mediastream10c");
      }
    }

    public string MovieDetailFont
    {
      get
      {
        if (cboxMRMovieDetail.Text.StartsWith("mediastream10tc"))
          return "mediastream10tc";
        else
          return cboxMRMovieDetail.Text;
      }

      set
      {
        if (value.StartsWith("mediastream10tc"))
          cboxMRMovieDetail.SelectedIndex = cboxMRMovieDetail.Items.IndexOf("mediastream10tc (Bold)");
        else
          cboxMRMovieDetail.SelectedIndex = cboxMRMovieDetail.Items.IndexOf("mediastream10c");
      }
    }

    private void btSaveAndExit_Click(object sender, EventArgs e)
    {
      this.Hide();
    }
  }
}
