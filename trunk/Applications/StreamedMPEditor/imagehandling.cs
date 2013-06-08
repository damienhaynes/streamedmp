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
using System.Threading;
using System.Drawing.Imaging;
using System.Security.Cryptography;


namespace StreamedMPEditor
{
  public partial class formStreamedMpEditor
  {
    // Image handling declares
    List<string> fileResults = new List<string>();

    int imagePointer = 0;
    int totalImages = 0;
    int pBoxElement = 0;

    private void GetDefaultBackgroundImages()
    {
      Image workingImage = null;
      Label[] bgLabels = new Label[bgItems.Count];
      Label[] bgCount = new Label[bgItems.Count];
      Button[] bgButtons = new Button[bgItems.Count];
      ToolTip labelsToolTip;

      for (int i = 0; i < 48; i++)
        defImgs.picBoxes[i] = null;

      defImgs.count = bgItems.Count;
      selectPanel.Visible = false;
      pBoxElement = 0;
      imagePointer = 0;
      totalImages = 0;
      pBoxElement = 0;
      defImgs.activePicBox = 0;
      defImgs.activeSelectPbox = 0;
      defImgs.activeBGItem = 0;
      imagePointer = 0;
      defaultBackgrounds.Controls.Clear();
      fileResults.Clear();

      //
      // Set the default base directory for backgrounds (need to work out how to handle custom directories)
      //
      int xPos = 16, yPos = 51;
      int bg_item_count = 1;
      foreach (backgroundItem bgItem in bgItems)
      {
        if (bgItem.fanartHandlerEnabled)
          continue;

        PictureBox newPBox = new PictureBox();
        Label newBGlabel = new Label();
        Label newBGCount = new Label();
        Button newBGButton = new Button();
        labelsToolTip = new ToolTip(new System.ComponentModel.Container());

        // Create Picturebox Control and create/load thumbnail
        newPBox.Size = new Size(160, 80);
        newPBox.Location = new Point(xPos, yPos);

        if (!System.IO.File.Exists((imageDir(bgItem.image))))
        {
          string[] fileList = getFileListing(Path.GetDirectoryName(imageDir(bgItem.image)), "*.*", true);
          createDefaultJpg(Path.GetDirectoryName(imageDir(bgItem.image)));
        }
        else
        {
          string[] fileList = getFileListing(Path.GetDirectoryName(imageDir(bgItem.image)), "*.*", true);
        }
        totalImages = fileResults.Count();

        string dirParent = Path.GetDirectoryName(imageDir(bgItem.image));
        SkinInfo.mpPaths.fanartBasePath = dirParent;

        string img = imageDir(bgItem.image);
        if (File.Exists(img))
        {
          workingImage = Image.FromFile(img);
          newPBox.Image = workingImage.GetThumbnailImage(160, 80, null, new IntPtr());
          workingImage.Dispose();
        }

        defImgs.picBoxes[pBoxElement] = newPBox;
        defaultBackgrounds.Controls.Add(defImgs.picBoxes[pBoxElement]);

        //Create the label
        newBGlabel.Size = new Size(90, 15);
        newBGlabel.Location = new Point(xPos, (yPos - 21));
        if (bgItem.mname.Count > 1)
        {
          string toolTipString = "This is a shared background Folder\nbetween these menu entries\n\n";
          for (int i = 0; i < bgItem.mname.Count; ++i)
          {
            toolTipString += "Menu Item: " + bgItem.mname[i] + "\n";
          }
          labelsToolTip.SetToolTip(newBGlabel, toolTipString);
          labelsToolTip.ToolTipIcon = ToolTipIcon.Info;
          labelsToolTip.IsBalloon = true;
          labelsToolTip.ToolTipTitle = "Default Background Images";
          newBGlabel.Text = bgItem.mname[0] + " +";
        }
        else
          newBGlabel.Text = bgItem.mname[0];

        newBGlabel.Font = new Font(newBGlabel.Font, FontStyle.Bold);
        bgLabels[pBoxElement] = newBGlabel;
        defaultBackgrounds.Controls.Add(bgLabels[pBoxElement]);

        //Create number of files label
        newBGCount.Location = new Point(xPos, (yPos + 82));
        newBGCount.Size = new Size(160, 15);
        newBGCount.Text = "Images Available: " + (totalImages).ToString();
        bgCount[pBoxElement] = newBGCount;
        defaultBackgrounds.Controls.Add(bgCount[pBoxElement]);

        //Create change button
        newBGButton.Size = new Size(61, 21);
        newBGButton.Location = new Point((xPos + 97), (yPos - 24));
        newBGButton.Text = "Change";
        newBGButton.Tag = pBoxElement.ToString();
        newBGButton.Click += new System.EventHandler(bgChangeButton_Click);
        newBGButton.Name = bgItem.mname[0];
        if (totalImages < 1)
          newBGButton.Enabled = false;
        else
          newBGButton.Enabled = true;

        bgButtons[pBoxElement] = newBGButton;
        defaultBackgrounds.Controls.Add(bgButtons[pBoxElement]);

        // Increment the position and pointer counters - currently supports 20 backgrounds
        pBoxElement++;
        xPos += 185;
        if (pBoxElement == 4)
        {
          yPos += 126;
          xPos = 16;
        }
        if (pBoxElement == 8 || pBoxElement == 12 || pBoxElement == 16 || pBoxElement == 20)
        {
          yPos += 126;
          xPos = 16;
        }
        bg_item_count++;
        if (bg_item_count > 12)
          defaultBackgrounds.AutoScroll = true;
      }

      // Configure the Panel
      selectPanel.Size = new Size(756, 120);
      selectPanel.Location = new Point(12, 101);
      selectPanel.BackColor = Color.White;
      selectPanel.BorderStyle = BorderStyle.FixedSingle;

      //Configure the 3 PictureBox Controls
      xPos = 70;
      yPos = 5;
      for (int i = 0; i < 3; i++)
      {
        defImgs.NewPicBoxes[i].Size = new Size(160, 80);
        defImgs.NewPicBoxes[i].Location = new Point(xPos, yPos);
        defImgs.NewPicBoxes[i].Name = "pBox" + i.ToString();
        defImgs.NewPicBoxes[i].Tag = i.ToString();
        defImgs.NewPicBoxes[i].BorderStyle = BorderStyle.Fixed3D;
        defImgs.NewPicBoxes[i].Cursor = Cursors.Hand;
        xPos += 230;
      }

      //Configure next and previous buttons
      nextBatch.Enabled = false;
      nextBatch.Location = new Point(500, 90);
      nextBatch.Size = new Size(60, 20);
      nextBatch.Text = "Next";
      nextBatch.UseVisualStyleBackColor = true;

      prevBatch.Enabled = false;
      prevBatch.Location = new Point(200, 90);
      prevBatch.Size = new Size(60, 20);
      prevBatch.UseVisualStyleBackColor = true;
      prevBatch.Text = "Previous";
      if (totalImages > 3)
        prevBatch.Enabled = true;

      imgCancel.Location = new Point(350, 90);
      imgCancel.Size = new Size(60, 20);
      imgCancel.UseVisualStyleBackColor = true;
      imgCancel.Text = "Cancel";

      selectPanel.BringToFront();
    }

