using System.IO;
using System.Text;
namespace QA
{
  public static class File {
    public static string Read(string path){
      string r;
      try {
        using (FileStream file = new FileStream(path
                                              , FileMode.Open
                                              , FileAccess.Read
                                              , FileShare.ReadWrite))
        using (StreamReader sr = new StreamReader(file, Encoding.UTF8)){
          r = sr.ReadToEnd();
        }
        return r;
      } catch {return "";}
    }
    public static void Write(string path, byte[] buffer){
      using (FileStream file = new FileStream(path
                                        , FileMode.Create
                                        , FileAccess.Write
                                        , FileShare.ReadWrite)){
        file.Seek(0, SeekOrigin.End);
        file.Write(buffer, 0, buffer.Length);
      }
    }
  }
}