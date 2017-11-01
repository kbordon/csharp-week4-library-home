using System;
using System.Collections.Generic;

namespace Library.Models
{
	public class Book
	{
    private int _id;
    public void SetId(int id) {_id = id;}
    public int GetId() {return _id;}

    private string _title;
    public void SetTitle(string title) {_title = title;}
    public string GetTitle() {return _title;}

    public Book(string title, int id = 0)
    {
      SetTitle(title);
      SetId(id);
    }

    public void Save()
    {
      Query saveBook = new Query("INSERT INTO books (title) VALUES (@Title)");
      saveBook.AddParameter("@Title", GetTitle());
      saveBook.Execute();
      SetId((int)saveBook.GetCommand().LastInsertedId);
    }

    public static void ClearAll()
    {
      Query clearBooks = new Query("DELETE FROM books");
      clearBooks.Execute();
    }

    public static List<Book>GetAll()
    {
      List<Book> allBooks = new List<Book> {};
      Query getAllBooks = new Query("SELECT * FROM books");
      var rdr = getAllBooks.Read();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string title = rdr.GetString(1);
        Book newBook = new Book(title, id);
        allBooks.Add(newBook);
      }
      return allBooks;
    }

    public static Book Find(int bookId)
    {
      Query findBook = new Query("SELECT * FROM books WHERE book_id = @BookId");
      findBook.AddParameter("@BookId", bookId.ToString());
      var rdr = findBook.Read();
      int id = 0;
      string title = "";
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        title = rdr.GetString(1);
      }
      Book foundBook = new Book(title, id);
      return foundBook;
    }

    public void Update()
    {
      Query updateBook = new Query("UPDATE books SET title = @Title WHERE book_id = @BookId");
      updateBook.AddParameter("@Title", GetTitle());
      updateBook.AddParameter("@BookId", GetId().ToString());
      updateBook.Execute();

    }

    public void Delete()
    {
      Query deleteBook = new Query("DELETE FROM books WHERE book_id = @BookId");
      deleteBook.AddParameter("@BookId", GetId().ToString());
      deleteBook.Execute();
    }

  }
}
