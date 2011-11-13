using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using SMPCheckSum;

namespace StreamedMPConfig
{
  class Helper
  {
    private static readonly logger smcLog = logger.GetInstance();

    #region Path Helpers
    public static string MakeValidFileName(string name)
    {
      string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
      string invalidReStr = string.Format(@"[{0}]+", invalidChars);
      return Regex.Replace(name, invalidReStr, "_");
    }

    #endregion

    #region XML Helpers
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
      catch (XmlException e)
      {
        smcLog.WriteLog(string.Format("Error: Cannot Load skin {0} xml file: ", file), LogLevel.Error);
        smcLog.WriteLog(e.Message, LogLevel.Info);
        return null;
      }
      return doc;
    }

    /// <summary>
    /// Gets an elements value from section node
    /// </summary>   
    public static string ReadEntryValue(string elementName, XmlNode node)
    {
      string entryValue = string.Empty;

      XmlNode path = node.SelectSingleNode("entry[@name=\"" + elementName + "\"]");
      if (path != null)
      {
        entryValue = path.InnerText;
        return entryValue;
      }

      return "false";
    }

    /// <summary>
    /// Sets a new path for a skin files <import></import>
    /// </summary>   
    public static void SetSkinImport(string file, string importtag, string value)
    {      
      CheckSum checkSum = new CheckSum();
      XmlDocument doc = LoadXMLDocument(file);
      if (doc == null) return;

      // build xpath string
      string xpath = string.Format("/window/controls/import[@tag='{0}']", importtag);

      XmlNode node = doc.DocumentElement.SelectSingleNode(xpath);
      if (node == null)
        return;

      try
      {
        smcLog.WriteLog(string.Format("Setting skin import '<import tag='{0}'>{1}</import>' in '{2}'", importtag, value, file), LogLevel.Debug);
        node.InnerText = value;
        doc.Save(file);
        checkSum.Replace(file);
      }
      catch (Exception ex)
      {
        smcLog.WriteLog("Exception setting skin import: " + ex.Message, LogLevel.Error);
      }
    }

    /// <summary>
    /// Set Define in Skin
    /// </summary>
    public static void SetSkinDefine(string file, string define, string value)
    {
      CheckSum checkSum = new CheckSum();
      XmlDocument doc = LoadXMLDocument(file);
      if (doc == null) return;

      string path = "/window/define";
      XmlNodeList nodes = doc.SelectNodes(path);
      if (nodes == null)
        return;

      try
      {
        foreach (XmlNode node in nodes)
        {
          if (node.InnerText.StartsWith(define))
          {
            smcLog.WriteLog(string.Format("Setting skin define '{0}' with value '{1}' in '{2}'", define, value, file), LogLevel.Debug);
            node.InnerText = string.Format("{0}:{1}", define, value);
          }
        }

        doc.Save(file);
        checkSum.Replace(file);
      }
      catch (Exception ex)
      {
        smcLog.WriteLog("Exception setting skin define: " + ex.Message, LogLevel.Error);
      }
    }

    /// <summary>
    /// Set XML Property
    /// </summary>
    public static void SetNodeText(string file, string path, string value)
    {
      CheckSum checkSum = new CheckSum();
      XmlDocument doc = LoadXMLDocument(file);
      if (doc == null) return;

      XmlNode node = doc.SelectSingleNode(path);
      if (node == null)
        return;

      try
      {
        smcLog.WriteLog(string.Format("Setting skin text property '{0}' with value '{1}'", path, value), LogLevel.Debug);
        node.InnerText = value;

        doc.Save(file);
        checkSum.Replace(file);
      }
      catch (Exception ex)
      {
        smcLog.WriteLog("Exception setting skin text property: " + ex.Message, LogLevel.Error);
      }
    }
    #endregion;

    #region Assembly Helpers
    public static bool IsAssemblyAvailable(string name, Version ver)
    {
      bool result = false;

      smcLog.WriteLog(string.Format("Checking whether assembly {0} is available and loaded...", name), LogLevel.Info);

      Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
      foreach (Assembly a in assemblies)
      {
        try
        {
          if (a.GetName().Name == name)            
          {
            if (a.GetName().Version >= ver)
            {
              smcLog.WriteLog(string.Format("Assembly {0} v{1} is available and loaded.", name, a.GetName().Version.ToString()), LogLevel.Info);
              return true;
            }
            else
            {
              smcLog.WriteLog(string.Format("Assembly {0} v{1} is available but does not meet version requirements v{2}.", name, a.GetName().Version.ToString(), ver.ToString()), LogLevel.Info);
              return false;
            }
          }
        }
        catch (Exception e)
        {
          smcLog.WriteLog(string.Format("Assembly.GetName() call failed for '{0}'!\nException: {1}", a.Location, e), LogLevel.Error);
          result = false;
        }
      }

      if (!result)
      {
        smcLog.WriteLog(string.Format("Assembly {0} is not loaded (not available?), trying to load it manually...", name), LogLevel.Info);
        try
        {          
          Assembly assembly = Assembly.ReflectionOnlyLoad(name);
          if (assembly.GetName().Version >= ver)
          {
            smcLog.WriteLog(string.Format("Assembly {0} v{1} is available and loaded successfully.", name, assembly.GetName().Version.ToString()), LogLevel.Info);
            result = true;
          }
          else
          {
            smcLog.WriteLog(string.Format("Assembly {0} v{1} is either un-available or does not meet version requirements.", name, assembly.GetName().Version.ToString()), LogLevel.Info);
            result = false;
          }
        }
        catch (Exception e)
        {
          smcLog.WriteLog(string.Format("Assembly {0} is unavailable, load unsuccessful: {1}:{2}", name, e.GetType(), e.Message), LogLevel.Info);
          result = false;
        }
      }
      return result;
    }
    #endregion

  }
}
