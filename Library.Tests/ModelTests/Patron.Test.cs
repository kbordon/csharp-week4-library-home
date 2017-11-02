using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
namespace Library.Tests
{
  [TestClass]
  public class PatronTests : IDisposable
  {

    public PatronTests()
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
      int result = Book.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SaveBookToDatabase_1()
    {
      Patron patron1 = new Patron("Lisa Simpson");
      patron1.Save();

      Assert.AreEqual(1, Patron.GetAll().Count);
    }

    [TestMethod]
    public void Find_FindsPatronInDatabase_True()
    {
      Patron newPatron = new Patron("Lisa Simpson");
      newPatron.Save();

      Patron foundPatron = Patron.Find(newPatron.GetId());
      Assert.AreEqual(newPatron, foundPatron);
    }

    [TestMethod]
    public void Update_UpdatesPatronTitleInDatabase_False()
    {
      Patron newPatron = new Patron("Marjorie Bouvier");
      newPatron.Save();

      newPatron.SetName("Marge Simpson");
      newPatron.Update();

      Patron foundPatron = Patron.Find(newPatron.GetId());
      Assert.AreEqual(foundPatron, newPatron);
    }

    [TestMethod]
    public void Delete_DeletePatronInDatabase_0()
    {
      Patron newPatron = new Patron("Montgomery Burns");
      newPatron.Save();
      newPatron.Delete();

      int result = Patron.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    // [TestMethod]
    // public void AddAuthor_JoinsAuthorToBook_2()
    // {
    //   Book newBook = new Book("Good Omens");
    //   newBook.Save();
    //
    //   Author author1 = new Author("Neil Gaiman");
    //   author1.Save();
    //   Author author2 = new Author("Terry Prachett");
    //   author2.Save();
    //   newBook.AddAuthor(author1);
    //   newBook.AddAuthor(author2);
    //   Console.WriteLine(newBook.GetId());
    //
    //   Assert.AreEqual(2, newBook.GetAllAuthors().Count);
    //
    // }
  }
}
