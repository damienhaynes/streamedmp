using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using SMPCheckSum;

namespace StreamedMPConfig
{
  class Helper
  {
    private static readonly logger smcLog = logger.GetInstance();

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
    /// <param name="section"></param>
    /// <param name="elementName"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    public static string ReadEntryValue(string section, string elementName, XmlNode node)
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
    /// <param name="file"></param>
    /// <param name="importname"></param>
    /// <param name="value"></param>
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

      smcLog.WriteLog(string.Format("Setting skin import '<import tag='{0}'>{1}</import>' in '{2}'", importtag, value, file), LogLevel.Info);
      node.InnerText = value;
      doc.Save(file);
      checkSum.Replace(file);
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
          if (a.GetName().Name == name && a.GetName().Version >= ver)
          {
            smcLog.WriteLog(string.Format("Assembly {0} v{1} is available and loaded.", name, a.GetName().Version.ToString()), LogLevel.Info);
            result = true;
            break;
          }
        }
        catch (Exception e)
        {
          smcLog.WriteLog(string.Format("Assembly.GetName() call failed for '{0}'!\nException: {1}", a.Location, e), LogLevel.Error);
        }
      }

      if (!result)
      {
        smcLog.WriteLog(string.Format("Assembly {0} is not loaded (not available?), trying to load it manually...", name), LogLevel.Info);
        try
        {          
          Assembly assembly = Assembly.ReflectionOnlyLoad(name);
          smcLog.WriteLog(string.Format("Assembly {0} is available and loaded successfully.", name), LogLevel.Info);
          result = true;
        }
        catch (Exception e)
        {
          smcLog.WriteLog(string.Format("Assembly {0} is unavailable, load unsuccessful: {1}:{2}", name, e.GetType(), e.Message), LogLevel.Info);
        }
      }

      return result;
    }
    #endregion

  }
}
