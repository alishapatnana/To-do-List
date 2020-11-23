using CreateList.Models;
using CreateList.Models.ViewModel;
using CreateList.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateListTest
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
        public void AddItemListTest()
        {
            var itemrepo = new ItemRepository(itemcontextmock.Object);
            var itemobj = itemrepo.AddItem(new ItemListViewModel {Name="Conference",Category="Client Meeting"});
            Assert.IsNotNull(itemobj);
        }
        [Test]
        public  void AddItemTestFail()
        {
            var itemrepo = new ItemRepository(itemcontextmock.Object);
            var itemobj = itemrepo.AddItem(new ItemListViewModel { Name = "Conference", Category = "Client Meeting 001" });
            var ans = itemobj.ToString();
            Assert.IsNotNull(ans);
        }
    }
}