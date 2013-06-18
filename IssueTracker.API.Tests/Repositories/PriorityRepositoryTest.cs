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
    public class PriorityRepositoryTest
    {
        private IDbConnectionFactory dbFactory;
        private PersonRepository personRepository;

        [TestInitialize]
        public void InitTests()
        {
            dbFactory = new OrmLiteConnectionFactory(":memory:", false, SqliteOrmLiteDialectProvider.Instance);
            dbFactory.Run(db => db.CreateTable<Priority>());

            var users = new InMemoryAuthRepository();

            personRepository = new PersonRepository(users);
        }

        [TestMethod]
        public void AddReturnsId()
        {
            var repository = new PriorityRepository(dbFactory, personRepository);

            var response = repository.Add(new Priority());

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Id, 1);
        }
        [TestMethod]
        public void AddPersists()
        {
            var repository = new PriorityRepository(dbFactory, personRepository);

            repository.Add(new Priority { Name = "Test Item" });

            dbFactory.Run(db =>
            {
                var response = db.Select<Priority>();

                Assert.AreEqual(response.Count, 1);
                Assert.AreEqual(response[0].Name, "Test Item");
            });
        }

        [TestMethod]
        public void GetByIdReturnsItem()
        {
            dbFactory.Run(db => db.Insert(new Priority { Id = 1, Name = "Test Item" }));

            var repository = new PriorityRepository(dbFactory, personRepository);

            var response = repository.GetById(1);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Name, "Test Item");
        }
        [TestMethod]
        public void GetByIdReturnsNull()
        {
            dbFactory.Run(db => db.Insert(new Priority { Id = 1, Name = "Test Item" }));

            var repository = new PriorityRepository(dbFactory, personRepository);

            var response = repository.GetById(2);

            Assert.IsNull(response);
        }

        [TestMethod]
        public void GetAllReturnsEmpty()
        {
            var repository = new PriorityRepository(dbFactory, personRepository);

            var response = repository.GetAll();

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 0);
        }
        [TestMethod]
        public void GetAllReturnsItems()
        {
            dbFactory.Run(db =>
            {
                db.Insert(new Priority { Id = 1, Name = "Test Item" });
                db.Insert(new Priority { Id = 2, Name = "Test Item 2" });
            });

            var repository = new PriorityRepository(dbFactory, personRepository);

            var response = repository.GetAll();

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 2);
            Assert.AreEqual(response.Single(x => x.Id == 1).Name, "Test Item");
            Assert.AreEqual(response.Single(x => x.Id == 2).Name, "Test Item 2");
        }

        [TestMethod]
        public void UpdatePersists()
        {
            dbFactory.Run(db => db.Insert(new Priority { Id = 1, Name = "Test Item" }));

            var repository = new PriorityRepository(dbFactory, personRepository);

            repository.Update(new Priority { Id = 1, Name = "Test Edit" });

            dbFactory.Run(db =>
            {
                var response = db.Select<Priority>();

                Assert.AreEqual(response.Count, 1);
                Assert.AreEqual(response[0].Name, "Test Edit");
            });
        }
        [TestMethod]
        public void UpdateIsSingular()
        {
            dbFactory.Run(db =>
            {
                db.Insert(new Priority { Id = 1, Name = "Test Item" });
                db.Insert(new Priority { Id = 2, Name = "Test Item 2" });
            });

            var repository = new PriorityRepository(dbFactory, personRepository);

            repository.Update(new Priority { Id = 1, Name = "Test Edit" });

            dbFactory.Run(db =>
            {
                var response = db.Select<Priority>();

                Assert.AreEqual(response.Count, 2);
                Assert.AreEqual(response.Single(x => x.Id == 1).Name, "Test Edit");
                Assert.AreEqual(response.Single(x => x.Id == 2).Name, "Test Item 2");
            });
        }
        [TestMethod]
        public void UpdateFails()
        {
            dbFactory.Run(db => db.Insert(new Priority { Id = 1, Name = "Test Item" }));

            var repository = new PriorityRepository(dbFactory, personRepository);

            repository.Update(new Priority { Id = 2, Name = "Test Edit" });
            dbFactory.Run(db =>
            {
                var response = db.Select<Priority>();

                Assert.AreEqual(response.Count, 1);
                Assert.AreEqual(response[0].Name, "Test Item");
            });
        }

        [TestMethod]
        public void DeletePersists()
        {
            dbFactory.Run(db => db.Insert(new Priority { Id = 1, Name = "Test Item" }));

            var repository = new PriorityRepository(dbFactory, personRepository);

            repository.Delete(1);
            dbFactory.Run(db =>
            {
                var response = db.Select<Priority>();

                Assert.AreEqual(response.Count, 0);
            });
        }
        [TestMethod]
        public void DeleteIsSingular()
        {
            dbFactory.Run(db =>
            {
                db.Insert(new Priority { Id = 1, Name = "Test Item" });
                db.Insert(new Priority { Id = 2, Name = "Test Item 2" });
            });

            var repository = new PriorityRepository(dbFactory, personRepository);

            repository.Delete(1);
            dbFactory.Run(db =>
            {
                var response = db.Select<Priority>();

                Assert.AreEqual(response.Count, 1);
                Assert.AreEqual(response[0].Name, "Test Item 2");
            });
        }
        [TestMethod]
        public void DeleteFails()
        {
            dbFactory.Run(db => db.Insert(new Priority { Id = 1, Name = "Test Item" }));

            var repository = new PriorityRepository(dbFactory, personRepository);

            repository.Delete(2);
            dbFactory.Run(db =>
            {
                var response = db.Select<Priority>();

                Assert.AreEqual(response.Count, 1);
                Assert.AreEqual(response[0].Name, "Test Item");
            });
        }

        [TestMethod]
        public void MoveIncreasesOrder()
        {
            dbFactory.Run(db =>
            {
                db.Insert(new Priority { Id = 1, Name = "Item 1", Order = 1 });
                db.Insert(new Priority { Id = 2, Name = "Item 2", Order = 2 });
                db.Insert(new Priority { Id = 3, Name = "Item 3", Order = 3 });
            });

            var repository = new PriorityRepository(dbFactory, personRepository);

            repository.Move(3, -1);
            dbFactory.Run(db =>
            {
                var response = db.Select<Priority>();

                Assert.AreEqual(response.Count, 3);
                Assert.AreEqual(response.Single(x => x.Id == 1).Order, 1);
                Assert.AreEqual(response.Single(x => x.Id == 2).Order, 2);
                Assert.AreEqual(response.Single(x => x.Id == 3).Order, 4);
            });
        }
        [TestMethod]
        public void MoveDecreasesOrder()
        {
            dbFactory.Run(db =>
            {
                db.Insert(new Priority { Id = 1, Name = "Item 1", Order = 1 });
                db.Insert(new Priority { Id = 2, Name = "Item 2", Order = 2 });
                db.Insert(new Priority { Id = 3, Name = "Item 3", Order = 3 });
            });

            var repository = new PriorityRepository(dbFactory, personRepository);

            repository.Move(1, 1);
            dbFactory.Run(db =>
            {
                var response = db.Select<Priority>();

                Assert.AreEqual(response.Count, 3);
                Assert.AreEqual(response.Single(x => x.Id == 1).Order, 0);
                Assert.AreEqual(response.Single(x => x.Id == 2).Order, 2);
                Assert.AreEqual(response.Single(x => x.Id == 3).Order, 3);
            });
        }
        [TestMethod]
        public void MoveShiftsIncreasing1()
        {
            dbFactory.Run(db =>
            {
                db.Insert(new Priority { Id = 1, Name = "Item 1", Order = 1 });
                db.Insert(new Priority { Id = 2, Name = "Item 2", Order = 2 });
                db.Insert(new Priority { Id = 3, Name = "Item 3", Order = 3 });
            });

            var repository = new PriorityRepository(dbFactory, personRepository);

            repository.Move(1, -1);
            dbFactory.Run(db =>
            {
                var response = db.Select<Priority>();

                Assert.AreEqual(response.Count, 3);
                Assert.AreEqual(response.Single(x => x.Id == 1).Order, 2);
                Assert.AreEqual(response.Single(x => x.Id == 2).Order, 1);
                Assert.AreEqual(response.Single(x => x.Id == 3).Order, 3);
            });
        }
        [TestMethod]
        public void MoveShiftsIncreasing2()
        {
            dbFactory.Run(db =>
            {
                db.Insert(new Priority { Id = 1, Name = "Item 1", Order = 1 });
                db.Insert(new Priority { Id = 2, Name = "Item 2", Order = 2 });
                db.Insert(new Priority { Id = 3, Name = "Item 3", Order = 3 });
            });

            var repository = new PriorityRepository(dbFactory, personRepository);

            repository.Move(1, -2);
            dbFactory.Run(db =>
            {
                var response = db.Select<Priority>();

                Assert.AreEqual(response.Count, 3);
                Assert.AreEqual(response.Single(x => x.Id == 1).Order, 3);
                Assert.AreEqual(response.Single(x => x.Id == 2).Order, 1);
                Assert.AreEqual(response.Single(x => x.Id == 3).Order, 2);
            });
        }
        [TestMethod]
        public void MoveShiftsDecreasing1()
        {
            dbFactory.Run(db =>
            {
                db.Insert(new Priority { Id = 1, Name = "Item 1", Order = 1 });
                db.Insert(new Priority { Id = 2, Name = "Item 2", Order = 2 });
                db.Insert(new Priority { Id = 3, Name = "Item 3", Order = 3 });
            });

            var repository = new PriorityRepository(dbFactory, personRepository);

            repository.Move(3, 1);
            dbFactory.Run(db =>
            {
                var response = db.Select<Priority>();

                Assert.AreEqual(response.Count, 3);
                Assert.AreEqual(response.Single(x => x.Id == 1).Order, 1);
                Assert.AreEqual(response.Single(x => x.Id == 2).Order, 3);
                Assert.AreEqual(response.Single(x => x.Id == 3).Order, 2);
            });
        }
        [TestMethod]
        public void MoveShiftsDecreasing2()
        {
            dbFactory.Run(db =>
            {
                db.Insert(new Priority { Id = 1, Name = "Item 1", Order = 1 });
                db.Insert(new Priority { Id = 2, Name = "Item 2", Order = 2 });
                db.Insert(new Priority { Id = 3, Name = "Item 3", Order = 3 });
            });

            var repository = new PriorityRepository(dbFactory, personRepository);

            repository.Move(3, 2);
            dbFactory.Run(db =>
            {
                var response = db.Select<Priority>();

                Assert.AreEqual(response.Count, 3);
                Assert.AreEqual(response.Single(x => x.Id == 1).Order, 2);
                Assert.AreEqual(response.Single(x => x.Id == 2).Order, 3);
                Assert.AreEqual(response.Single(x => x.Id == 3).Order, 1);
            });
        }
    }
}
