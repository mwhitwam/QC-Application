using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace QA {
  public class HttpController {
    private string name;
    public delegate string fn(string s);
    private static Dictionary<string, fn> call; 
    private HttpListener listener;
    private Thread t;

    private void AddPrefixes(string name){
      listener.Prefixes.Add(string.Format("http://localhost:8080/{0}/", name));
      listener.Prefixes.Add(string.Format("http://127.0.0.1:8080/{0}/", name));

    }
    public void AddPath(string path, fn function){
      call.Add(path, function);
    }
    private void AddPaths(string name) {
      call = new Dictionary<string, fn>();
      call.Add("/", Page);
      call.Add("Page/", Page);
      call.Add("xml/", ReadXML);
      call.Add("svg/", ReadSVG);
      call.Add("css/", ReadCSS);
      call.Add("src/", ReadSource);
      call.Add("query/", QueryDB);
    }
    private void Build(string name){
      AddPrefixes(name);
      AddPaths(name);
    }
    private string ReadXML(string s){
      return File.Read(string.Format("xml\\{0}", s));
    }
    private string ReadSVG(string s){
      return File.Read(string.Format("svg\\{0}", s));
    }
    private string ReadCSS(string s){
      return File.Read(string.Format("css\\{0}", s));
    }
    private string ReadSource(string s){
      return File.Read(string.Format("src\\{0}", s));
    }
    private string Page(string s){
      return File.Read(string.Format("Page\\{0}.html", s.Substring(0, s.Length-1)));
    }
    private string QueryDB(string sql){
      DBController db = DBControllerFactory.CreateDBController("SQLServer");
      return db.QueryFile(sql);
    }
    private string Respond(string[] s){
      fn f;
      try {
        if (call.TryGetValue(s[s.Length-2], out f)){
          return f(s[s.Length-1]);
        }
      }catch{}
      return "";
    }
    private void Listen() {
      listener = new HttpListener();
      Build(name);
      listener.Start();
      while (listener.IsListening){
        HttpListenerContext context = listener.GetContext();
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;
        string responseString = Respond(request.Url.Segments);
        byte[] buffer=Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer,0,buffer.Length);
        output.Close();
      }
      listener.Stop();
    }
    public HttpController(string n){
      name = n;
      t = new Thread(new ThreadStart(Listen));      
      t.Start();
      while (!t.IsAlive);
    }
  }
}