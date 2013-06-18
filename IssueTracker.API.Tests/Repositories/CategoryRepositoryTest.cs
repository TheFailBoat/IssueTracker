using System.Linq;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using ServiceStack.ServiceInterface.Auth;

namespace IssueTracker.API.Tests.Repositories
{
    [TestClass]
    public class CategoryRepositoryTest
    {
        private IDbConnectionFactory dbFactory;
        private PersonRepository personRepository;

        [TestInitialize]
        public void InitTests()
        {
            dbFactory = new OrmLiteConnectionFactory(":memory:", false, SqliteOrmLiteDialectProvider.Instance);
            dbFactory.Run(db => db.CreateTable<Category>());

            var users = new InMemoryAuthRepository();

            personRepository = new PersonRepository(users);
        }

        [TestMethod]
        public void AddReturnsId()
        {
            var repository = new CategoryRepository(dbFactory, personRepository);

            var response = repository.Add(new Category());

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Id, 1);
        }
        [TestMethod]
        public void AddPersists()
        {
            var repository = new CategoryRepository(dbFactory, personRepository);

            repository.Add(new Category { Name = "Test Item" });

            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Category>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Name, "Test Item");
                              });
        }

        [TestMethod]
        public void GetByIdReturnsItem()
        {
            dbFactory.Run(db => db.Insert(new Category { Id = 1, Name = "Test Item" }));

            var repository = new CategoryRepository(dbFactory, personRepository);

            var response = repository.GetById(1);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Name, "Test Item");
        }
        [TestMethod]
        public void GetByIdReturnsNull()
        {
            dbFactory.Run(db => db.Insert(new Category { Id = 1, Name = "Test Item" }));

            var repository = new CategoryRepository(dbFactory, personRepository);

            var response = repository.GetById(2);

            Assert.IsNull(response);
        }

        [TestMethod]
        public void GetAllReturnsEmpty()
        {
            var repository = new CategoryRepository(dbFactory, personRepository);

            var response = repository.GetAll();

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 0);
        }
        [TestMethod]
        public void GetAllReturnsItems()
        {
            dbFactory.Run(db =>
                              {
                                  db.Insert(new Category { Id = 1, Name = "Test Item" });
                                  db.Insert(new Category { Id = 2, Name = "Test Item 2" });
                              });

            var repository = new CategoryRepository(dbFactory, personRepository);

            var response = repository.GetAll();

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 2);
            Assert.AreEqual(response.Single(x => x.Id == 1).Name, "Test Item");
            Assert.AreEqual(response.Single(x => x.Id == 2).Name, "Test Item 2");
        }

        [TestMethod]
        public void UpdatePersists()
        {
            dbFactory.Run(db => db.Insert(new Category { Id = 1, Name = "Test Item" }));

            var repository = new CategoryRepository(dbFactory, personRepository);

            repository.Update(new Category { Id = 1, Name = "Test Edit" });

            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Category>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Name, "Test Edit");
                              });
        }
        [TestMethod]
        public void UpdateIsSingular()
        {
            dbFactory.Run(db =>
                              {
                                  db.Insert(new Category { Id = 1, Name = "Test Item" });
                                  db.Insert(new Category { Id = 2, Name = "Test Item 2" });
                              });

            var repository = new CategoryRepository(dbFactory, personRepository);

            repository.Update(new Category { Id = 1, Name = "Test Edit" });

            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Category>();

                                  Assert.AreEqual(response.Count, 2);
                                  Assert.AreEqual(response.Single(x => x.Id == 1).Name, "Test Edit");
                                  Assert.AreEqual(response.Single(x => x.Id == 2).Name, "Test Item 2");
                              });
        }
        [TestMethod]
        public void UpdateFails()
        {
            dbFactory.Run(db => db.Insert(new Category { Id = 1, Name = "Test Item" }));

            var repository = new CategoryRepository(dbFactory, personRepository);

            repository.Update(new Category { Id = 2, Name = "Test Edit" });
            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Category>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Name, "Test Item");
                              });
        }

        [TestMethod]
        public void DeletePersists()
        {
            dbFactory.Run(db => db.Insert(new Category { Id = 1, Name = "Test Item" }));

            var repository = new CategoryRepository(dbFactory, personRepository);

            repository.Delete(1);
            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Category>();

                                  Assert.AreEqual(response.Count, 0);
                              });
        }
        [TestMethod]
        public void DeleteIsSingular()
        {
            dbFactory.Run(db =>
                              {
                                  db.Insert(new Category { Id = 1, Name = "Test Item" });
                                  db.Insert(new Category { Id = 2, Name = "Test Item 2" });
                              });

            var repository = new CategoryRepository(dbFactory, personRepository);

            repository.Delete(1);
            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Category>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Name, "Test Item 2");
                              });
        }
        [TestMethod]
        public void DeleteFails()
        {
            dbFactory.Run(db => db.Insert(new Category { Id = 1, Name = "Test Item" }));

            var repository = new CategoryRepository(dbFactory, personRepository);

            repository.Delete(2);
            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Category>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Name, "Test Item");
                              });
        }
    }
}