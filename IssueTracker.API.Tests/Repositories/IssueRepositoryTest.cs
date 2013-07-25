using System.Linq;
using IssueTracker.API.Entities;
using IssueTracker.API.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using ServiceStack.ServiceInterface.Auth;

namespace IssueTracker.API.Tests.Repositories
{
    [TestClass]
    public class IssueRepositoryTest
    {
        private IDbConnectionFactory dbFactory;
        private PersonRepository personRepository;

        [TestInitialize]
        public void InitTests()
        {
            dbFactory = new OrmLiteConnectionFactory(":memory:", false, SqliteOrmLiteDialectProvider.Instance);
            dbFactory.Run(db => db.CreateTable<IssueEntity>());

            var users = new InMemoryAuthRepository();

            personRepository = new PersonRepository(users, null);
        }

        [TestMethod]
        public void AddReturnsId()
        {
            var repository = new IssueRepository(dbFactory, personRepository);

            var response = repository.Add(new IssueEntity());

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Id, 1);
        }
        [TestMethod]
        public void AddPersists()
        {
            var repository = new IssueRepository(dbFactory, personRepository);

            repository.Add(new IssueEntity { Title = "Test Item" });

            dbFactory.Run(db =>
                              {
                                  var response = db.Select<IssueEntity>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Title, "Test Item");
                              });
        }

        [TestMethod]
        public void GetByIdReturnsItem()
        {
            dbFactory.Run(db => db.Insert(new IssueEntity { Id = 1, Title = "Test Item" }));

            var repository = new IssueRepository(dbFactory, personRepository);

            var response = repository.GetById(1);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Title, "Test Item");
        }
        [TestMethod]
        public void GetByIdReturnsNull()
        {
            dbFactory.Run(db => db.Insert(new IssueEntity { Id = 1, Title = "Test Item" }));

            var repository = new IssueRepository(dbFactory, personRepository);

            var response = repository.GetById(2);

            Assert.IsNull(response);
        }

        [TestMethod]
        public void GetAllReturnsEmpty()
        {
            var repository = new IssueRepository(dbFactory, personRepository);

            var response = repository.GetAll();

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 0);
        }
        [TestMethod]
        public void GetAllReturnsItems()
        {
            dbFactory.Run(db =>
                              {
                                  db.Insert(new IssueEntity { Id = 1, Title = "Test Item" });
                                  db.Insert(new IssueEntity { Id = 2, Title = "Test Item 2" });
                              });

            var repository = new IssueRepository(dbFactory, personRepository);

            var response = repository.GetAll();

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 2);
            Assert.AreEqual(response.Single(x => x.Id == 1).Title, "Test Item");
            Assert.AreEqual(response.Single(x => x.Id == 2).Title, "Test Item 2");
        }

        [TestMethod]
        public void UpdatePersists()
        {
            dbFactory.Run(db => db.Insert(new IssueEntity { Id = 1, Title = "Test Item" }));

            var repository = new IssueRepository(dbFactory, personRepository);

            repository.Update(new IssueEntity { Id = 1, Title = "Test Edit" });

            dbFactory.Run(db =>
                              {
                                  var response = db.Select<IssueEntity>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Title, "Test Edit");
                              });
        }
        [TestMethod]
        public void UpdateIsSingular()
        {
            dbFactory.Run(db =>
                              {
                                  db.Insert(new IssueEntity { Id = 1, Title = "Test Item" });
                                  db.Insert(new IssueEntity { Id = 2, Title = "Test Item 2" });
                              });

            var repository = new IssueRepository(dbFactory, personRepository);

            repository.Update(new IssueEntity { Id = 1, Title = "Test Edit" });

            dbFactory.Run(db =>
                              {
                                  var response = db.Select<IssueEntity>();

                                  Assert.AreEqual(response.Count, 2);
                                  Assert.AreEqual(response.Single(x => x.Id == 1).Title, "Test Edit");
                                  Assert.AreEqual(response.Single(x => x.Id == 2).Title, "Test Item 2");
                              });
        }
        [TestMethod]
        public void UpdateFails()
        {
            dbFactory.Run(db => db.Insert(new IssueEntity { Id = 1, Title = "Test Item" }));

            var repository = new IssueRepository(dbFactory, personRepository);

            repository.Update(new IssueEntity { Id = 2, Title = "Test Edit" });
            dbFactory.Run(db =>
                              {
                                  var response = db.Select<IssueEntity>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Title, "Test Item");
                              });
        }

        [TestMethod]
        public void DeletePersists()
        {
            dbFactory.Run(db => db.Insert(new IssueEntity { Id = 1, Title = "Test Item" }));

            var repository = new IssueRepository(dbFactory, personRepository);

            repository.Delete(1);
            dbFactory.Run(db =>
                              {
                                  var response = db.Select<IssueEntity>();

                                  Assert.AreEqual(response.Count, 0);
                              });
        }
        [TestMethod]
        public void DeleteIsSingular()
        {
            dbFactory.Run(db =>
                              {
                                  db.Insert(new IssueEntity { Id = 1, Title = "Test Item" });
                                  db.Insert(new IssueEntity { Id = 2, Title = "Test Item 2" });
                              });

            var repository = new IssueRepository(dbFactory, personRepository);

            repository.Delete(1);
            dbFactory.Run(db =>
                              {
                                  var response = db.Select<IssueEntity>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Title, "Test Item 2");
                              });
        }
        [TestMethod]
        public void DeleteFails()
        {
            dbFactory.Run(db => db.Insert(new IssueEntity { Id = 1, Title = "Test Item" }));

            var repository = new IssueRepository(dbFactory, personRepository);

            repository.Delete(2);
            dbFactory.Run(db =>
                              {
                                  var response = db.Select<IssueEntity>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Title, "Test Item");
                              });
        }
    }
}