    private void inialiseImgControls()
    {
      // Initilise the various img controls - only needs to be done once

      // Create Panel - add to the main form
      this.Controls.Add(selectPanel);

      // Create the 3 picture box controls and add to the select panel
      for (int i = 0; i < 3; i++)
      {
        PictureBox pBox = new PictureBox();
        pBox.Click += new System.EventHandler(pBox_Click);
        defImgs.NewPicBoxes[i] = pBox;
        selectPanel.Controls.Add(defImgs.NewPicBoxes[i]);
      }

      // Create and add the next and previous buttons to the select panel
      nextBatch.Click += new System.EventHandler(nextButton_Click);
      selectPanel.Controls.Add(nextBatch);

      prevBatch.Click += new System.EventHandler(prevButton_Click);
      selectPanel.Controls.Add(prevBatch);

      imgCancel.Click += new System.EventHandler(imgCancel_Click);
      selectPanel.Controls.Add(imgCancel);
    }

    private void imgCancel_Click(object sender, EventArgs e)
    {
      imageReset(true);
    }

    public void bgChangeButton_Click(object sender, EventArgs e)
    {
      imageReset(false);
      globalSettings.Visible = false;

      string ctrlName = ((Button)sender).Name;
      string tag = ((Button)sender).Tag.ToString();
      defImgs.activePicBox = int.Parse(tag);

      Image workingImage = null;
      imagePointer = 0;
      nextBatch.Name = "button" + ctrlName;
      prevBatch.Name = "button" + ctrlName;

      // Find the bgItem that matches the button pressed
      foreach (backgroundItem bgItem in bgItems)
      {
        if (bgItem.mname[0] == ctrlName)
        {
          defImgs.activeDir = Path.GetDirectoryName(imageDir(bgItem.image));
          string[] fileList = getFileListing(defImgs.activeDir, "*.*", true);
          for (imagePointer = 0; imagePointer < 3; imagePointer++)
          {
            if (imagePointer >= totalImages) break;
            workingImage = Image.FromFile(fileList[imagePointer]);
            defImgs.NewPicBoxes[imagePointer].Visible = true;
            defImgs.newDefault[imagePointer] = fileList[imagePointer];
            defImgs.NewPicBoxes[imagePointer].Image = workingImage.GetThumbnailImage(160, 80, null, new IntPtr());
            defImgs.NewPicBoxes[imagePointer].Name = bgItem.name;
            workingImage.Dispose();
          }
        }
      }

      if (totalImages > 3)
        nextBatch.Enabled = true;
      prevBatch.Enabled = false;
      selectPanel.Visible = true;
    }

