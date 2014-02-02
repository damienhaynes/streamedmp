using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace SMPCheckSum
{
  public class CheckSum
  {
    #region Public Methods
    
    //
    // Add Checksum to specified file if one does not exist and return the calculated checksum.
    //
    public string Add(string xmlFileName)
    {
      if (readChksum(xmlFileName) == null)
      {
        rewriteXMLFile(xmlFileName);
        return addCheckSum(xmlFileName, GetMD5HashFromFile(xmlFileName, 0));
      }
      else
        return readChksum(xmlFileName);
    }
    //
    // Read the Checksum and return it, NULL is returned if no Checksum exists
    //
    public string Get(string xmlFileName)
    {
      return readChksum(xmlFileName);
    }
    //
    // Read the existing Checksum and compare with re-calculated checksum on the file after stripping off the embedded checksum.
    //
    public bool Compare(string xmlFileName)
    {
      if (readChksum(xmlFileName) == null)
        return false;

      if (readChksum(xmlFileName).CompareTo(GetMD5HashFromFile(xmlFileName, 50)) == 0)
        return true;
      else
        return false;
    }
    //
    // Recalulate and replace existing Checksum on the file
    //
    public string Replace(string xmlFileName)
    {
      if (readChksum(xmlFileName) != null)
        stripChecksum(xmlFileName, 50);

      rewriteXMLFile(xmlFileName);
      return addCheckSum(xmlFileName, GetMD5HashFromFile(xmlFileName, 0));
    }
    //
    // Recalulate and replace existing Checksum on all xml files
    // Typically used by installer after install/modify
    //
    public void ReplaceAllFiles(string xmlPath)
    {
      string[] xmlFileNames = Directory.GetFiles(xmlPath, "*.xml", SearchOption.AllDirectories);
      
      foreach (string xmlFileName in xmlFileNames)
      {
        try
        {
          if (readChksum(xmlFileName) != null)
            stripChecksum(xmlFileName, 50);

          rewriteXMLFile(xmlFileName);
          addCheckSum(xmlFileName, GetMD5HashFromFile(xmlFileName, 0));
        }
        catch { }
      }     
    }

    //
    // Remove the Checksum from the file
    //
    public void Remove(string xmlFileName)
    {
      if (readChksum(xmlFileName) != null)
      {
        stripChecksum(xmlFileName, 50);
      }
    }

    #endregion

    #region Private Methods

    string readChksum(string xmlFileName)
    {
      string chkSum = null;
      checkAndThrow(xmlFileName);
      try
      {
          XmlTextReader reader = new XmlTextReader(xmlFileName);
          while (!reader.Value.StartsWith("Checksum:") && !reader.EOF)
          {
              reader.Read();
          }
          if (reader.EOF)
          {
              reader.Close();
              return null;
          }
          else
          {
              chkSum = reader.Value.Substring(9);
              reader.Close();
              return chkSum;
          }
      }
      catch { return null; }
    }

    void stripChecksum(string xmlFileName, int bytesToIgnore)
    {
      checkAndThrow(xmlFileName);

      FileStream readFile = new FileStream(xmlFileName, FileMode.Open);
      int bytesToRead = ((int)readFile.Length - bytesToIgnore);
      byte[] theFile = new byte[bytesToRead];

      readFile.Read(theFile, 0, bytesToRead);
      readFile.Close();

      FileStream writeFile = new FileStream(xmlFileName, FileMode.Create);
      writeFile.Write(theFile, 0, bytesToRead);
      writeFile.Close();
    }

    string GetMD5HashFromFile(string xmlFileName, int bytestoIgnore)
    {
      checkAndThrow(xmlFileName);

      FileStream file = new FileStream(xmlFileName, FileMode.Open);
      MD5 md5 = new MD5CryptoServiceProvider();
      int bytesToRead = ((int)file.Length - bytestoIgnore);
      byte[] theFile = new byte[bytesToRead];

      file.Read(theFile, 0, bytesToRead);

      byte[] retVal = md5.ComputeHash(theFile);
      file.Close();

      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < retVal.Length; i++)
      {
        sb.Append(retVal[i].ToString("x2"));
      }
      return sb.ToString();
    }

    void rewriteXMLFile(string xmlFileName)
    {
      checkAndThrow(xmlFileName);

      XmlDocument doc = new XmlDocument();
      try
      {
          doc.Load(xmlFileName);
          Encoding encoding = Encoding.GetEncoding("utf-8");
          XmlTextWriter writer = new XmlTextWriter(Path.Combine(Path.GetDirectoryName(xmlFileName), "temp.xml"), encoding);
          writer.Formatting = Formatting.Indented;
          doc.Save(writer);
          writer.Close();
          File.Delete(xmlFileName);
          File.Move(Path.Combine(Path.GetDirectoryName(xmlFileName), "temp.xml"), xmlFileName);
      }
      catch { return; }
    }

    string addCheckSum(string xmlFileName, string chksum)
    {
      XmlDocument xmlDoc = new XmlDocument();
      try
      {
          xmlDoc.Load(xmlFileName);

          XmlComment chkSumComment;
          chkSumComment = xmlDoc.CreateComment("Checksum:" + chksum);

          XmlElement root = xmlDoc.DocumentElement;
          xmlDoc.InsertAfter(chkSumComment, root);
          xmlDoc.Save(xmlFileName);
          return chksum;
      }
      catch { return null; }
    }

    void checkAndThrow(string xmlFileName)
    {
      if (!File.Exists(xmlFileName))
        throw new FileNotFoundException("File does not exist", xmlFileName);

      if (Path.GetExtension(xmlFileName).ToLower() != ".xml")
        throw new ArgumentException("Only XML files are supported", xmlFileName);
    }

    #endregion
  }
}
