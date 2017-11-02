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
    //
    // public void Update()
    // {
    //   Query updateBook = new Query("UPDATE books SET title = @Title WHERE book_id = @BookId");
    //   updateBook.AddParameter("@Title", GetTitle());
    //   updateBook.AddParameter("@BookId", GetId().ToString());
    //   updateBook.Execute();
    //
    // }
    //
    // public void Delete()
    // {
    //   Query deleteBook = new Query("DELETE FROM books WHERE book_id = @BookId");
    //   deleteBook.AddParameter("@BookId", GetId().ToString());
    //   deleteBook.Execute();
    // }
    //
    // public void AddAuthor(Author author)
    // {
    //   Query addAuthor = new Query(@"
    //   SET foreign_key_checks = 0;
    //   INSERT INTO authors_books (author_id, book_id) VALUES (@AuthorId, @BookId);
	// 		SET foreign_key_checks = 1");
    //   addAuthor.AddParameter("@BookId", GetId().ToString());
    //   addAuthor.AddParameter("@AuthorId", author.GetId().ToString());
    //   addAuthor.Execute();
    // }
    //
    // public List<Author> GetAllAuthors()
    // {
    //   List<Author> bookAuthors = new List<Author>{};
    //   Query getAllAuthors = new Query("SELECT authors.* FROM authors_books JOIN authors ON authors_books.author_id = authors.author_id WHERE book_id = @BookId");
    //
    //   getAllAuthors.AddParameter("@BookId", GetId().ToString());
    //   var rdr = getAllAuthors.Read();
    //   while (rdr.Read())
    //   {
    //     Console.WriteLine("AAHHHHHHHHHHHH?");
    //     int id = rdr.GetInt32(0);
    //     string title = rdr.GetName(1);
    //     Author bookAuthor = new Author(title, id);
    //     bookAuthors.Add(bookAuthor);
    //   }
    //   return bookAuthors;
    // }

  }
}