    private void pBox_Click(object sender, EventArgs e)
    {
      string ctrlName = ((PictureBox)sender).Name;
      string tag = ((PictureBox)sender).Tag.ToString();

      switch (ctrlName)
      {
        case "pBox0":
          defImgs.picBoxes[defImgs.activePicBox].Image = defImgs.NewPicBoxes[int.Parse(tag)].Image;
          break;
        case "pBox1":
          defImgs.picBoxes[defImgs.activePicBox].Image = defImgs.NewPicBoxes[int.Parse(tag)].Image;
          break;
        case "pBox2":
          defImgs.picBoxes[defImgs.activePicBox].Image = defImgs.NewPicBoxes[int.Parse(tag)].Image;
          break;
      }
      // Set the default pic for chosen background image and clean up/reset
      string fromFile = defImgs.newDefault[int.Parse(tag)];
      string defaultFile = defImgs.activeDir + "\\default.jpg";
      string defaultBackup = defImgs.activeDir + "\\default-backup.jpg";
      string bGround = defImgs.NewPicBoxes[int.Parse(tag)].Name;
      if (fromFile != defaultFile)
      {
        imageReset(true);

        int i = 0;
        foreach (menuItem menItem in menuItems)
        {
          if (bGround.Contains(menItem.name))
          {

            if (menuItems[i].disableBGSharing)
            {
              // This is a stand alone menu so set the image for this item to what has been selected
              menuItems[i].defaultImage = bgFolderName + "\\" + menuItems[i].bgFolder + "\\" + Path.GetFileName(fromFile);
              break;
            }
            else
            {
              // This is a shared or possible shared background so we need to set the default image selected
              if (defaultIsCopy(defaultFile))
                File.Delete(defaultFile);
              else
              {
                File.Copy(defaultFile, defaultBackup);
                File.Delete(defaultFile);
              }

              File.Copy(fromFile, defaultFile, true);
              break;
            }
          }
          i++;
        }
        reloadBackgroundItems();
      }
      else
        imageReset(true);
    }

    private bool defaultIsCopy(string fileToCheck)
    {
      Bitmap defBmp = new Bitmap(fileToCheck);
      FileInfo fDefInfo = new FileInfo(fileToCheck);
      string[] fileList = getFileListing(Path.GetDirectoryName(imageDir(fileToCheck)), "*.*", true);

      foreach (string fileName in fileList)
      {
        // First check if the files size is the same, if not then 
        // dont need to compare the file contents and can skip to the next file.
        FileInfo fInfo = new FileInfo(fileName);
        if (fDefInfo.Length != fInfo.Length)
          continue;

        // File sizes are the same, check the contents
        Bitmap bmp2 = new Bitmap(fileName);
        if (compareImages(defBmp, bmp2) == CompareResult.ciCompareOk)
        {
          defBmp.Dispose();
          return true;
        }
      }
      defBmp.Dispose();
      return false;
    }


    private void imageReset(bool fullReset)
    {
      if (fullReset)
      {
        selectPanel.Visible = false;
        globalSettings.Visible = true;
      }
      defImgs.activePicBox = 0;
      defImgs.activeSelectPbox = 0;
      defImgs.activeBGItem = 0;
      defImgs.activeDir = null;
      imagePointer = 0;
      fileResults.Clear();
      defImgs.NewPicBoxes[0].Image = null;
      defImgs.NewPicBoxes[1].Image = null;
      defImgs.NewPicBoxes[2].Image = null;
      defImgs.NewPicBoxes[0].Visible = false;
      defImgs.NewPicBoxes[1].Visible = false;
      defImgs.NewPicBoxes[2].Visible = false;
    }

