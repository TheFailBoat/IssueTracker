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
    public class CommentRepositoryTest
    {
        private IDbConnectionFactory dbFactory;
        private PersonRepository personRepository;
        private IssueRepository issueRepository;

        [TestInitialize]
        public void InitTests()
        {
            dbFactory = new OrmLiteConnectionFactory(":memory:", false, SqliteOrmLiteDialectProvider.Instance);
            dbFactory.Run(db => db.CreateTable<Comment>());

            var users = new InMemoryAuthRepository();

            personRepository = new PersonRepository(users, null);
            issueRepository = new IssueRepository(dbFactory, personRepository);

            // TODO add issues
        }

        [TestMethod]
        public void AddReturnsId()
        {
            var repository = new CommentRepository(dbFactory, personRepository, issueRepository);

            var response = repository.Add(new Comment());

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Id, 1);
        }
        [TestMethod]
        public void AddPersists()
        {
            var repository = new CommentRepository(dbFactory, personRepository, issueRepository);

            repository.Add(new Comment { Message = "Test Item" });

            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Comment>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Message, "Test Item");
                              });
        }

        [TestMethod]
        public void GetByIdReturnsItem()
        {
            dbFactory.Run(db => db.Insert(new Comment { Id = 1, Message = "Test Item" }));

            var repository = new CommentRepository(dbFactory, personRepository, issueRepository);

            var response = repository.GetById(1);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Message, "Test Item");
        }
        [TestMethod]
        public void GetByIdReturnsNull()
        {
            dbFactory.Run(db => db.Insert(new Comment { Id = 1, Message = "Test Item" }));

            var repository = new CommentRepository(dbFactory, personRepository, issueRepository);

            var response = repository.GetById(2);

            Assert.IsNull(response);
        }

        [TestMethod]
        public void GetAllReturnsEmpty()
        {
            var repository = new CommentRepository(dbFactory, personRepository, issueRepository);

            var response = repository.GetAll();

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 0);
        }
        [TestMethod]
        public void GetAllReturnsItems()
        {
            dbFactory.Run(db =>
                              {
                                  db.Insert(new Comment { Id = 1, Message = "Test Item" });
                                  db.Insert(new Comment { Id = 2, Message = "Test Item 2" });
                              });

            var repository = new CommentRepository(dbFactory, personRepository, issueRepository);

            var response = repository.GetAll();

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 2);
            Assert.AreEqual(response.Single(x => x.Id == 1).Message, "Test Item");
            Assert.AreEqual(response.Single(x => x.Id == 2).Message, "Test Item 2");
        }

        [TestMethod]
        public void UpdatePersists()
        {
            dbFactory.Run(db => db.Insert(new Comment { Id = 1, Message = "Test Item" }));

            var repository = new CommentRepository(dbFactory, personRepository, issueRepository);

            repository.Update(new Comment { Id = 1, Message = "Test Edit" });

            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Comment>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Message, "Test Edit");
                              });
        }
        [TestMethod]
        public void UpdateIsSingular()
        {
            dbFactory.Run(db =>
                              {
                                  db.Insert(new Comment { Id = 1, Message = "Test Item" });
                                  db.Insert(new Comment { Id = 2, Message = "Test Item 2" });
                              });

            var repository = new CommentRepository(dbFactory, personRepository, issueRepository);

            repository.Update(new Comment { Id = 1, Message = "Test Edit" });

            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Comment>();

                                  Assert.AreEqual(response.Count, 2);
                                  Assert.AreEqual(response.Single(x => x.Id == 1).Message, "Test Edit");
                                  Assert.AreEqual(response.Single(x => x.Id == 2).Message, "Test Item 2");
                              });
        }
        [TestMethod]
        public void UpdateFails()
        {
            dbFactory.Run(db => db.Insert(new Comment { Id = 1, Message = "Test Item" }));

            var repository = new CommentRepository(dbFactory, personRepository, issueRepository);

            repository.Update(new Comment { Id = 2, Message = "Test Edit" });
            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Comment>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Message, "Test Item");
                              });
        }

        [TestMethod]
        public void DeletePersists()
        {
            dbFactory.Run(db => db.Insert(new Comment { Id = 1, Message = "Test Item" }));

            var repository = new CommentRepository(dbFactory, personRepository, issueRepository);

            repository.Delete(1);
            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Comment>();

                                  Assert.AreEqual(response.Count, 0);
                              });
        }
        [TestMethod]
        public void DeleteIsSingular()
        {
            dbFactory.Run(db =>
                              {
                                  db.Insert(new Comment { Id = 1, Message = "Test Item" });
                                  db.Insert(new Comment { Id = 2, Message = "Test Item 2" });
                              });

            var repository = new CommentRepository(dbFactory, personRepository, issueRepository);

            repository.Delete(1);
            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Comment>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Message, "Test Item 2");
                              });
        }
        [TestMethod]
        public void DeleteFails()
        {
            dbFactory.Run(db => db.Insert(new Comment { Id = 1, Message = "Test Item" }));

            var repository = new CommentRepository(dbFactory, personRepository, issueRepository);

            repository.Delete(2);
            dbFactory.Run(db =>
                              {
                                  var response = db.Select<Comment>();

                                  Assert.AreEqual(response.Count, 1);
                                  Assert.AreEqual(response[0].Message, "Test Item");
                              });
        }
    }
}