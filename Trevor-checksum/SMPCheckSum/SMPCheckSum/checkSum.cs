using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace SMPCheckSum
{
  class CheckSum
  {

    public CheckSum()
    {
    }


    public string Add(string xmlFileName)
    {
      rewriteXMLFile(xmlFileName);
      return addCheckSum(xmlFileName, GetMD5HashFromFile(xmlFileName,0));
    }

    public string Read(string xmlFileName)
    {
      return readChksum(xmlFileName);
    }

    public bool Compare (string xmlFileName)
    {
      if (readChksum(xmlFileName) == null)
        return false;

      if (readChksum(xmlFileName).CompareTo(GetMD5HashFromFile(xmlFileName, 50)) == 0)
        return true;
      else
        return false;
    }


    string GetMD5HashFromFile(string fileName, int bytestoIgnore)
    {
      FileStream file = new FileStream(fileName, FileMode.Open);
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

    string readChksum(string xmlFileName)
    {
      string chkSum = null;
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

    void rewriteXMLFile(string xmlFileName)
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(xmlFileName);
      Encoding encoding = Encoding.GetEncoding("utf-8");
      XmlTextWriter writer = new XmlTextWriter(Path.Combine(Path.GetDirectoryName(xmlFileName),"temp.xml"), encoding);
      writer.Formatting = Formatting.Indented;
      doc.Save(writer);
      writer.Close();
      System.IO.File.Delete(xmlFileName);
      File.Move(Path.Combine(Path.GetDirectoryName(xmlFileName), "temp.xml"), xmlFileName);
    }

    string addCheckSum(string xmlFileName, string chksum)
    {
      XmlDocument xmlDoc = new XmlDocument();

      xmlDoc.Load(xmlFileName);

      XmlComment chkSumComment;
      chkSumComment = xmlDoc.CreateComment("Checksum:" + chksum);

      XmlElement root = xmlDoc.DocumentElement;
      xmlDoc.InsertAfter(chkSumComment, root);
      xmlDoc.Save(xmlFileName);
      return chksum;
    }

  }
}
