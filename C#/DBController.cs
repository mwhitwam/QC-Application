//using System;

namespace QA {
  public static class DBControllerFactory {
    public static DBController CreateDBController(string dbType){
      switch (dbType) {
        case "SQLServer"  : return new SQLController();
        //case "MySQL"      : return new MySQLController();
        default           : return null;
      }
    }
    public static DBController CreateDBController(string dbType, string ConnectionString){
      switch (dbType) {
        case "SQLServer"  : return new SQLController(ConnectionString);
        //case "MySQL"      : return new MySQLController(ConnectionString);
        default           : return null;
      }
    }
  }

  public abstract class DBController {
    protected const string XMLHEAD = "<XML xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\n";
    protected const string XMLFOOT = "</XML>";
    protected string CONNECTION_STRING;// = "Data Source=gvdb; Initial Catalog=PROD_INV; User ID=enq; Password=enq;";

    public DBController(){
    }
    public DBController(string s){
      CONNECTION_STRING = s;
    }

    public string QueryFile(string sql){return Query(File.Read(string.Format("sql\\{0}.sql",sql)));}

    public abstract string Query(string sql);
  }
}