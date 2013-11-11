using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StreamedMPEditor;

namespace SMPEditor
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      formStreamedMpEditor appStart = new formStreamedMpEditor();
      if (args.Length != 0)
      {
        if (args[0].ToLower().Contains("regenerateonly"))
          appStart.reGenerateMenu(true);
      }
      else
        appStart.ShowPlugin();
    }
  }
}