using AuthServices.Controllers;
using AuthServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AuthServicesTest
{
    public class Tests
    {
        List<UserModel> user = new List<UserModel>();
        IQueryable<UserModel> userdata;
        Mock<DbSet<UserModel>> mockSet;
        Mock<AppDbContext> usercontextmock;
        [SetUp]
        public void Setup()
        {
            user = new List<UserModel>()
            {
                new UserModel{UserName="alisha",Password="ali"}

            };
            userdata = user.AsQueryable();
            mockSet = new Mock<DbSet<UserModel>>();
            mockSet.As<IQueryable<UserModel>>().Setup(m => m.Provider).Returns(userdata.Provider);
            mockSet.As<IQueryable<UserModel>>().Setup(m => m.Expression).Returns(userdata.Expression);
            mockSet.As<IQueryable<UserModel>>().Setup(m => m.ElementType).Returns(userdata.ElementType);
            mockSet.As<IQueryable<UserModel>>().Setup(m => m.GetEnumerator()).Returns(userdata.GetEnumerator());
            var p = new DbContextOptions<AppDbContext>();
            usercontextmock = new Mock<AppDbContext>(p);
            usercontextmock.Setup(x => x.UserList).Returns(mockSet.Object);



        }


        [Test]
        public void LoginTest()
        {

            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
            var controller = new AuthController(usercontextmock.Object, config.Object);
            var login = controller.Login(new UserModel { UserName = "alisha", Password = "ali" });


            var Actualstatuscode = (HttpStatusCode)login.GetType().GetProperty("StatusCode").GetValue(login);

            var ExpectedStatusCode = HttpStatusCode.OK;
            Assert.AreEqual(ExpectedStatusCode, Actualstatuscode);






        }
        [Test]
        public void LoginFailTest()
        {

            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
            var controller = new AuthController(usercontextmock.Object, config.Object);
            var login = controller.Login(new UserModel { UserName = "asdjb", Password = "zsc" });
            var Actualstatuscode = (HttpStatusCode)login.GetType().GetProperty("StatusCode").GetValue(login);
            var ExpectedStatusCode = HttpStatusCode.NotFound;

            Assert.AreEqual(ExpectedStatusCode, Actualstatuscode);






        }

    }
}