namespace QA
{
    public class App
    {
      const string NAME = "QualityAssurance";
      private static readonly App instance = new App();
      private HttpController http = new HttpController(NAME);

      public static App Instance
      {
        get
        {
          return instance;
        }
      }

      public App()
      {
      }
      public static System.Diagnostics.Process Show()
      {
        return System.Diagnostics.Process.Start(string.Format("http://localhost:8080/{0}/", NAME));
      }
    }
}