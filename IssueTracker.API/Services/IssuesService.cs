﻿using System;
using IssueTracker.API.Entities;
using IssueTracker.API.Repositories;
using IssueTracker.API.Security;
using IssueTracker.API.Utilities;
using IssueTracker.Data.Issues;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.Text;

namespace IssueTracker.API.Services
{
    internal class IssuesService : BaseService
    {
        public IssuesService(ISecurityService securityService)
            : base(securityService)
        {
        }

        public IIssueRepository IssueRepository { get; set; }

        public ListIssuesResponse Get(ListIssues request)
        {
            var page = Math.Max(request.Page.GetValueOrDefault(1) - 1, 0);
            var pageSize = Math.Max(request.PageSize.GetValueOrDefault(100) - 1, 5);

            return new ListIssuesResponse { Issues = IssueRepository.GetByPage(page, pageSize).ToDto() };
        }
        public GetIssueResponse Get(GetIssue request)
        {
            var issue = IssueRepository.GetById(request.Id);
            if (issue == null) throw HttpError.NotFound("issue {0} not found".Fmt(request.Id));

            return new GetIssueResponse { Issue = issue.ToDto() };
        }

        public UpdateIssueResponse Post(UpdateIssue request)
        {
            var issue = IssueRepository.Add(request.TranslateTo<IssueEntity>());
            if (issue == null) throw HttpError.NotFound("issue not created");

            return new UpdateIssueResponse { Issue = issue.ToDto() };
        }

        public UpdateIssueResponse Put(UpdateIssue request)
        {
            var issue = IssueRepository.GetById(request.Id);
            if (issue == null) throw HttpError.NotFound("issue {0} not found".Fmt(request.Id));

            issue = issue.PopulateWith(request);
            issue = IssueRepository.Update(issue);
            if (issue == null) throw HttpError.NotFound("issue {0} not updated".Fmt(request.Id));

            return new UpdateIssueResponse { Issue = issue.ToDto() };
        }
    }
}