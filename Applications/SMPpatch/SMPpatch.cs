using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;


namespace SMPpatch
{
  public partial class SMPpatch : Form
  {

    List<patchFile> patchFiles = new List<patchFile>();
    public string tempExtractPath = Path.Combine(Path.GetTempPath(), "StreamedMPPatch" + DateTime.Now.Ticks.ToString());
    public XmlTextReader reader;
    bool unattendedInatall = false;
    bool restartMediaPortal = false;
    bool restartConfiguration = false;



    public SMPpatch()
    {
      InitializeComponent();

      //Check for any command line argments
      btInstallPatch.Enabled = true;
      restartConfiguration = false;
      restartMediaPortal = false;
      foreach (string arg in Environment.GetCommandLineArgs())
      {
        // Run unattended - This will run the program minimised and exit
        if (arg.ToLower().Contains("unattended"))
        {
          unattendedInatall = true;
          btInstallPatch.Enabled = false;
          this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }
        else
          this.WindowState = System.Windows.Forms.FormWindowState.Normal;

        // Do we restart MediaPortal after the update 
        if (arg.ToLower().Contains("restartmp"))
        {
          restartMediaPortal = true;
        }
        // Do we restart Configuration after the update 
        if (arg.ToLower().Contains("restartconfiguration"))
        {
          restartConfiguration = true;
        }
      }
    }

    private void SMPpatch_Load(object sender, EventArgs e)
    {
      string processess = null;

      CheckProcesses mediaportal = new CheckProcesses("mediaportal");
      CheckProcesses configuration = new CheckProcesses("configuration");
      CheckProcesses smpeditor = new CheckProcesses("smpeditor");

      SkinInfo.GetMediaPortalSkinPath();
      introBox.SelectionStart = 0;

      // Sleep for 2 secs to enable MediaPortal to shutdown before doing checks for running processess
      Thread.Sleep(2000);

      while (!checkRunningProcess())
      {
        // Check running process and wait 5sec for processes to exit
        if (!checkRunningProcess())
          if (mediaportal.running)
            processess = "MediaPortal\n";
        if (configuration.running)
          processess += "Configuration.exe\n";
        if (smpeditor.running)
          processess += "StreamedMP basicHome Editor\n";

        DialogResult result = MessageBox.Show("The Follow Process are Still Running\n\n" + processess + "\nPlease close application and Retry\n\nPressing Cancel will Abort the Upgrade Process",
              "Retry",
              MessageBoxButtons.RetryCancel,
              MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2);

        if (result == DialogResult.Cancel)
        {
          Application.Exit();
        }
        processess = null;
      }

      // Create the temp directory to stroe the extracted patches
      Directory.CreateDirectory(tempExtractPath);

      readPatchControl();
      if (unattendedInatall)
      {
        installThePatches();

        UpdateMessage updateDone = new UpdateMessage();
        SkinInfo skInfo = new SkinInfo();
        updateDone.statusMessage = "StreamedMP Sucessfully Updated to Version : " + skInfo.skinVersion.ToString();
        updateDone.Show();
        for (int i = 0; i < 5; i++)
        {
          if (restartMediaPortal)
            updateDone.countDown = "Restarting MediaPortal : " + (5 - i).ToString();
          if (restartConfiguration)
            updateDone.countDown = "Restarting Configuration : " + (5 - i).ToString();
          updateDone.Refresh();
          Thread.Sleep(1000);
        }
        exitAndCleanup();
      }
    }

    bool checkRunningProcess()
    {

      CheckProcesses mediaportal = new CheckProcesses("mediaportal");
      CheckProcesses configuration = new CheckProcesses("configuration");
      CheckProcesses smpeditor = new CheckProcesses("smpeditor");

      if (!mediaportal.running)
      {
        mediaportalStatus.ForeColor = Color.Green;
        mediaportalStatus.Text = "Shutdown";
      }
      else
        return false;

      if (!configuration.running)
      {
        configurationStatus.ForeColor = Color.Green;
        configurationStatus.Text = "Shutdown";
      }
      else
        return false;

      if (!smpeditor.running)
      {
        smpeditorStatus.ForeColor = Color.Green;
        smpeditorStatus.Text = "Shutdown";
      }
      else
        return false;

      return true;
    }

