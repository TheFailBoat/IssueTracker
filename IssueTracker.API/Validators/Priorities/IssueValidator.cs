using IssueTracker.API.Repositories;
using IssueTracker.Data;
using ServiceStack.FluentValidation;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Validators.Priorities
{
    public class IssueValidator : AbstractValidator<Issue>
    {
        public ICategoryRepository CategoryRepository { get; set; }
        public IStatusRepository StatusRepository { get; set; }
        public IPriorityRepository PriorityRepository { get; set; }

        public IssueValidator()
        {
            RuleSet(ApplyTo.Post | ApplyTo.Delete, () => RuleFor(r => r.Id).NotNull());
            RuleSet(ApplyTo.Put | ApplyTo.Post, () =>
            {
                RuleFor(x => x.CategoryId).Must(x => CategoryRepository.GetById(x) != null);
                RuleFor(x => x.StatusId).Must(x => StatusRepository.GetById(x) != null);
                RuleFor(x => x.PriorityId).Must(x => PriorityRepository.GetById(x) != null);
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Progress).InclusiveBetween(0, 100);
            });

        }
    }
}