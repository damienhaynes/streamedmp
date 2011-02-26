using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using MediaPortal.Configuration;

namespace SMPMenuGen
{
  class Helper
  {
    public bool pluginEnabled(Plugins plugin)
    {
      PluginDetails pd = new PluginDetails(plugin);

      // check if plugin is enabled/disabled
      // if we dont find they entry then we assume enabled as we know it
      // is installed so most likley configuration has not yet been run
      // to write the entry to MediaPortal.xml
      string returnValue = null;

      try
      {
        using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.MPSettings())
        {
          returnValue = xmlreader.GetValueAsString("plugins", pd.name, "");
        }
      }
      catch (Exception e)
      {
        //showError("Error reading MediaPortal.xml : " + e.Message, formStreamedMpEditor.errorCode.readError);        
      }

      if (string.IsNullOrEmpty(returnValue))
      {
        // error looking up in mediaportal.xml, check if file exists
        if (string.IsNullOrEmpty(pd.filename))
          return false;
        else
          return File.Exists(pd.filename);
      }

      if (returnValue.ToLower() == "no")
        return false;
      else
        return true;
    }


    public string fileVersion(string fileToCheck)
    {
      if (File.Exists(fileToCheck))
      {
        FileVersionInfo fv = FileVersionInfo.GetVersionInfo(fileToCheck);
        return fv.FileVersion;
        
      }
      else
        return "0.0.0.0";
    }

    public string readMPConfiguration(string sectionName, string entryName)
    {
      try
      {
        using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
        {
          return xmlreader.GetValueAsString(sectionName, entryName, "");
        }
      }
      catch (Exception e)
      {
        //showError("Error reading MediaPortal.xml : " + e.Message, formStreamedMpEditor.errorCode.readError);
        return string.Empty;
      }
    }

    public void writeMPConfiguration(string sectionName, string entryName, string entryValue)
    {
      try
      {
        using (MediaPortal.Profile.Settings xmlwriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
        {
          xmlwriter.SetValue(sectionName, entryName, entryValue);
        }
      }
      catch (Exception e)
      {
        //showError("Error writing MediaPortal.xml : " + e.Message, formStreamedMpEditor.errorCode.readError);
      }
      MediaPortal.Profile.Settings.SaveCache();
    }

    #region Assembly Helpers
    public static bool IsAssemblyAvailable(string name, Version ver)
    {
      return IsAssemblyAvailable(name, ver, null);
    }
    public static bool IsAssemblyAvailable(string name, Version ver, string filename)
    {
      bool result = false;

      Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
      foreach (Assembly a in assemblies)
      {
        try
        {
          if (a.GetName().Name == name)          
          {
            if (a.GetName().Version >= ver)
            {
              return true;
            }
            else
            {
              return false;
            }            
          }
        }
        catch
        {
          result = false;
        }
      }

      if (!result)
      {  
        try
        {
          Assembly assembly = null;
          if (string.IsNullOrEmpty(filename))
          {
            assembly = Assembly.ReflectionOnlyLoad(name);
            if (assembly.GetName().Version >= ver) result = true;
          }
          else
          {
            assembly = Assembly.ReflectionOnlyLoadFrom(filename);
            if (assembly.GetName().Version >= ver) result = true;            
          }          
        }
        catch
        {
          result = false;
        }
      }

      return result;
    }

    public enum Plugins
    {
        FanartHandler,
        SleepControl,
        Stocks,
        PowerControl,
        HTPCInfo,
        DriveFreeSpace,
        InfoService,
        MovingPictures,
        MPTVSeries,
        ForTheRecord,
        UpdateControl
    }

    class PluginDetails
    {
      public string filename = string.Empty;
      public string name = string.Empty;

      public PluginDetails(Plugins plugin)
      {
        switch (plugin)
        {
          case Plugins.DriveFreeSpace:
            this.filename = Path.Combine(SkinInfo.mpPaths.pluginPath, @"process\DriveFreeSpace.dll");
            this.name = "DriveFreeSpace";
            break;
          case Plugins.FanartHandler:
            this.filename = Path.Combine(SkinInfo.mpPaths.pluginPath, @"process\FanartHandler.dll");
            this.name = "Fanart Handler";
            break;
          case Plugins.ForTheRecord:
            this.filename = Path.Combine(SkinInfo.mpPaths.pluginPath, @"windows\ForTheRecord.UI.MediaPortal.dll");
            this.name = "For The Record TV";
            break;
          case Plugins.HTPCInfo:
            this.filename = Path.Combine(SkinInfo.mpPaths.pluginPath, @"windows\HTPCInfo.dll");
            this.name = "HTPC Info";
            break;
          case Plugins.InfoService:
            this.filename = Path.Combine(SkinInfo.mpPaths.pluginPath, @"windows\HTPCInfo.dll");
            this.name = "InfoService";
            break;
          case Plugins.MovingPictures:
            this.filename = Path.Combine(SkinInfo.mpPaths.pluginPath, @"windows\MovingPictures.dll");
            this.name = "Moving Pictures";
            break;
          case Plugins.MPTVSeries:
            this.filename = Path.Combine(SkinInfo.mpPaths.pluginPath, @"windows\MP-TVSeries.dll");
            this.name = "MP-TVSeries";
            break;
          case Plugins.PowerControl:
            this.filename = Path.Combine(SkinInfo.mpPaths.pluginPath, @"windows\PowerControl.dll");
            this.name = "Power Controls";
            break;
          case Plugins.SleepControl:
            this.filename = Path.Combine(SkinInfo.mpPaths.pluginPath, @"windows\SleepControl.dll");
            this.name = "Sleep Control";
            break;
          case Plugins.Stocks:
            this.filename = Path.Combine(SkinInfo.mpPaths.pluginPath, @"windows\Stocks.dll");
            this.name = "Stocks";
            break;
          case Plugins.UpdateControl:
            this.filename = Path.Combine(SkinInfo.mpPaths.pluginPath, @"windows\UpdateControl.dll");
            this.name = "Update Control";
            break;
        }
      }
    }

    #endregion

  }
}
