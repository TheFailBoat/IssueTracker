﻿using System.Collections.Generic;
using System.Linq;
using IssueTracker.API.Entities;
using IssueTracker.API.Repositories;
using IssueTracker.API.Security;
using IssueTracker.Data;
using ServiceStack.Common;

namespace IssueTracker.API.Utilities
{
    internal static class DtoExtensions
    {
        #region Issues

        public static List<Issue> ToDto(this List<IssueEntity> from)
        {
            return from.Select(x => x.ToDto()).ToList();
        }

        public static Issue ToDto(this IssueEntity from)
        {
            return from.TranslateTo<Issue>();
        }
        public static Issue ToDto(this IssueEntity from, IInsecureRepository<ICommentRepository> commentRepository)
        {
            var issue = from.ToDto();

            issue.CommentIds = commentRepository.Repository.GetForIssue(from.Id).Select(x => x.Id).ToList();

            return issue;
        }

        #endregion
        #region Comments

        public static List<Comment> ToDto(this List<CommentEntity> from)
        {
            return from.Select(x => x.ToDto()).ToList();
        }

        public static Comment ToDto(this CommentEntity from)
        {
            return from.TranslateTo<Comment>();
        }

        #endregion

        #region Users

        public static List<User> ToDto(this List<UserEntity> from)
        {
            return from.Select(x => x.ToDto()).ToList();
        }

        public static User ToDto(this UserEntity from)
        {
            return new User
            {
                Id = from.Id,
                CustomerId = from.CustomerId,
                Name = from.Username,
                Email = from.Email,
                IsAdmin = from.IsAdmin,
                IsMod = from.IsMod || from.IsAdmin,
            };
        }

        #endregion
        #region Customers

        public static List<Customer> ToDto(this List<CustomerEntity> from)
        {
            return from.Select(x => x.ToDto()).ToList();
        }

        public static Customer ToDto(this CustomerEntity from)
        {
            return new Customer
            {
                Id = from.Id,
                Name = from.Name
            };
        }

        #endregion

        #region Categories

        public static List<Category> ToDto(this List<CategoryEntity> from)
        {
            return from.Select(x => x.ToDto()).ToList();
        }

        public static Category ToDto(this CategoryEntity from)
        {
            return from.TranslateTo<Category>();
        }

        #endregion
        #region Priorities

        public static List<Priority> ToDto(this List<PriorityEntity> from)
        {
            return from.Select(x => x.ToDto()).ToList();
        }

        public static Priority ToDto(this PriorityEntity from)
        {
            return from.TranslateTo<Priority>();
        }

        #endregion
        #region Statuses

        public static List<Status> ToDto(this List<StatusEntity> from)
        {
            return from.Select(x => x.ToDto()).ToList();
        }

        public static Status ToDto(this StatusEntity from)
        {
            return from.TranslateTo<Status>();
        }

        #endregion
    }
}