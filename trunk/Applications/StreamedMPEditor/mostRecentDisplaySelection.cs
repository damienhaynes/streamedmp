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
  public partial class mostRecentDisplaySelection : Form
  {
    public mostRecentDisplaySelection()
    {
      InitializeComponent();

      rbSubOff.Checked = true;
      rbSubTVSeries.Checked = false;
      rbSubAddedMovies.Checked = false;
      rbSubWatchedMovies.Checked = false;
      rbSubMusic.Checked = false;
      rbSubTV.Checked = false;
    }

    public formStreamedMpEditor.displayMostRecent mrToDisplay
    {
      get { return selectedMostRecent(); }
      set { setMostRecent(value); }
    }

    private formStreamedMpEditor.displayMostRecent selectedMostRecent()
    {
      if (rbSubTVSeries.Checked)
        return formStreamedMpEditor.displayMostRecent.tvSeries;

      if (rbSubAddedMovies.Checked)
        return formStreamedMpEditor.displayMostRecent.moviesAdded;

      if (rbSubWatchedMovies.Checked)
        return formStreamedMpEditor.displayMostRecent.moviesWatched;

      if (rbSubMusic.Checked)
        return formStreamedMpEditor.displayMostRecent.music;

      if (rbSubTV.Checked)
        return formStreamedMpEditor.displayMostRecent.recordedTV;

      return formStreamedMpEditor.displayMostRecent.off;
    
    }
    private void setMostRecent(formStreamedMpEditor.displayMostRecent rbSetting)
    {
      switch (rbSetting)
      {
        case formStreamedMpEditor.displayMostRecent.off:
          rbSubOff.Checked = true;
          break;
        case formStreamedMpEditor.displayMostRecent.tvSeries:
          rbSubTVSeries.Checked = true;
          break;
        case formStreamedMpEditor.displayMostRecent.moviesAdded:
          rbSubAddedMovies.Checked = true;
          break;
        case formStreamedMpEditor.displayMostRecent.moviesWatched:
          rbSubWatchedMovies.Checked = true;
          break;
        case formStreamedMpEditor.displayMostRecent.music:
          rbSubMusic.Checked = true;
          break;
        case formStreamedMpEditor.displayMostRecent.recordedTV:
          rbSubTV.Checked = true;
          break;
      }
    }

    private void btSaveAndClose_Click(object sender, EventArgs e)
    {
      this.Hide();
    }
  }
}
