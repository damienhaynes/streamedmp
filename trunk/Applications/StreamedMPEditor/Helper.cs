using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
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

    public void loadPrettyItems(ref System.Windows.Forms.ComboBox cboQuickSelect, List<string> ids)
    {
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

        // Dont Add item if its not available
        if (ids.Contains(pItem.id))
          formStreamedMpEditor.prettyItems.Add(pItem);
      }
      // Load list
      foreach (formStreamedMpEditor.prettyItem p in formStreamedMpEditor.prettyItems)
      {
        cboQuickSelect.Items.Add(p.name);
      }
      cboQuickSelect.SelectedIndex = 0;

    }

    public bool pluginEnabled(string pluginName)
    {
      // check if plugin is enabled/disabled
      // if we dont find they entry then we assume enabled as we know it
      // is installed so most likley configuration has not yet been run
      // to write the entry to MediaPortal.xml
      string returnValue;

      try
      {
        using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
        {
          returnValue = xmlreader.GetValueAsString("plugins", pluginName, "");
        }
      }
      catch (Exception e)
      {
        showError("Error reading MediaPortal.xml : " + e.Message, formStreamedMpEditor.errorCode.readError);
        return true;
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
      try
      {
        using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
        {
          return xmlreader.GetValueAsString(sectionName, entryName, "");
        }
      }
      catch (Exception e)
      {
        showError("Error reading MediaPortal.xml : " + e.Message, formStreamedMpEditor.errorCode.readError);
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
        showError("Error writing MediaPortal.xml : " + e.Message, formStreamedMpEditor.errorCode.readError);
      }
      MediaPortal.Profile.Settings.SaveCache();
    }


  }
}
