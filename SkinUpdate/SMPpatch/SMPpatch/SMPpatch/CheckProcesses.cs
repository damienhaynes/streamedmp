using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SMPpatch
{
  // Class to check if a process is active
  class CheckProcesses
  {
    private string pName;
    public CheckProcesses(string pName)
    {
      this.pName = pName;
    }

   public bool running
    {
      get
      {
        Process[] processes = Process.GetProcessesByName(pName);
        if (processes.Length == 0)
          return false;
        return true;
      }
    }
  }
}
