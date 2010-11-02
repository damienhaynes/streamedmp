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
        int yPos = 30;
        int xPos = 30;

        public SelectHardDrives()
        {
            InitializeComponent();

            DriveInfo[] hardDrives = DriveInfo.GetDrives(); 

            foreach (DriveInfo hd in hardDrives)
            {
                addDriveDetails(hd);
                xPos += 100;
                if (xPos > 430)
                {
                    xPos = 30;
                    yPos += 50;
                }
            }



        }

        void addDriveDetails(DriveInfo hd)
        {
            CheckBox cb = new CheckBox();

            cb.Location = new Point(xPos, yPos);
            cb.Name = "harddrive" + hd.Name;
            cb.Text = hd.Name;
            cb.Size = new System.Drawing.Size(80, 17);
            this.Controls.Add(cb);

            //this.checkBox1.AutoSize = true;
            //this.checkBox1.Location = new System.Drawing.Point(64, 28);
            //this.checkBox1.Name = "checkBox1";
            //this.checkBox1.Size = new System.Drawing.Size(80, 17);
            //this.checkBox1.TabIndex = 0;
            //this.checkBox1.Text = "checkBox1";
            //this.checkBox1.UseVisualStyleBackColor = true;

        }
    }
}
