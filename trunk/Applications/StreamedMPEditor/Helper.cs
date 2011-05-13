using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using MediaPortal.Configuration;

namespace StreamedMPEditor
{
  class Helper
  {
    public string[] getSkinFileList(ref System.Windows.Forms.ListBox listBox, ref List<string> idList)
    {
      string[] files = System.IO.Directory.GetFiles(SkinInfo.mpPaths.streamedMPpath);
      foreach (string file in files)
      {
        try
        {
          if (file.StartsWith("common") == false && file.Contains("Dialog") == false && file.Contains("dialog") == false && file.Contains("wizard") == false && file.Contains("xml.backup") == false)
          {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNode node = doc.DocumentElement.SelectSingleNode("/window/id");
            idList.Add(node.InnerText);
            listBox.Items.Add(file.Remove(0, file.LastIndexOf(@"\") + 1).Replace(".xml", ""));
          }
        }
        catch { }
      }
      return files;
    }

    public void loadPrettyItems(List<string> ids)
    {
      formStreamedMpEditor.prettyItems.Clear();
      XmlDocument doc = new XmlDocument();
      Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("StreamedMPEditor.xmlFiles.QuickSelectList.xml");
      try
      {
        doc.Load(stream);
      }
      catch (Exception e)
      {
        showError("Exception while loading QuickSelectList.xml\n\n" + e.Message, formStreamedMpEditor.errorCode.loadError);
        formStreamedMpEditor.basicHomeLoadError = true;
        return;
      }
      XmlNodeList nodeList = doc.DocumentElement.SelectNodes("/items/item");
      foreach (XmlNode node in nodeList)
      {
        formStreamedMpEditor.prettyItem pItem = new formStreamedMpEditor.prettyItem();
        XmlNode innerNode = node.SelectSingleNode("name");
        if (innerNode != null)
          pItem.name = innerNode.InnerText;

        innerNode = node.SelectSingleNode("name2");
        if (innerNode != null)
          pItem.nameExtension = innerNode.InnerText;

        innerNode = node.SelectSingleNode("context");
        if (innerNode != null)
          pItem.contextlabel = innerNode.InnerText;

        innerNode = node.SelectSingleNode("folder");
        if (innerNode != null)
          pItem.folder = innerNode.InnerText;

        innerNode = node.SelectSingleNode("fanartproperty");
        if (innerNode != null)
          pItem.fanartProperty = innerNode.InnerText;

        innerNode = node.SelectSingleNode("fanarthandlerenabled");
        if (innerNode != null)
          pItem.fanartHandlerEnabled = bool.Parse(innerNode.InnerText);

        innerNode = node.SelectSingleNode("xmlfile");
        if (innerNode != null) pItem.xmlfile = innerNode.InnerText;

        innerNode = node.SelectSingleNode("id");
        if (innerNode != null)
          pItem.id = innerNode.InnerText;

        innerNode = node.SelectSingleNode("isweather");
        if (innerNode != null)
          pItem.isweather = bool.Parse(innerNode.InnerText);

        innerNode = node.SelectSingleNode("takesparmeter");
        if (innerNode != null)
          pItem.takesParmeter = bool.Parse(innerNode.InnerText);

        innerNode = node.SelectSingleNode("pluginparmeter");
        if (innerNode != null)
          pItem.pluginParmeter = innerNode.InnerText;

        // Dont Add item if its not available
        if (ids.Contains(pItem.id))
          formStreamedMpEditor.prettyItems.Add(pItem);
      
      }
    }

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
        showError("Error reading MediaPortal.xml : " + e.Message, formStreamedMpEditor.errorCode.readError);        
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

    public DialogResult showError(string errorText, formStreamedMpEditor.errorCode errorType)
    {
      DialogResult rtype = new DialogResult();
      switch (errorType)
      {
        case formStreamedMpEditor.errorCode.info:
          MessageBox.Show(errorText,
              "Click OK to continue.",
              MessageBoxButtons.OK,
              MessageBoxIcon.Information,
              MessageBoxDefaultButton.Button1);
          break;
        case formStreamedMpEditor.errorCode.infoQuestion:
          return MessageBox.Show(errorText,
              "Continue Editing",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button1);
        case formStreamedMpEditor.errorCode.loadError:
          MessageBox.Show("Error loading menu, file seems invalid\r\rReason: " + errorText + "  ",
              "Click OK to continue.",
              MessageBoxButtons.OK,
              MessageBoxIcon.Error,
              MessageBoxDefaultButton.Button1);
          break;
        case formStreamedMpEditor.errorCode.readError:
          MessageBox.Show("Error loading menu, file seems invalid\r\rReason: " + errorText + "  ",
              "Click OK to continue.",
              MessageBoxButtons.OK,
              MessageBoxIcon.Error,
              MessageBoxDefaultButton.Button1);
          break;
        case formStreamedMpEditor.errorCode.major:
          MessageBox.Show(errorText + "\r\rEditor will now terminate.",
              "Click OK to Exit",
              MessageBoxButtons.OK,
              MessageBoxIcon.Error,
              MessageBoxDefaultButton.Button1);
          Application.Exit();
          break;
      }
      return rtype;
    }

    public string readMPConfiguration(string sectionName, string entryName)
    {
      return readMPConfiguration(sectionName, entryName, string.Empty);
    }
    public string readMPConfiguration(string sectionName, string entryName, string defaultValue)
    {
      try
      {
        using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
        {
          return xmlreader.GetValueAsString(sectionName, entryName, defaultValue);
        }
      }
      catch (Exception e)
      {
        showError("Error reading MediaPortal.xml : " + e.Message, formStreamedMpEditor.errorCode.readError);
        return defaultValue;
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
        showError("Error writing MediaPortal.xml : " + e.Message, formStreamedMpEditor.errorCode.readError);
      }
      MediaPortal.Profile.Settings.SaveCache();
    }

    /// <summary>
    /// Loads and returns an XML Document
    /// </summary>
    /// <param name="file"></param>    
    public static XmlDocument LoadXMLDocument(string file)
    {
      // Check if File Exist
      if (!File.Exists(file))
        return null;

      XmlDocument doc = new XmlDocument();
      try
      {
        doc.Load(file);
      }
      catch (XmlException)
      {
        return null;
      }
      return doc;
    }

    /// <summary>
    /// Gets an elements value from a xml node
    /// </summary>   
    public static string ReadEntryValue(string xPath, XmlNode node, string defaultValue)
    {
      string entryValue = string.Empty;

      XmlNode path = node.SelectSingleNode(xPath);
      if (path != null)
      {
        entryValue = path.InnerText;
        return entryValue;
      }

      return defaultValue;
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
