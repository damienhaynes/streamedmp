// **************************************************************************************
// This class is a modified version of the logger class from InfoServce by edsche
// permission has been requested to use this
// **************************************************************************************
using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using MediaPortal.GUI.Library;

namespace StreamedMPConfig
{
  public enum LogLevel
  {
    Info    = 1,
    Warning = 2,
    Error   = 3,
    Debug   = 4,
  }

  public class tempLogger
  {
    public tempLogger(string message, LogLevel level)
    {
      this.message = message;
      this.level = level;
    }

    public string message { get; set; }
    public LogLevel level { get; set; }
  }

  public class logger
  {
    private logger()
    {
    }

    private static volatile logger _Logger = null;

    public static logger GetInstance()
    {
      // DoubleLock
      if (_Logger == null)
      {
        lock (m_lock)
        {
          if (_Logger == null) _Logger = new logger();
        }
      }
      return _Logger;
    }

    private static object m_lock = new object();

    private static System.IO.StreamWriter _output = null;
    private static System.IO.StreamWriter Output
    {
      get
      {
        if (_output == null && !string.IsNullOrEmpty(LogFile))
        {
          _output = File.CreateText(LogFile);
          _output.Close();
          _output.Dispose();
        }
        return _output;
      }
      set
      {
        _output = value;
      }
    }

    private static List<tempLogger> _tempLogger = new List<tempLogger>();

    public static bool LogDebug { get; set; }
    public static bool LogError { get; set; }
    public static bool LogWarning { get; set; }

    private static string _logFile = string.Empty;
    public static string LogFile
    {
      get { return _logFile; }
      set
      {
        if (string.IsNullOrEmpty(_logFile))
        {
          if (System.IO.File.Exists(value))
          {
            string dir = System.IO.Path.GetDirectoryName(value) + "\\";
            string filename = System.IO.Path.GetFileNameWithoutExtension(value) + ".bak";
            if (System.IO.File.Exists(dir + filename)) System.IO.File.Delete(dir + filename);
            System.IO.File.Move(value, dir + filename);
          }
          _logFile = value;
        }
      }
    }

    /// <summary>
    /// Writes Formatted String to Log at Information Level
    /// </summary>    
    public void WriteLog(string format, params object[] arg)
    {
      WriteLog(string.Format(format, arg), LogLevel.Info);
    }

    /// <summary>
    /// Writes Formatted String to Log
    /// </summary>    
    public void WriteLog(string format, LogLevel level, params object[] arg)
    {
      WriteLog(string.Format(format, arg), level);
    }

    /// <summary>
    /// Writes log at Information Level
    /// </summary>
    public void WriteLog(string message)
    {
      WriteLog(message, LogLevel.Info);
    }

    public void WriteLog(string message, LogLevel level)
    {
      try
      {
        if (Output == null)
        {
          _tempLogger.Add(new tempLogger(message, level));
          return;
        }

        if ((level == LogLevel.Debug && LogDebug) || (level == LogLevel.Error && LogError) || (level == LogLevel.Warning && LogWarning) || level == LogLevel.Info)
        {
          lock (Output)
          {
            if (File.Exists(LogFile))
              Output = File.AppendText(LogFile);
            else
              Output = File.CreateText(LogFile);

            Output.WriteLine(DateTime.Now + " [" + level + "] " +string.Format("{0:D4}", message));
            Output.Flush();
            Output.Close();
            Output.Dispose();
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    public static void CloseLog()
    {
      try
      {
        if (Output != null)
        {
          GetInstance().FlushTempLog();
          Output.Flush();
          Output.Close();
          Output = null;
          LogFile = string.Empty;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    public void FlushTempLog(string toFile)
    {
      LogFile = toFile;
      LogDebug = true;
      LogError = true;
      LogWarning = true;
      GetInstance().FlushTempLog();
    }
    public void FlushTempLog()
    {
      try
      {
        if (Output != null && _tempLogger != null && _tempLogger.Count > 0)
        {
          WriteLog("Flushing the memory log", LogLevel.Debug);
          foreach (tempLogger tempLogger in _tempLogger)
          {
            WriteLog(tempLogger.message, tempLogger.level);
          }
          _tempLogger.Clear();
          WriteLog("End flushing the memory log", LogLevel.Debug);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}
