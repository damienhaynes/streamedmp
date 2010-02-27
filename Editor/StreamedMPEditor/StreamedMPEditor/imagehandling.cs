﻿using System;
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

namespace StreamedMPEditor
{
  public partial class streamedMpEditor
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


      for (int i = 0; i < 24; i++)
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
          string[] fileList = getFileListing(Path.GetDirectoryName(imageDir(bgItem.image)), "*.*");
          createDefaultJpg(Path.GetDirectoryName(imageDir(bgItem.image)));
        }

        totalImages = Directory.GetFiles(Path.GetDirectoryName(imageDir(bgItem.image))).Length;
        if (totalImages > 1)
            totalImages--;
        string dirParent = Path.GetDirectoryName(imageDir(bgItem.image));
        mpPaths.fanartBasePath = dirParent;

        workingImage = Image.FromFile(imageDir(bgItem.image));
        newPBox.Image = workingImage.GetThumbnailImage(160, 80, null, new IntPtr());
        workingImage.Dispose();

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

        //Create numbe of files label
        newBGCount.Location = new Point(xPos, (yPos + 82));
        newBGCount.Size = new Size(160, 15);
        newBGCount.Text = "Images Avaiable: " + (totalImages).ToString();
        //newBGCount.Font = new Font(newBGCount.Font, FontStyle.Bold);
        bgCount[pBoxElement] = newBGCount;
        defaultBackgrounds.Controls.Add(bgCount[pBoxElement]);

        //Create change button
        newBGButton.Size = new Size(61, 21);
        newBGButton.Location = new Point((xPos + 97), (yPos - 24));
        newBGButton.Text = "Change";
        newBGButton.Tag = pBoxElement.ToString();
        newBGButton.Click += new System.EventHandler(bgChangeButton_Click);
        newBGButton.Name = bgItem.mname[0];
        if (totalImages < 2)
          newBGButton.Enabled = false;
        else
          newBGButton.Enabled = true;

        bgButtons[pBoxElement] = newBGButton;
        defaultBackgrounds.Controls.Add(bgButtons[pBoxElement]);

        // Increment the position and pointer counters - currently supports 12 backgrounds
        pBoxElement++;
        xPos += 185;
        if (pBoxElement == 4)
        {
          yPos += 126;
          xPos = 16;
        }
        if (pBoxElement == 8)
        {
          yPos += 126;
          xPos = 16;
        }
      }

      
        // Configure the Panel
      selectPanel.Size = new Size(756, 120);
      selectPanel.Location = new Point(12, 101);
      selectPanel.BackColor = Color.White;
      selectPanel.BorderStyle = BorderStyle.FixedSingle;


      //Configure the 3 PictureBox Controls
      xPos = 70;
      yPos = 5;
      for (int i = 0;i < 3; i++)
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
          string[] fileList = getFileListing(defImgs.activeDir,"*.*");
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
        string bGround = defImgs.NewPicBoxes[int.Parse(tag)].Name;
        if (fromFile != defaultFile)
        {
            imageReset(true);
            //File.Delete(defaultFile);
            //File.Copy(fromFile, defaultFile, true);
            int i = 0;
            foreach (menuItem menItem in menuItems)
            {
                if (bGround.Contains(menItem.name))
                {

                    if (menuItems[i].disableBGSharing)
                    {
                        // This is a stand alone menu so set the image for this item to what has been selected
                        menuItems[i].defaultImage = "animations\\" + menuItems[i].bgFolder + "\\" + Path.GetFileName(fromFile);
                        break;
                    }
                    else
                    {
                        // This is a shared or possible shared background so we need to set the default image selected
                        File.Delete(defaultFile);
                        File.Copy(fromFile, defaultFile, true);
                    }
                }
                i++;
            }
            reloadBackgroundItems();

        }
        else
            imageReset(true);
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
            string[] fileList = getFileListing(Path.GetDirectoryName(imageDir(bgItem.image)), "*.*");
            
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
              string[] fileList = getFileListing(Path.GetDirectoryName(imageDir(bgItem.image)), "*.*");
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
        // Check if there is defult.jog already and exit if there is
        if (System.IO.File.Exists(imageDir + "default.jpg"))
            return;
        
        // Check if there is a trailing backslash
        if (!imageDir.EndsWith(@"\"))
        {
            imageDir += @"\";
        }
        // Take the first file in the directoy and copy to default.jpg (overwriteing existing)
        string sourceImgFile = getFileListing(imageDir, "*.*")[0];
        System.IO.File.Copy(sourceImgFile, imageDir + "default.jpg", true);
        // Delete the Source file
        // System.IO.File.Delete(sourceImgFile);
    }

    private string imageDir(string image)
    {
      if (!image.StartsWith("Animations\\") && !image.Contains(":"))
        return mpPaths.streamedMPpath + "media\\" + image;
      else
        return image;

    }

    private string[] getFileListing(string imageDir, string fileMask)
    {
      string fcompare;
      totalImages = 0;
      fileResults.Clear();
      DirectoryInfo dInfo = new DirectoryInfo(imageDir);
      //get list of files from directory
      foreach (FileInfo fInfo in dInfo.GetFiles(fileMask))
      {
        fcompare = fInfo.Name.ToLower();
        if (fcompare != "default.jpg")
        {
            fileResults.Add(fInfo.FullName);
            totalImages++;
        }
      }
      return fileResults.ToArray();
    }
  }
}