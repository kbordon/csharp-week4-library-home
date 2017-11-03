using System;
using System.Collections.Generic;

namespace Library.Models
{
	public class Author
	{
    private int _id;
    public void SetId(int id) {_id = id;}
    public int GetId() {return _id;}

    private string _name;
    public void SetName(string name) {_name = name;}
    public string GetName() {return _name;}

    public Author(string name, int id = 0)
    {
      SetName(name);
      SetId(id);
    }

    public void Save()
    {
      Query saveAuthor = new Query("INSERT INTO authors (name) VALUES (@Name)");
      saveAuthor.AddParameter("@Name", GetName());
      saveAuthor.Execute();
      SetId((int)saveAuthor.GetCommand().LastInsertedId);
    }

    public static void ClearAll()
    {
      Query clearAuthors = new Query("DELETE FROM authors_books; DELETE FROM copies; DELETE FROM authors; DELETE FROM checkouts;");
      clearAuthors.Execute();
    }

    public static List<Author>GetAll()
    {
      List<Author> allAuthors = new List<Author> {};
      Query getAllAuthors = new Query("SELECT * FROM authors");
      var rdr = getAllAuthors.Read();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Author newAuthor = new Author(name, id);
        allAuthors.Add(newAuthor);
      }
      return allAuthors;
    }

    public static Author Find(int authorId)
    {
      Query findAuthor = new Query("SELECT * FROM authors WHERE author_id = @AuthorId");
      findAuthor.AddParameter("@AuthorId", authorId.ToString());
      var rdr = findAuthor.Read();
      int id = 0;
      string name = "";
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
      }
      Author foundAuthor = new Author(name, id);
      return foundAuthor;
    }

    public void Update()
    {
      Query updateAuthor = new Query("UPDATE authors SET name = @Name WHERE author_id = @AuthorId");
      updateAuthor.AddParameter("@Name", GetName());
      updateAuthor.AddParameter("@AuthorId", GetId().ToString());
      updateAuthor.Execute();

    }

    public void Delete()
    {
      Query deleteAuthor = new Query("DELETE FROM authors WHERE author_id = @AuthorId");
      deleteAuthor.AddParameter("@AuthorId", GetId().ToString());
      deleteAuthor.Execute();
    }

  }
}