    void readPatchControl()
    {
      // Extract the control file
      FileInfo outFile = new FileInfo(Path.Combine(tempExtractPath, "PatchControl.xml"));
      FileStream streamOutFile = outFile.OpenWrite();
      extractFile(GetEmbeddedFile("SMPpatch", "EmbeddedPatches.PatchControl.xml"), streamOutFile);

      // Read the file
      string elementName = "";
      reader = new XmlTextReader(Path.Combine(tempExtractPath, "PatchControl.xml"));
      reader.MoveToContent();
      if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "patches"))
      {
        while (reader.Read())
        {
          if (reader.NodeType == XmlNodeType.Element && reader.Name == "patch")
          {
            patchFile thisPatch = new patchFile();
            while (reader.Read())
            {
              if (reader.NodeType == XmlNodeType.Element)
                elementName = reader.Name;
              else
              {
                if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                {
                  switch (elementName)
                  {
                    case "name":
                      thisPatch.patchFileName = reader.Value;
                      break;
                    case "action":
                      thisPatch.patchAction = reader.Value;
                      break;
                    case "location":
                      thisPatch.patchLocation = reader.Value;
                      break;
                  }
                }
              }
              if (reader.NodeType == XmlNodeType.EndElement)
              {
                if (reader.Name == "patch")
                {
                  handlePatch(thisPatch.patchFileName, thisPatch);
                  break;
                }
              }
            }
          }
        }
        reader.Close();
      }
    }

    void handlePatch(string patchName, patchFile pf)
    {
      FileInfo outFile = new FileInfo(Path.Combine(tempExtractPath, patchName));
      FileStream streamOutFile = outFile.OpenWrite();
      extractFile(GetEmbeddedFile("SMPpatch", "EmbeddedPatches." + patchName), streamOutFile);
      // Now get the version and fill in the info
      fillInfo(patchName, pf);
    }

    void fillInfo(string fileName, patchFile pf)
    {
      pf.patchVersion = fileVersion(Path.Combine(tempExtractPath, fileName));

      // if patch file is a plugin
      if (pf.patchLocation.ToLower().StartsWith("process") || pf.patchLocation.ToLower().StartsWith("windows"))
      {
        pf.destinationPath = Path.Combine(SkinInfo.mpPaths.pluginPath, pf.patchLocation);
        pf.installedVersion = fileVersion(Path.Combine(pf.destinationPath, fileName));
      }
      if (pf.patchLocation.ToLower().StartsWith("mediaportal"))
      {
        pf.destinationPath = SkinInfo.mpPaths.sMPbaseDir;
        pf.installedVersion = fileVersion(Path.Combine(pf.destinationPath, fileName));
      }

      patchFiles.Add(pf);
      ListViewItem item = new ListViewItem(new[] { pf.patchFileName, pf.patchVersion, pf.installedVersion });
      item.ImageIndex = 1;
      thePatches.Items.Add(item);
    }

    string fileVersion(string fileToCheck)
    {
      FileVersionInfo fv = FileVersionInfo.GetVersionInfo(fileToCheck);
      return fv.FileVersion;
    }


    #region Extract Patches

    /// <summary>
    /// Extracts an embedded file out of a given assembly.
    /// </summary>
    /// <param name="assemblyName">The namespace of you assembly.</param>
    /// <param name="fileName">The name of the file to extract.</param>
    /// <returns>A stream containing the file data.</returns>
    public static Stream GetEmbeddedFile(string assemblyName, string fileName)
    {
      try
      {
        System.Reflection.Assembly a = System.Reflection.Assembly.Load(assemblyName);
        Stream str = a.GetManifestResourceStream(assemblyName + "." + fileName);

        if (str == null)
          throw new Exception("Could not locate embedded resource '" + fileName + "' in assembly '" + assemblyName + "'");
        return str;
      }
      catch (Exception e)
      {
        throw new Exception(assemblyName + ": " + e.Message);
      }
    }

    #region Overloads

    public static Stream GetEmbeddedFile(System.Reflection.Assembly assembly, string fileName)
    {
      string assemblyName = assembly.GetName().Name;
      return GetEmbeddedFile(assemblyName, fileName);
    }

    public static Stream GetEmbeddedFile(Type type, string fileName)
    {
      string assemblyName = type.Assembly.GetName().Name;
      return GetEmbeddedFile(assemblyName, fileName);
    }

    #endregion

    void extractFile(Stream inFile, Stream outFile)
    {
      const int size = 4096;
      byte[] bytes = new byte[4096];
      int numBytes;
      while ((numBytes = inFile.Read(bytes, 0, size)) > 0)
      {
        outFile.Write(bytes, 0, numBytes);
      }
      outFile.Close();
      inFile.Close();
    }

    #endregion

    #region Class Defines

    public class patchFile
    {
      public string patchFileName { get; set; }
      public string patchVersion { get; set; }
      public string patchAction { get; set; }
      public string patchLocation { get; set; }
      public string installedVersion { get; set; }
      public string destinationPath { get; set; }
    }

    #endregion

    private void SMPpatch_FormClosing(object sender, FormClosingEventArgs e)
    {
      exitAndCleanup();
    }

    private void btExit_Click(object sender, EventArgs e)
    {
      exitAndCleanup();
    }

    void exitAndCleanup()
    {
      // Cleanup
      if (Directory.Exists(tempExtractPath))
        Directory.Delete(tempExtractPath, true);

      // Check and start Mediaportal if required
      if (restartMediaPortal)
      {
        ProcessStartInfo process = new ProcessStartInfo("mediaportal.exe");
        process.WorkingDirectory = SkinInfo.mpPaths.sMPbaseDir;
        process.UseShellExecute = true;
        Process.Start(process);
      }
      // Check and start Mediaportal if required
      if (restartConfiguration)
      {
        ProcessStartInfo process = new ProcessStartInfo("configuration.exe");
        process.WorkingDirectory = SkinInfo.mpPaths.sMPbaseDir;
        process.UseShellExecute = true;
        Process.Start(process);
      }
      Application.Exit();
    }

    private void btInstallPatch_Click(object sender, EventArgs e)
    {
      installThePatches();
      clearCacheDir();
    }

    void installThePatches()
    {
      patchProgressBar.Step = (100 / patchFiles.Count);
      int i = 0;
      foreach (patchFile patch in patchFiles)
      {
        installPatch(patch);
        thePatches.Items[i].ImageIndex = 0;
        i++;
        if ((patchProgressBar.Value + patchProgressBar.Step) > patchProgressBar.Maximum)
          patchProgressBar.Value = 100;
        else
          patchProgressBar.Value += patchProgressBar.Step;
      }
      btInstallPatch.Enabled = false;
    }

    void installPatch(patchFile thePatch)
    {
      if (thePatch.patchAction.ToLower() == "install")
      {
        if (thePatch.patchLocation.ToLower().StartsWith("process") || thePatch.patchLocation.ToLower().StartsWith("windows"))
        {
          File.Copy(Path.Combine(tempExtractPath, thePatch.patchFileName), Path.Combine(thePatch.destinationPath, thePatch.patchFileName), true);
        }
        if (thePatch.patchLocation.ToLower().StartsWith("mediaportal"))
        {
          File.Copy(Path.Combine(tempExtractPath, thePatch.patchFileName), Path.Combine(thePatch.destinationPath, thePatch.patchFileName), true);
        }
      }
      if (thePatch.patchAction.ToLower() == "unzip")
      {
        installZip(thePatch);
      }

      if (thePatch.patchAction.ToLower() == "run")
      {
      }
    }


    void installZip(patchFile thePatch)
    {
      FastZip fz = new FastZip();
      fz.ExtractZip(Path.Combine(tempExtractPath, thePatch.patchFileName), Path.Combine(tempExtractPath, Path.GetFileNameWithoutExtension(thePatch.patchFileName)), "");
      checkAndCopy(Path.Combine(tempExtractPath, Path.GetFileNameWithoutExtension(thePatch.patchFileName)));
    }

    void checkAndCopy(string destinationPath)
    {
      String[] Files;
      Files = Directory.GetFileSystemEntries(destinationPath);
      foreach (string patchdir in Files)
      {
        if (patchdir.ToLower().Contains("language"))
          copyDirectory(Path.Combine(destinationPath, "language"), SkinInfo.mpPaths.langBasePath);
        if (patchdir.ToLower().Contains("streamedmp"))
          copyDirectory(Path.Combine(destinationPath, "StreamedMP"), Path.Combine(SkinInfo.mpPaths.skinBasePath, "StreamedMP"));
        if (patchdir.ToLower().Contains("thumbs"))
            copyDirectory(Path.Combine(destinationPath, "thumbs"), SkinInfo.mpPaths.thumbsPath);
      }
      patchProgressBar.Value += 10;
    }

    void copyDirectory(string patchSource, string patchDestination)
    {
      String[] patchFiles;

      if (patchDestination[patchDestination.Length - 1] != Path.DirectorySeparatorChar)
        patchDestination += Path.DirectorySeparatorChar;

      if (!Directory.Exists(patchDestination))
        Directory.CreateDirectory(patchDestination);

      patchFiles = Directory.GetFileSystemEntries(patchSource);

      foreach (string Element in patchFiles)
      {
        if (Directory.Exists(Element))
          copyDirectory(Element, patchDestination + Path.GetFileName(Element));
        else
        {
          File.Copy(Element, patchDestination + Path.GetFileName(Element), true);
        }
      }
    }

    private void clearCacheDir()
    {
      try
      {
        System.IO.Directory.Delete(Path.Combine(SkinInfo.mpPaths.cacheBasePath,"StreamedMP"), true);
        //showError("Skin cache has been cleared\n\nOk To Continue", errorCode.info);
      }
      catch
      {
        //showError("Exception while deleteing Cache\n\n" + ex.Message, errorCode.info);
      }
    }

  }
}
