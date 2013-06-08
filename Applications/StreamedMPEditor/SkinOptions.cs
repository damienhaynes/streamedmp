using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Security;
using System.Diagnostics;
using System.Net;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;

namespace StreamedMPEditor
{
  public partial class formStreamedMpEditor
  {
    private void useSkinWeatherIcons_Click(object sender, EventArgs e)
    {
      if (!File.Exists(SkinInfo.mpPaths.sMPbaseDir + "\\Weather\\128x128.zip"))
      {
        zipIcons(SkinInfo.mpPaths.sMPbaseDir + "\\Weather\\128x128.zip", SkinInfo.mpPaths.sMPbaseDir + "\\Weather\\128x128");
        deleteIcons(SkinInfo.mpPaths.sMPbaseDir + "\\Weather\\128x128");
        copyInIcons(SkinInfo.mpPaths.streamedMPpath + "Media\\Animations\\weathericons\\static\\128x128", SkinInfo.mpPaths.sMPbaseDir + "\\Weather\\128x128");
        useSkinWeatherIcons.Text = "Restore Standard Weather Icons";
        clearCacheDir();
        DialogResult result = helper.showError("MediaPortal supplied weather icons have been replaced with the \nskin supplied static weather icons and the cache cleared.\n\nSelecting this link again will restore the defaut icons.", errorCode.info);
      }
      else
      {
        deleteIcons(SkinInfo.mpPaths.sMPbaseDir + "\\Weather\\128x128");
        unzipIcons(SkinInfo.mpPaths.sMPbaseDir + "\\Weather\\128x128.zip", SkinInfo.mpPaths.sMPbaseDir + "\\Weather\\128x128");
        useSkinWeatherIcons.Text = "Replace Standard Weather Icons with Skin Supplied Versions";
        clearCacheDir();
        DialogResult result = helper.showError("MediaPortal supplied weather icons have been restored and cache cleared.", errorCode.info);
      }
    }

    private void zipIcons(string zipName, string filesToZip)
    {
      FastZip fz = new FastZip();
      fz.CreateZip(zipName, filesToZip, false, "");
    }

    private void deleteIcons(string fromDirectory)
    {
      string[] files = Directory.GetFiles(fromDirectory);
      foreach (string file in files)
        File.Delete(file);
    }

    private void copyInIcons(string fromHere, string toHere)
    {
      string[] files = Directory.GetFiles(fromHere);
      foreach (string file in files)
        File.Copy(file, toHere + "\\" + Path.GetFileName(file), true);
    }
    private void unzipIcons(string zipName, string filesToUnzip)
    {
      FastZip fz = new FastZip();
      fz.ExtractZip(zipName, filesToUnzip, "");
      File.Delete(zipName);
    }
  }
}