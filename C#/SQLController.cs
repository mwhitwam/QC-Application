using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;


namespace QA {
  public class SQLController : DBController {    
    public SQLController(){
      CONNECTION_STRING = "Data Source=gvdb; Initial Catalog=PROD_INV; User ID=enq; Password=enq;";
    }
    public SQLController(string ConnectionString) {
      CONNECTION_STRING = ConnectionString;
    }
    public override string Query(string sql){
      string msg = string.Format("[{0}] SQL Server DB-> Query Started", DateTime.Now);
      using(SqlConnection conn = new SqlConnection(CONNECTION_STRING)) {
        conn.Open();
        SqlCommand command = conn.CreateCommand();
        command.CommandText = sql; //+ "\n";
        XmlDocument xmlDoc = new XmlDocument();
        using (XmlReader reader = command.ExecuteXmlReader()){
          xmlDoc.Load(reader);
          XmlDeclaration xml_declaration;
          xml_declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
          XmlElement document_element = xmlDoc.DocumentElement;
          xmlDoc.InsertBefore(xml_declaration, document_element);
          xmlDoc.DocumentElement.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
          xmlDoc.DocumentElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
          xmlDoc.Save("xml\\results.xml");
        }
        conn.Close();
        System.Console.WriteLine(string.Format("{0}-> Query completed [{1}]", msg,  DateTime.Now));
        using (StringWriter writer = new StringWriter())
        using (XmlWriter textWriter = XmlWriter.Create(writer)){
          xmlDoc.WriteTo(textWriter);
          textWriter.Flush();
          return writer.GetStringBuilder().ToString();
        }
      }
    }

  }
}