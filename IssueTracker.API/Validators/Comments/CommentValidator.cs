using IssueTracker.API.Repositories;
using IssueTracker.Data;
using ServiceStack.FluentValidation;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Validators.Comments
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public IIssueRepository IssueRepository { get; set; }

        public CommentValidator()
        {
            RuleSet(ApplyTo.Post | ApplyTo.Delete, () => RuleFor(r => r.Id).NotNull());
            RuleSet(ApplyTo.Put | ApplyTo.Post, () =>
            {
                RuleFor(x => x.IssueId).Must(x => IssueRepository.GetById(x) != null);
                RuleFor(x => x.Message).NotEmpty();
            });
        }
    }
}