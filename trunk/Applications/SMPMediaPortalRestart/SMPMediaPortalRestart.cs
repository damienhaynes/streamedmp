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
    int xpos;
    int ypos;
    string splashScreenImage;

    public SMPMediaPortalRestart()
    {
      InitializeComponent();
      this.TopMost = true;
    }

    private void SMPMediaPortalRestart_Load(object sender, EventArgs e)
    {
      // The Parameter we need is in postion 1, assume MediaPortal not starting in fullscreen
      bool weIsFullscreen = false;
        string[] args = Environment.GetCommandLineArgs();
        if (args.Length >= 2)
        {
          weIsFullscreen = bool.Parse(args[1]);
          splashScreenImage = args[2];
        }
      
      pbSplashScreen.Image = Image.FromFile(splashScreenImage);

      lbStatus.Parent = pbSplashScreen;
      lbStatus.BackColor = Color.Transparent;
      if (weIsFullscreen)
      {
        lbStatus.Text = "Restarting MediaPortal in 3 Seconds";
        xpos = (Screen.PrimaryScreen.Bounds.Width / 2) - (lbStatus.Width / 2);
        ypos = (Screen.PrimaryScreen.Bounds.Height / 2) - (lbStatus.Height / 2);
      }
      else
      {
        lbStatus.Font = ChangeFontSize(lbStatus.Font, (float)9.00);
        lbStatus.Text = "Restarting MediaPortal in 3 Seconds";
        xpos = (600 / 2) - (lbStatus.Width / 2);
        ypos = (338 / 2) - (lbStatus.Height / 2);
      }

      if (weIsFullscreen) 
      {
        // MP set to start in fullscreen - fullscreen splash screen
        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        this.Location = new Point(0, 0);
        pbSplashScreen.Size = new Size(Screen.PrimaryScreen.Bounds.Width + 1, Screen.PrimaryScreen.Bounds.Height + 1);
        lbStatus.Location = new Point(xpos, ypos);
      }
      else
      {
        // MP Not set to start fullscreen - small splash screen
        this.Size = new Size(600,338);
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - 300, Screen.PrimaryScreen.Bounds.Height/2 - 169);
        pbSplashScreen.Size = new Size(600,338);
        lbStatus.Location = new Point(xpos, ypos);
      }

      this.Show();

      for (int i = 3; i > 0; i--)
      {
        lbStatus.Text = string.Format("Restarting MediaPortal in {0} Seconds", i);
        this.Refresh();
        Thread.Sleep(1000);
      }
      ProcessStartInfo process = new ProcessStartInfo("MediaPortal.exe");
      process.UseShellExecute = true;
      Process.Start(process);
      Thread.Sleep(1000);
      Application.Exit();
    }


    static public Font ChangeFontSize(Font font, float fontSize)
    {
      if (font != null)
      {
        float currentSize = font.Size;
        if (currentSize != fontSize)
        {
          font = new Font(font.Name, fontSize,
              font.Style, font.Unit,
              font.GdiCharSet, font.GdiVerticalFont);
        }
      }
      return font;
    }

  }
}
