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
      Book.ClearAll();
    }

    [TestMethod]
    public void GetAll_ReturnsDatabaseEmptyAtFirst_0()
    {
      List<Book> result = Book.GetAll();

      Assert.AreEqual(0, result);
    }
  }
}
