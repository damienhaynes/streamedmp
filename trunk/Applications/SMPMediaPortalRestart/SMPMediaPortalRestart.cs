using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace SMPMediaPortalRestart
{
  public partial class SMPMediaPortalRestart : Form
  {
    public SMPMediaPortalRestart()
    {
      InitializeComponent();
    }

    private void SMPMediaPortalRestart_Load(object sender, EventArgs e)
    {
      // The Parameter we need is in postion 1
      lbStatus.Text = "Restarting MediaPortal in 0 Seconds";
      int xpos = (Screen.PrimaryScreen.Bounds.Width / 2) - (lbStatus.Width/2);
      int ypos = (Screen.PrimaryScreen.Bounds.Height / 2);
      lbStatus.Location = new Point(xpos, ypos);
      this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
      this.Location = new Point(0, 0);
      pbSplashScreen.Size = new Size(Screen.PrimaryScreen.Bounds.Width + 1, Screen.PrimaryScreen.Bounds.Height + 1);
      lbStatus.Parent = pbSplashScreen;
      lbStatus.BackColor = Color.Transparent;
      this.Show();
      for (int i = 5; i > 0; i--)
      {
        lbStatus.Text = string.Format("Restarting MediaPortal in {0} Seconds", i);
        this.Refresh();
        Thread.Sleep(1000);
      }
      ProcessStartInfo process = new ProcessStartInfo("MediaPortal.exe");
      process.UseShellExecute = true;
      Process.Start(process);
      Application.Exit();
    }
  }
}
