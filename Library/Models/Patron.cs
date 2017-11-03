using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Library.Models
{
	public class Patron
	{
    private int _id;
    public void SetId(int id) {_id = id;}
    public int GetId() {return _id;}

    private string _name;
    public void SetName(string name) {_name = name;}
    public string GetName() {return _name;}

    public Patron(string name, int id = 0)
    {
      SetName(name);
      SetId(id);
    }

    public override bool Equals(System.Object otherPatron)
    {
        if (!(otherPatron is Patron))
        {
            return false;
        }
        else
        {
            Patron newPatron = (Patron) otherPatron;
            bool idEquality = this.GetId() == newPatron.GetId();
            bool nameEquality = this.GetName() == newPatron.GetName();
            return (idEquality && nameEquality);
        }
    }
    public override int GetHashCode()
    {
        return this.GetName().GetHashCode();
    }

    public void Save()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO patrons (name) VALUES (@Name);";
        cmd.Parameters.Add(new MySqlParameter("@Name", GetName()));
        cmd.ExecuteNonQuery();
        SetId((int)cmd.LastInsertedId);
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        // Query saveBook = new Query("INSERT INTO books (title) VALUES (@Name)");
        // saveBook.AddParameter("@Title", GetTitle());
        // saveBook.Execute();
        // SetId((int)saveBook.GetCommand().LastInsertedId);
    }

    public static void ClearAll()
    {
      Query clearPatrons = new Query("DELETE FROM patrons;");
      clearPatrons.Execute();
    }

    public static List<Patron>GetAll()
    {
        List<Patron> allPatrons = new List<Patron> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM patrons;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while (rdr.Read())
        {
            int id = rdr.GetInt32(0);
            string name = rdr.GetString(1);
            Patron newPatron = new Patron(name, id);
            allPatrons.Add(newPatron);
        }
        return allPatrons;

        // List<Patron> allPatrons = new List<Patron> {};
        // Query getAllPatrons = new Query("SELECT * FROM patrons");
        // var rdr = getAllPatrons.Read();
        // while(rdr.Read())
        // {
        //     int id = rdr.GetInt32(0);
        //     string name = rdr.GetString(1);
        //     Patron newPatron = new Patron(name, id);
        //     allPatrons.Add(newPatron);
        // }
        // return allPatrons;
    }
    //
    public static Patron Find(int bookId)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM patrons WHERE patron_id = @PatronId;";
        cmd.Parameters.Add(new MySqlParameter("@PatronId", bookId));
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int id = 0;
        string name = "";
        while (rdr.Read())
        {
            id = rdr.GetInt32(0);
            name = rdr.GetString(1);
        }
        //   Query findBook = new Query("SELECT * FROM books WHERE book_id = @BookId");
        //   findBook.AddParameter("@BookId", bookId.ToString());
        //   var rdr = findBook.Read();
        //   int id = 0;
        //   string title = "";
        //   while(rdr.Read())
        //   {
        //     id = rdr.GetInt32(0);
        //     title = rdr.GetString(1);
        //   }
        Patron foundPatron = new Patron(name, id);
        return foundPatron;
    }

    public void Update()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE patrons SET name = @Name WHERE patron_id = @PatronId;";
        cmd.Parameters.Add(new MySqlParameter("@Name", GetName()));
        cmd.Parameters.Add(new MySqlParameter("@PatronId", GetId()));
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    //   Query updateBook = new Query("UPDATE books SET title = @Title WHERE book_id = @BookId");
    //   updateBook.AddParameter("@Title", GetTitle());
    //   updateBook.AddParameter("@BookId", GetId().ToString());
    //   updateBook.Execute();

    }

    public void Delete()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM patrons WHERE patron_id = @PatronId;";
        cmd.Parameters.Add(new MySqlParameter("@PatronId", GetId()));
        cmd.ExecuteReader();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    //   Query deleteBook = new Query("DELETE FROM books WHERE book_id = @BookId");
    //   deleteBook.AddParameter("@BookId", GetId().ToString());
    //   deleteBook.Execute();
    }

    public void Checkout(DateTime date, int copyId)
    {
        string dueDate = date.AddDays(28).ToString("yyyy-MM-dd HH:mm:ss");
        string checkOutDate = date.ToString("yyyy-MM-dd HH:mm:ss");
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO checkouts (patron_id, copy_id, check_out, due_date) VALUES (@PatronId, @CopyId, @CheckOut, @DueDate);";
        cmd.Parameters.Add(new MySqlParameter("@PatronId", GetId()));
        cmd.Parameters.Add(new MySqlParameter("@CopyId", copyId));
        cmd.Parameters.Add(new MySqlParameter("@CheckOut", checkOutDate));
        cmd.Parameters.Add(new MySqlParameter("@DueDate", dueDate));
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public Dictionary<string, object> GetHistory()
    {
        Dictionary<string, object> history = new Dictionary<string, object>{};
		List<DateTime> dueDates = new List<DateTime>{};
		List<DateTime> checkouts = new List<DateTime>{};
		List<Book> patronBooks = new List<Book>{};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT books.*, checkouts.check_out, checkouts.due_date FROM checkouts JOIN copies ON (checkouts.copy_id = copies.copy_id) JOIN books on (copies.book_id = books.book_id) WHERE checkouts.patron_id = @PatronId;";
		cmd.Parameters.Add(new MySqlParameter("@PatronId", GetId()));
		var rdr = cmd.ExecuteReader() as MySqlDataReader;
		while (rdr.Read())
		{
			int id = rdr.GetInt32(0);
			string title = rdr.GetString(1);
			patronBooks.Add(new Book(title, id));
			DateTime check = rdr.GetDateTime(2);
			checkouts.Add(check);
			DateTime due = rdr.GetDateTime(3);
			dueDates.Add(due);
		}
		history.Add("books", patronBooks);
		history.Add("check-dates", checkouts);
		history.Add("due-dates", dueDates);
        return history;
    }

	public string GetDueDate(Book loan)
	{
		string dueDate = "";
		MySqlConnection conn = DB.Connection();
		conn.Open();
		var cmd = conn.CreateCommand() as MySqlCommand;
		cmd.CommandText = @"SELECT checkouts.due_date FROM copies JOIN checkouts ON copies.copy_id = checkouts.copy_id WHERE checkouts.patron_id = @PatronId and copies.book_id = @BookId LIMIT 1;";
		// this assumes the user can only check out one copy of a specific book. modify return into a list of strings otherwise.
		cmd.Parameters.Add(new MySqlParameter("@PatronId", GetId()));
		cmd.Parameters.Add(new MySqlParameter("@BookId", loan.GetId()));
		var rdr = cmd.ExecuteReader() as MySqlDataReader;
		while (rdr.Read())
		{
			dueDate = rdr.GetDateTime(0).ToString("yyyy-MM-dd hh:MM:ss");
		}
		conn.Close();
		if (conn != null)
		{
			conn.Dispose();
		}
		return dueDate;
	}

  }
}
