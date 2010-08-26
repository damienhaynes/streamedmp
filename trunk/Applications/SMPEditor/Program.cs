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
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      formStreamedMpEditor appStart = new formStreamedMpEditor();
      appStart.ShowPlugin();
    }
  }
}