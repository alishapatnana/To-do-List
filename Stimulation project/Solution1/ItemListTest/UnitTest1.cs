using ItemList.Models;
using ItemList.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemListTest
{
    public class Tests
    {
        List<ItemListModel> item = new List<ItemListModel>();
        IQueryable<ItemListModel> itemdata;
        Mock<DbSet<ItemListModel>> mockSet;
        Mock<AppDbContext> itemcontextmock;
        [SetUp]
        public void Setup()
        {
           item = new List<ItemListModel>()
           {
                new ItemListModel{Id = 20, Category="Submit Forms",Name="Client meet"},
           };
            itemdata = item.AsQueryable();
            mockSet = new Mock<DbSet<ItemListModel>>();
            mockSet.As<IQueryable<ItemListModel>>().Setup(m => m.Provider).Returns(itemdata.Provider);
            mockSet.As<IQueryable<ItemListModel>>().Setup(m => m.Expression).Returns(itemdata.Expression);
            mockSet.As<IQueryable<ItemListModel>>().Setup(m => m.ElementType).Returns(itemdata.ElementType);
            mockSet.As<IQueryable<ItemListModel>>().Setup(m => m.GetEnumerator()).Returns(itemdata.GetEnumerator());
            var p = new DbContextOptions<AppDbContext>();
            itemcontextmock = new Mock<AppDbContext>(p);
            itemcontextmock.Setup(x => x.ItemList).Returns(mockSet.Object);



        }


        [Test]
        public void GetAll()
        {
            var itemrepo = new ItemRepository(itemcontextmock.Object);
            var itemlist = itemrepo.GetItemList();
            Assert.NotNull(itemlist);
        }
        [Test]
        public void GetByIdTest()
        {
            var itemrepo = new ItemRepository(itemcontextmock.Object);
            var itemobj = itemrepo.GetById(20);
            Assert.IsNotNull(itemobj);
        }
        [Test]
        public void GetByIdTestFail()
        {
            var itemrepo = new ItemRepository(itemcontextmock.Object);
            var itemobj = itemrepo.GetById(22);
            Assert.NotNull(itemobj);
        }

    }
}