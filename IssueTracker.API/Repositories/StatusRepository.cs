﻿using System;
using System.Collections.Generic;
using System.Data;
using IssueTracker.Data;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface IStatusRepository : IRepository<Status>
    {
        void Move(long id, long amount);
    }

    internal class StatusRepository : IStatusRepository, IDisposable
    {
        private readonly IDbConnectionFactory dbFactory;
        private readonly IPersonRepository personRepository;
        private IDbConnection db;

        public StatusRepository(IDbConnectionFactory dbFactory, IPersonRepository personRepository)
        {
            this.dbFactory = dbFactory;
            this.personRepository = personRepository;
        }

        private IDbConnection Db
        {
            get
            {
                return db ?? (db = dbFactory.Open());
            }
        }


        public List<Status> GetAll()
        {
            return Db.Select<Status>();
        }

        public Status GetById(long id)
        {
            return Db.IdOrDefault<Status>(id);
        }

        public Status Add(Status status)
        {
            if (!personRepository.GetCurrent().IsEmployee)
            {
                return null;
            }

            status.Id = 0;

            Db.Insert(status);
            status.Id = Db.GetLastInsertId();

            return status;
        }

        public Status Update(Status status)
        {
            if (!personRepository.GetCurrent().IsEmployee)
            {
                return null;
            }

            Db.Update(status);

            return status;
        }

        public bool Delete(long id)
        {
            if (!personRepository.GetCurrent().IsEmployee)
            {
                return false;
            }

            Db.DeleteById<Status>(id);

            return true;
        }

        public void Delete(Status status)
        {
            Delete(status.Id);
        }

        public void Move(long id, long amount)
        {
            if (amount == 0) return;

            var status = GetById(id);

            // minus the amount because it is amount to move UP
            var newPosition = status.Order - amount;
            var direction = amount > 0 ? 1 : -1;

            var prioritiesToUpdate = amount > 0
             ? Db.Select<Status>(x => x.Order >= newPosition && x.Order < status.Order)
             : Db.Select<Status>(x => x.Order > status.Order && x.Order <= newPosition);

            foreach (var statusToShift in prioritiesToUpdate)
            {
                statusToShift.Order += direction;
                Update(statusToShift);
            }

            status.Order = newPosition;
            Update(status);
        }

        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }
    }
}