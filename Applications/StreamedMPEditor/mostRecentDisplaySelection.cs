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
      rbSubMovies.Checked = false;
      rbSubMusic.Checked = false;
      rbSubTV.Checked = false;
      rbSubFreeDriveSpace.Checked = false;
      rbSubPowerControl.Checked = false;
      rbSubSleepControl.Checked = false;
      rbSubStocks.Checked = false;
      rbSubHtpcInfo.Checked = false;

    }

    public bool disableMusicRB 
    {
      set
      {
        rbSubMusic.Enabled = value; ;
      }
    }

    public bool disableRecordedTVRB
    {
      set
      {
        rbSubTV.Enabled = value; ;
      }
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

      if (rbSubMovies.Checked)
        return formStreamedMpEditor.displayMostRecent.movies;

      if (rbSubMusic.Checked)
        return formStreamedMpEditor.displayMostRecent.music;

      if (rbSubTV.Checked)
        return formStreamedMpEditor.displayMostRecent.recordedTV;

      if (rbSubFreeDriveSpace.Checked)
          return formStreamedMpEditor.displayMostRecent.freeDriveSpace;

      if (rbSubPowerControl.Checked)
          return formStreamedMpEditor.displayMostRecent.powerControl;

      if (rbSubSleepControl.Checked)
          return formStreamedMpEditor.displayMostRecent.sleepControl;

      if (rbSubStocks.Checked)
          return formStreamedMpEditor.displayMostRecent.stocks;

      if (rbSubHtpcInfo.Checked)
          return formStreamedMpEditor.displayMostRecent.htpcInfo;

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
            case formStreamedMpEditor.displayMostRecent.movies:
                rbSubMovies.Checked = true;
                break;
            case formStreamedMpEditor.displayMostRecent.music:
                rbSubMusic.Checked = true;
                break;
            case formStreamedMpEditor.displayMostRecent.recordedTV:
                rbSubTV.Checked = true;
                break;
            case formStreamedMpEditor.displayMostRecent.freeDriveSpace:
                rbSubFreeDriveSpace.Checked = true;
                break;
            case formStreamedMpEditor.displayMostRecent.powerControl:
                rbSubPowerControl.Checked = true;
                break;
            case formStreamedMpEditor.displayMostRecent.sleepControl:
                rbSubSleepControl.Checked = true;
                break;
            case formStreamedMpEditor.displayMostRecent.stocks:
                rbSubStocks.Checked = true;
                break;
            case formStreamedMpEditor.displayMostRecent.htpcInfo:
                rbSubHtpcInfo.Checked = true;
                break;

        }
    }

    private void btSaveAndClose_Click(object sender, EventArgs e)
    {
      this.Hide();
    }

  }
}
