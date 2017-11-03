using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
namespace Library.Tests
{
  [TestClass]
  public class BookTests : IDisposable
  {

    public BookTests()
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
      Book book1 = new Book("Goosebumps");
      book1.Save();

      Assert.AreEqual(1, Book.GetAll().Count);
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
    public void Update_UpdatesBookTitleInDatabase_False()
    {
      Book newBook = new Book("The Golden Compass");
      newBook.Save();

      newBook.SetTitle("The Not-So-Golden Compass");
      newBook.Update();

      Book foundBook = Book.Find(newBook.GetId());
      Assert.AreNotEqual(foundBook.GetTitle(), "The Golden Compass");
    }

    [TestMethod]
    public void Delete_DeleteBookInDatabase_0()
    {
      Book newBook = new Book("The Mist");
      newBook.Save();
      newBook.Delete();

      int result = Book.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void AddAuthor_JoinsAuthorToBook_2()
    {
      Book newBook = new Book("Good Omens");
      newBook.Save();

      Author author1 = new Author("Neil Gaiman");
      author1.Save();
      Author author2 = new Author("Terry Prachett");
      author2.Save();
      newBook.AddAuthor(author1);
      newBook.AddAuthor(author2);
      Console.WriteLine(newBook.GetId());

      Assert.AreEqual(2, newBook.GetAllAuthors().Count);

    }

    [TestMethod]
    public void AddCopy_AddsBookCopyToDatabase_4()
    {
        Book book1 = new Book("Harry Potter and the Replenishing Blue Bar");
        book1.Save();

        book1.AddCopy(4);
        int copies = book1.GetNumberOfCopies();
        Assert.AreEqual(4, copies);
    }

    [TestMethod]
    public void GetNumberofAllCopies_GetsNumberofAllCopiesofBookInDatabase_3()
    {
        Book book1 = new Book("Harry Potter and the Waning Celebrity");
        book1.Save();

        Book book2 = new Book("Harry Potter and the Scandalous Divorce");
        book2.Save();

        book1.AddCopy(3);
        book2.AddCopy();
        int copies = book1.GetNumberOfCopies();
        Assert.AreEqual(3, copies);
    }

    [TestMethod]
    public void GetAvailableCopiesIds_GetNumberOfCopiesAvailableToCheckout_4()
    {
        Book book1 = new Book("Harry Potter and the Waning Celebrity");
        book1.Save();

        book1.AddCopy(3);
        int copies = book1.GetAvailableCopiesIds().Count;
        Assert.AreEqual(3, copies);
    }

    [TestMethod]
    public void GetOverdueBooks_GetsListofOverdueBooks_List()
    {
        Patron newPatron = new Patron("Montgomery Burns");
        newPatron.Save();
        Book book1 = new Book("Harry Potter and the Waning Celebrity");
        book1.Save();
        book1.AddCopy();
        Book book2 = new Book("Harry Potter and the Scandalous Divorce");
        book2.Save();
        book2.AddCopy();
        DateTime pastDate = new DateTime(1999, 12,31);
        int copyId = book1.GetAvailableCopiesIds()[0];
        int copyId2 = book2.GetAvailableCopiesIds()[0];
        newPatron.Checkout(pastDate, copyId);
        newPatron.Checkout(pastDate, copyId2);
        List<Book> overdue = Book.GetOverdueBooks();
        Assert.AreEqual(2, overdue.Count);
    }
    // This test satisfies getting a list of overdue books, but it would probably be more realistic to include list of patrons who checked out said books.


  }
}
