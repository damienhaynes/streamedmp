using System;
using System.Collections.Generic;
using System.IO;
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
    private static XmlDocument LoadXMLDocument(string file)
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

  }
}
