using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
namespace Library.Tests
{
  [TestClass]
  public class AuthorTests : IDisposable
  {

    public AuthorTests()
    {
      DB.DatabaseTest();
    }

    public void Dispose()
    {
      Author.ClearAll();
      Book.ClearAll();
      Patron.ClearAll();
    }

    [TestMethod]
    public void GetAll_ReturnsDatabaseEmptyAtFirst_0()
    {
      int result = Author.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SaveAuthorToDatabase_1()
    {
      Author author1 = new Author("R. L. Stine");
      author1.Save();

      Assert.AreEqual(1, Author.GetAll().Count);
    }

    [TestMethod]
    public void Find_FindsBookInDatabase_True()
    {
      Book newBook = new Book("Mr. Fantastic Fox");
      newBook.Save();

      Book foundBook = Book.Find(newBook.GetId());
      Assert.AreEqual(newBook.GetId(), foundBook.GetId());
    }

    [TestMethod]
    public void Update_UpdatesAuthorNameInDatabase_False()
    {
      Author newAuthor = new Author("J. K. Rowling");
      newAuthor.Save();

      newAuthor.SetName("NORLY Rowling");
      newAuthor.Update();

      Author foundAuthor = Author.Find(newAuthor.GetId());
      Assert.AreNotEqual(foundAuthor.GetName(), "J. K. Rowling");
    }

    [TestMethod]
    public void Delete_DeleteAuthorInDatabase_0()
    {
      Author newAuthor = new Author("Anne Rice");
      newAuthor.Save();
      newAuthor.Delete();

      int result = Author.GetAll().Count;
      Assert.AreEqual(0, result);
    }
  }
}
