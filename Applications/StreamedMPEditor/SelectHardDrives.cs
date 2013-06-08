using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace StreamedMPEditor
{
  public partial class SelectHardDrives : Form
  {
    Helper helper = new Helper();
    List<string> hardDrives;

    public SelectHardDrives()
    {
      InitializeComponent();

      hardDrives = getConfiguredDrives();

      if (!(formStreamedMpEditor.driveFreeSpaceList == "false"))
      {
        lboxAvailableDrives.Items.Clear();
        string[] configuredDrives = formStreamedMpEditor.driveFreeSpaceList.Split(',');
        foreach (string hd in configuredDrives)
        {
          lboxSelectedDrives.Items.Add(hd + " (" + getDriveVolumeName(hd) + ")");
        }
        foreach (string hd in hardDrives)
        {
          string driveToAdd = hd + " (" + getDriveVolumeName(hd) + ")";
          if (!lboxSelectedDrives.Items.Contains(driveToAdd))
            lboxAvailableDrives.Items.Add(driveToAdd);
        }
      }
      else
      {
        foreach (string hd in hardDrives)
        {
          if (!string.IsNullOrEmpty(hd))
            addDriveDetails(hd);
        }
      }
    }

    void addDriveDetails(string hdName)
    {
      lboxAvailableDrives.Items.Add(hdName + " (" + getDriveVolumeName(hdName) + ")");
    }

    List<string> getConfiguredDrives()
    {
      string configuredDrives = helper.readMPConfiguration("DriveFreeSpace", "SelectedDirectories");
      string[] driveList = configuredDrives.Split(',');
      return driveList.ToList();
    }

    private void btAdd_Click(object sender, EventArgs e)
    {
      if (lboxAvailableDrives.SelectedIndex != -1 && lboxSelectedDrives.Items.Count < 3)
      {
        lboxSelectedDrives.Items.Add(lboxAvailableDrives.SelectedItem);
        lboxAvailableDrives.Items.RemoveAt(lboxAvailableDrives.SelectedIndex);
      }
    }

    private void btRemove_Click(object sender, EventArgs e)
    {
      if (lboxSelectedDrives.SelectedIndex != -1)
      {
        lboxAvailableDrives.Items.Clear();
        lboxSelectedDrives.Items.RemoveAt(lboxSelectedDrives.SelectedIndex);

        foreach (string hd in hardDrives)
        {
          string thisDrive = hd + " (" + getDriveVolumeName(hd) + ")";
          if (!lboxSelectedDrives.Items.Contains(thisDrive))
            lboxAvailableDrives.Items.Add(thisDrive);
        }
      }
    }

    private void btUp_Click(object sender, EventArgs e)
    {
      if (lboxSelectedDrives.SelectedIndex == 0 || lboxSelectedDrives.SelectedItem == null)
        return;

      int index = lboxSelectedDrives.SelectedIndex;

      Object listItem = lboxSelectedDrives.SelectedItem;
      lboxSelectedDrives.Items.RemoveAt(index);
      lboxSelectedDrives.Items.Insert(index - 1, listItem);
      lboxSelectedDrives.SelectedIndex = index - 1;
    }

    private void btDown_Click(object sender, EventArgs e)
    {
      if (lboxSelectedDrives.SelectedIndex == (lboxSelectedDrives.Items.Count - 1) || lboxSelectedDrives.SelectedItem == null)
        return;

      int index = lboxSelectedDrives.SelectedIndex;

      Object listItem = lboxSelectedDrives.SelectedItem;
      lboxSelectedDrives.Items.RemoveAt(index);
      lboxSelectedDrives.Items.Insert(index + 1, listItem);
      if (lboxSelectedDrives.SelectedIndex == (lboxSelectedDrives.Items.Count - 1))
        lboxSelectedDrives.SelectedIndex = index;
      else
        lboxSelectedDrives.SelectedIndex = index + 1;
    }

    string getDriveVolumeName(string theHD)
    {
      try
      {
        DriveInfo hdInfo = new DriveInfo(theHD);
        return hdInfo.VolumeLabel;
      }
      catch
      {
        return string.Empty;
      }
    }

    private void btSaveAndClose_Click(object sender, EventArgs e)
    {
      formStreamedMpEditor.driveFreeSpaceDrives.Clear();
      foreach (string hd in hardDrives)
      {
        if (!string.IsNullOrEmpty(hd))
        {
          string thisDrive = hd + " (" + getDriveVolumeName(hd) + ")";
          if (lboxSelectedDrives.Items.Contains(thisDrive))
            formStreamedMpEditor.driveFreeSpaceDrives.Add(hd);
        }
      }

      this.Hide();
    }
  }
}
