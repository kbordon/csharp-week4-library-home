using System;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{

  public class DB
  {
    public static void DatabaseTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }

    public static MySqlConnection Connection()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      return conn;
    }
  }

  public class Query : IDisposable
  {
    private MySqlCommand _cmd;
    private MySqlConnection _conn;

    private Query lastQuery;

    public void Dispose()
    {
      _conn.Close();
      if (_conn != null)
      {
        _conn.Dispose();
      }
    }

    public MySqlCommand GetCommand()
    {
      return _cmd;
    }

    public MySqlConnection GetConnection()
    {
      return _conn;
    }

    public Query(string query)
    {
      if (lastQuery != null)
      {
        lastQuery.Dispose();
      }
      _conn = DB.Connection();
      _cmd = _conn.CreateCommand();
      _conn.Open();
      _cmd.CommandText = @query;
      lastQuery = this;
    }

    public void AddParameter(string key, string value)
    {
      MySqlParameter parameter = new MySqlParameter();
      parameter.ParameterName = key;
      parameter.Value = value;
      _cmd.Parameters.Add(parameter);
    }

    public void Execute()
    {
      _cmd.ExecuteNonQuery();
    }
    public MySqlDataReader Read()
    {
      MySqlDataReader rdr = _cmd.ExecuteReader() as MySqlDataReader;
      return rdr;
    }
  }
}