    private void nextButton_Click(object sender, EventArgs e)
    {
      Image workingImage = null;
      string ctrlName = ((Button)sender).Name.Substring(6, ((Button)sender).Name.Length - 6);
      foreach (backgroundItem bgItem in bgItems)
      {
        if (bgItem.mname[0] == ctrlName)
        {
          string[] fileList = getFileListing(Path.GetDirectoryName(imageDir(bgItem.image)), "*.*", true);

          for (int i = 0; i < 3; i++)
          {
            if (imagePointer < fileList.Length)
            {
              workingImage = Image.FromFile(fileList[imagePointer]);
              defImgs.newDefault[i] = fileList[imagePointer];
              defImgs.NewPicBoxes[i].Image = workingImage.GetThumbnailImage(160, 80, null, new IntPtr());
              defImgs.NewPicBoxes[i].Visible = true;
              workingImage.Dispose();
            }
            else
            {
              defImgs.NewPicBoxes[i].Image = null;
              defImgs.NewPicBoxes[i].Visible = false;
              nextBatch.Enabled = false;
            }
            imagePointer++;
            if (imagePointer > 3)
              prevBatch.Enabled = true;
            else
              prevBatch.Enabled = false;

            if (fileList.Length == imagePointer)
              nextBatch.Enabled = false; ;
          }
        }
      }
    }
    private void prevButton_Click(object sender, EventArgs e)
    {
      Image workingImage = null;
      string ctrlName = ((Button)sender).Name.Substring(6, ((Button)sender).Name.Length - 6);
      if ((imagePointer - 6) >= 0)
      {
        imagePointer -= 6;
      }
      else
      {
        imagePointer = 0;
      }

      foreach (backgroundItem bgItem in bgItems)
      {
        if (bgItem.mname[0] == ctrlName)
        {
          string[] fileList = getFileListing(Path.GetDirectoryName(imageDir(bgItem.image)), "*.*", true);
          for (int i = 0; i < 3; i++)
          {
            defImgs.NewPicBoxes[i].Visible = true;
            nextBatch.Enabled = true;
            workingImage = Image.FromFile(fileList[imagePointer]);
            defImgs.newDefault[i] = fileList[imagePointer];
            defImgs.NewPicBoxes[i].Image = workingImage.GetThumbnailImage(160, 80, null, new IntPtr());
            workingImage.Dispose();
            imagePointer++;
          }
        }
      }
      if (imagePointer <= 3)
        prevBatch.Enabled = false;
    }

    private void createDefaultJpg(string imageDir)
    {
      // Check if there is default.jpg already and exit if there is
      if (File.Exists(Path.Combine(imageDir, "default.jpg")))
        return;

      // Check if there is a trailing backslash
      if (!imageDir.EndsWith(@"\"))
      {
        imageDir += @"\";
      }
      // Take the first file in the directory and copy to default.jpg (overwriting existing)
      var fileListing = getFileListing(imageDir, "*.*", true);
      if (fileListing.Count() > 0)
      {
        string sourceImgFile = fileListing.First();
        File.Copy(sourceImgFile, imageDir + "default.jpg", true);
        // Delete the Source file
        File.Delete(sourceImgFile);
      }
    }

    private string imageDir(string image)
    {
      if (image.ToLower().StartsWith(bgFolderName.ToLower() + "\\") && !image.Contains(":"))
        return SkinInfo.mpPaths.streamedMPpath + "media\\" + image;
      else
        return image;
    }

    public string[] getFileListing(string imageDir, string fileMask, bool imagelisting)
    {
      string fcompare;
      totalImages = 0;
      fileResults.Clear();
      DirectoryInfo dInfo = new DirectoryInfo(imageDir);
      if (!dInfo.Exists) return fileResults.ToArray();

      //get list of files from directory
      foreach (FileInfo fInfo in dInfo.GetFiles(fileMask))
      {
        if (imagelisting)
        {
          fcompare = fInfo.Name.ToLower();
          if (fcompare != "default.jpg")
          {
            switch (fInfo.Extension.ToLower())
            {
              case ".jpg":
              case ".jpeg":
              case ".png":
              case ".bmp":
                fileResults.Add(fInfo.FullName);
                totalImages++;
                break;
            }
          }
        }
        else
        {
          fileResults.Add(fInfo.FullName);
          totalImages++;
        }
      }
      return fileResults.ToArray();
    }

    private CompareResult compareImages(Bitmap bmp1, Bitmap bmp2)
    {
      CompareResult cr = CompareResult.ciCompareOk;

      //Test to see if we have the same size of image
      if (bmp1.Size != bmp2.Size)
      {
        cr = CompareResult.ciSizeMismatch;
      }
      else
      {
        //Convert each image to a byte array
        ImageConverter ic = new ImageConverter();
        byte[] btImage1 = new byte[1];
        btImage1 = (byte[])ic.ConvertTo(bmp1, btImage1.GetType());
        byte[] btImage2 = new byte[1];
        btImage2 = (byte[])ic.ConvertTo(bmp2, btImage2.GetType());

        //Compute a hash for each image
        SHA256Managed shaM = new SHA256Managed();
        byte[] hash1 = shaM.ComputeHash(btImage1);
        byte[] hash2 = shaM.ComputeHash(btImage2);

        //Compare the hash values
        for (int i = 0; i < hash1.Length && i < hash2.Length && cr == CompareResult.ciCompareOk; i++)
        {
          if (hash1[i] != hash2[i])
            cr = CompareResult.ciPixelMismatch;
        }
      }
      return cr;
    }
  }
}